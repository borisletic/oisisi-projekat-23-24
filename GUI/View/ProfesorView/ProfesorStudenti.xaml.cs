using CLI.Controller;
using CLI.Model;
using CLI.Observer;
using GUI.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
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

namespace GUI.View.ProfesorView
{
    /// <summary>
    /// Interaction logic for ProfesorStudenti.xaml
    /// </summary>
    public partial class ProfesorStudenti : Window , IObserver
    {
        public ObservableCollection<StudentDTO> Student { get; set; }
        public StudentDTO SelectedStudent { get; set; }
        private StudentController studentController { get; set; }

        private ProfesorDTO profesor;
        List<Student> temp = new List<Student>(); //lista koja cuva studente da ne radi Update 

        public ProfesorStudenti(StudentController studentController,ProfesorDTO SelectedProfesor)
        {
            InitializeComponent();
            DataContext = this;
            Student = new ObservableCollection<StudentDTO>();
            this.studentController = studentController;

            profesor = SelectedProfesor;
            Update();
        }

        public void Update()
        {
            Student.Clear();
            foreach (Predmet predmet in profesor.Predmeti) 
            {
                foreach (Student student in studentController.GetAllStudents()) 
                {
                    foreach (Predmet predmetStudent in student.Nepolozeni_Ispiti) 
                    {
                        if (predmet.Id == predmetStudent.Id) 
                        {
                            student.Naziv_Predmeta = predmet.Naziv_Predmeta;
                            Student.Add(new StudentDTO(student));
                            temp.Add(student);
                        }
                    }
                }
            }
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearch.Text;
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    MessageBox.Show("Morate uneti makar prezime za pretragu studenta.Druge kombinacije(prezime ime) ili (indeks ime prezime)", "Info");
                    foreach (Student student in temp) 
                    {
                    
                    }
                }
                else
                {
                    List<Student> lista_studenata = new List<Student>();
                    string[] info = searchText.Split(" "); // 1 i 2 reci [0] prezime [1] ime kod 3 reci [0]indeks [1]ime [2]prezime
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
                            s.Ime.StartsWith(info[1], StringComparison.OrdinalIgnoreCase)).ToList();
                            break;
                        case 3:
                            //Trazimo studenta sa datim indeksom imenom prezimenom 
                            //StackOverflow kod
                            Student.Clear();
                            lista_studenata.Clear();
                            lista_studenata = studentController.GetAllStudents().Where(s => s.Prezime.Contains(info[2], StringComparison.OrdinalIgnoreCase) &&
                            s.Ime.StartsWith(info[1], StringComparison.OrdinalIgnoreCase) && s.Indeks.ToString().StartsWith(info[0], StringComparison.OrdinalIgnoreCase)).ToList();
                            break;
                    }
                    foreach (Student temp in lista_studenata)
                    {
                        Student.Add(new StudentDTO(temp));
                    }
                }
        }
    }
}
