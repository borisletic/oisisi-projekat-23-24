using GUI.View;
using CLI.DAO;
using CLI.Observer;
using CLI.Model;
using System.Collections.ObjectModel;
using GUI.DTO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using CLI.Controller;
using System.Reflection;
using System.IO;
using System;
using System.Windows.Media.Imaging;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;
using System.Net;
using System.ComponentModel;
using System.Collections.Immutable;
using GUI.View.ProfesorView;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window, IObserver
    {
        public ObservableCollection<StudentDTO> Student { get; set; }
        public StudentDTO SelectedStudent { get; set; }
        private StudentController studentController { get; set; }

        public ObservableCollection<ProfesorDTO> Profesor { get; set; }
        public ProfesorDTO SelectedProfesor { get; set; }
        private ProfesorController profesorController { get; set; }

        public ObservableCollection<PredmetDTO> Predmet { get; set; }
        public PredmetDTO SelectedPredmet { get; set; }
        private PredmetController predmetController { get; set; }

        private OcenaController ocenaController { get; set; }

        private string promena_taba = "Studenti";
        private int broj_strane = 1;
        private int max_broj_strana_student;
        private int max_broj_strana_predmet;
        private int max_broj_strana_profesor;
        private string sortCriteria = "Id";
        private SortDirection sortDirection = SortDirection.Ascending;

        //461 TODO: da sortira i indeks

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            InitializeImages();
            

            CurrentTime.Text = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
            tabControl.SelectedIndex = 0;
            TabItem SelectedTab = tabControl.SelectedItem as TabItem;
            CurrentTab.Text = SelectedTab.Header.ToString();

            double ScreenWidth = SystemParameters.PrimaryScreenWidth;
            double ScreenHeight = SystemParameters.PrimaryScreenHeight;
            Width = ScreenWidth * 3 / 4;
            Height = ScreenHeight * 3 / 4;

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            Profesor = new ObservableCollection<ProfesorDTO>();
            profesorController = new ProfesorController();
            profesorController.Subscribe(this);

            Predmet = new ObservableCollection<PredmetDTO>();
            predmetController = new PredmetController(profesorController._profesor);
            predmetController.Subscribe(this);

            Student = new ObservableCollection<StudentDTO>();
            studentController = new StudentController();
            studentController.Subscribe(this);

            ocenaController = new OcenaController(studentController._student,predmetController._predmet);
            ocenaController.Subscribe(this);

            max_broj_strana_student = (int)Math.Ceiling(studentController.GetAllStudents().Count / 16.0);
            max_broj_strana_predmet = (int)Math.Ceiling(predmetController.GetAllPredmeti().Count / 16.0);
            max_broj_strana_profesor = (int)Math.Ceiling(profesorController.GetAllProfessors().Count / 16.0);
            lblStranica.Content = $"Stranica {broj_strane} od {max_broj_strana_student}";
            Update();

            //jer je kod ocajan
            foreach (Predmet predmet in predmetController.GetAllPredmeti()) 
            {
                Profesor p = new Profesor();
                if (predmet.Predmetni_Profesor.Id != 0)
                {
                    p = profesorController.GetProfesorById(predmet.Predmetni_Profesor.Id);
                    p.Spisak_Predmeta_Profesora.Add(predmet);
                    predmet.Predmetni_Profesor = p;
                }
            }
        }
        public void Update()
        {
            Student.Clear();
            foreach (Student student in studentController.GetAllStudents(broj_strane, 16, sortCriteria, sortDirection)) Student.Add(new StudentDTO(student));

            Profesor.Clear();
            foreach (Profesor profesor in profesorController.GetAllProfessors(broj_strane, 16, sortCriteria, sortDirection)) Profesor.Add(new ProfesorDTO(profesor));

            Predmet.Clear();
            foreach (Predmet predmet in predmetController.GetAllPredmeti(broj_strane, 16, sortCriteria, sortDirection)) Predmet.Add(new PredmetDTO(predmet));
        }

        private void tabControl_CurrentTab(object sender, MouseButtonEventArgs e)
        {
            TabItem SelectedTab = tabControl.SelectedItem as TabItem;
            CurrentTab.Text = SelectedTab.Header.ToString();
            if (SelectedTab.Header.ToString() != promena_taba)
            {
                Reset_Prikaza();
                promena_taba = SelectedTab.Header.ToString();
            }
        }

        private void Reset_Prikaza() 
        {
            broj_strane = 1;
            sortCriteria = "Id";
            sortDirection = SortDirection.Ascending;
            if (tabControl.SelectedItem is TabItem selectedTab && selectedTab.Header.ToString() == "Studenti")
            {
                if (max_broj_strana_student == 1) max_broj_strana_student++;
                lblStranica.Content = $"Stranica {broj_strane} od {max_broj_strana_student}";
            }
            else if (tabControl.SelectedItem is TabItem selectedTab1 && selectedTab1.Header.ToString() == "Predmeti")
            {
                if (max_broj_strana_predmet == 1) max_broj_strana_predmet++;
                lblStranica.Content = $"Stranica {broj_strane} od {max_broj_strana_predmet}";
            }
            else if (tabControl.SelectedItem is TabItem selectedTab2 && selectedTab2.Header.ToString() == "Profesori") 
            {
                if (max_broj_strana_profesor == 1) max_broj_strana_profesor++;
                lblStranica.Content = $"Stranica {broj_strane} od {max_broj_strana_profesor}";
            }
            Update();
        }

        private void Open_Tab(object sender, RoutedEventArgs e)
        {
            TabItem selectedTab;
            MenuItem menuItem = sender as MenuItem;
            if (menuItem != null && menuItem.Header.ToString() == "Studenti")
            {
                tabControl.SelectedIndex = 0;
                Reset_Prikaza();
            }
            else if (menuItem != null && menuItem.Header.ToString() == "Profesori")
            {
                tabControl.SelectedIndex = 1;
                Reset_Prikaza();
            }
            else if (menuItem != null && menuItem.Header.ToString() == "Predmeti")
            {
                tabControl.SelectedIndex = 2;
                Reset_Prikaza();
            }
            else if (menuItem != null && menuItem.Header.ToString() == "Katedre") 
            {
                KatedraAdd(sender, e);
            }
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SearchBox(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text;
            if (tabControl.SelectedItem is TabItem selectedTab && selectedTab.Header.ToString() == "Studenti")
            {
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    MessageBox.Show("Morate uneti makar prezime za pretragu studenta.Druge kombinacije(prezime ime) ili (indeks ime prezime)", "Info");
                    Update();
                    Reset_Prikaza();
                }
                else
                {
                    List<Student> lista_studenata = new List<Student>();
                    string[] info = searchText.Split(" "); // 1 i 2 reci [0] prezime [1] ime kod 3 reci [0]indeks [1]ime [2]prezime
                    Update();
                    switch (info.Length)
                    {
                        case 1:
                            //Trazimo studenta sa datim prezimenom ili delom prezimena
                            //StackOverflow kod
                            Student.Clear();
                            lista_studenata.Clear();
                            lista_studenata = studentController.GetAllStudents().Where(s => s.Prezime.StartsWith(info[0], StringComparison.OrdinalIgnoreCase)).ToList();
                            break;
                        case 2:
                            //Trazimo studenta sa datim prezimenom i imenom ili delom prezimena i imena
                            //StackOverflow kod
                            Student.Clear();
                            lista_studenata.Clear();

                            lista_studenata = studentController.GetAllStudents().Where(s => s.Prezime.Contains(info[0], StringComparison.OrdinalIgnoreCase) &&
                            s.Ime.StartsWith(info[1],StringComparison.OrdinalIgnoreCase)).ToList();
                            break;
                        case 3:
                            //Trazimo studenta sa datim indeksom imenom prezimenom 
                            //StackOverflow kod
                            Student.Clear();
                            lista_studenata.Clear();
                            lista_studenata = studentController.GetAllStudents().Where(s => s.Prezime.Contains(info[2], StringComparison.OrdinalIgnoreCase) &&
                            s.Ime.StartsWith(info[1], StringComparison.OrdinalIgnoreCase) && s.Indeks.ToString().StartsWith(info[0],StringComparison.OrdinalIgnoreCase)).ToList();
                            break;
                    }
                    foreach (Student temp in lista_studenata)
                    {
                        Student.Add(new StudentDTO(temp));

                    }
                    if(max_broj_strana_student > 1)
                    lblStranica.Content = $"Stranica {broj_strane} od {--max_broj_strana_student}";
                }
            }
            else if (tabControl.SelectedItem is TabItem selectedTab1 && selectedTab1.Header.ToString() == "Predmeti") 
            {
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    MessageBox.Show("Morate uneti naziv predmeta(deo naziva) za pretragu.Moguce je traziti i u ovom formatu [naziv_predmeta,sifra_predmeta]", "Info");
                    Update();
                    Reset_Prikaza();
                }
                else 
                {
                    List<Predmet> lista_predmeta = new List<Predmet>();
                    string[] info = searchText.Split(",");
                    switch (info.Length)
                    {
                        case 1:
                            //Trazimo predmet sa delom naziva predmeta
                            Predmet.Clear();
                            lista_predmeta.Clear();

                            lista_predmeta = predmetController.GetAllPredmeti().Where(p => p.Naziv_Predmeta.Contains(info[0], StringComparison.OrdinalIgnoreCase)).ToList();
                            break;
                        case 2:
                            //Trazimo predmet sa delom naziva predmeta i sifrom predmeta
                            Predmet.Clear();
                            lista_predmeta.Clear();

                            lista_predmeta = predmetController.GetAllPredmeti().Where(p => p.Naziv_Predmeta.Contains(info[0], StringComparison.OrdinalIgnoreCase) &&
                            p.Sifra_Predmeta.StartsWith(info[1], StringComparison.OrdinalIgnoreCase)).ToList();   
                            break;
                    }
                    foreach (Predmet temp in lista_predmeta)
                    {
                        Predmet.Add(new PredmetDTO(temp));
                    }
                    if (max_broj_strana_predmet > 1)
                        lblStranica.Content = $"Stranica {broj_strane} od {--max_broj_strana_predmet}";

                }
            }
            else if (tabControl.SelectedItem is TabItem selectedTab2 && selectedTab2.Header.ToString() == "Profesori")
            {
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    MessageBox.Show("Morate uneti makar prezime(deo prezimena) ili ime za pretragu profesora.Druge kombinacije(deo_prezimena ime)", "Info");
                    Update();
                    Reset_Prikaza();
                }
                else 
                {
                    List<Profesor> lista_predmeta = new List<Profesor>();
                    string[] info = searchText.Split(" ");
                    switch (info.Length)
                    {
                        case 1:
                            //Trazimo profesora sa datim delom prezimena ili imena
                            Profesor.Clear();
                            lista_predmeta.Clear();

                            lista_predmeta = profesorController.GetAllProfessors().Where(s => s.Prezime.Contains(info[0], StringComparison.OrdinalIgnoreCase) ||
                            s.Ime.StartsWith(info[0], StringComparison.OrdinalIgnoreCase)).ToList();
                            break;
                        case 2:
                            //Trazimo profesora sa datim delom prezimena i imena
                            Profesor.Clear();
                            lista_predmeta.Clear();

                            lista_predmeta = profesorController.GetAllProfessors().Where(s => s.Prezime.Contains(info[0], StringComparison.OrdinalIgnoreCase) &&
                            s.Ime.StartsWith(info[1], StringComparison.OrdinalIgnoreCase)).ToList();
                            break;
                    }
                    foreach (Profesor temp in lista_predmeta)
                    {
                        Profesor.Add(new ProfesorDTO(temp));
                    }
                    if (max_broj_strana_profesor > 1)
                        lblStranica.Content = $"Stranica {broj_strane} od {--max_broj_strana_profesor}";
                }
            }
        }

        private void StudentPrethodnaStrana(object sender, RoutedEventArgs e)
        {
            if (tabControl.SelectedItem is TabItem selectedTab && selectedTab.Header.ToString() == "Studenti")
            {
                if (broj_strane != 1)
                {
                    broj_strane--;
                    lblStranica.Content = $"Stranica {broj_strane} od {max_broj_strana_student}";
                    Update();
                }
            }
            else if (tabControl.SelectedItem is TabItem selectedTab1 && selectedTab1.Header.ToString() == "Predmeti")
            {
                if (broj_strane != 1)
                {
                    broj_strane--;
                    lblStranica.Content = $"Stranica {broj_strane} od {max_broj_strana_predmet}";
                    Update();
                }
            }
            else if (tabControl.SelectedItem is TabItem selectedTab2 && selectedTab2.Header.ToString() == "Profesori")
            {
                if(broj_strane != 1) 
                {
                    broj_strane--;
                    lblStranica.Content = $"Stranica {broj_strane} od {max_broj_strana_profesor}";
                    Update();
                }      
            }
        }

        private void StudentSledecaStrana(object sender, RoutedEventArgs e)
        {
            if (tabControl.SelectedItem is TabItem selectedTab && selectedTab.Header.ToString() == "Studenti")
            {
                if (broj_strane != max_broj_strana_student)
                {
                    broj_strane++;
                    lblStranica.Content = $"Stranica {broj_strane} od {max_broj_strana_student}";
                    Update();
                }
            }
            else if (tabControl.SelectedItem is TabItem selectedTab1 && selectedTab1.Header.ToString() == "Predmeti")
            {
                if (broj_strane != max_broj_strana_predmet)
                {
                    broj_strane++;
                    lblStranica.Content = $"Stranica {broj_strane} od {max_broj_strana_predmet}";
                    Update();
                }
            }
            else if (tabControl.SelectedItem is TabItem selectedTab2 && selectedTab2.Header.ToString() == "Profesori")
            {
                if (broj_strane != max_broj_strana_profesor)
                {
                    broj_strane++;
                    lblStranica.Content = $"Stranica {broj_strane} od {max_broj_strana_profesor}";
                    Update();
                }
            }
        }

        private void EditSelected(object sender, RoutedEventArgs e)
        {

            if (tabControl.SelectedItem is TabItem selectedTab && selectedTab.Header.ToString() == "Studenti")
            {
                if (SelectedStudent == null)
                {
                    MessageBox.Show("Morate izabrati studenta", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    UpdateStudent studentiUpdate = new UpdateStudent(studentController,predmetController, SelectedStudent,ocenaController);

                    studentiUpdate.Owner = this;  // Set the owner to MainWindow
                    studentiUpdate.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    studentiUpdate.ShowDialog();
                }
            }
            else if (tabControl.SelectedItem is TabItem selectedTab1 && selectedTab1.Header.ToString() == "Predmeti")
            {
                if (SelectedPredmet == null)
                {
                    MessageBox.Show("Morate izabrati predmet", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    UpdatePredmet predmetiUpdate = new UpdatePredmet(profesorController,predmetController, SelectedPredmet);

                    predmetiUpdate.Owner = this;  // Set the owner to MainWindow
                    predmetiUpdate.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    predmetiUpdate.Show();
                }
            }
            else if (tabControl.SelectedItem is TabItem selectedTab2 && selectedTab2.Header.ToString() == "Profesori")
            {
                if (SelectedProfesor == null)
                {
                    MessageBox.Show("Morate izabrati profesora", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    UpdateProfesor profesoriUpdate = new UpdateProfesor(profesorController, SelectedProfesor,predmetController);

                    profesoriUpdate.Owner = this;  // Set the owner to MainWindow
                    profesoriUpdate.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    profesoriUpdate.Show();
                }

            }
            
        }
        private void AddNew(object sender, RoutedEventArgs e)
        {
            if (tabControl.SelectedItem is TabItem selectedTab && selectedTab.Header.ToString() == "Studenti")
            {
                AddStudent addStudent = new AddStudent(studentController);
                addStudent.Owner = this;  // Set the owner to MainWindow
                addStudent.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                addStudent.Show();
                Update();
            }
            else if (tabControl.SelectedItem is TabItem selectedTab1 && selectedTab1.Header.ToString() == "Predmeti")
            {
                AddPredmet addPredmet = new AddPredmet(predmetController);
                addPredmet.Owner = this;  // Set the owner to MainWindow
                addPredmet.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                addPredmet.Show();
                Update();
            }
            else if (tabControl.SelectedItem is TabItem selectedTab2 && selectedTab2.Header.ToString() == "Profesori")
            {
                AddProfesor addProfesor = new AddProfesor(profesorController);
                addProfesor.Owner = this;  // Set the owner to MainWindow
                addProfesor.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                addProfesor.Show();
                Update();
            }
        }

        private void RemoveSelected(object sender, RoutedEventArgs e)
        {
                if (tabControl.SelectedItem is TabItem selectedTab && selectedTab.Header.ToString() == "Studenti")
                {
                    if (SelectedStudent == null)
                    {
                        MessageBox.Show("Morate izabrati studenta kojeg zelite da obrisete", "Info");
                    }
                    else
                    {
                        MessageBoxResult result = MessageBox.Show("Da li ste sigurni da zelite da obrisete izabranog studenta", "Brisanje studenta", MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            studentController.Delete(SelectedStudent.Id);
                        }
                    }
                }
                else if (tabControl.SelectedItem is TabItem selectedTab1 && selectedTab1.Header.ToString() == "Predmeti")
                {
                    if (SelectedPredmet == null)
                    {
                        MessageBox.Show("Morate izabrati predmet koji zelite da obrisete");
                    }
                    else
                    {
                        MessageBoxResult result = MessageBox.Show("Da li ste sigurni da zelite da obrisete izabranog profesora", "Brisanje profesora", MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            predmetController.Delete(SelectedPredmet.Id);
                        }
                    }
                }
                else if (tabControl.SelectedItem is TabItem selectedTab2 && selectedTab2.Header.ToString() == "Profesori")
                {
                    if (SelectedProfesor == null)
                    {
                        MessageBox.Show("Morate izabrati profesora kojeg zelite da obrisete");
                    }
                    else
                    {
                        MessageBoxResult result = MessageBox.Show("Da li ste sigurni da zelite da obrisete izabrani predmet", "Brisanje predmeta", MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            ProfesorDTO profesorDTO = new ProfesorDTO();
                            profesorDTO = SelectedProfesor;
                            profesorController.Delete(profesorDTO.Id);
                            //Posto nemamo uvezano profesora direktno sa predmetom zbog grekse od samog pocetka,brisemo ovde predmet koji je bio predavan od obrisanog profesora
                            foreach (Predmet predmet in predmetController.GetAllPredmeti()) 
                            {
                                if (profesorDTO.Id == predmet.Predmetni_Profesor.Id) 
                                {
                                    predmet.Predmetni_Profesor = new Profesor();
                                    predmetController.Update(predmet);
                                }
                            }
                        }
                    }
                }
        }

        private void AboutBox(object sender, RoutedEventArgs e)
        {
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            MessageBox.Show($"Trenutna verzija:{version}\nOva aplikacija trenutno cita studente,profesore i predmete iz txt/csv fajla i ubacuje ih u DataGrid za njihov prikaz." +
                    $"Pomocu MenuBara ili ToolBara dodajemo,menjamo ili brisemo podatke iz njih.\n\nNikola Tesic:Rodjen 16.10.2002.Studiram na FTN-u Racunarstvo i Automatiku,usmerenje PRNI.OISISI je kul predmet(za ocenu)\n\n" +
                    $"Boris Letic: Praise Lord Gaben!", "Verzija", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void InitializeImages() 
        {
           // Plus1.Source = new BitmapImage(new Uri("Slike/Plus.png)",UriKind.Relative));
        }

        private void StudentiSort(object sender, DataGridSortingEventArgs e)
        {
            //glavna logika sa StackOverflowa
            ListSortDirection direction;
            //posto ne moze da se uzme direktno vrednost od e,moramo da proverimo da li je ascending
            //i ako nije da mu damo tu vrednost,ne znam da li je rezon tacan
            direction = (e.Column.SortDirection != ListSortDirection.Ascending) ? ListSortDirection.Ascending : ListSortDirection.Descending;
            sortDirection = (SortDirection)direction;
            switch (e.Column.Header.ToString())
            {
                case "Ime":
                    sortCriteria = "Ime";
                    break;

                case "Prezime":
                    sortCriteria = "Prezime";
                    break;

                case "Indeks":
                    sortCriteria = "Indeks";
                    break;

                case "Prosek":
                    sortCriteria = "Prosek";
                    break;

                case "Status":
                    sortCriteria = "Status";
                    break;

                case "Godina Studija":
                    sortCriteria = "Godina Studija";
                    break;
            }
            Update();
        }

        private void ProfesorSort(object sender, DataGridSortingEventArgs e)
        {
            ListSortDirection direction;
            direction = (e.Column.SortDirection != ListSortDirection.Ascending) ? ListSortDirection.Ascending : ListSortDirection.Descending;
            sortDirection = (SortDirection)direction;
            switch (e.Column.Header.ToString())
            {
                case "Ime":
                    sortCriteria = "Ime";
                    break;

                case "Prezime":
                    sortCriteria = "Prezime";
                    break;

                case "Zvanje":
                    sortCriteria = "Zvanje";
                    break;

                case "Email":
                    sortCriteria = "Email";
                    break;
            }
            Update();
        }

        private void KatedraAdd(object sender, RoutedEventArgs e)
        {
            KatedraWindow katedra = new KatedraWindow(profesorController);
            katedra.Owner = this;  // Set the owner to MainWindow
            katedra.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            katedra.Show();
        }

        private void PrikaziStudente(object sender, MouseButtonEventArgs e)
        {

            ProfesorStudenti profesorStudenti = new ProfesorStudenti(studentController,SelectedProfesor);
            profesorStudenti.Owner = this;  // Set the owner to MainWindow
            profesorStudenti.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            profesorStudenti.Show();
        }
    }
}

