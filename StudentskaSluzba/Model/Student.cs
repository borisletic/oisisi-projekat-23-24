using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using StudentskaSluzba.Storage.Serialization;


namespace StudentskaSluzba.Model;



public class Student : ISerializable
{
   public int Id { get; set; }
   public string Ime { get; set; }
   public string Prezime { get; set; }
   public string Datum_Rodjenja { get; set;}
   public string Adresa_Stanovanja { get; set; }
   public int Kontakt_Telefon {  get; set; }
   public string Email { get; set; }
   public string Broj_Indeksa {  get; set; }
   public int Trenutna_Godina_Studija {  get; set; }
   public enum Status { B, S }
    public Status StatusStudenta { get; set; }
    public int Prosecna_Ocena {  get; set; }
   public List<string> Polozeni_Ispiti { get; set; }
   public List<string> Nepolozeni_Ispiti { get; set; }

    public Student() 
    {
        
        Polozeni_Ispiti = new List<string>();
        Nepolozeni_Ispiti = new List<string>();
    
    }
    Status status;
    public Student(string ime, string prezime, string datum_rodjenja, string adresa_stanovanja, int kontakt_telefon, string email, string broj_indeksa, int trenutna_godina_studija, int prosecna_ocena, List<string> polozeni_ispiti, List<string> nepolozeni_ispiti, Status status)
    {
        Ime = ime;
        Prezime = prezime;
        Datum_Rodjenja = datum_rodjenja;
        Adresa_Stanovanja = adresa_stanovanja;
        Kontakt_Telefon = kontakt_telefon;
        Email = email;
        Broj_Indeksa = broj_indeksa;
        Trenutna_Godina_Studija = trenutna_godina_studija;
        Prosecna_Ocena = prosecna_ocena;
        Polozeni_Ispiti = polozeni_ispiti;
        Nepolozeni_Ispiti = nepolozeni_ispiti;
        this.status = status;

    }

    public Student(string ime, string prezime, string datum_rodjenja, string adresa_stanovanja, int kontakt_telefon, string email, string broj_indeksa, int trenutna_godina_studija, int prosecna_ocena, List<string> polozeni_ispiti, List<string> nepolozeni_ispiti, Status status, int id)
    {
        Ime = ime;
        Prezime = prezime;
        Datum_Rodjenja = datum_rodjenja;
        Adresa_Stanovanja = adresa_stanovanja;
        Kontakt_Telefon = kontakt_telefon;
        Email = email;
        Broj_Indeksa = broj_indeksa;
        Trenutna_Godina_Studija = trenutna_godina_studija;
        Prosecna_Ocena = prosecna_ocena;
        Polozeni_Ispiti = polozeni_ispiti;
        Nepolozeni_Ispiti = nepolozeni_ispiti;
        this.status = status;
        Id = id;
    }

    public override string ToString()
    {
        return $"Id: {Id, 5} | Ime: {Ime,9} | Prezime: {Prezime,9} | Datum Rodjenja: {Datum_Rodjenja,21} | Adresa Stanovanja: {Adresa_Stanovanja,21} | Kontakt telefon: {Kontakt_Telefon,21} | E-mail: {Email,21} | Broj indeksa: {Broj_Indeksa,21} | Trenutna godina studija: {Trenutna_Godina_Studija,21} | Prosecna ocena: {Prosecna_Ocena , 21} | Polozeni ispiti: {Polozeni_Ispiti,51} | Nepolozeni ispiti: {Nepolozeni_Ispiti,51} | Status: {status, 21} |";
    }

    public string[] ToCSV()
    {
        return ToCSV(GetPolozeni_Ispiti(), GetNepolozeni_Ispiti());
    }

    public List<string> GetNepolozeni_Ispiti()
    {
        return Nepolozeni_Ispiti;
    }

    public List<string> GetPolozeni_Ispiti()
    {
        return Polozeni_Ispiti;
    }

    public string[] ToCSV(List<string> polozeni_Ispiti, List<string> nepolozeni_Ispiti)
    {
        string polozeniString = string.Join(",", polozeni_Ispiti);
        string nepolozeniString = string.Join(",", nepolozeni_Ispiti);
        string[] csvValues =
        {
            Id.ToString(),
            Ime,
            Prezime,
            Datum_Rodjenja,
            Adresa_Stanovanja,
            Kontakt_Telefon.ToString(),
            Email,
            Broj_Indeksa,
            Trenutna_Godina_Studija.ToString(),
            Prosecna_Ocena.ToString(),
            polozeniString,
            nepolozeniString,
            StatusStudenta.ToString()
            
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        if (values.Length >= 12)
        {
            Id = int.Parse(values[0]);
            Ime = values[1];
            Prezime = values[2];
            Datum_Rodjenja = values[3];
            Adresa_Stanovanja = values[4];
            Kontakt_Telefon = int.Parse(values[5]);
            Email = values[6];
            Broj_Indeksa = values[7];
            Trenutna_Godina_Studija = int.Parse(values[8]);
            Prosecna_Ocena = int.Parse(values[9]);

            Polozeni_Ispiti = new List<string>();
            string subjects = values[10];
            string[] subjectsArray = subjects.Split(',');
            foreach (string subject in subjectsArray)
            {
                Polozeni_Ispiti.Add(subject.Trim());
            }

            Nepolozeni_Ispiti = new List<string>();
            string fsubjects = values[11];
            string[] fsubjectsArray = fsubjects.Split(',');
            foreach (string fsubject in fsubjectsArray)
            {
                Nepolozeni_Ispiti.Add(fsubject.Trim());
            }
        }
    }
}
