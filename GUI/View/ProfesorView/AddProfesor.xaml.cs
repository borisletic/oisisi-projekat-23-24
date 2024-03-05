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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for AddProfesor.xaml
    /// </summary>
    public partial class AddProfesor : Window, INotifyPropertyChanged
    {
        public ProfesorDTO Profesor { get; set; }

        private ProfesorController profesorController;

        public event PropertyChangedEventHandler? PropertyChanged;
        public AddProfesor(ProfesorController profesorController)
        {
            InitializeComponent();
            DataContext = this;
            Profesor = new ProfesorDTO();
            this.profesorController = profesorController;
        }
        //Odustani
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Potvrdi
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (txtIme.Text != "" && txtPrezime.Text != "" && txtDatum.Text != "" && txtAdresa.Text != "" && txtKontakt.Text != ""
                && txtEmail.Text != "" && txtLicna.Text != "" && txtZvanje.Text != "" && txtStaz.Text != "")
            {
                Profesor.Ime = txtIme.Text;
                Profesor.Prezime = txtPrezime.Text;
                Profesor.Datum_Rodjenja = DateTime.ParseExact(txtDatum.Text, "dd.MM.yyyy", null);

                string[] razdvojeni = txtAdresa.Text.Split(',');// [0] ulica  [1] broj [2] grad [3] drzava
                Adresa adresa = new Adresa();
                adresa.Ulica = razdvojeni[0];
                adresa.Broj = razdvojeni[1];
                adresa.Grad = razdvojeni[2];
                adresa.Drzava = razdvojeni[3];
                profesorController.AddAdresa(adresa);  // sa ovim pravi novi id i ubacuje ga direktno u objekat adresa
                Profesor.Adresa_Stanovanja = adresa;

                Profesor.Kontakt = txtKontakt.Text;
                Profesor.Email = txtEmail.Text;
                Profesor.Licna = txtLicna.Text;
                Profesor.Zvanje = txtZvanje.Text;
                Profesor.Staz = Convert.ToInt32(txtStaz.Text);
                Profesor.KatedraId = 0;

                profesorController.Add(Profesor.ToProfesor());

                Close();
            }
            else
            {
                MessageBox.Show("Ne sme biti praznih polja", "Info");
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

        private void Adresa_FocusDrop(object sender, RoutedEventArgs e)
        {
            if (txtAdresa.Text == "")
            {
                txtAdresa.Foreground = new SolidColorBrush(Colors.Silver);
                txtAdresa.Text = "Ulica,Broj,Grad,Drzava";
            }
        }
    }
}
