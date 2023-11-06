using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.Model;
using StudentskaSluzba.Storage;
using static StudentskaSluzba.Model.Profesor;
using static StudentskaSluzba.Model.Student;

namespace StudentskaSluzba.DAO;

class ProfesorDAO
{
    private readonly List<Profesor> _professors;
    private readonly Storage<Profesor> _storage;

    public ProfesorDAO()
    {
        _storage = new Storage<Profesor>("profesori.txt");
        _professors = _storage.Load();
    }

    private int GenerateId()
    {
        if (_professors.Count == 0) return 0;
        return _professors[^1].Id + 1;

    }

    public Profesor AddProfessor(Profesor profesor)
    {
        profesor.Id = GenerateId();
        _professors.Add(profesor);
        _storage.Save(_professors);
        return profesor;
    }
    
    public Profesor? UpdateProfessor(Profesor profesor)
    {
        Profesor? oldProfesor = GetProfessorById(profesor.Id);
        if (oldProfesor is null) return null;

        oldProfesor.Ime = profesor.Ime;
        oldProfesor.Prezime = profesor.Prezime;
        oldProfesor.Datum_Rodjenja = profesor.Datum_Rodjenja;
        oldProfesor.Adresa_Stanovanja = profesor.Adresa_Stanovanja;
        oldProfesor.Kontakt_Telefon = profesor.Kontakt_Telefon;
        oldProfesor.Email = profesor.Email;
        oldProfesor.Broj_Licne_Karte = profesor.Broj_Licne_Karte;
        oldProfesor.Zvanje = profesor.Zvanje;
        oldProfesor.Godine_Staza = profesor.Godine_Staza;
        oldProfesor.Spisak_Predmeta_Na_Kojima_Je_Profesor = profesor.Spisak_Predmeta_Na_Kojima_Je_Profesor;
        

        _storage.Save(_professors);
        return oldProfesor;
    }

    public Profesor? RemoveProfessor(int id)
    {
        Profesor? profesor = GetProfessorById(id);
        if (profesor == null) return null;

        _professors.Remove(profesor);
        _storage.Save(_professors);
        return profesor;
    }

    private Profesor? GetProfessorById(int id)
    {
        return _professors.Find(s => s.Id == id);
    }

    public List<Profesor> GetAllProfessors()
    {
        return _professors;
    }

    public List<Profesor> GetAllProfessors(int page, int pageSize, string sortCriteria, SortDirection sortDirection)
    {
        IEnumerable<Profesor> professors = _professors;

       
        switch (sortCriteria)
        {
            case "Id":
                professors = _professors.OrderBy(x => x.Id);
                break;
            case "Ime":
                professors = _professors.OrderBy(x => x.Ime);
                break;
            case "Prezime":
                professors = _professors.OrderBy(x => x.Prezime);
                break;
            case "Datum rodjenja":
                professors = _professors.OrderBy(x => x.Datum_Rodjenja);
                break;
            case "Adresa stanovanja":
                professors = _professors.OrderBy(x => x.Adresa_Stanovanja);
                break;
            case "Kontakt telefon":
                professors = _professors.OrderBy(x => x.Kontakt_Telefon);
                break;
            case "Email":
                professors = _professors.OrderBy(x => x.Email);
                break;
            case "Broj licne karte":
                professors = _professors.OrderBy(x => x.Broj_Licne_Karte);
                break;
            case "Zvanje":
                professors = _professors.OrderBy(x => x.Zvanje);
                break;
            case "Godine staza":
                professors = _professors.OrderBy(x => x.Godine_Staza);
                break;
            case "Spisak predmeta na kojima je profesor":
                professors = _professors.OrderBy(x => x.Spisak_Predmeta_Na_Kojima_Je_Profesor);
                break;
           
        }

        // promeni redosled ukoliko ima potrebe za tim
        if (sortDirection == SortDirection.Descending)
            professors = professors.Reverse();

        // paginacija
        professors = professors.Skip((page - 1) * pageSize).Take(pageSize);

        return professors.ToList();
    }


}
