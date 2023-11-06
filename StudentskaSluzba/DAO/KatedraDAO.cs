using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.Model;
using StudentskaSluzba.Storage;
using static StudentskaSluzba.Model.Katedra;

namespace StudentskaSluzba.DAO;

 class KatedraDAO
{
    private readonly List<Katedra> _katedra;
    private readonly Storage<Katedra> _storage;

    public KatedraDAO()
    {
        _storage = new Storage<Katedra>("katedre.txt");
        _katedra = _storage.Load();
    }

    private int GenerateId()
    {
        if (_katedra.Count == 0) return 0;
        return _katedra[^1].Id + 1;

    }

    public Katedra AddKatedra(Katedra katedra)
    {
        katedra.Id = GenerateId();
        _katedra.Add(katedra);
        _storage.Save(_katedra);
        return katedra;
    }

    public Katedra? UpdateKatedra(Katedra katedra)
    {
        Katedra? oldKatedra = GetKatedraById(katedra.Id);
        if (oldKatedra is null) return null;

        oldKatedra.Sifra_Katedre = katedra.Sifra_Katedre;
        oldKatedra.Naziv_Katedre = katedra.Naziv_Katedre;
        oldKatedra.Sef_Katedre = katedra.Sef_Katedre;
        oldKatedra.Spisak_Profesora_Na_Katedri = katedra.Spisak_Profesora_Na_Katedri;
      


        _storage.Save(_katedra);
        return oldKatedra;
    }

    public Katedra? RemoveKatedra(int id)
    {
        Katedra? katedra = GetKatedraById(id);
        if (katedra == null) return null;

        _katedra.Remove(katedra);
        _storage.Save(_katedra);
        return katedra;
    }

    private Katedra? GetKatedraById(int id)
    {
        return _katedra.Find(s => s.Id == id);
    }

    public List<Katedra> GetAllKatedra()
    {
        return _katedra;
    }

    public List<Katedra> GetAllKatedra(int page, int pageSize, string sortCriteria, SortDirection sortDirection)
    {
        IEnumerable<Katedra> katedra = _katedra;

        
        switch (sortCriteria)
        {
            case "Id":
                katedra = _katedra.OrderBy(x => x.Id);
                break;
            case "Sifra katedre":
                katedra = _katedra.OrderBy(x => x.Sifra_Katedre);
                break;
            case "Naziv katedre":
                katedra = _katedra.OrderBy(x => x.Naziv_Katedre);
                break;
            case "Sef katedre":
                katedra = _katedra.OrderBy(x => x.Sef_Katedre);
                break;
            case "Spisak profesora na katedri":
                katedra = _katedra.OrderBy(x => x.Spisak_Profesora_Na_Katedri);
                break;
            

        }

        // promeni redosled ukoliko ima potrebe za tim
        if (sortDirection == SortDirection.Descending)
            katedra = katedra.Reverse();

        // paginacija
        katedra = katedra.Skip((page - 1) * pageSize).Take(pageSize);

        return katedra.ToList();
    }
}
