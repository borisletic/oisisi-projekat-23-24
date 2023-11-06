using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.Storage.Serialization;

namespace StudentskaSluzba.Model;

public class Profesor : ISerializable
{
    public int Id { get; set; }
    public string Ime { get; set; }
    public string Prezime { get; set; }
    public string Datum_Rodjenja { get; set; }
    public string Adresa_Stanovanja { get; set; }
    public int Kontakt_Telefon { get; set; }
    public string Email { get; set; }
    public int Broj_Licne_Karte {  get; set; }
    public string Zvanje {  get; set; }
    public int Godine_Staza {  get; set; }
    public List<string> Spisak_Predmeta_Na_Kojima_Je_Profesor {  get; set; }

    public Profesor() 
    {
        Spisak_Predmeta_Na_Kojima_Je_Profesor = new List<string>();
    }

    public Profesor(string ime, string prezime, string datum_rodjenja, string adresa_stanovanja, int kontakt_telefon, string email, int broj_licne_karte, string zvanje, int godine_staza, List<string> spisak_predmeta_na_kojima_je_profesor)
    {
        Ime = ime;
        Prezime = prezime;
        Datum_Rodjenja = datum_rodjenja;
        Adresa_Stanovanja = adresa_stanovanja;
        Kontakt_Telefon = kontakt_telefon;
        Email = email;
        Broj_Licne_Karte = broj_licne_karte;
        Zvanje = zvanje;
        Godine_Staza = godine_staza;
        Spisak_Predmeta_Na_Kojima_Je_Profesor = spisak_predmeta_na_kojima_je_profesor;

    }

    public Profesor(string ime, string prezime, string datum_rodjenja, string adresa_stanovanja, int kontakt_telefon, string email, int broj_licne_karte, string zvanje, int godine_staza, List<string> spisak_predmeta_na_kojima_je_profesor, int id)
    {
        Ime = ime;
        Prezime = prezime;
        Datum_Rodjenja = datum_rodjenja;
        Adresa_Stanovanja = adresa_stanovanja;
        Kontakt_Telefon = kontakt_telefon;
        Email = email;
        Broj_Licne_Karte = broj_licne_karte;
        Zvanje = zvanje;
        Godine_Staza = godine_staza;
        Spisak_Predmeta_Na_Kojima_Je_Profesor = spisak_predmeta_na_kojima_je_profesor;
        Id = id;

    }

    public override string ToString()
    {
        return $"Id: {Id, 5} | Ime: {Ime,20} | Prezime: {Prezime,20} | Datum Rodjenja: {Datum_Rodjenja,20} | Adresa Stanovanja: {Adresa_Stanovanja,20} | Kontakt telefon: {Kontakt_Telefon,20} | E-mail: {Email,20} | Broj licne karte: {Broj_Licne_Karte,20} | Zvanje: {Zvanje,20} | Godine staza: {Godine_Staza,20} | Spisak predmeta na kojima je profesor: {Spisak_Predmeta_Na_Kojima_Je_Profesor, 50} |";
    }

    public string[] ToCSV()
    {
        return ToCSV(GetSpisak_Predmeta_Na_Kojima_Je_Profesor());
    }

    public List<string> GetSpisak_Predmeta_Na_Kojima_Je_Profesor()
    {
        return Spisak_Predmeta_Na_Kojima_Je_Profesor;
    }

    public string[] ToCSV(List<string> spisak_predmeta_na_kojima_je_profesor)
    {
        string spisak_predmeta_na_kojima_je_profesor_string = string.Join(",", spisak_predmeta_na_kojima_je_profesor);
        string[] csvValues =
        {
            Id.ToString(),
            Ime,
            Prezime,
            Datum_Rodjenja,
            Adresa_Stanovanja,
            Kontakt_Telefon.ToString(),
            Email,
            Broj_Licne_Karte.ToString(),
            Zvanje, 
            Godine_Staza.ToString(),
            spisak_predmeta_na_kojima_je_profesor_string

        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        if (values.Length >= 11)
        {
            Id = int.Parse(values[0]);
            Ime = values[1];
            Prezime = values[2];
            Datum_Rodjenja = values[3];
            Adresa_Stanovanja = values[4];
            Kontakt_Telefon = int.Parse(values[5]);
            Email = values[6];
            Broj_Licne_Karte = int.Parse(values[7]);
            Zvanje = values[8];
            Godine_Staza = int.Parse(values[9]);


            Spisak_Predmeta_Na_Kojima_Je_Profesor = new List<string>();


            string subjects = values[10];
            string[] subjectsArray = subjects.Split(',');


            foreach (string subject in subjectsArray)
            {
                Spisak_Predmeta_Na_Kojima_Je_Profesor.Add(subject.Trim());
            }

        }
    }
}
