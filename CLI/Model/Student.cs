using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using CLI.DAO;
using CLI.Serialization;
using static CLI.Model.Student;


namespace CLI.Model
{
    public class Student : ISerializable
    {
        public int Id;
        public string Ime;
        public string Prezime;
        public DateTime Datum_Rodjenja;
        public Adresa Adresa_Stanovanja;
        public int Adresa_Id;
        public string Kontakt_Telefon;
        public string Email;
        public Indeks Indeks;
        public int Trenutna_Godina_Studija;
        public string Naziv_Predmeta; //iskljucivo samo da prikaze naziv predmeta kada trazi studente koji pohadjaju predmeta od odredjenog profesora
    
        public enum Status { B, S }
        public Status StatusStudenta;
        public double Prosecna_Ocena;
        public List<Ocena> Polozeni_Ispiti;
        public List<Predmet> Nepolozeni_Ispiti;
    
        public Student()
        {
            Ime = string.Empty;
            Prezime = string.Empty;
            Datum_Rodjenja = DateTime.Now;
            Adresa_Stanovanja = new Adresa();
            Kontakt_Telefon = string.Empty;
            Email = string.Empty;
            Indeks = new Indeks();
            Trenutna_Godina_Studija = 0;
            Prosecna_Ocena = 0;
            StatusStudenta = Status.S;
            Polozeni_Ispiti = new List<Ocena>();
            Nepolozeni_Ispiti = new List<Predmet>();
            Naziv_Predmeta = string.Empty;

        }
        public override string ToString()
        {
            return Id + Prezime + Ime + Datum_Rodjenja;
        }

        public Student(int id,string ime, string prezime, DateTime datum_rodjenja, Adresa adresa_stanovanja, string kontakt_telefon, string email, Indeks broj_indeksa, int trenutna_godina_studija, double prosecna_ocena, Status status,List<Ocena> polozeni_ispiti,List<Predmet> nepolozeni_ispiti)
        {
            Id = id;
            Ime = ime;
            Prezime = prezime;
            Datum_Rodjenja = datum_rodjenja;
            Adresa_Stanovanja = adresa_stanovanja;
            Kontakt_Telefon = kontakt_telefon;
            Email = email;
            Indeks = broj_indeksa;
            Trenutna_Godina_Studija = trenutna_godina_studija;
            Prosecna_Ocena = prosecna_ocena;
            Polozeni_Ispiti = polozeni_ispiti;
            Nepolozeni_Ispiti = nepolozeni_ispiti;
            this.StatusStudenta = status;
        }

        public string[] ToCSV()
        {
            List<string> idnepolozen = new List<string>();
            string IdNepolozen = string.Empty;
            //System.Diagnostics.Debug.WriteLine(Nepolozeni_Ispiti.Count);
            if (Nepolozeni_Ispiti.Count != 0)
            {
                foreach (Predmet predmet in Nepolozeni_Ispiti)
                {
                    idnepolozen.Add(predmet.Id.ToString());
                }
                IdNepolozen = string.Join(',', idnepolozen); // id-evi nepolozenih predmeta od studenta
            }
            string[] csvValues =
            {
            Id.ToString(),
            Ime,
            Prezime,
            Datum_Rodjenja.ToString("dd.MM.yyyy") ?? string.Empty,
            Adresa_Stanovanja.Id.ToString(),
            Kontakt_Telefon,
            Email,
            Indeks.ToString(),
            Trenutna_Godina_Studija.ToString(),
            StatusStudenta.ToString(),
            IdNepolozen
            };
            return csvValues;
        }
        
        public void FromCSV(string[] values)
        {
            Indeks indeks = new Indeks();
            PredmetDAO predmetDAO = new PredmetDAO();
            Id = int.Parse(values[0]);
            Ime = values[1];
            Prezime = values[2];
            Datum_Rodjenja = DateTime.ParseExact(values[3], "dd.MM.yyyy", null);
            Adresa_Stanovanja.Id = int.Parse(values[4]);
            Kontakt_Telefon = values[5];
            Email = values[6];

            string[] razdvojeni = values[7].Split('-');// [0] Ra  [1] broj [2]godina upisa
            indeks.Oznaka_Smera = razdvojeni[0];
            indeks.Broj_Upisa = int.Parse(razdvojeni[1]);
            indeks.Godina_Upisa = int.Parse(razdvojeni[2]);
            Indeks = indeks;

            Trenutna_Godina_Studija = int.Parse(values[8]);
            if (Enum.TryParse<Status>(values[9], out Status NacinFinansiranja))
            {
                StatusStudenta = NacinFinansiranja;
            }
            else
            {
                System.Console.WriteLine("Greška prilikom konverzije statusa iz stringa.");
            }
            string[] nepolozeni = values[10].Split(',');// id-evi nepolozenih predmeta
            if (nepolozeni[0] == string.Empty)
            {
                //Ako ne dodam nista nece se ni ispisati 0
            }
            else 
            {
                foreach (string idn in nepolozeni)
                {
                    Nepolozeni_Ispiti.Add(predmetDAO.GetPredmetById(int.Parse(idn)));
                }
            }
        }
    }
}
