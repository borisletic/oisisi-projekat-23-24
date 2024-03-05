using CLI.Model;
using CLI.Observer;
using CLI.Storage;

namespace CLI.DAO
{

    public class IndeksDAO
    {
        private readonly List<Indeks> _indeks;
        private readonly Storage<Indeks> _storage;

        public Subject IndeksSubject;

        public IndeksDAO()
        {
            _storage = new Storage<Indeks>("indeksi.txt");
            _indeks = _storage.Load();
            IndeksSubject = new Subject();
        }

        private int GenerateId()
        {
            if (_indeks.Count == 0) return 0;
            return _indeks[^1].Id + 1;

        }

        public Indeks AddIndeks(Indeks indeks)
        {
            indeks.Id = GenerateId();
            _indeks.Add(indeks);
            _storage.Save(_indeks);
            IndeksSubject.NotifyObservers();
            return indeks;
        }

        public Indeks UpdateIndeks(Indeks indeks)
        {
            Indeks? oldIndeks = GetIndeksById(indeks.Id);
            if (oldIndeks is null) return null;

            oldIndeks.Oznaka_Smera = indeks.Oznaka_Smera;
            oldIndeks.Broj_Upisa = indeks.Broj_Upisa;
            oldIndeks.Godina_Upisa = indeks.Godina_Upisa;




            _storage.Save(_indeks);
            IndeksSubject.NotifyObservers();
            return oldIndeks;
        }

        public Indeks RemoveIndeks(int id)
        {
            Indeks? indeks = GetIndeksById(id);
            if (indeks == null) return null;

            _indeks.Remove(indeks);
            _storage.Save(_indeks);
            IndeksSubject.NotifyObservers();
            return indeks;
        }

        public Indeks GetIndeksById(int id)
        {
            return _indeks.Find(s => s.Id == id);
        }

        public List<Indeks> GetAllIndeks()
        {
            return _indeks;
        }

        public List<Indeks> GetAllIndeks(int page, int pageSize, string sortCriteria, SortDirection sortDirection)
        {
            IEnumerable<Indeks> indeks = _indeks;


            switch (sortCriteria)
            {
                case "Id":
                    indeks = _indeks.OrderBy(x => x.Id);
                    break;
                case "Oznaka smera":
                    indeks = _indeks.OrderBy(x => x.Oznaka_Smera);
                    break;
                case "Broj upisa":
                    indeks = _indeks.OrderBy(x => x.Broj_Upisa);
                    break;
                case "Godina upisa":
                    indeks = _indeks.OrderBy(x => x.Godina_Upisa);
                    break;



            }

            // promeni redosled ukoliko ima potrebe za tim
            if (sortDirection == SortDirection.Descending)
                indeks = indeks.Reverse();

            // paginacija
            indeks = indeks.Skip((page - 1) * pageSize).Take(pageSize);

            return indeks.ToList();
        }


    }
}
