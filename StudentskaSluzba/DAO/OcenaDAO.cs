using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.Model;
using StudentskaSluzba.Storage;
using static StudentskaSluzba.Model.Ocena;

namespace StudentskaSluzba.DAO;

 class OcenaDAO
{
    private readonly List<Ocena> _ocena;
    private readonly Storage<Ocena> _storage;

    public OcenaDAO()
    {
        _storage = new Storage<Ocena>("ocene.txt");
        _ocena = _storage.Load();
    }

    private int GenerateId()
    {
        if (_ocena.Count == 0) return 0;
        return _ocena[^1].Id + 1;

    }

    public Ocena AddOcena(Ocena ocena)
    {
        ocena.Id = GenerateId();
        _ocena.Add(ocena);
        _storage.Save(_ocena);
        return ocena;
    }

    public Ocena? UpdateOcena(Ocena ocena)
    {
        Ocena? oldOcena = GetOcenaById(ocena.Id);
        if (oldOcena is null) return null;

        oldOcena.Student_Koji_Je_Polozio = ocena.Student_Koji_Je_Polozio;
        oldOcena.Predmet = ocena.Predmet;
        oldOcena.Brojcana_Vrednost_Ocene = ocena.Brojcana_Vrednost_Ocene;
        oldOcena.Datum_Polaganja_Ispita = ocena.Datum_Polaganja_Ispita;
       


        _storage.Save(_ocena);
        return oldOcena;
    }

    public Ocena? RemoveOcena(int id)
    {
        Ocena? ocena = GetOcenaById(id);
        if (ocena == null) return null;

        _ocena.Remove(ocena);
        _storage.Save(_ocena);
        return ocena;
    }

    private Ocena? GetOcenaById(int id)
    {
        return _ocena.Find(s => s.Id == id);
    }

    public List<Ocena> GetAllOcena()
    {
        return _ocena;
    }

    public List<Ocena> GetAllOcena(int page, int pageSize, string sortCriteria, SortDirection sortDirection)
    {
        IEnumerable<Ocena> ocena = _ocena;

        
        switch (sortCriteria)
        {
            case "Id":
                ocena = _ocena.OrderBy(x => x.Id);
                break;
            case "Student koji je polozio":
                ocena = _ocena.OrderBy(x => x.Student_Koji_Je_Polozio);
                break;
            case "Predmet":
                ocena = _ocena.OrderBy(x => x.Predmet);
                break;
            case "Brojcana vrednost ocene":
                ocena = _ocena.OrderBy(x => x.Brojcana_Vrednost_Ocene);
                break;
            case "Datum polaganja ispita":
                ocena = _ocena.OrderBy(x => x.Datum_Polaganja_Ispita);
                break;
           

        }

        // promeni redosled ukoliko ima potrebe za tim
        if (sortDirection == SortDirection.Descending)
            ocena = ocena.Reverse();

        // paginacija
        ocena = ocena.Skip((page - 1) * pageSize).Take(pageSize);

        return ocena.ToList();
    }



}
