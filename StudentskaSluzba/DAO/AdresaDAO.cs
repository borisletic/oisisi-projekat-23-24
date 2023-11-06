using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.Model;
using StudentskaSluzba.Storage;
using static StudentskaSluzba.Model.Adresa;

namespace StudentskaSluzba.DAO;

 class AdresaDAO
{
    private readonly List<Adresa> _adresa;
    private readonly Storage<Adresa> _storage;

    public AdresaDAO()
    {
        _storage = new Storage<Adresa>("adrese.txt");
        _adresa = _storage.Load();
    }

    private int GenerateId()
    {
        if (_adresa.Count == 0) return 0;
        return _adresa[^1].Id + 1;

    }

    public Adresa AddAdresa(Adresa adresa)
    {
        adresa.Id = GenerateId();
        _adresa.Add(adresa);
        _storage.Save(_adresa);
        return adresa;
    }

    public Adresa? UpdateAdresa(Adresa adresa)
    {
        Adresa? oldAdresa = GetAdresaById(adresa.Id);
        if (oldAdresa is null) return null;

        oldAdresa.Ulica = adresa.Ulica;
        oldAdresa.Broj = adresa.Broj;
        oldAdresa.Grad = adresa.Grad;
        oldAdresa.Drzava = adresa.Drzava;




        _storage.Save(_adresa);
        return oldAdresa;
    }

    public Adresa? RemoveAdresa(int id)
    {
        Adresa? adresa = GetAdresaById(id);
        if (adresa == null) return null;

        _adresa.Remove(adresa);
        _storage.Save(_adresa);
        return adresa;
    }

    private Adresa? GetAdresaById(int id)
    {
        return _adresa.Find(s => s.Id == id);
    }

    public List<Adresa> GetAllAdresa()
    {
        return _adresa;
    }

    public List<Adresa> GetAllAdresa(int page, int pageSize, string sortCriteria, SortDirection sortDirection)
    {
        IEnumerable<Adresa> adresa = _adresa;


        switch (sortCriteria)
        {
            case "Id":
                adresa = _adresa.OrderBy(x => x.Id);
                break;
            case "Ulica":
                adresa = _adresa.OrderBy(x => x.Ulica);
                break;
            case "Broj":
                adresa = _adresa.OrderBy(x => x.Broj);
                break;
            case "Grad":
                adresa = _adresa.OrderBy(x => x.Grad);
                break;
            case "Drzava":
                adresa = _adresa.OrderBy(x => x.Drzava);
                break;



        }

        // promeni redosled ukoliko ima potrebe za tim
        if (sortDirection == SortDirection.Descending)
            adresa = adresa.Reverse();

        // paginacija
        adresa = adresa.Skip((page - 1) * pageSize).Take(pageSize);

        return adresa.ToList();
    }



}
