using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Serialization;

namespace CLI.Model
{

    public class Profesor : ISerializable
    {
        public int Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime Datum_Rodjenja { get; set; }
        public Adresa Adresa_Stanovanja { get; set; }
        public string Kontakt_Telefon { get; set; }
        public string Email { get; set; }
        public string Broj_Licne_Karte { get; set; }
        public string Zvanje { get; set; }
        public int Godine_Staza { get; set; }
        public int KatedraId { get; set; }
        public List<Predmet> Spisak_Predmeta_Profesora { get; set; }

        public Profesor()
        {
            Ime = string.Empty;
            Prezime = string.Empty;
            Datum_Rodjenja = DateTime.Now;
            Adresa_Stanovanja = new Adresa();
            Kontakt_Telefon = string.Empty;
            Email = string.Empty;
            Broj_Licne_Karte = string.Empty;
            Zvanje = string.Empty;
            Godine_Staza = 0;
            KatedraId = 0;
            Spisak_Predmeta_Profesora = new List<Predmet>();
        }

        public override string ToString()
        {
            return Ime + ' ' + Prezime;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
            Id.ToString(),
            Ime,
            Prezime,
            Datum_Rodjenja.ToString("dd.MM.yyyy"),
            Adresa_Stanovanja.Id.ToString(),
            Kontakt_Telefon,
            Email,
            Broj_Licne_Karte,
            Zvanje,
            Godine_Staza.ToString(),
            KatedraId.ToString()
        };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
                Id = int.Parse(values[0]);
                Ime = values[1];
                Prezime = values[2];
                Datum_Rodjenja = DateTime.ParseExact(values[3], "dd.MM.yyyy", null);
                Adresa_Stanovanja.Id = int.Parse(values[4]);
                Kontakt_Telefon = values[5];
                Email = values[6];
                Broj_Licne_Karte = values[7];
                Zvanje = values[8];
                Godine_Staza = int.Parse(values[9]);
                KatedraId = int.Parse(values[10]);
        }
    }
}