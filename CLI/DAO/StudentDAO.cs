using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLI.Observer;
using CLI.Model;
using CLI.Storage;
using CLI.Controller;

namespace CLI.DAO
{

    public class StudentDAO
    {
        private readonly List<Student> _students;
        private readonly Storage<Student> _storage;

        public Subject StudentSubject;

        public StudentDAO()
        {
            _storage = new Storage<Student>("studenti.txt");
            _students = _storage.Load();
            StudentSubject = new Subject();
            AdresaDAO adresaDAO = new AdresaDAO();
            foreach (Student student in GetAllStudents()) 
            {
                student.Adresa_Stanovanja = adresaDAO.GetAdresaById(student.Adresa_Stanovanja.Id);
            }
        }

        private int GenerateId()
        {
            if (_students.Count == 0) return 0;
            return _students[^1].Id + 1;

        }

        public Student AddStudent(Student student)
        {
            student.Id = GenerateId();
            _students.Add(student);
            _storage.Save(_students);
            StudentSubject.NotifyObservers();
            return student;
        }
        public Student UpdateStudent(Student student)
        {
            Student oldStudent = GetStudentById(student.Id);
            if (oldStudent is null) return null;

            oldStudent.Id = student.Id;
            oldStudent.Ime = student.Ime;
            oldStudent.Prezime = student.Prezime;
            oldStudent.Datum_Rodjenja = student.Datum_Rodjenja;
            oldStudent.Adresa_Stanovanja = student.Adresa_Stanovanja;
            oldStudent.Kontakt_Telefon = student.Kontakt_Telefon;
            oldStudent.Email = student.Email;
            oldStudent.Indeks = student.Indeks;
            oldStudent.Trenutna_Godina_Studija = student.Trenutna_Godina_Studija;
            oldStudent.StatusStudenta = student.StatusStudenta;
            oldStudent.Prosecna_Ocena = student.Prosecna_Ocena;
            oldStudent.Polozeni_Ispiti = student.Polozeni_Ispiti;
            oldStudent.Nepolozeni_Ispiti = student.Nepolozeni_Ispiti;


            _storage.Save(_students);
            StudentSubject.NotifyObservers();
            return oldStudent;
        }

        public Student RemoveStudent(int id)
        {
            Student? student = GetStudentById(id);
            if (student == null) return null;

            _students.Remove(student);
            _storage.Save(_students);
            StudentSubject.NotifyObservers();
            return student;
        }

        public Student GetStudentById(int id)
        {
            foreach (Student student in GetAllStudents())
            {
                if (student.Id == id)
                {
                    return student;
                }
            }
            return null;
        }

        public List<Student> GetAllStudents()
        {
            return _students;
        }

        public IEnumerable<Student> GetAllStudents(int page, int pageSize, string sortCriteria,SortDirection sortDirection)
        {
            IEnumerable<Student> students = _students;
            //case-ovi su radjeni prema headerima od kolona iz datagrida za studente
            switch (sortCriteria)
            {
                case "Ime":
                    students = _students.OrderBy(x => x.Ime);
                    break;
                case "Prezime":
                    students = _students.OrderBy(x => x.Prezime);
                    break;
                case "Godina Studija":
                    students = _students.OrderBy(x => x.Trenutna_Godina_Studija);
                    break;
                case "Status":
                    students = _students.OrderBy(x => x.StatusStudenta);
                    break;
                case "Prosek":
                    students = _students.OrderBy(x => x.Prosecna_Ocena);
                    break;
                case "Indeks":
                    students = _students.OrderBy(x => x.Indeks.Broj_Upisa);
                    break;
            }

            if (sortDirection == SortDirection.Descending)
                students = students.Reverse();

            // paginacija
           
            students = students.Skip((page - 1) * pageSize).Take(pageSize);
            return students.ToList();
        }
    }
}
