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

    class KatedraDAO
    {
        private readonly List<Katedra> _katedra;
        private readonly Storage<Katedra> _storage;

        public Subject KatedraSubject;
        public KatedraDAO(ProfesorDAO profesorDAO)
        {
            _storage = new Storage<Katedra>("katedre.txt");
            _katedra = _storage.Load();
            KatedraSubject = new Subject();
            foreach (Profesor profesor in profesorDAO.GetAllProfesors())
            {
                foreach (Katedra katedra in GetAllKatedra())
                {
                    if (profesor.KatedraId == katedra.Id)
                    {
                        katedra.Spisak_Profesora.Add(profesor);
                    }
                }
            }
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
            oldKatedra.Spisak_Profesora = katedra.Spisak_Profesora;

            _storage.Save(_katedra);
            KatedraSubject.NotifyObservers();
            return oldKatedra;
        }
        public Katedra? GetKatedraById(int id)
        {
            return _katedra.Find(s => s.Id == id);
        }

        public List<Katedra> GetAllKatedra()
        {
            return _katedra;
        }
    }
}
