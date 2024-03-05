using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.DAO;
using CLI.Serialization;
using static CLI.Model.Predmet;
using static CLI.Model.Student;

namespace CLI.Model
{

    public class Predmet : ISerializable
    {
        public int Id { get; set; }
        public string Sifra_Predmeta { get; set; }
        public string Naziv_Predmeta { get; set; }
        public enum Semestar { L, Z }
        public Semestar SemestarPredmeta { get; set; }
        public int Godina_Predmeta { get; set; }
        public Profesor Predmetni_Profesor { get; set; }
        public int ESPB { get; set; }
        public List<Student> Studenti_Polozeni { get; set; }
        public List<Student> Studenti_Nepolozeni { get; set; }

        public Predmet()
        {
            Sifra_Predmeta = string.Empty;
            Naziv_Predmeta = string.Empty;
            SemestarPredmeta = Semestar.Z;
            Godina_Predmeta = 0;
            Predmetni_Profesor = new Profesor();
            ESPB = 0;
            Studenti_Polozeni = new List<Student>();
            Studenti_Nepolozeni = new List<Student>();

        }
        public Predmet(string sifra, string naziv, Profesor predprof, Semestar semestar, int godina, int espb)
        {
            Sifra_Predmeta = sifra;
            Naziv_Predmeta = naziv;
            SemestarPredmeta = semestar;
            Godina_Predmeta = godina;
            Predmetni_Profesor = predprof;
            ESPB = espb;

        }

        public Predmet(string sifra, string naziv, Semestar semestar, int godina, Profesor predprof, int espb, List<Student> stpol, List<Student> stfail, int id)
        {
            Sifra_Predmeta = sifra;
            Naziv_Predmeta = naziv;
            SemestarPredmeta = semestar;
            Godina_Predmeta = godina;
            Predmetni_Profesor = predprof;
            ESPB = espb;
            Studenti_Polozeni = stpol;
            Studenti_Nepolozeni = stfail;
            Id = id;
        }

        public override string ToString()
        {
            return Naziv_Predmeta + " " + ESPB;
        }

        public string Spisak_Studenata(List<Student> Lista_Studenata)
        {
            if (Lista_Studenata != null)
            {
                return string.Join(",", Lista_Studenata.Select(s => s.Id));
            }
            else return string.Empty;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
            Id.ToString(),
            Sifra_Predmeta.ToString(),
            Naziv_Predmeta,
            Predmetni_Profesor.Id.ToString(),
            SemestarPredmeta.ToString(),
            Godina_Predmeta.ToString(),
            ESPB.ToString()
        };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Sifra_Predmeta = values[1];
            Naziv_Predmeta = values[2];
            Predmetni_Profesor.Id = int.Parse(values[3]);
            SemestarPredmeta = (Semestar)Enum.Parse(typeof(Semestar), values[4]);

            Godina_Predmeta = int.Parse(values[5]);
            ESPB = int.Parse(values[6]);
        }

    }
}
