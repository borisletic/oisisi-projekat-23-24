/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.DAO;
using StudentskaSluzba.Model;
using static StudentskaSluzba.Model.Indeks;

namespace StudentskaSluzba.Console;

 class IndeksConsoleView
{
    private readonly IndeksDAO _indeksDao;

    public IndeksConsoleView(IndeksDAO indeksDao)
    {
        _indeksDao = indeksDao;
    }

    private void PrintIndeks(List<Indeks> indeks)
    {
        System.Console.WriteLine("Indeks: ");
        string header = $"Id {"",5} | Oznaka upisa {"",20} | Broj upisa {"",20} | Broj godine {"",20} |";
        System.Console.WriteLine(header);
        foreach (Indeks i in indeks)
        {
            System.Console.WriteLine(i);
        }
    }


    private Indeks InputIndeks()
    {
        System.Console.WriteLine("Unesite oznaku upisa: ");
        string oznakaupisa = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Unesite broj upisa: ");
        int brojupisa = ConsoleViewUtils.SafeInputInt();

        System.Console.WriteLine("Unesite godinu upisa: ");
        int godinaupisa = ConsoleViewUtils.SafeInputInt();

        return new Indeks(oznakaupisa, brojupisa, godinaupisa);
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
                ShowAllIndeks();
                break;
            case "2":
                AddIndeks();
                break;
            case "3":
                UpdateIndeks();
                break;
            case "4":
                RemoveIndeks();
                break;
            case "5":
                ShowAndSortIndeks();
                break;
        }
    }

    private void ShowAllIndeks()
    {
        PrintIndeks(_indeksDao.GetAllIndeks());
    }

    private void RemoveIndeks()
    {
        int brup = InputId();
        Indeks? removedIndeks = _indeksDao.RemoveIndeks(brup);
        if (removedIndeks is null)
        {
            System.Console.WriteLine("Indeks not found");
            return;
        }

        System.Console.WriteLine("Indeks removed");
    }

    private void UpdateIndeks()
    {
        int brup = InputId();
        Indeks indeks = InputIndeks();
        indeks.Id = brup;
        Indeks? updatedIndeks = _indeksDao.UpdateIndeks(indeks);
        if (updatedIndeks == null)
        {
            System.Console.WriteLine("Indeks not found");
            return;
        }

        System.Console.WriteLine("Indeks updated");
    }

    private void AddIndeks()
    {
        Indeks indeks = InputIndeks();
        _indeksDao.AddIndeks(indeks);
        System.Console.WriteLine("Indeks added");
    }

    private void ShowAndSortIndeks()
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

        PrintIndeks(_indeksDao.GetAllIndeks(page, pageSize, sortCriteria, sortDirection));
    }

    private void ShowMenu()
    {
        System.Console.WriteLine("\nChoose an option: ");
        System.Console.WriteLine("1: Show All Indeks");
        System.Console.WriteLine("2: Add Indeks");
        System.Console.WriteLine("3: Update Indeks");
        System.Console.WriteLine("4: Remove Indeks");
        System.Console.WriteLine("5: Show and sort Indeks");
        System.Console.WriteLine("0: Close");
    }



}
*/