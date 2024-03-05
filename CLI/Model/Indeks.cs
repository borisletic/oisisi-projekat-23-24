using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Serialization;

namespace CLI.Model {

    public class Indeks : ISerializable
    {
        public int Id { get; set; }
        public string Oznaka_Smera { get; set; }
        public int Broj_Upisa { get; set; }
        public int Godina_Upisa { get; set; }
        public Indeks()
        {
            Oznaka_Smera = string.Empty;
            Broj_Upisa = 0;
            Godina_Upisa = 0;
        }

        public Indeks(string oznaka, int broj, int godina)
        {
            Oznaka_Smera = oznaka;
            Broj_Upisa = broj;
            Godina_Upisa = godina;
        }

        public Indeks(string oznaka, int broj, int godina, int id)
        {
            Oznaka_Smera = oznaka;
            Broj_Upisa = broj;
            Godina_Upisa = godina;
            Id = id;
        }

        public override string ToString()
        {
            return Oznaka_Smera + '-' + Broj_Upisa + '-' + Godina_Upisa;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
            Id.ToString(),
            Oznaka_Smera,
            Broj_Upisa.ToString(),
            Godina_Upisa.ToString()
        };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
                Id = int.Parse(values[0]);
                Oznaka_Smera = values[1];
                Broj_Upisa = int.Parse(values[2]);
                Godina_Upisa = int.Parse(values[3]);
        }
    }
}
