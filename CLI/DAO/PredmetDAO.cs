using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Model;
using CLI.Observer;
using CLI.Storage;


namespace CLI.DAO
{

    public class PredmetDAO
    {
        private readonly List<Predmet> _predmet;
        private readonly Storage<Predmet> _storage;

        public Subject PredmetSubject;

        public PredmetDAO()
        {
            _storage = new Storage<Predmet>("predmeti.txt");
            _predmet = _storage.Load();
            PredmetSubject = new Subject();
            ProfesorDAO profesorDAO = new ProfesorDAO();
            foreach (Predmet predmet in GetAllPredmeti()) 
            {
                if (predmet.Predmetni_Profesor.Id != 0)
                {
                    //radilo bi da je kod normalan
                    //predmet.Predmetni_Profesor = profesorDAO.GetProfesorById(predmet.Predmetni_Profesor.Id);
                    //profesorDAO.GetProfesorById(predmet.Predmetni_Profesor.Id).Spisak_Predmeta_Profesora.Add(predmet); 
                }
            }
        }

        private int GenerateId()
        {
            if (_predmet.Count == 0) return 0;
            return _predmet[^1].Id + 1;

        }
        public Predmet AddPredmet(Predmet predmet)
        {
            predmet.Id = GenerateId();
            _predmet.Add(predmet);
            _storage.Save(_predmet);
            PredmetSubject.NotifyObservers();
            return predmet;
        }

        public Predmet? UpdatePredmet(Predmet predmet)
        {
            Predmet? oldPredmet = GetPredmetById(predmet.Id);
            if (oldPredmet is null) return null;

            oldPredmet.Sifra_Predmeta = predmet.Sifra_Predmeta;
            oldPredmet.Naziv_Predmeta = predmet.Naziv_Predmeta;
            oldPredmet.SemestarPredmeta = predmet.SemestarPredmeta;
            oldPredmet.Godina_Predmeta = predmet.Godina_Predmeta;
            oldPredmet.Predmetni_Profesor = predmet.Predmetni_Profesor;
            oldPredmet.ESPB = predmet.ESPB;
            oldPredmet.Studenti_Polozeni = predmet.Studenti_Polozeni;
            oldPredmet.Studenti_Nepolozeni = predmet.Studenti_Nepolozeni;


            _storage.Save(_predmet);
            PredmetSubject.NotifyObservers();
            return oldPredmet;
        }

        public Predmet? RemovePredmet(int id)
        {
            Predmet? predmet = GetPredmetById(id);
            if (predmet == null) return null;

            _predmet.Remove(predmet);
            _storage.Save(_predmet);
            PredmetSubject.NotifyObservers();
            return predmet;
        }

        public Predmet? GetPredmetById(int id)
        {
            return _predmet.Find(s => s.Id == id);
        }

        public List<Predmet> GetAllPredmeti()
        {
            return _predmet;
        }

        public IEnumerable<Predmet> GetAllPredmeti(int page, int pageSize, string sortCriteria, SortDirection sortDirection)
        {
            IEnumerable<Predmet> predmet = _predmet;


            switch (sortCriteria)
            {
                case "Sifra predmeta":
                    predmet = _predmet.OrderBy(x => x.Sifra_Predmeta);
                    break;
                case "Naziv predmeta":
                    predmet = _predmet.OrderBy(x => x.Naziv_Predmeta);
                    break;
                case "Semestar predmeta":
                    predmet = _predmet.OrderBy(x => x.SemestarPredmeta);
                    break;
                case "Godina predmeta":
                    predmet = _predmet.OrderBy(x => x.Godina_Predmeta);
                    break;
                case "Predmetni profesor":
                    predmet = _predmet.OrderBy(x => x.Predmetni_Profesor);
                    break;
                case "ESPB":
                    predmet = _predmet.OrderBy(x => x.ESPB);
                    break;
            }

            // promeni redosled ukoliko ima potrebe za tim
            if (sortDirection == SortDirection.Descending)
                predmet = predmet.Reverse();

            // paginacija
            predmet = predmet.Skip((page - 1) * pageSize).Take(pageSize);

            return predmet;
        }


    }
}
