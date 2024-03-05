using CLI.Controller;
using CLI.DAO;
using CLI.Model;
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

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for AddStudent.xaml
    /// </summary>
    public partial class AddStudent : Window , INotifyPropertyChanged
    {
        private StudentDTO Student = new StudentDTO();

        private StudentController studentController;


        public event PropertyChangedEventHandler? PropertyChanged;
        public AddStudent(StudentController studentsController)
        {
            InitializeComponent();
            ddlGodinaStudija.Items.Add("1");
            ddlGodinaStudija.Items.Add("2");
            ddlGodinaStudija.Items.Add("3");
            ddlGodinaStudija.Items.Add("4");

            ddlNacinFinansiranja.Items.Add("Budzet");
            ddlNacinFinansiranja.Items.Add("Samofinansiranje");

            DataContext = this;
            this.studentController = studentsController;
        }
        //Odustani
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Potvrdi
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (txtIme.Text != "" && txtPrezime.Text != "" && txtDatumRodjenja.Text != "" && txtAdresa.Text != "" && txtEmail.Text != ""
                && txtBrojTelefona.Text != "" && txtBrojIndeksa.Text != "" &&  ddlGodinaStudija.Text != "" && ddlNacinFinansiranja.Text != "")
            {

                Student.Ime = txtIme.Text;
                Student.Prezime = txtPrezime.Text;
                Student.Datum_Rodjenja = DateTime.ParseExact(txtDatumRodjenja.Text, "dd.MM.yyyy", null);


                string[] razdvojeni = txtAdresa.Text.Split(',');// [0] ulica  [1] broj [2] grad [3] drzava
                Adresa adresa = new Adresa();
                adresa.Ulica = razdvojeni[0];
                adresa.Broj = razdvojeni[1];
                adresa.Grad = razdvojeni[2];
                adresa.Drzava = razdvojeni[3];
                studentController.AddAdresa(adresa);  // sa ovim pravi novi id i ubacuje ga direktno u objekat adresa
                Student.Adresa_Stanovanja = adresa;

                Student.Email = txtEmail.Text;
                Student.Kontakt_Telefon = txtBrojTelefona.Text;


                razdvojeni = txtBrojIndeksa.Text.Split('-');// [0] Ra  [1] broj [2] godina upisa
                Indeks indeksPrikaz = new Indeks();
                indeksPrikaz.Oznaka_Smera = razdvojeni[0];
                indeksPrikaz.Broj_Upisa = int.Parse(razdvojeni[1]);
                indeksPrikaz.Godina_Upisa = int.Parse(razdvojeni[2]);
               // studentController.AddIndeks(indeksPrikaz); losa funkcija
                Student.Indeks = indeksPrikaz;


                Student.Trenutna_Godina_Studija = int.Parse(ddlGodinaStudija.Text);
                Student.StatusStudenta = (Student.Status)(ddlNacinFinansiranja.Text.Equals("Budzet") ? 0 : 1);
                Student.Prosecna_Ocena = 0;

                Student s = new Student();
                s.Nepolozeni_Ispiti.Clear();
                s.Polozeni_Ispiti.Clear();
                Student.Predmeti = s.Nepolozeni_Ispiti; //dodavanje prazne liste nepolozenih ispita
                Student.Ocena = s.Polozeni_Ispiti; //dodavanje prazne liste polozenih ispita

                studentController.Add(Student.ToStudent());
                Close();
            }
            else
            {
                MessageBox.Show("Ne sme biti praznih polja","Info");
            }
        }

        private void Adresa_Focus(object sender, RoutedEventArgs e)
        {
            if (txtAdresa.Text == "Ulica,Broj,Grad,Drzava")
            {
                txtAdresa.Foreground = new SolidColorBrush(Colors.Black);
                txtAdresa.Text = "";
            }

        }

        private void Adresa_DropFocus(object sender, RoutedEventArgs e)
        {
            if (txtAdresa.Text == "")
            {
                txtAdresa.Foreground = new SolidColorBrush(Colors.Silver);
                txtAdresa.Text = "Ulica,Broj,Grad,Drzava";
            }
        }

        private void Indeks_Focus(object sender, RoutedEventArgs e)
        {
            if (txtBrojIndeksa.Text == "AA-1-1990")
            {
                txtBrojIndeksa.Foreground = new SolidColorBrush(Colors.Black);
                txtBrojIndeksa.Text = "";
            }
        }

        private void Indeks_DropFocus(object sender, RoutedEventArgs e)
        {
            if (txtBrojIndeksa.Text == "")
            {
                txtBrojIndeksa.Foreground = new SolidColorBrush(Colors.Silver);
                txtBrojIndeksa.Text = "AA-1-1990";
            }
        }

        private void Datum_Focus(object sender, RoutedEventArgs e)
        {
            if (txtDatumRodjenja.Text == "01.01.1900")
            {
                txtDatumRodjenja.Foreground = new SolidColorBrush(Colors.Black);
                txtDatumRodjenja.Text = "";
            }
        }

        private void Datum_DropFocus(object sender, RoutedEventArgs e)
        {
            if (txtDatumRodjenja.Text == "")
            {
                txtDatumRodjenja.Foreground = new SolidColorBrush(Colors.Silver);
                txtDatumRodjenja.Text = "01.01.1900";
            }
        }
    }
}
