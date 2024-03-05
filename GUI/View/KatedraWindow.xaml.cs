using CLI.Controller;
using CLI.Model;
using CLI.Observer;
using GUI.DTO;
using GUI.View.StudentView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Katedra.xaml
    /// </summary>
    public partial class KatedraWindow : Window,IObserver
    {
        public ObservableCollection<ProfesorDTO> Profesor { get; set; }
        public ProfesorDTO SelectedProfesor { get; set; }
        private ProfesorController profesorController { get; set; }
        private Katedra katedraObjekat = new Katedra();

       
        public KatedraWindow(ProfesorController profesorController)
        {
            InitializeComponent();
            DataContext = this;    
            Profesor = new ObservableCollection<ProfesorDTO>();
            this.profesorController = profesorController;
        }
        public void Update() 
        {
            Profesor.Clear();
            foreach (Profesor profesor in katedraObjekat.Spisak_Profesora)
            {
                if(katedraObjekat.Sef_Katedre != profesor.Id)
                Profesor.Add(new ProfesorDTO(profesor));
            }
        }

        private void PostaviZaSefa(object sender, RoutedEventArgs e)
        {
            if (SelectedProfesor == null)
            {
                MessageBox.Show("Morate izabrati profesora kojeg zelite da unapredite", "Info");
            }
            else if (katedraObjekat.Id == 1 && SelectedProfesor.Staz >= 5 && SelectedProfesor.Zvanje == "vanredni profesor" || SelectedProfesor.Zvanje == "redovni profesor")
            {
                MessageBoxResult result = MessageBox.Show("Da li ste sigurni da zelite da izaberete ovog profesora za sefa katedre za informatiku?", "Postavljanje sefa katedre", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    katedraObjekat.Sef_Katedre = SelectedProfesor.Id;
                    lblSef.Content = "Sef katedre: " + SelectedProfesor.Ime + " " + SelectedProfesor.Prezime;
                    profesorController.UpdateKatedra(katedraObjekat);
                    Update();
                }
            }
            else if (katedraObjekat.Id == 2 && SelectedProfesor.Staz >= 5 && SelectedProfesor.Zvanje == "vanredni profesor" || SelectedProfesor.Zvanje == "redovni profesor")
            {
                MessageBoxResult result = MessageBox.Show("Da li ste sigurni da zelite da izaberete ovog profesora za sefa katedre za matematiku?", "Postavljanje sefa katedre", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    katedraObjekat.Sef_Katedre = SelectedProfesor.Id;
                    lblSef.Content = "Sef katedre: " + SelectedProfesor.Ime + " " + SelectedProfesor.Prezime;
                    profesorController.UpdateKatedra(katedraObjekat);
                    Update();
                }
            }
            else 
            {
                MessageBox.Show("Profesor ne zadovoljava uslove da postane sef katedre", "Info");
            }
        }

        private void IzaberiKatedru(object sender, RoutedEventArgs e)
        {
            SelectKatedra selectWindow = new SelectKatedra(profesorController);
            selectWindow.Owner = this;  // Set the owner to MainWindow
            selectWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            selectWindow.ShowDialog();

            katedraObjekat = selectWindow.katedraObjekat;
            if (katedraObjekat != null)
            {
                lblKatedra.Content = katedraObjekat.Naziv_Katedre;

                foreach (Profesor profesor in profesorController.GetAllProfessors())
                {
                    if (profesor.Id == katedraObjekat.Sef_Katedre)
                    {
                        lblSef.Content = "Sef katedre: " + profesor.Ime + " " + profesor.Prezime;
                        break;
                    }
                }
                Update();
            }
        }
    }
}
