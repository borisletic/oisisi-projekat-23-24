using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.Storage.Serialization;

namespace StudentskaSluzba.Model;

public class Ocena : ISerializable
{
    public int Id { get; set; }
    public string Student_Koji_Je_Polozio { get; set; }
    public string Predmet { get; set; }

    public int brojcanaVrednostOcene;

    public int Brojcana_Vrednost_Ocene
    {
        get { return brojcanaVrednostOcene; }
        set
        {
            if (value >= 6 && value <= 10)
            {
                brojcanaVrednostOcene = value;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Ocena mora biti u intervalu od 6 do 10.");
            }
        }
    }
    public string Datum_Polaganja_Ispita { get; set; }

    public Ocena() { }

    public Ocena(string studkpol, string pred, int ocvalue, string datpolisp) 
    {

        Student_Koji_Je_Polozio = studkpol;
        Predmet = pred;
        Brojcana_Vrednost_Ocene = ocvalue;
        Datum_Polaganja_Ispita = datpolisp;
    }

    public Ocena(string studkpol, string pred, int ocvalue, string datpolisp, int id)
    {

        Student_Koji_Je_Polozio = studkpol;
        Predmet = pred;
        Brojcana_Vrednost_Ocene = ocvalue;
        Datum_Polaganja_Ispita = datpolisp;
        Id = id;
    }

    public override string ToString()
    {
        return $"Id: {Id,5} | Student koji je polozio: {Student_Koji_Je_Polozio,20} | Predmet: {Predmet,20} | Brojcana vrednost ocene: {Brojcana_Vrednost_Ocene,20} | Datum polaganja ispita: {Datum_Polaganja_Ispita, 20} |";
    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(),
            Student_Koji_Je_Polozio,
            Predmet,
            Brojcana_Vrednost_Ocene.ToString(),
            Datum_Polaganja_Ispita
            
        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        if (values.Length >= 5)
        {
            Id = int.Parse(values[0]);
            Student_Koji_Je_Polozio = values[1];
            Predmet = values[2];
            Brojcana_Vrednost_Ocene = int.Parse(values[3]);
            Datum_Polaganja_Ispita = values[4];
        }


    }





}
