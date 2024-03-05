using CLI.Controller;
using CLI.DAO;
using CLI.Model;
using GUI.DTO;
using System;
using System.Collections.Generic;
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

namespace GUI.View.StudentView
{
    /// <summary>
    /// Interaction logic for AddPredmetToStudent.xaml
    /// </summary>
    public partial class AddPredmetToStudent : Window
    {
        private StudentController studentController;

        private Student studentObjekat;

        private StudentDTO student = new StudentDTO();

        List<Predmet> listboxpredmeti = new List<Predmet>();
        public AddPredmetToStudent(StudentController studentController,PredmetController predmetController,List<Predmet> predmeti,Student Student) 
            //predmeti-lista predmeta koji ne trebaju da budu u listboxu godina-trenutna godina studenta
        {
            InitializeComponent();

            studentObjekat = Student; //trenutno sa kojim studentom radimo
            foreach(Predmet predmet in predmetController.GetAllPredmeti())
            {
                bool nasao = false;
                foreach(Predmet predmet2 in predmeti)
                {
                    if (predmet2.Id == predmet.Id)
                    {
                        nasao = true;
                        break;
                    }
                }
                if (nasao == false && predmet.Godina_Predmeta <= Student.Trenutna_Godina_Studija) 
                {
                    listboxpredmeti.Add(predmet);
                }
            }
            //dodavanje u listu
            List<string> list = new List<string>();
            foreach (Predmet p in listboxpredmeti) 
            {
                string temp = p.Sifra_Predmeta + "-" + p.Naziv_Predmeta;
                list.Add(temp);
            }
            listBox1.ItemsSource = list;

            DataContext = this;
            this.studentController = studentController;
            
        }

        //Odustani
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Dugme za dodavanje predmeta u datagrid
        private void DodajPredmet(object sender, RoutedEventArgs e)
        {
            if (listBox1.SelectedIndex != -1) 
            {
                string pred = listBox1.SelectedItem.ToString();
                string izdvojenaSifra = pred.Substring(0, pred.IndexOf('-'));
                studentObjekat.Nepolozeni_Ispiti.Add(listboxpredmeti.Find(p => p.Sifra_Predmeta == izdvojenaSifra));
                //Find kada nadje predmet iz listboxa,doda ga u listu Predmeta od Studenta
                //student je od StudentaDTO
                student.Id = studentObjekat.Id;
                student.Ime = studentObjekat.Ime;
                student.Prezime =  studentObjekat.Prezime;
                student.Datum_Rodjenja = studentObjekat.Datum_Rodjenja;
                student.Adresa_Stanovanja = studentObjekat.Adresa_Stanovanja;
                student.Email = studentObjekat.Email;
                student.Kontakt_Telefon = studentObjekat.Kontakt_Telefon;
                student.Indeks = studentObjekat.Indeks;
                student.Trenutna_Godina_Studija = studentObjekat.Trenutna_Godina_Studija;
                student.StatusStudenta = studentObjekat.StatusStudenta;
                student.Predmeti = studentObjekat.Nepolozeni_Ispiti;

                studentController.Update(student.ToStudent());
                this.Close();
            }
        }
    }
}
