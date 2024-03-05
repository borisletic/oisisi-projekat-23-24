using CLI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.DTO
{
    public class OcenaDTO : INotifyPropertyChanged, IDataErrorInfo
    {
        public int Id { get; set; }

        private Student student = new Student();
        public Student Student
        {
            get 
            {       
                return student;
            }
            set
            {
                if (value != student) 
                {
                    student = value;
                    OnPropertyChanged("Student");
                }
            }
        }

        private Predmet predmet = new Predmet();

        public Predmet Predmet
        {
            get
            {
                return predmet;
            }
            set
            {
                if (value != predmet)
                {
                    predmet = value;
                    OnPropertyChanged("Predmet");
                }
            }
        }
        public int PredmetId
        {
            get
            {
                return predmet.Id;
            }
            set
            {
                if (value != predmet.Id)
                {
                    predmet.Id = value;
                    OnPropertyChanged("PredmetId");
                }
            }
        }
        public string Naziv_Predmeta
        {
            get
            {
                return predmet.Naziv_Predmeta;
            }
            set
            {
                if (value != predmet.Naziv_Predmeta)
                {
                    predmet.Naziv_Predmeta = value;
                    OnPropertyChanged("Naziv_Predmeta");
                }
            }
        }

        public int ESPB
        {
            get
            {
                return predmet.ESPB;
            }
            set
            {
                if (value != predmet.ESPB)
                {
                    predmet.ESPB = value;
                    OnPropertyChanged("ESPB");
                }
            }
        }

        public string Sifra_Predmeta
        {
            get
            {
                return predmet.Sifra_Predmeta;
            }
            set
            {
                if (value != predmet.Sifra_Predmeta)
                {
                    predmet.Sifra_Predmeta = value;
                    OnPropertyChanged("Sifra_Predmeta");
                }
            }
        }

        private int ocenaBroj;
        public int Ocena_Broj
        {
            get
            {
                return ocenaBroj;
            }
            set
            {
                if (value != ocenaBroj)
                {
                    ocenaBroj = value;
                    OnPropertyChanged("Ocena_Broj");
                }
            }
        }

        private DateTime datum_Polaganja;
        public DateTime Datum_Polaganja
        {
            get
            {
                return datum_Polaganja;
            }
            set
            {
                if (value != datum_Polaganja)
                {
                    datum_Polaganja = value;
                    OnPropertyChanged("Datum_Polaganja");
                }
            }
        }


        public string Error => throw new NotImplementedException();
        public string this[string columnName] => throw new NotImplementedException();

        public OcenaDTO(Ocena ocena)
        {
            this.Id = ocena.Id;
            this.Student = ocena.Student_Koji_Je_Polozio;
            this.Predmet = ocena.Predmet;
            this.Ocena_Broj = ocena.Ocena_Broj;
            this.Datum_Polaganja = ocena.Datum_Polaganja;
        }

        public OcenaDTO()
        {
        }

        public Ocena ToOcena()
        {
            Ocena ocena = new Ocena();
            ocena.Id = this.Id;
            ocena.Student_Koji_Je_Polozio = this.student;
            ocena.Predmet = this.predmet;
            ocena.Ocena_Broj = this.ocenaBroj;
            ocena.Datum_Polaganja = this.datum_Polaganja;
            return ocena;
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
