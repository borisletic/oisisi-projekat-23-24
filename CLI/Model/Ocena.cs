using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CLI.DAO;
using CLI.Serialization;

namespace CLI.Model
{

    public class Ocena : ISerializable
    {
        public int Id { get; set; }
        public Student Student_Koji_Je_Polozio { get; set; }
        public Predmet Predmet { get; set; }

        public int Ocena_Broj;
        public DateTime Datum_Polaganja { get; set; }

        public Ocena()
        {
            Ocena_Broj = 0;
            Datum_Polaganja = DateTime.Now;
            Student_Koji_Je_Polozio = new Student();
            Predmet = new Predmet();
        }

        public override string ToString()
        {
            return Id.ToString() + " " + Student_Koji_Je_Polozio.Id + " " + Predmet.Id + " " + Ocena_Broj + " " + Datum_Polaganja;
        }
        public Ocena(Student studkpol, Predmet pred, int ocvalue, DateTime datpolisp, int id)
        {

            Student_Koji_Je_Polozio = studkpol;
            Predmet = pred;
            Ocena_Broj = ocvalue;
            Datum_Polaganja = datpolisp;
            Id = id;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
            Id.ToString(),
            Student_Koji_Je_Polozio.Id.ToString(),
            Predmet.Id.ToString(),
            Ocena_Broj.ToString(),
            Datum_Polaganja.ToString("dd.MM.yyyy")
        };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            if (values.Length >= 5)
            {
                Id = int.Parse(values[0]);
                Student_Koji_Je_Polozio.Id = int.Parse(values[1]);
                Predmet.Id = int.Parse(values[2]);
                Ocena_Broj = int.Parse(values[3]);
                Datum_Polaganja = DateTime.ParseExact(values[4], "dd.MM.yyyy", null);
            }
        }
    }
}