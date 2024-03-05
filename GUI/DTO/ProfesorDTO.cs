using CLI.DAO;
using CLI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.DTO
{
    public class ProfesorDTO : INotifyPropertyChanged, IDataErrorInfo
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

        private string kontakt;
        public string Kontakt
        {
            get
            {
                return kontakt;
            }
            set
            {
                if (value != kontakt)
                {
                    kontakt = value;
                    OnPropertyChanged("Kontakt telefon");
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
                    OnPropertyChanged("E-mail");
                }
            }
        }

        private string licna;
        public string Licna
        {
            get
            {
                return licna;
            }
            set
            {
                if (value != licna)
                {
                    licna = value;
                    OnPropertyChanged("Broj licne karte");
                }
            }
        }

        private string zvanje;
        public string Zvanje
        {
            get
            {
                return zvanje;
            }
            set
            {
                if (value != zvanje)
                {
                    zvanje = value;
                    OnPropertyChanged("Zvanje");
                }
            }
        }

        private int staz;
        public int Staz
        {
            get
            {
                return staz;
            }
            set
            {
                if (value != staz)
                {
                    staz = value;
                    OnPropertyChanged("Godine staza");
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

        private int katedraId;

        public int KatedraId
        {
            get
            {
                return katedraId;
            }
            set
            {
                if (value != katedraId)
                {
                    katedraId = value;
                    OnPropertyChanged("KatedraId");
                }
            }
        }

        public string Error => throw new NotImplementedException();

        public string this[string columnName] => throw new NotImplementedException();

        public Profesor ToProfesor()
        {
            Profesor profesor = new Profesor();
            profesor.Id = this.Id;
            profesor.Ime = this.ime;
            profesor.Prezime = this.prezime;
            profesor.Datum_Rodjenja = this.datum_Rodjenja;
            profesor.Adresa_Stanovanja = this.adresa;
            profesor.Kontakt_Telefon = this.kontakt;
            profesor.Email = this.email;
            profesor.Broj_Licne_Karte = this.licna;
            profesor.Zvanje = this.zvanje;
            profesor.Godine_Staza = this.staz;
            profesor.Spisak_Predmeta_Profesora = this.predmeti;
            profesor.KatedraId = this.katedraId;
            return profesor;
        }

        public ProfesorDTO()
        {
        }

        public ProfesorDTO(Profesor profesor)
        {
            this.Id = profesor.Id;
            this.Ime = profesor.Ime;
            this.Prezime = profesor.Prezime;
            this.Datum_Rodjenja = profesor.Datum_Rodjenja;
            this.Adresa_Stanovanja = profesor.Adresa_Stanovanja;
            this.Kontakt = profesor.Kontakt_Telefon;
            this.Email = profesor.Email;
            this.Licna = profesor.Broj_Licne_Karte;
            this.Zvanje = profesor.Zvanje;
            this.Staz = profesor.Godine_Staza;
            this.Predmeti = profesor.Spisak_Predmeta_Profesora;
            this.KatedraId = profesor.KatedraId;

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
