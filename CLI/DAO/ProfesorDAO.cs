using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Model;
using CLI.Observer;
using CLI.Storage;

namespace CLI.DAO
{

    public class ProfesorDAO
    {
        private readonly List<Profesor> _professors;
        private readonly Storage<Profesor> _storage;

        public Subject ProfesorSubject;

        public ProfesorDAO()
        {
            _storage = new Storage<Profesor>("profesori.txt");
            _professors = _storage.Load();
            ProfesorSubject = new Subject();
            AdresaDAO adresaDAO = new AdresaDAO();
            foreach (Profesor profesor in GetAllProfesors()) 
            {
                profesor.Adresa_Stanovanja = adresaDAO.GetAdresaById(profesor.Adresa_Stanovanja.Id);
            }
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
            ProfesorSubject.NotifyObservers();
            return profesor;
        }

        public Profesor? UpdateProfesor(Profesor profesor)
        {
            Profesor? oldProfesor = GetProfesorById(profesor.Id);
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
            oldProfesor.Spisak_Predmeta_Profesora = profesor.Spisak_Predmeta_Profesora;


            _storage.Save(_professors);
            ProfesorSubject.NotifyObservers();
            return oldProfesor;
        }

        public Profesor? RemoveProfesor(int id)
        {
            Profesor? profesor = GetProfesorById(id);
            if (profesor == null) return null;

            _professors.Remove(profesor);
            _storage.Save(_professors);
            ProfesorSubject.NotifyObservers();
            return profesor;
        }

        public Profesor? GetProfesorById(int id)
        {
            return _professors.Find(p => p.Id == id);
        }

        public List<Profesor> GetAllProfesors()
        {
            return _professors;
        }

        public IEnumerable<Profesor> GetAllProfesors(int page, int pageSize, string sortCriteria, SortDirection sortDirection)
        {
            IEnumerable<Profesor> professors = _professors;


            switch (sortCriteria)
            {
                case "Ime":
                    professors = _professors.OrderBy(x => x.Ime);
                    break;
                case "Prezime":
                    professors = _professors.OrderBy(x => x.Prezime);
                    break;
                case "Email":
                    professors = _professors.OrderBy(x => x.Email);
                    break;
                case "Zvanje":
                    professors = _professors.OrderBy(x => x.Zvanje);
                    break;

            }

            // promeni redosled ukoliko ima potrebe za tim
            if (sortDirection == SortDirection.Descending)
                professors = professors.Reverse();

            // paginacija
            professors = professors.Skip((page - 1) * pageSize).Take(pageSize);

            return professors;
        }

    }
}
