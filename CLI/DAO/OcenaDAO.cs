using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CLI.Model;
using CLI.Observer;
using CLI.Storage;

namespace CLI.DAO
{

    public class OcenaDAO
    {
        private readonly List<Ocena> _ocena;
        private readonly Storage<Ocena> _storage;

        public Subject OcenaSubject;
        public OcenaDAO(StudentDAO studentDAO,PredmetDAO predmetDAO)
        {
            _storage = new Storage<Ocena>("polozeni.txt");
            _ocena = _storage.Load();
            OcenaSubject = new Subject();
            double i = 0;
            double broj = 0;
            foreach (Ocena ocena in GetAllOcena()) 
            {
                ocena.Student_Koji_Je_Polozio = studentDAO.GetStudentById(ocena.Student_Koji_Je_Polozio.Id);
                ocena.Predmet = predmetDAO.GetPredmetById(ocena.Predmet.Id);
                if (ocena.Student_Koji_Je_Polozio != null)
                {
                    studentDAO.GetStudentById(ocena.Student_Koji_Je_Polozio.Id).Polozeni_Ispiti.Add(ocena);
                }
            }

            foreach (Student student in studentDAO.GetAllStudents()) 
            {
                if (student.Polozeni_Ispiti.Count == 0)
                {
                    student.Prosecna_Ocena = 0.0;
                }
                else 
                {
                    broj = 0;
                    i = 0;
                    foreach (Ocena ocena in student.Polozeni_Ispiti) 
                    {
                        i++;
                        broj += ocena.Ocena_Broj;
                    }
                    student.Prosecna_Ocena = broj/i;
                }
            }
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
            foreach (Ocena ocena1 in _ocena) 
            {
                System.Diagnostics.Debug.WriteLine(ocena1.Id);
            }
            OcenaSubject.NotifyObservers();
            return ocena;
        }

        public Ocena? UpdateOcena(Ocena ocena)
        {
            Ocena? oldOcena = GetOcenaById(ocena.Id);
            if (oldOcena is null) return null;

            oldOcena.Student_Koji_Je_Polozio = ocena.Student_Koji_Je_Polozio;
            oldOcena.Predmet = ocena.Predmet;
            oldOcena.Ocena_Broj = ocena.Ocena_Broj;
            oldOcena.Datum_Polaganja = ocena.Datum_Polaganja;



            _storage.Save(_ocena);
            OcenaSubject.NotifyObservers();
            return oldOcena;
        }

        public Ocena? RemoveOcena(int studentId, int predmetId)
        {       
            Ocena? ocena = GetOcenaByIdPosebno(studentId,predmetId);
            _ocena.Remove(ocena);
            _storage.Save(_ocena);
            OcenaSubject.NotifyObservers();
            return ocena;
        }
        public Ocena GetOcenaByIdPosebno(int studentId, int predmetId) 
        {
            return _ocena.Find(o => o.Student_Koji_Je_Polozio.Id == studentId && o.Predmet.Id == predmetId);
        }
        public Ocena? GetOcenaById(int id)
        {
            return _ocena.Find(s => s.Id == id);
        }

        public List<Ocena> GetAllOcena()
        {
            return _ocena;
        }

    }
}
