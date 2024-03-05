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

namespace GUI.View.ProfesorView
{
    /// <summary>
    /// Interaction logic for AddPredmetToProfesor.xaml
    /// </summary>
    public partial class AddPredmetToProfesor : Window
    {
        private Predmet? predmetObjekat;

        private PredmetController predmetController;

        private PredmetDTO predmet = new PredmetDTO();
        private ProfesorDTO profa;

        private ProfesorController profesorController;

        private List<Predmet> listboxpredmeti = new List<Predmet>(); //kada budemo trazili izabrani predmet iz listboxa
        private List<string> list = new List<string>(); //za popunjavanje listboxa
        public AddPredmetToProfesor(ProfesorController profesorController,PredmetController predmetController,ProfesorDTO SelectedProfesor)
        {
            InitializeComponent();
            DataContext = this;
            this.predmetController = predmetController;
            this.profesorController = profesorController;

            profa = SelectedProfesor;

            foreach (Predmet predmet in predmetController.GetAllPredmeti()) 
            {
                //Provera da li neko predaje taj predmet od profesora i ako izabrani profesor vec ne predaje taj predmet
                if (predmet.Predmetni_Profesor.Id == 0) 
                {
                       listboxpredmeti.Add(predmet);
                       string temp = predmet.Sifra_Predmeta + "-" + predmet.Naziv_Predmeta;
                       list.Add(temp);
                }
            }
            listBox1.ItemsSource = list;
        }

        private void Odustani(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DodajPredmet(object sender, RoutedEventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                string pred = listBox1.SelectedItem.ToString(); //selectovan predmet iz listboxa
                string izdvojenaSifra = pred.Substring(0, pred.IndexOf('-'));
                predmetObjekat = listboxpredmeti.Find(p => p.Sifra_Predmeta == izdvojenaSifra);
                profa.Predmeti.Add(predmetObjekat);
                //Find trazi predmet koji smo izabrali iz listboxa,i kada ga nadje sacuva ga u objekat predmetObjekat

                predmet.Id = predmetObjekat.Id;
                predmet.Sifra_Predmeta = predmetObjekat.Sifra_Predmeta;
                predmet.Naziv_Predmeta = predmetObjekat.Naziv_Predmeta;
                predmet.Semestar = predmetObjekat.SemestarPredmeta;
                predmet.Godina_Predmeta = predmetObjekat.Godina_Predmeta;
                predmet.Profesor = profa.ToProfesor();
                predmet.Espb = predmetObjekat.ESPB;

                predmetController.Update(predmet.ToPredmet());
                

                this.Close();
            }
        }
    }
}
/*
 *  Ista prica kao u UpdateProfesor
 *  foreach (Predmet predmet in predmetController.GetAllPredmeti())
            {
                bool nasao = false;
                foreach (Predmet predmet2 in SelectedProfesor.Predmeti) //predmeti koje profesor trenutno predaje
                {
                    if (predmet2.Id == predmet.Id)
                    {
                        nasao = true; //izdvaja predmete koje ne predaje
                        break;
                    }
                }
                if (nasao == false)
                {
                    listboxpredmeti.Add(predmet);
                }
            }
            */
