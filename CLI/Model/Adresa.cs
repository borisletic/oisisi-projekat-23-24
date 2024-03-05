using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Observer;
using CLI.Serialization;

namespace CLI.Model
{

    public class Adresa : ISerializable
    {
        public int Id { get; set; }
        public string Ulica { get; set; }
        public string Broj { get; set; }
        public string Grad { get; set; }
        public string Drzava { get; set; }

        public Adresa()
        {
            Ulica = string.Empty;
            Broj = string.Empty;
            Grad = string.Empty;
            Drzava = string.Empty;
        }

        public Adresa(string ulica, string broj, string grad, string drzava)
        {
            Ulica = ulica;
            Broj = broj;
            Grad = grad;
            Drzava = drzava;

        }

        public Adresa(string ulica, string broj, string grad, string drzava, int id)
        {
            Ulica = ulica;
            Broj = broj;
            Grad = grad;
            Drzava = drzava;
            Id = id;

        }

        public override string ToString()
        {
            return Ulica + ',' + Broj + ',' + Grad + ',' + Drzava;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
            Id.ToString(),
            Ulica,
            Broj,
            Grad,
            Drzava
        };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            if (values.Length >= 5)
            {
                Id = int.Parse(values[0]);
                Ulica = values[1];
                Broj = values[2];
                Grad = values[3];
                Drzava = values[4];
            }
        }


    }
}
