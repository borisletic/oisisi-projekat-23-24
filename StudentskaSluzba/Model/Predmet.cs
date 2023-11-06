using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.Storage.Serialization;
using static StudentskaSluzba.Model.Student;

namespace StudentskaSluzba.Model;

public class Predmet : ISerializable
{
    public int Id { get; set; }
    public int Sifra_Predmeta {  get; set; }
    public string Naziv_Predmeta { get; set; }
    public enum Semestar { L, Z }
    public Semestar SemestarPredmeta { get; set; }
    public int Godina_Predmeta { get; set; }
    public string Predmetni_Profesor {  get; set; }
    public int ESPB {  get; set; }
    public List<string> Studenti_Polozeni { get; set; }
    public List<string> Studenti_Nepolozeni { get; set; }

    public Predmet() 
    {
        Studenti_Polozeni = new List<string>();
        Studenti_Nepolozeni = new List<string>();

    }
    Semestar semestar;
    public Predmet(int sifra, string naziv, Semestar semestar, int godina, string predprof, int espb, List<string> stpol, List<string> stfail)
    {
        Sifra_Predmeta = sifra;
        Naziv_Predmeta = naziv;
        this.semestar = semestar;
        Godina_Predmeta = godina;
        Predmetni_Profesor = predprof;
        ESPB = espb;
        Studenti_Polozeni = stpol;
        Studenti_Nepolozeni = stfail;
        
    }

    public Predmet(int sifra, string naziv, Semestar semestar, int godina, string predprof, int espb, List<string> stpol, List<string> stfail, int id)
    {
        Sifra_Predmeta = sifra;
        Naziv_Predmeta = naziv;
        this.semestar = semestar;
        Godina_Predmeta = godina;
        Predmetni_Profesor = predprof;
        ESPB = espb;
        Studenti_Polozeni = stpol;
        Studenti_Nepolozeni = stfail;
        Id = id;
    }

    public override string ToString()
    {
        return $"Id: {Id, 5} | Sifra predmeta: {Sifra_Predmeta,20} | Naziv predmeta: {Naziv_Predmeta,20} | Semestar: {semestar, 20} | Godina predmeta: {Godina_Predmeta,20} | Predmetni profesor: {Predmetni_Profesor,20} | ESPB: {ESPB,20} | Studenti polozeni: {Studenti_Polozeni,20} | Studenti nepolozeni: {Studenti_Nepolozeni,20} |";
    }

    public string[] ToCSV()
    {
        return ToCSV(GetStudenti_Polozeni(), GetStudenti_Nepolozeni());
    }

    public List<string> GetStudenti_Nepolozeni()
    {
        return Studenti_Nepolozeni;
    }

    public List<string> GetStudenti_Polozeni()
    {
        return Studenti_Polozeni;
    }

    public string[] ToCSV(List<string> studenti_polozeni, List<string> studenti_nepolozeni)
    {
        string studenti_polozeni_string = string.Join(",", studenti_polozeni);
        string studenti_nepolozeni_string = string.Join(",", studenti_nepolozeni);

        string[] csvValues =
        {
            Id.ToString(),
            Sifra_Predmeta.ToString(),
            Naziv_Predmeta,
            SemestarPredmeta.ToString(),
            Godina_Predmeta.ToString(),
            Predmetni_Profesor,
            ESPB.ToString(),
            studenti_polozeni_string,
            studenti_nepolozeni_string

        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        if (values.Length >= 8)
        {
            Id = int.Parse(values[0]);
            Sifra_Predmeta = int.Parse(values[1]);
            Naziv_Predmeta = values[2];

            Godina_Predmeta = int.Parse(values[3]);
            Predmetni_Profesor = values[4];
            ESPB = int.Parse(values[5]);



            Studenti_Polozeni = new List<string>();


            string stupol = values[6];
            string[] stpolArray = stupol.Split(',');


            foreach (string studpol in stpolArray)
            {
                Studenti_Polozeni.Add(studpol.Trim());
            }

            Studenti_Nepolozeni = new List<string>();


            string stunepol = values[7];
            string[] stnepolArray = stunepol.Split(',');


            foreach (string studnepol in stnepolArray)
            {
                Studenti_Nepolozeni.Add(studnepol.Trim());
            }
        }
    }


}
