using CLI.Observer;
using CLI.DAO;
using CLI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace CLI.Controller
{
    public class StudentController
    {
        public StudentDAO _student { get; init; }
        private readonly IndeksDAO _indeks;
        private readonly AdresaDAO _adresa;

        public StudentController()
        {
            _student = new StudentDAO();
            _indeks = new IndeksDAO();
            _adresa = new AdresaDAO();
        }

        public List<Student> GetAllStudents()
        {
            return _student.GetAllStudents();
        }

        public void Add(Student student)
        {
            _student.AddStudent(student);
        }

        public void Update(Student student)
        {
            _student.UpdateStudent(student);
        }

        public void Delete(int studentId)
        {
            _student.RemoveStudent(studentId);
        }

        public void AddAdresa(Adresa adresa) 
        { 
            _adresa.AddAdresa(adresa);
        }

        public void AddIndeks(Indeks indeks)
        {
            _indeks.AddIndeks(indeks);
        }

        public void UpdateAdresa(Adresa adresa)
        {
            _adresa.UpdateAdresa(adresa);
        }

        public void UpdateIndeks(Indeks indeks)
        {
            _indeks.UpdateIndeks(indeks);
        }

        public void RemoveIndeks(int id)
        {
            _indeks.RemoveIndeks(id);
        }

        public List<Indeks> GetAllIndeks() 
        {
            return _indeks.GetAllIndeks();
        }

        public Student GetStudentById(int id)
        {
            return _student.GetStudentById(id);
        }

        public IEnumerable<Student> GetAllStudents(int page, int pageSize,string sortCriteria,SortDirection sortDirection)
        {
            return _student.GetAllStudents(page, pageSize,sortCriteria,sortDirection);
        }

        public void Subscribe(IObserver observer)
        {
            _student.StudentSubject.Subscribe(observer);
        }
    }
}
