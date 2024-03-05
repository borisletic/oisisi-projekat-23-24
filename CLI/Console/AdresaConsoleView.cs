/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.DAO;
using StudentskaSluzba.Model;
using static StudentskaSluzba.Model.Adresa;

namespace StudentskaSluzba.Console;

 class AdresaConsoleView
{
    private readonly AdresaDAO _adresaDao;

    public AdresaConsoleView(AdresaDAO adresaDao)
    {
        _adresaDao = adresaDao;
    }

    private void PrintAdresa(List<Adresa> adresa)
    {
        System.Console.WriteLine("Adresa: ");
        string header = $"Id {"",5} | Ulica {"",20} | Broj {"",20} | Grad {"",20} | Drzava {"",20} |";
        System.Console.WriteLine(header);
        foreach (Adresa a in adresa)
        {
            System.Console.WriteLine(a);
        }
    }


    private Adresa InputAdresa()
    {
        System.Console.WriteLine("Unesite ulicu: ");
        string ulica = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Unesite broj: ");
        string broj = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Unesite grad: ");
        string grad = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Unesite drzavu: ");
        string drzava = System.Console.ReadLine() ?? string.Empty;

        return new Adresa(ulica, broj, grad, drzava);
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
                ShowAllAdresa();
                break;
            case "2":
                AddAdresa();
                break;
            case "3":
                UpdateAdresa();
                break;
            case "4":
                RemoveAdresa();
                break;
            case "5":
                ShowAndSortAdresa();
                break;
        }
    }

    private void ShowAllAdresa()
    {
        PrintAdresa(_adresaDao.GetAllAdresa());
    }

    private void RemoveAdresa()
    {
        int br = InputId();
        Adresa? removedAdresa = _adresaDao.RemoveAdresa(br);
        if (removedAdresa is null)
        {
            System.Console.WriteLine("Adresa not found");
            return;
        }

        System.Console.WriteLine("Adresa removed");
    }

    private void UpdateAdresa()
    {
        int br = InputId();
        Adresa adresa = InputAdresa();
        adresa.Id = br;
        Adresa? updatedAdresa = _adresaDao.UpdateAdresa(adresa);
        if (updatedAdresa == null)
        {
            System.Console.WriteLine("Adresa not found");
            return;
        }

        System.Console.WriteLine("Adresa updated");
    }

    private void AddAdresa()
    {
        Adresa adresa = InputAdresa();
        _adresaDao.AddAdresa(adresa);
        System.Console.WriteLine("Adresa added");
    }

    private void ShowAndSortAdresa()
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

        PrintAdresa(_adresaDao.GetAllAdresa(page, pageSize, sortCriteria, sortDirection));
    }

    private void ShowMenu()
    {
        System.Console.WriteLine("\nChoose an option: ");
        System.Console.WriteLine("1: Show All Adresa");
        System.Console.WriteLine("2: Add Adresa");
        System.Console.WriteLine("3: Update Adresa");
        System.Console.WriteLine("4: Remove Adresa");
        System.Console.WriteLine("5: Show and sort Adresa");
        System.Console.WriteLine("0: Close");
    }



}
*/