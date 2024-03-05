using CLI.Controller;
using CLI.Model;
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

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for SelectKatedra.xaml
    /// </summary>
    public partial class SelectKatedra : Window
    {
        private ProfesorController profesorController;

        public Katedra katedraObjekat;

        private List<Katedra> listboxkatedra = new List<Katedra>();
        private List<string> list = new List<string>();
        public SelectKatedra(ProfesorController profesorController)
        {
            InitializeComponent();
            DataContext = this;
            this.profesorController = profesorController;

            foreach (Katedra katedra in profesorController.GetAllKatedra())
            {
                listboxkatedra.Add(katedra);
                string temp = katedra.Naziv_Katedre;
                list.Add(temp);
            }
            listBox1.ItemsSource = list;
        }

        private void Izaberi_Katedru(object sender, RoutedEventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                string pred = listBox1.SelectedItem.ToString();
                katedraObjekat = listboxkatedra.Find(k => k.Naziv_Katedre == pred);
                //Find ubaci u katedruObjekat izabranu katedru iz listboxa
                this.Close();
            }
        }

        private void Odustani(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
