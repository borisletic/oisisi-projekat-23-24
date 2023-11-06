using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.Storage.Serialization;

namespace StudentskaSluzba.Model;

public class Adresa : ISerializable
{
    public int Id { get; set; }
    public string Ulica {  get; set; }
    public int Broj {  get; set; }
    public string Grad {  get; set; }
    public string Drzava {  get; set; }

    public Adresa() { }

    public Adresa(string ulica, int broj, string grad, string drzava)
    {
        Ulica = ulica;
        Broj = broj;
        Grad = grad;
        Drzava = drzava;

    }

    public Adresa(string ulica, int broj, string grad, string drzava, int id)
    {
        Ulica = ulica;
        Broj = broj;
        Grad = grad;
        Drzava = drzava;
        Id = id;

    }

    public override string ToString()
    {
        return $"Id: {Id,5} | Ulica: {Ulica,20} | Broj: {Broj,20} | Grad: {Grad,20} | Drzava: {Drzava, 20} |";
    }

    public string[] ToCSV()
    {
        string[] csvValues =
        {
            Id.ToString(),
            Ulica,
            Broj.ToString(),
            Grad,
            Drzava

        };
        return csvValues;
    }

    public void FromCSV(string[] values)
    {
        if (values.Length >= 5)
        {
            Id = int.Parse(values[0]);
            Ulica = values[1];
            Broj = int.Parse(values[2]);
            Grad = values[3];
            Drzava = values[4];
        }
    }



}
