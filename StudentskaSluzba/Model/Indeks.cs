using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.Storage.Serialization;

namespace StudentskaSluzba.Model;

public class Indeks : ISerializable
{
    public int Id { get; set; }
    public string Oznaka_Smera { get; set; }
    public int Broj_Upisa { get; set; }
    public int Godina_Upisa { get; set; }
    public Indeks() { }

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
        return $"Id: {Id,5} | Oznaka smera: {Oznaka_Smera,20} | Broj upisa: {Broj_Upisa,20} | Godina upisa: {Godina_Upisa,20} |";
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
        if (values.Length >= 4)
        {
            Id = int.Parse(values[0]);
            Oznaka_Smera = values[1];
            Broj_Upisa = int.Parse(values[2]);
            Godina_Upisa = int.Parse(values[3]);

        }
    }






}
