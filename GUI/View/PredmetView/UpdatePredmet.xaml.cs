using CLI.Controller;
using CLI.DAO;
using CLI.Model;
using CLI.Observer;
using GUI.DTO;
using GUI.View.PredmetView;
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
    /// Interaction logic for UpdatePredmet.xaml
    /// </summary>
    public partial class UpdatePredmet : Window
    {
        public PredmetDTO predmet = new PredmetDTO();

        private PredmetController predmetController;

        private ProfesorController profesorController;

        public event PropertyChangedEventHandler? PropertyChanged;
        public UpdatePredmet(ProfesorController profesorController,PredmetController predmetController, PredmetDTO SelectedPredmet)
        {
            InitializeComponent();
            DataContext = this;
            this.predmetController = predmetController;
            this.profesorController = profesorController;

            ddlGodinaPredmeta.Items.Add("1");
            ddlGodinaPredmeta.Items.Add("2");
            ddlGodinaPredmeta.Items.Add("3");
            ddlGodinaPredmeta.Items.Add("4");

            ddlSemestar.Items.Add("Ljetni");
            ddlSemestar.Items.Add("Zimski");

            predmet = SelectedPredmet;

            predmet.Id = SelectedPredmet.Id;
            txtSP.Text = SelectedPredmet.Sifra_Predmeta;
            txtNP.Text = SelectedPredmet.Naziv_Predmeta;
            ddlSemestar.SelectedItem = (SelectedPredmet.Semestar == 0 ? "Ljetni" : "Zimski");

            txtPP.Text = SelectedPredmet.Profesor.Ime + " " + SelectedPredmet.Profesor.Prezime; ;
            predmet.Profesor = SelectedPredmet.Profesor;
            if (!string.IsNullOrWhiteSpace(txtPP.Text))
            {
                ButtonDodaj.IsEnabled = false;
            }
            else 
            {
                ButtonObrisi.IsEnabled = false;
            }

            ddlGodinaPredmeta.SelectedItem = SelectedPredmet.Godina_Predmeta.ToString();
            txtESPB.Text = SelectedPredmet.Espb.ToString();

            

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (txtSP.Text != "" && txtNP.Text != "" && ddlSemestar.Text != "" && ddlGodinaPredmeta.Text != "" && txtPP.Text != ""
                && txtESPB.Text != "")
            {
                predmet.Sifra_Predmeta = txtSP.Text;
                predmet.Naziv_Predmeta = txtNP.Text;
                predmet.Semestar = (Predmet.Semestar)(ddlSemestar.Text.Equals("Ljetni") ? 0 : 1);
                predmet.Godina_Predmeta = Convert.ToInt32(ddlGodinaPredmeta.Text);
                predmet.Espb = Convert.ToInt32(txtESPB.Text);

                predmetController.Update(predmet.ToPredmet());
                this.Close();
                
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DodajProfesora(object sender, RoutedEventArgs e)
        {
            AddProfessorToPredmet studentiUpdate = new AddProfessorToPredmet(profesorController, predmetController, predmet);
            studentiUpdate.Owner = this;  // Set the owner to MainWindow
            studentiUpdate.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            studentiUpdate.ShowDialog();
            if (predmet.Profesor.Ime != string.Empty) {
                txtPP.Text = predmet.Profesor.Ime + " " + predmet.Profesor.Prezime;
                ButtonDodaj.IsEnabled = false;
                ButtonObrisi.IsEnabled = true;
            }
        }

        private void ObrisiProfesora(object sender, RoutedEventArgs e)
        {
                MessageBoxResult result = MessageBox.Show("Da li ste sigurni da zelite da uklonite profesora sa predmeta", "Brisanje profesora sa predmeta", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    predmet.Profesor.Spisak_Predmeta_Profesora.RemoveAll(p => p.Id == predmet.Id); //brisemo oznaceni predmet iz spiska predmeta koji je profesor predavao 
                    predmet.Profesor = new Profesor();        
                    predmetController.Update(predmet.ToPredmet());
                    txtPP.Text = "";
                    ButtonObrisi.IsEnabled = false;
                    ButtonDodaj.IsEnabled = true;
                }         
        }
    }
}
