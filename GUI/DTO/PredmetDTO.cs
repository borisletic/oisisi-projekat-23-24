using CLI.Model;
using CLI.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.DTO
{
    public class PredmetDTO : INotifyPropertyChanged, IDataErrorInfo
    {
        public int Id { get; set; }

        private string sifra_Predmeta;
        public string Sifra_Predmeta
        {
            get
            {
                return sifra_Predmeta;
            }
            set
            {
                if (value != sifra_Predmeta)
                {
                    sifra_Predmeta = value;
                    OnPropertyChanged("Sifra_Predmeta");
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

        private int espb;

        public int Espb
        {
            get
            {
                return espb;
            }
            set
            {
                if (value != espb)
                {
                    espb = value;
                    OnPropertyChanged("Espb");
                }
            }
        }

        private int godina_Predmeta;

        public int Godina_Predmeta
        {
            get
            {
                return godina_Predmeta;
            }
            set
            {
                if (value != godina_Predmeta)
                {
                    godina_Predmeta = value;
                    OnPropertyChanged("Godina_Predmeta");
                }
            }
        }

        private Profesor profesor = new Profesor();
        public Profesor Profesor
        {
            get
            {
                return profesor;
            }

            set
            {
                if (value != profesor)
                {
                    profesor = value;
                    OnPropertyChanged("Profesor");
                }
            }
        }


        private Predmet.Semestar semestar;
        public Predmet.Semestar Semestar
        {
            get
            {
                return semestar;
            }
            set
            {
                if (!value.Equals(semestar))
                {
                    semestar = value;
                    OnPropertyChanged("Semestar");
                }
            }
        }

        public string Error => throw new NotImplementedException();

        public string this[string columnName] => throw new NotImplementedException();

        public Predmet ToPredmet()
        {
            Predmet predmet = new Predmet();

            predmet.Id = this.Id;
            predmet.Sifra_Predmeta = this.sifra_Predmeta;
            predmet.Naziv_Predmeta = this.naziv_Predmeta;
            predmet.Predmetni_Profesor = this.profesor;
            predmet.ESPB = this.espb;
            predmet.SemestarPredmeta = this.semestar;
            predmet.Godina_Predmeta = this.godina_Predmeta;
            return predmet;
        }

        public PredmetDTO()
        {
        }

        public PredmetDTO(Predmet predmet)
        {
            this.Id = predmet.Id;
            this.Naziv_Predmeta = predmet.Naziv_Predmeta;
            this.Sifra_Predmeta = predmet.Sifra_Predmeta;
            this.Profesor = predmet.Predmetni_Profesor;
            this.Espb = predmet.ESPB;
            this.Semestar = predmet.SemestarPredmeta;
            this.Godina_Predmeta = predmet.Godina_Predmeta;
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
