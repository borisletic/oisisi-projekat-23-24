using CLI.Controller;
using CLI.Model;
using GUI.DTO;
using System;
using System.Collections.Generic;
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

namespace GUI.View.PredmetView
{
    /// <summary>
    /// Interaction logic for AddProfessorToPredmet.xaml
    /// </summary>
    public partial class AddProfessorToPredmet : Window
    {

        private PredmetController predmetController;

        private ProfesorController profesorController;

        private PredmetDTO predmet;

        private List<Profesor> listboxpredmeti = new List<Profesor>(); //kada budemo trazili izabrani predmet iz listboxa
        private List<string> list = new List<string>(); //za popunjavanje listboxa
        public AddProfessorToPredmet(ProfesorController profesorController,PredmetController predmetController,PredmetDTO SelectedPredmet)
        {
            InitializeComponent();
            DataContext = this;
            this.predmetController = predmetController;
            this.profesorController = profesorController;

            predmet = SelectedPredmet;

            foreach (Profesor profesor in profesorController.GetAllProfessors())
            {
                    listboxpredmeti.Add(profesor);
                    string temp = profesor.Ime + " " + profesor.Prezime;
                    list.Add(temp);
            }
            listBox1.ItemsSource = list;
        }

        private void DodajProfesora(object sender, RoutedEventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                string pred = listBox1.SelectedItem.ToString();
                string[] izdvojenProfesor = pred.Split(" ");
                predmet.Profesor = listboxpredmeti.Find(prof => prof.Ime == izdvojenProfesor[0] && prof.Prezime == izdvojenProfesor[1]);
                //Find kada nadje profesora iz listboxa po imenu i prezimenu ubacuje u objekat predmeta koji cuva koji profesor ga predaj

                predmet.Profesor.Spisak_Predmeta_Profesora.Add(predmet.ToPredmet());
                predmetController.Update(predmet.ToPredmet());
                this.Close();
            }
        }

        private void Odustani(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
