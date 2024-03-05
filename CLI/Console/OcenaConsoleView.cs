/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.DAO;
using StudentskaSluzba.Model;
using static StudentskaSluzba.Model.Ocena;

namespace StudentskaSluzba.Console;

 class OcenaConsoleView
{
    private readonly OcenaDAO _ocenaDao;

    public OcenaConsoleView(OcenaDAO ocenaDao)
    {
        _ocenaDao = ocenaDao;
    }

    private void PrintOcena(List<Ocena> ocena)
    {
        System.Console.WriteLine("Ocene: ");
        string header = $"Id {"",5} | Student koji je polozio {"",20} | Predmet {"",20} | Brojcana vrednost ocene {"",20} | Datum polaganja ispita {"",20} |";
        System.Console.WriteLine(header);
        foreach (Ocena o in ocena)
        {
            System.Console.WriteLine(o);
        }
    }


    private Ocena InputOcena()
    {
        System.Console.WriteLine("Unesite studenta koji je polozio: ");
        string skjp = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Unesite naziv predmeta: ");
        string naziv = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Unesite brojcanu vednost ocene: ");
        int brvroc = ConsoleViewUtils.SafeInputInt();

        System.Console.WriteLine("Unesite datum polaganja predmeta: ");
        string datpol = System.Console.ReadLine() ?? string.Empty;



        return new Ocena(skjp, naziv, brvroc, datpol);
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
                ShowAllOcena();
                break;
            case "2":
                AddOcena();
                break;
            case "3":
                UpdateOcena();
                break;
            case "4":
                RemoveOcena();
                break;
            case "5":
                ShowAndSortOcena();
                break;
        }
    }

    private void ShowAllOcena()
    {
        PrintOcena(_ocenaDao.GetAllOcena());
    }
    
    private void RemoveOcena()
    {
        int brvr = InputId();
        Ocena? removedOcena = _ocenaDao.RemoveOcena(brvr);
        if (removedOcena is null)
        {
            System.Console.WriteLine("Ocena not found");
            return;
        }

        System.Console.WriteLine("Ocena removed");
    }

    private void UpdateOcena()
    {
        int brvr = InputId();
        Ocena ocena = InputOcena();
        ocena.Id = brvr;
        Ocena? updatedOcena = _ocenaDao.UpdateOcena(ocena);
        if (updatedOcena == null)
        {
            System.Console.WriteLine("Ocena not found");
            return;
        }

        System.Console.WriteLine("Ocena updated");
    }

    private void AddOcena()
    {
        Ocena ocena = InputOcena();
        _ocenaDao.AddOcena(ocena);
        System.Console.WriteLine("Ocena added");
    }

    private void ShowAndSortOcena()
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

        PrintOcena(_ocenaDao.GetAllOcena(page, pageSize, sortCriteria, sortDirection));
    }

    private void ShowMenu()
    {
        System.Console.WriteLine("\nChoose an option: ");
        System.Console.WriteLine("1: Show All ocene");
        System.Console.WriteLine("2: Add ocena");
        System.Console.WriteLine("3: Update ocena");
        System.Console.WriteLine("4: Remove ocena");
        System.Console.WriteLine("5: Show and sort ocena");
        System.Console.WriteLine("0: Close");
    }




}
*/