using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.Model;
using StudentskaSluzba.Storage;
using static StudentskaSluzba.Model.Student;

namespace StudentskaSluzba.DAO;

class StudentDAO
{
    private readonly List<Student> _students;
    private readonly Storage<Student> _storage;

    public StudentDAO()
    {
        _storage = new Storage<Student>("studenti.txt");
        _students = _storage.Load();
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
        return student;
    }
     //Status StatusStudenta;
    public Student? UpdateStudent(Student student)
    {
        Student? oldStudent = GetStudentById(student.Id);
        if (oldStudent is null) return null;

        oldStudent.Ime = student.Ime;
        oldStudent.Prezime = student.Prezime;
        oldStudent.Datum_Rodjenja = student.Datum_Rodjenja;
        oldStudent.Adresa_Stanovanja = student.Adresa_Stanovanja;
        oldStudent.Kontakt_Telefon = student.Kontakt_Telefon;
        oldStudent.Email = student.Email;
        oldStudent.Broj_Indeksa = student.Broj_Indeksa;
        oldStudent.Trenutna_Godina_Studija = student.Trenutna_Godina_Studija;
        oldStudent.Prosecna_Ocena = student.Prosecna_Ocena;
        oldStudent.Polozeni_Ispiti = student.Polozeni_Ispiti;
        oldStudent.Nepolozeni_Ispiti = student.Nepolozeni_Ispiti;
        oldStudent.StatusStudenta = student.StatusStudenta;

        _storage.Save(_students);
        return oldStudent;
    }

    public Student? RemoveStudent(int id)
    {
        Student? student = GetStudentById(id);
        if (student == null) return null;

        _students.Remove(student);
        _storage.Save(_students);
        return student;
    }

    private Student? GetStudentById(int id)
    {
        return _students.Find(s => s.Id == id);
    }

    public List<Student> GetAllStudents()
    {
        return _students;
    }

    public List<Student> GetAllStudents(int page, int pageSize, string sortCriteria, SortDirection sortDirection)
    {
        IEnumerable<Student> students = _students;

        
        switch (sortCriteria)
        {
            case "Id":
                students = _students.OrderBy(x => x.Id);
                break;
            case "Ime":
                students = _students.OrderBy(x => x.Ime);
                break;
            case "Prezime":
                students = _students.OrderBy(x => x.Prezime);
                break;
            case "Datum rodjenja":
                students = _students.OrderBy(x => x.Datum_Rodjenja);
                break;
            case "Adresa stanovanja":
                students = _students.OrderBy(x => x.Adresa_Stanovanja);
                break;
            case "Kontakt telefon":
                students = _students.OrderBy(x => x.Kontakt_Telefon);
                break;
            case "Email":
                students = _students.OrderBy(x => x.Email);
                break;
            case "Broj indeksa":
                students = _students.OrderBy(x => x.Broj_Indeksa);
                break;
            case "Trenutna godina studija":
                students = _students.OrderBy(x => x.Trenutna_Godina_Studija);
                break;
            case "Prosecna ocena":
                students = _students.OrderBy(x => x.Prosecna_Ocena);
                break;
            case "Polozeni ispiti":
                students = _students.OrderBy(x => x.Polozeni_Ispiti);
                break;
            case "Nepolozeni ispiti":
                students = _students.OrderBy(x => x.Nepolozeni_Ispiti);
                break;
            case "Status":
                students = _students.OrderBy(x => x.StatusStudenta);
                break;
        }

        // promeni redosled ukoliko ima potrebe za tim
        if (sortDirection == SortDirection.Descending)
            students = students.Reverse();

        // paginacija
        students = students.Skip((page - 1) * pageSize).Take(pageSize);

        return students.ToList();
    }




}
