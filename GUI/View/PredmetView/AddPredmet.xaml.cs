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
    /// Interaction logic for AddPredmet.xaml
    /// </summary>
    public partial class AddPredmet : Window, INotifyPropertyChanged
    {
        public PredmetDTO Predmet = new PredmetDTO();

        private PredmetController predmetController;

        public event PropertyChangedEventHandler? PropertyChanged;
        public AddPredmet(PredmetController predmetController)
        {
            InitializeComponent();
            ddlSemestar.Items.Add("Ljetni");
            ddlSemestar.Items.Add("Zimski");
            
            ddlGodinaPredmeta.Items.Add("1");
            ddlGodinaPredmeta.Items.Add("2");
            ddlGodinaPredmeta.Items.Add("3");
            ddlGodinaPredmeta.Items.Add("4");

            DataContext = this;
            this.predmetController = predmetController;
        }
        //Odustani
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Potvrdi
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (txtSP.Text != "" && txtNP.Text != "" && ddlSemestar.Text != "" && ddlGodinaPredmeta.Text != "" && txtESPB.Text != "")
            {
                Predmet = new PredmetDTO();

                Predmet.Sifra_Predmeta = txtSP.Text;
                Predmet.Naziv_Predmeta = txtNP.Text;
                Predmet.Semestar = (Predmet.Semestar)(ddlSemestar.Text.Equals("Ljetni") ? 0 : 1);
                Predmet.Godina_Predmeta = Convert.ToInt32(ddlGodinaPredmeta.Text);
                Predmet.Espb = Convert.ToInt32(txtESPB.Text);
                Predmet.Profesor = new Profesor();  

                predmetController.Add(Predmet.ToPredmet());
                this.Close();
            }
            else
            {
                MessageBox.Show("Ne sme biti praznih polja", "Info");
            }
        }
    }
}
