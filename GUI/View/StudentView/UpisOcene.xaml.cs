using CLI.Controller;
using CLI.Model;
using CLI.Observer;
using CLI.DAO;
using GUI.DTO;
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
using System.Windows.Shapes;

namespace GUI.View.StudentView
{

    public partial class UpisOcene : Window
    {
        private StudentDTO student = new StudentDTO();

        private OcenaDTO ocena = new OcenaDTO();

        private OcenaController ocenaController;

        private StudentController studentController;

        private Predmet predmetObjekat = new Predmet();

        private Ocena ocenaObjekat = new Ocena();

        public event PropertyChangedEventHandler? PropertyChanged;
        public UpisOcene(OcenaController ocenaController, StudentController studentController, PredmetDTO SelectedPredmet,StudentDTO SelectedStudent)
        {
            InitializeComponent();
            DataContext = this;

            ddlOcena.Items.Add("6");
            ddlOcena.Items.Add("7");
            ddlOcena.Items.Add("8");
            ddlOcena.Items.Add("9");
            ddlOcena.Items.Add("10");

            this.studentController = studentController;
            this.ocenaController = ocenaController;

            student = SelectedStudent;
            predmetObjekat = SelectedPredmet.ToPredmet();

            txtSifra.Text = SelectedPredmet.Sifra_Predmeta.ToString();
            txtNaziv.Text = SelectedPredmet.Naziv_Predmeta.ToString();

        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (txtSifra.Text != "" && txtNaziv.Text != "" && ddlOcena.Text != "" && txtDatum.Text != "")
            {
                ocena.Student = student.ToStudent();
                ocena.Predmet = predmetObjekat;

                ocena.Ocena_Broj = int.Parse(ddlOcena.Text);

                ocena.Datum_Polaganja = DateTime.ParseExact(txtDatum.Text, "dd.MM.yyyy", null);

                ocenaController.Add(ocena.ToOcena());
                student.Predmeti.RemoveAll(p => p.Id == predmetObjekat.Id);
                student.Ocena.Add(ocena.ToOcena());
                student.Prosecna_Ocena = 0;
                foreach (Ocena ocena in student.Ocena)
                {
                    student.Prosecna_Ocena += ocena.Ocena_Broj;
                }
                student.Prosecna_Ocena /= student.Ocena.Count;
                studentController.Update(student.ToStudent());

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




    }
}
