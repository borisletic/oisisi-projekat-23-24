using CLI.Controller;
using CLI.DAO;
using CLI.Model;
using CLI.Observer;
using GUI.DTO;
using GUI.View.ProfesorView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for UpdateProfesor.xaml
    /// </summary>
    public partial class UpdateProfesor : Window , IObserver
    {
        public ProfesorDTO profesor;
        public ObservableCollection<PredmetDTO> Predmet { get; set; }
        public PredmetDTO SelectedPredmet { get; set; }
        private PredmetController predmetController { get; set; }

        private Adresa adresaObjekat = new Adresa();

        private ProfesorController profesorController;

        public event PropertyChangedEventHandler? PropertyChanged;
        public UpdateProfesor(ProfesorController profesorController, ProfesorDTO SelectedProfesor,PredmetController predmetController)
        {
            InitializeComponent();
            DataContext = this;
            this.profesorController = profesorController;

            Predmet = new ObservableCollection<PredmetDTO>();
            this.predmetController = predmetController;     

            profesor = SelectedProfesor;

            txtIme.Text = SelectedProfesor.Ime;
            txtPrezime.Text = SelectedProfesor.Prezime;
            txtDatum.Text = SelectedProfesor.Datum_Rodjenja.ToString("dd.MM.yyyy");

            adresaObjekat = SelectedProfesor.Adresa_Stanovanja;
            txtAdresa.Text = adresaObjekat.Ulica + "," + adresaObjekat.Broj + "," + adresaObjekat.Grad + "," + adresaObjekat.Drzava;

            txtKontakt.Text = SelectedProfesor.Kontakt;
            txtEmail.Text = SelectedProfesor.Email;
            txtLicna.Text = SelectedProfesor.Licna;
            txtZvanje.Text = SelectedProfesor.Zvanje;
            txtStaz.Text = SelectedProfesor.Staz.ToString();

            Update();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (txtIme.Text != "" && txtPrezime.Text != "" && txtDatum.Text != "" && txtAdresa.Text != "" && txtKontakt.Text != ""
                && txtEmail.Text != "" && txtLicna.Text != "" && txtZvanje.Text != "" && txtStaz.Text != "")
            {
                profesor.Ime = txtIme.Text;
                profesor.Prezime = txtPrezime.Text;
                profesor.Datum_Rodjenja = DateTime.ParseExact(txtDatum.Text, "dd.MM.yyyy", null);

                string[] razdvojeni = txtAdresa.Text.Split(',');// [0] ulica  [1] broj [2] grad [3] drzava
                adresaObjekat.Ulica = razdvojeni[0];
                adresaObjekat.Broj = razdvojeni[1];
                adresaObjekat.Grad = razdvojeni[2];
                adresaObjekat.Drzava = razdvojeni[3];
                profesorController.UpdateAdresa(adresaObjekat);
                profesor.Adresa_Stanovanja = adresaObjekat;

                profesor.Kontakt = txtKontakt.Text;
                profesor.Email = txtEmail.Text;
                profesor.Licna = txtLicna.Text;
                profesor.Zvanje = txtZvanje.Text;
                profesor.Staz = Convert.ToInt32(txtStaz.Text);

                if (profesor.Predmeti == null)
                {
                    List<Predmet> prazno = new List<Predmet>();
                    prazno.Clear();
                    profesor.Predmeti = prazno; //dodavanje prazne liste Predmeta koje predaje
                }
                
                profesorController.Update(profesor.ToProfesor());
                this.Close();
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

        public void Update()
        {
            Predmet.Clear();
            foreach (Predmet predmet in profesor.Predmeti) 
            {
                Predmet.Add(new PredmetDTO(predmet));
            }
        }

        private void DodajPredmet(object sender, RoutedEventArgs e)
        {
            AddPredmetToProfesor studentiUpdate = new AddPredmetToProfesor(profesorController,predmetController,profesor);

            studentiUpdate.Owner = this;  // Set the owner to MainWindow
            studentiUpdate.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            studentiUpdate.ShowDialog();
            Update();
        }

        private void UkloniPredmet(object sender, RoutedEventArgs e)
        {
            PredmetDTO predmet = new PredmetDTO();
            predmet = SelectedPredmet;
            if (SelectedPredmet == null)
            {
                MessageBox.Show("Niste izabrali predmet koji treba da se ukloni", "Info");
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Da li ste sigurni da zelite da uklonite ovaj predmet", "Uklanjanje predmeta", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    profesor.Predmeti.RemoveAll(p => p.Id == predmet.Id);
                    predmet.Profesor = new Profesor();
                    predmetController.Update(predmet.ToPredmet());
                    Update();
                }
            }
        }
    }
}


/*prethodno resenje misleci da predmet moze biti predavan od strane vise profesora,a podatak za to nije bio dat u test podacima
    profesorObjekat = profesorController.GetProfesorById(SelectedProfesor.Id);
    Profesor = SelectedProfesor;

    Predmet.Clear();
                foreach (Predmet predmet in predmetController.GetAllPredmeti())
                {
                    if (profesorObjekat.Id == predmet.Predmetni_Profesor.Id)
                    {
                        Predmet.Add(new PredmetDTO(predmet)); //inicijalna lista koju je ucitao iz fajla
                    }
                }
                foreach (Predmet predmet in profesorObjekat.Spisak_Predmeta_Profesora) 
                {
                    Predmet.Add(new PredmetDTO(predmet));   //ovde se dodaju one koje smo dodali programski,brise se kada se iskljuci program
                }
 */