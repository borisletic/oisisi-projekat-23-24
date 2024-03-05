using CLI.Controller;
using CLI.Model;
using CLI.DAO;
using GUI.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using CLI.Observer;
using GUI.View.StudentView;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for UpdateStudent.xaml
    /// </summary>
    public partial class UpdateStudent : Window , IObserver
    {
        private StudentDTO student = new StudentDTO();

        private StudentController studentController;

        public ObservableCollection<OcenaDTO> Ocena { get; set; }
        public OcenaDTO SelectedOcena { get; set; }
        private OcenaController ocenaController { get; set; }
        public ObservableCollection<PredmetDTO> Predmet { get; set; }
        public PredmetDTO SelectedPredmet { get; set; }
        private PredmetController predmetController { get; set; }

        private Indeks indeksPrikaz = new Indeks();

        private Adresa adresaObjekat;

        private Student studentObjekat = new Student();

        private List<Predmet> dodajPredmet = new List<Predmet>(); // Predmeti koji nebi trebalo da budu u listi kada se dodaje novi predmet za studenta

        public event PropertyChangedEventHandler? PropertyChanged;
        public UpdateStudent(StudentController studentController,PredmetController predmetController,StudentDTO SelectedStudent,OcenaController ocenaController)
        {
            InitializeComponent();
            DataContext = this;
            this.studentController = studentController;

            student = SelectedStudent;
            studentObjekat = SelectedStudent.ToStudent();

            ddlGodinaStudija.Items.Add("1");
            ddlGodinaStudija.Items.Add("2");
            ddlGodinaStudija.Items.Add("3");
            ddlGodinaStudija.Items.Add("4");

            ddlNacinFinansiranja.Items.Add("Budzet");
            ddlNacinFinansiranja.Items.Add("Samofinansiranje");

            lblProsek.Content = "Prosecna ocena: 0";
            lblESPB.Content = "ESPB: 0";

            Predmet = new ObservableCollection<PredmetDTO>();
            this.predmetController = predmetController;

            Ocena = new ObservableCollection<OcenaDTO>();
            this.ocenaController = ocenaController;

            txtIme.Text = SelectedStudent.Ime;
            txtPrezime.Text = SelectedStudent.Prezime;
            txtEmail.Text = SelectedStudent.Email;
            txtDatumRodjenja.Text = SelectedStudent.Datum_Rodjenja.ToString("dd.MM.yyyy");

            adresaObjekat = SelectedStudent.Adresa_Stanovanja;
            txtAdresa.Text = adresaObjekat.Ulica + "," + adresaObjekat.Broj.ToString() + "," + adresaObjekat.Grad + "," + adresaObjekat.Drzava;

            txtBrojTelefona.Text = SelectedStudent.Kontakt_Telefon.ToString();
            txtBrojIndeksa.Text = SelectedStudent.Indeks.Oznaka_Smera + "-" + SelectedStudent.Indeks.Broj_Upisa + "-" + SelectedStudent.Indeks.Godina_Upisa;
            ddlGodinaStudija.SelectedItem = SelectedStudent.Trenutna_Godina_Studija.ToString();
            ddlNacinFinansiranja.SelectedItem = (SelectedStudent.StatusStudenta == 0 ? "Budzet" : "Samofinansiranje");

            Update();
        }

        public void Update()
        {
            int espb = 0;
            Ocena.Clear();
            dodajPredmet.Clear();
            foreach (Ocena ocena in studentObjekat.Polozeni_Ispiti)
            {
                    Ocena.Add(new OcenaDTO(ocena));
                    espb += ocena.Predmet.ESPB;
                    dodajPredmet.Add(ocena.Predmet); //dodaje polozene predmete u listu predmeta koje student slusao
            }
                lblProsek.Content = $"Prosecna ocena: {student.Prosecna_Ocena:F2}";
                lblESPB.Content = $"ESPB: {espb}";

            Predmet.Clear();
            foreach(Predmet predmet in studentObjekat.Nepolozeni_Ispiti) 
            {
                Predmet.Add(new PredmetDTO(predmet));
                dodajPredmet.Add(predmet); //dodaje nepolozene predmete u listu predmeta koje je student slusao
            }
        }

        private void Polaganje(object sender, RoutedEventArgs e)
        {
            if (SelectedPredmet != null)
            {
                UpisOcene upisOcene = new UpisOcene(ocenaController, studentController, SelectedPredmet,student);
                upisOcene.Owner = this;  // Set the owner to MainWindow
                upisOcene.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                upisOcene.ShowDialog();
                Update();
            }
            else
            {
                MessageBox.Show("Mora da se izabere predmet za polaganje", "Info");
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (txtIme.Text != "" && txtPrezime.Text != "" && txtDatumRodjenja.Text != "" && txtAdresa.Text != "" && txtEmail.Text != ""
                && txtBrojTelefona.Text != "" && txtBrojIndeksa.Text != "" && ddlGodinaStudija.Text != "" && ddlNacinFinansiranja.Text != "")
            {

                student.Ime = txtIme.Text;
                student.Prezime = txtPrezime.Text;
                student.Datum_Rodjenja = DateTime.ParseExact(txtDatumRodjenja.Text, "dd.MM.yyyy", null);

                string[] razdvojeni = txtAdresa.Text.Split(',');// [0] ulica  [1] broj [2] grad [3] drzava
                adresaObjekat.Ulica = razdvojeni[0];
                adresaObjekat.Broj = razdvojeni[1];
                adresaObjekat.Grad = razdvojeni[2];
                adresaObjekat.Drzava = razdvojeni[3];
                studentController.UpdateAdresa(adresaObjekat);
                //student.Adresa_Stanovanja = adresaPrikaz.Id; //student.Adresa_Stanovanja je u ovom slucaju int jer smo ga tako oznacili u DTO

                student.Email = txtEmail.Text;
                student.Kontakt_Telefon = txtBrojTelefona.Text;


                razdvojeni = txtBrojIndeksa.Text.Split('-');// [0] Ra  [1] broj [2] godina_upisa
                indeksPrikaz.Id = student.Id;
                indeksPrikaz.Oznaka_Smera = razdvojeni[0];
                indeksPrikaz.Broj_Upisa = int.Parse(razdvojeni[1]);
                indeksPrikaz.Godina_Upisa = int.Parse(razdvojeni[2]);
                //studentController.UpdateIndeks(indeksPrikaz); losa funkcija
                student.Indeks = indeksPrikaz; // student.Indeks je iz DTO 

                student.Trenutna_Godina_Studija = int.Parse(ddlGodinaStudija.Text);
                student.StatusStudenta = (Student.Status)(ddlNacinFinansiranja.Text.Equals("Budzet") ? 0 : 1);


                student.Predmeti = studentObjekat.Nepolozeni_Ispiti; //dodavanje prazne liste nepolozenih ispita
                student.Ocena = studentObjekat.Polozeni_Ispiti; //dodavanje prazne liste polozenih ispita
                

                studentController.Update(student.ToStudent());

                Close();
            }
            else
            {
                MessageBox.Show("Ne sme biti praznih polja", "Info");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void addPredmetToSt(object sender, RoutedEventArgs e)
        {
            AddPredmetToStudent prozor = new AddPredmetToStudent(studentController,predmetController,dodajPredmet,studentObjekat);
            prozor.Owner = this;  // Set the owner to MainWindow
            prozor.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            prozor.ShowDialog();
            Update();
        }

        private void PonistiOcenu(object sender, RoutedEventArgs e)
        {
            OcenaDTO ocena = new OcenaDTO();
            ocena = SelectedOcena;
            if (ocena == null) 
            {
                MessageBox.Show("Niste izabrali ocenu za ponistavanje", "Info");
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Da li ste sigurni da zelite da ponistite ocenu", "Ponistavanje ocene", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    ocenaController.Delete(ocena.Student.Id,ocena.Predmet.Id);
                    studentObjekat.Polozeni_Ispiti.RemoveAll(o => o.Student_Koji_Je_Polozio.Id == ocena.Student.Id && o.Predmet.Id == ocena.Predmet.Id);
                    //prosek
                    student.Prosecna_Ocena = 0;
                    foreach (Ocena o in student.Ocena) 
                    {
                        student.Prosecna_Ocena += o.Ocena_Broj;
                    }
                    student.Prosecna_Ocena = student.Ocena.Count != 0 ? student.Prosecna_Ocena / student.Ocena.Count : 0;
                    student.Predmeti.Add(ocena.Predmet);
                    studentController.Update(student.ToStudent());
                    Update();
                }
            }        
        }

        //Obrisi ocenu iz nepolozenih predmeta
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            PredmetDTO predmet = new PredmetDTO();
            predmet = SelectedPredmet;
            if (predmet == null)
            {
                MessageBox.Show("Niste izabrali predmet za brisanje", "Info");
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Da li ste sigurni da zelite da obrisete predmet", "Brisanje predmeta", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    studentObjekat.Nepolozeni_Ispiti.RemoveAll(p => p.Id == predmet.Id);
                    studentController.Update(studentObjekat);
                    Update();
                }
            }
        }
    }
}
