using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.DAO;
using StudentskaSluzba.Model;
using static StudentskaSluzba.Model.Profesor;
using static StudentskaSluzba.Model.Student;

namespace StudentskaSluzba.Console;

class ProfesorConsoleView
{
    private readonly ProfesorDAO _profesorDao;

    public ProfesorConsoleView(ProfesorDAO profesorDao)
    {
        _profesorDao = profesorDao;
    }

    private void PrintProfessors(List<Profesor> professors)
    {
        System.Console.WriteLine("Professors: ");
        string header = $"Id {"",5} | Ime {"",20} | Prezime {"",20} | Datum Rodjenja {"",20} | Adresa Stanovanja {"",20} | Kontakt telefon {"",20} | E-mail {"",20} | Broj licne karte {"",20} | Zvanje {"",20} | Godine staza {"",20} | Spisak predmeta na kojima je profesor {"",50} |";
        System.Console.WriteLine(header);
        foreach (Profesor p in professors)
        {
            System.Console.WriteLine(p);
        }
    }


    private Profesor InputProfessor()
    {
        System.Console.WriteLine("Unesite ime profesora: ");
        string ime = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Unesite prezime profesora: ");
        string prezime = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Unesite datum rodjenja profesora: ");
        string datum_rodjenja = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Unesite adresu stanovanja profesora: ");
        string adresa_stanovanja = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Unesite kontakt telefon profesora: ");
        int kontakt_telefon = ConsoleViewUtils.SafeInputInt();

        System.Console.WriteLine("Unesite email profesora: ");
        string email = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Unesite broj licne karte profesora: ");
        int broj_licne_karte = ConsoleViewUtils.SafeInputInt();

        System.Console.WriteLine("Unesite zvanje profesora: ");
        string zvanje = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Unesite godine staza profesora: ");
        int godine_staza = ConsoleViewUtils.SafeInputInt();

        System.Console.WriteLine("Unesite spisak predmeta na kojima je profesor: ");
        string inputpr = System.Console.ReadLine() ?? string.Empty;
        List<string> spnkjp = inputpr.Split(' ').ToList();

        

        return new Profesor(ime, prezime, datum_rodjenja, adresa_stanovanja, kontakt_telefon, email, broj_licne_karte, zvanje, godine_staza, spnkjp);
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
                ShowAllProfessors();
                break;
            case "2":
                AddProfessor();
                break;
            case "3":
                UpdateProfessor();
                break;
            case "4":
                RemoveProfessor();
                break;
            case "5":
                ShowAndSortProfessors();
                break;
        }
    }

    private void ShowAllProfessors()
    {
        PrintProfessors(_profesorDao.GetAllProfessors());
    }

    private void RemoveProfessor()
    {
        int lcn = InputId();
        Profesor? removedProfessor = _profesorDao.RemoveProfessor(lcn);
        if (removedProfessor is null)
        {
            System.Console.WriteLine("Professor not found");
            return;
        }

        System.Console.WriteLine("Professor removed");
    }

    private void UpdateProfessor()
    {
        int lcn = InputId();
        Profesor profesor = InputProfessor();
        profesor.Id = lcn;
        Profesor? updatedProfessor = _profesorDao.UpdateProfessor(profesor);
        if (updatedProfessor == null)
        {
            System.Console.WriteLine("Professor not found");
            return;
        }

        System.Console.WriteLine("Professor updated");
    }

    private void AddProfessor()
    {
        Profesor profesor = InputProfessor();
        _profesorDao.AddProfessor(profesor);
        System.Console.WriteLine("Professor added");
    }

    private void ShowAndSortProfessors()
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

        PrintProfessors(_profesorDao.GetAllProfessors(page, pageSize, sortCriteria, sortDirection));
    }

    private void ShowMenu()
    {
        System.Console.WriteLine("\nChoose an option: ");
        System.Console.WriteLine("1: Show All professors");
        System.Console.WriteLine("2: Add professors");
        System.Console.WriteLine("3: Update professor");
        System.Console.WriteLine("4: Remove professor");
        System.Console.WriteLine("5: Show and sort professors");
        System.Console.WriteLine("0: Close");
    }




}
