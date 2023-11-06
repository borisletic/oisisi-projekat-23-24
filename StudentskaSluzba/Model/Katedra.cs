using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.Storage.Serialization;

namespace StudentskaSluzba.Model;

public class Katedra : ISerializable
{
    public int Id { get; set; }
    public int Sifra_Katedre { get; set; }
    public string Naziv_Katedre { get; set; }
    public string Sef_Katedre { get; set; }
    public List<string> Spisak_Profesora_Na_Katedri {  get; set; }

    public Katedra() 
    {
        Spisak_Profesora_Na_Katedri = new List<string>();
    }

    public Katedra(int sifra, string naziv, string sef, List<string> spisak) 
    {
        Sifra_Katedre = sifra;
        Naziv_Katedre = naziv;
        Sef_Katedre = sef;
        Spisak_Profesora_Na_Katedri = spisak;
    
    }

    public Katedra(int sifra, string naziv, string sef, List<string> spisak, int id)
    {
        Sifra_Katedre = sifra;
        Naziv_Katedre = naziv;
        Sef_Katedre = sef;
        Spisak_Profesora_Na_Katedri = spisak;
        Id = id;

    }

    public override string ToString()
    {
        return $"Id: {Id,5} | Sifra katedre: {Sifra_Katedre,20} | Naziv katedre: {Naziv_Katedre,20} | Sef katedre: {Sef_Katedre,20} | Spisak profesora na katedri: {Spisak_Profesora_Na_Katedri, 20} |";
    }

    public string[] ToCSV()
    {
        return ToCSV(GetSpisak_Profesora_Na_Katedri());
    }

    public List<string> GetSpisak_Profesora_Na_Katedri()
    {
        return Spisak_Profesora_Na_Katedri;
    }

    public string[] ToCSV(List<string> spisak_Profesora_Na_Katedri)
    {
        string spisak_Profesora_Na_Katedri_string = string.Join(",", spisak_Profesora_Na_Katedri);
        string[] csvValues =
        {
            Id.ToString(),
            Sifra_Katedre.ToString(),
            Naziv_Katedre,
            Sef_Katedre,
            spisak_Profesora_Na_Katedri_string

        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        if (values.Length >= 5)
        {
            Id = int.Parse(values[0]);
            Sifra_Katedre = int.Parse(values[1]);
            Naziv_Katedre = values[2];
            Sef_Katedre = values[3];

            Spisak_Profesora_Na_Katedri = new List<string>();


            string spprof = values[4];
            string[] spprofArray = spprof.Split(',');


            foreach (string sp in spprofArray)
            {
                Spisak_Profesora_Na_Katedri.Add(sp.Trim());
            }

        }

    }




}
