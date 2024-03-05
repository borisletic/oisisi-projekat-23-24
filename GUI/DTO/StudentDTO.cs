using CLI.DAO;
using CLI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GUI.DTO
{
    public class StudentDTO : INotifyPropertyChanged, IDataErrorInfo
    {
        public int Id { get; set; }

        private string ime;
        public string Ime
        {
            get
            {
                return ime;
            }
            set
            {
                if (value != ime)
                {
                    ime = value;
                    OnPropertyChanged("Ime");
                }
            }
        }

        private string prezime;

        public string Prezime
        {
            get
            {
                return prezime;
            }
            set
            {
                if (value != prezime)
                {
                    prezime = value;
                    OnPropertyChanged("Prezime");
                }
            }
        }

        private DateTime datum_Rodjenja;

        public DateTime Datum_Rodjenja
        {
            get
            {
                return datum_Rodjenja;
            }
            set
            {
                if (value != datum_Rodjenja)
                {
                    datum_Rodjenja = value;
                    OnPropertyChanged("Datum_Rodjenja");
                }
            }
        }

        private Adresa adresa = new Adresa();
        public Adresa Adresa_Stanovanja
        {
            get
            {
                return adresa;
            }
            set
            {
                if (value != adresa)
                {
                    adresa = value;
                    OnPropertyChanged("Adresa_Stanovanja");
                }
            }
        }

        private string kontakt_Telefon;
        public string Kontakt_Telefon
        {
            get
            {
                return kontakt_Telefon;
            }
            set
            {
                if (value != kontakt_Telefon)
                {
                    kontakt_Telefon = value;
                    OnPropertyChanged("Kontakt_Telefon");
                }
            }
        }

        private string email;
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                if (value != email)
                {
                    email = value;
                    OnPropertyChanged("Email");
                }
            }
        }

        private int godina_Studija;
        public int Trenutna_Godina_Studija
        {
            get
            {
                return godina_Studija;
            }
            set
            {
                if (value != godina_Studija)
                {
                    godina_Studija = value;
                    OnPropertyChanged("Trenutna_Godina_Studija");
                }
            }
        }

        private Student.Status statusStudenta;
        public Student.Status StatusStudenta
        {
            get
            {
                return statusStudenta;
            }
            set
            {
                if (value != statusStudenta)
                {
                    statusStudenta = value;
                    OnPropertyChanged("StatusStudenta");
                }
            }
        }

        private Indeks indeks;
        public Indeks Indeks
        {
            get
            {
                return indeks;

            }
            set //Odavde cita iz notepada(value je Smer-Broj-Godina)
            {
                if (!value.Equals(indeks))
                {
                    indeks = value;
                    OnPropertyChanged("Indeks");
                }
            }
        }

        private double prosecna_Ocena;
        public double Prosecna_Ocena
        {
            get
            {
                return prosecna_Ocena;
            }
            set
            {
                if (value != prosecna_Ocena)
                {
                    prosecna_Ocena = value;
                    OnPropertyChanged("Prosecna_Ocena");
                }
            }
        }

        private List<Predmet> predmeti = new List<Predmet>();
        public List<Predmet> Predmeti
        {
            get
            {
                return predmeti;
            }
            set
            {
                if (value != predmeti)
                {
                    predmeti = value;
                    OnPropertyChanged("Predmeti");
                }
            }
        }

        //Sluzi samo da doda praznu listu ocena kada dodaje i edituje novog studenta
        private List<Ocena> ocena = new List<Ocena>();
        public List<Ocena> Ocena
        {
            get
            {
                return ocena;
            }
            set
            {
                if (value != ocena)
                {
                    ocena = value;
                    OnPropertyChanged("Ocena");
                }
            }
        }

        private string naziv_Predmeta;
        public string Naziv_Predmeta
        {
            get
            {
                return naziv_Predmeta;
            }
            set
            {
                if (value != naziv_Predmeta)
                {
                    naziv_Predmeta = value;
                    OnPropertyChanged("Naziv_Predmeta");
                }
            }
        }

        public string Error => throw new NotImplementedException();

        public string this[string columnName] => throw new NotImplementedException();

        public StudentDTO()
        {
        }
        public StudentDTO(Student student)
        {
            this.Id = student.Id;
            this.Ime = student.Ime;
            this.Prezime = student.Prezime;
            this.Datum_Rodjenja = student.Datum_Rodjenja;
            this.Adresa_Stanovanja = student.Adresa_Stanovanja;
            this.Kontakt_Telefon = student.Kontakt_Telefon;
            this.Email = student.Email;
            this.Indeks = student.Indeks;
            this.Trenutna_Godina_Studija = student.Trenutna_Godina_Studija;
            this.StatusStudenta = student.StatusStudenta;
            this.Prosecna_Ocena = student.Prosecna_Ocena;
            this.Predmeti = student.Nepolozeni_Ispiti;
            this.Ocena = student.Polozeni_Ispiti;
            this.Naziv_Predmeta = student.Naziv_Predmeta;
        }

         public Student ToStudent() 
         {
            Student student = new Student();
            student.Id = this.Id;
            student.Ime = ime;
            student.Prezime = prezime;
            student.Datum_Rodjenja = datum_Rodjenja;
            student.Adresa_Stanovanja = adresa;
            student.Kontakt_Telefon = kontakt_Telefon;
            student.Email = email;
            student.Indeks = indeks;
            student.Trenutna_Godina_Studija = godina_Studija;
            student.StatusStudenta = statusStudenta;
            student.Prosecna_Ocena = prosecna_Ocena;
            student.Nepolozeni_Ispiti = predmeti;
            student.Polozeni_Ispiti = ocena;
            return student;
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
