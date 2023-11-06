using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.DAO;
using StudentskaSluzba.Model;
using static StudentskaSluzba.Model.Student;

namespace StudentskaSluzba.Console;

class StudentConsoleView
{
    private readonly StudentDAO _studentDao;

    public StudentConsoleView(StudentDAO studentDao)
    {
        _studentDao = studentDao;
    }

    private void PrintStudents(List<Student> students)
    {
        System.Console.WriteLine("Students: ");
        string header = $"Id {"", 5} | Ime {"",20} | Prezime {"",20} | Datum Rodjenja {"",20} | Adresa Stanovanja {"",20} | Kontakt telefon {"",20} | E-mail {"",20} | Broj indeksa {"",20} | Trenutna godina studija {"",20} | Prosecna ocena {"",20} | Polozeni ispiti {"",50} | Nepolozeni ispiti {"",50} | Status {"",20} |";
        System.Console.WriteLine(header);
        foreach (Student s in students)
        {
            System.Console.WriteLine(s);
        }
    }


    private Student InputStudent()
    {
        System.Console.WriteLine("Unesite ime studenta: ");
        string ime = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Unesite prezime studenta: ");
        string prezime = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Unesite datum rodjenja studenta: ");
        string datum_rodjenja = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Unesite adresu stanovanja studenta: ");
        string adresa_stanovanja = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Unesite kontakt telefon studenta: ");
        int kontakt_telefon = ConsoleViewUtils.SafeInputInt();

        System.Console.WriteLine("Unesite email studenta: ");
        string email = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Unesite broj indeksa studenta: ");
        string broj_indeksa = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Unesite trenutnu godinu studija studenta: ");
        int trenutna_godina_studija = ConsoleViewUtils.SafeInputInt();

        System.Console.WriteLine("Unesite prosecnu ocenu studenta: ");
        int prosecna_ocena = ConsoleViewUtils.SafeInputInt();

        System.Console.WriteLine("Unesite polozene ispite studenta (razdvojene razmakom): ");
        string inputp = System.Console.ReadLine() ?? string.Empty;
        List<string> polozeni_ispiti = inputp.Split(' ').ToList();

        System.Console.WriteLine("Unesite nepolozene ispite studenta (razdvojene razmakom): ");
        string inputnp = System.Console.ReadLine() ?? string.Empty;
        List<string> nepolozeni_ispiti = inputnp.Split(' ').ToList();

        System.Console.WriteLine("Unesite status finansiranja studenta: ");
        string statusstudenta = System.Console.ReadLine() ?? string.Empty;

        Status status;

        
        if (Enum.TryParse(statusstudenta, true, out status))
        {
            
            System.Console.WriteLine($"Uneli ste: {status}");
        }
        else
        {
            
            System.Console.WriteLine("Uneli ste neispravnu vrstu finansiranja.");
        }

        return new Student(ime, prezime, datum_rodjenja, adresa_stanovanja, kontakt_telefon, email, broj_indeksa,trenutna_godina_studija, prosecna_ocena, polozeni_ispiti, nepolozeni_ispiti, status);
    }

    private int InputId()
    {
        System.Console.WriteLine("Unesite ID: ");
        return ConsoleViewUtils.SafeInputInt();
    }

    public void RunMenu()
    {
        while (true)
        {
            ShowMenu();
            string userInput = System.Console.ReadLine() ?? "0";
            if (userInput == "0") break;
            HandleMenuInput(userInput);
        }
    }


    private void HandleMenuInput(string input)
    {
        switch (input)
        {
            case "1":
                ShowAllStudents();
                break;
            case "2":
                AddStudent();
                break;
            case "3":
                UpdateStudent();
                break;
            case "4":
                RemoveStudent();
                break;
            case "5":
                ShowAndSortStudents();
                break;
        }
    }

    private void ShowAllStudents()
    {
        PrintStudents(_studentDao.GetAllStudents());
    }

    private void RemoveStudent()
    {
        int indx = InputId();
        Student? removedStudent = _studentDao.RemoveStudent(indx);
        if (removedStudent is null)
        {
            System.Console.WriteLine("Student not found");
            return;
        }

        System.Console.WriteLine("Student removed");
    }

    private void UpdateStudent()
    {
        int indx = InputId();
        Student student = InputStudent();
        student.Id = indx;
        Student? updatedStudent = _studentDao.UpdateStudent(student);
        if (updatedStudent == null)
        {
            System.Console.WriteLine("Student not found");
            return;
        }

        System.Console.WriteLine("Student updated");
    }

    private void AddStudent()
    {
        Student student = InputStudent();
        _studentDao.AddStudent(student);
        System.Console.WriteLine("Student added");
    }

    private void ShowAndSortStudents()
    {
        System.Console.WriteLine("\nEnter page: ");
        int page = ConsoleViewUtils.SafeInputInt();
        System.Console.WriteLine("\nEnter page size: ");
        int pageSize = ConsoleViewUtils.SafeInputInt();
        System.Console.WriteLine("\nEnter sort criteria: ");
        string sortCriteria = System.Console.ReadLine() ?? string.Empty;
        System.Console.WriteLine("\nEnter 0 for ascending, any key for descending: ");
        int sortDirectionInput = ConsoleViewUtils.SafeInputInt();
        SortDirection sortDirection = sortDirectionInput == 0 ? SortDirection.Ascending : SortDirection.Descending;

        PrintStudents(_studentDao.GetAllStudents(page, pageSize, sortCriteria, sortDirection));
    }

    private void ShowMenu()
    {
        System.Console.WriteLine("\nChoose an option: ");
        System.Console.WriteLine("1: Show All students");
        System.Console.WriteLine("2: Add student");
        System.Console.WriteLine("3: Update student");
        System.Console.WriteLine("4: Remove student");
        System.Console.WriteLine("5: Show and sort students");
        System.Console.WriteLine("0: Close");
    }

}
