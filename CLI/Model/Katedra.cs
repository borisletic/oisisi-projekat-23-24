using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.Design;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CLI.Serialization;

namespace CLI.Model
{

    public class Katedra : ISerializable
    {
        public int Id { get; set; }
        public string Sifra_Katedre { get; set; }
        public string Naziv_Katedre { get; set; }
        public int Sef_Katedre { get; set; }
        public List<Profesor> Spisak_Profesora { get; set; }

        public Katedra()
        {
            Sifra_Katedre = string.Empty;
            Naziv_Katedre = string.Empty;
            Sef_Katedre = 0;
            Spisak_Profesora = new List<Profesor>();
        }

        public override string ToString()
        {
            return $"Id: {Id,5} | Sifra katedre: {Sifra_Katedre,20} | Naziv katedre: {Naziv_Katedre,20} | Sef katedre: {Sef_Katedre,20} | Spisak profesora na katedri: {Spisak_Profesora,20} |";
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
            Id.ToString(),
            Sifra_Katedre.ToString(),
            Naziv_Katedre,
            Sef_Katedre.ToString()
        };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            if (values.Length >= 4)
            {
                Id = int.Parse(values[0]);
                Sifra_Katedre = values[1];
                Naziv_Katedre = values[2];
                Sef_Katedre = int.Parse(values[3]);

            }

        }



    }
}
