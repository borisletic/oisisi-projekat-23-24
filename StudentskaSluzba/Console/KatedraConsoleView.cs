using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.DAO;
using StudentskaSluzba.Model;
using static StudentskaSluzba.Model.Katedra;

namespace StudentskaSluzba.Console;

 class KatedraConsoleView
{
    private readonly KatedraDAO _katedraDao;

    public KatedraConsoleView(KatedraDAO katedraDao)
    {
        _katedraDao = katedraDao;
    }

    private void PrintKatedra(List<Katedra> katedra)
    {
        System.Console.WriteLine("Katedra: ");
        string header = $"Id {"",5} | Sifra katedre {"",20} | Naziv katedre {"",20} | Sef katedre {"",20} | Spisak profesora na katedri {"",50} |";
        System.Console.WriteLine(header);
        foreach (Katedra k in katedra)
        {
            System.Console.WriteLine(k);
        }
    }


    private Katedra InputKatedra()
    {
        System.Console.WriteLine("Unesite sifru katedre: ");
        int sifra = ConsoleViewUtils.SafeInputInt();

        System.Console.WriteLine("Unesite naziv katedre: ");
        string naziv = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Unesite sefa katedre: ");
        string sef = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Unesite spisak profesora na katedri: ");
        string sppnka = System.Console.ReadLine() ?? string.Empty;
        List<string> sppnk = sppnka.Split(' ').ToList();



        return new Katedra(sifra, naziv, sef, sppnk);
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
                ShowAllKatedra();
                break;
            case "2":
                AddKatedra();
                break;
            case "3":
                UpdateKatedra();
                break;
            case "4":
                RemoveKatedra();
                break;
            case "5":
                ShowAndSortKatedra();
                break;
        }
    }

    private void ShowAllKatedra()
    {
        PrintKatedra(_katedraDao.GetAllKatedra());
    }

    private void RemoveKatedra()
    {
        int sifra = InputId();
        Katedra? removedKatedra = _katedraDao.RemoveKatedra(sifra);
        if (removedKatedra is null)
        {
            System.Console.WriteLine("Katedra not found");
            return;
        }

        System.Console.WriteLine("Katedra removed");
    }

    private void UpdateKatedra()
    {
        int sifra = InputId();
        Katedra katedra = InputKatedra();
        katedra.Id = sifra;
        Katedra? updatedKatedra = _katedraDao.UpdateKatedra(katedra);
        if (updatedKatedra == null)
        {
            System.Console.WriteLine("Katedra not found");
            return;
        }

        System.Console.WriteLine("Katedra updated");
    }

    private void AddKatedra()
    {
        Katedra katedra = InputKatedra();
        _katedraDao.AddKatedra(katedra);
        System.Console.WriteLine("Katedra added");
    }

    private void ShowAndSortKatedra()
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

        PrintKatedra(_katedraDao.GetAllKatedra(page, pageSize, sortCriteria, sortDirection));
    }

    private void ShowMenu()
    {
        System.Console.WriteLine("\nChoose an option: ");
        System.Console.WriteLine("1: Show All katedre");
        System.Console.WriteLine("2: Add katedra");
        System.Console.WriteLine("3: Update katedra");
        System.Console.WriteLine("4: Remove katedra");
        System.Console.WriteLine("5: Show and sort katedra");
        System.Console.WriteLine("0: Close");
    }

}
