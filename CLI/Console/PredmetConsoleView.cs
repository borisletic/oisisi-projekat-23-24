/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentskaSluzba.DAO;
using StudentskaSluzba.Model;
using static StudentskaSluzba.Model.Predmet;
using static StudentskaSluzba.Model.Student;


namespace StudentskaSluzba.Console;

 class PredmetConsoleView
{
    private readonly PredmetDAO _predmetDao;

    public PredmetConsoleView(PredmetDAO predmetDao)
    {
        _predmetDao = predmetDao;
    }

    private void PrintPredmeti(List<Predmet> predmet)
    {
        System.Console.WriteLine("Predmeti: ");
        string header = $"Id {"",5} | Sifra predmeta {"",20} | Naziv predmeta {"",20} | Semestar {"",20} | Godina predmeta {"",20} | Predmetni profesor {"",20} | ESPB {"",20} | Studenti polozeni {"",50} | Studenti nepolozeni {"",50} |";
        System.Console.WriteLine(header);
        foreach (Predmet p in predmet)
        {
            System.Console.WriteLine(p);
        }
    }


    private Predmet InputPredmet()
    {
        
        System.Console.WriteLine("Unesite sifru predmeta: ");
        string sifra = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Unesite naziv predmeta: ");
        string naziv = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Unesite semestar predmeta: ");
        string semestarpredmeta = System.Console.ReadLine() ?? string.Empty;

        Semestar semestar;

        
        if (Enum.TryParse(semestarpredmeta, true, out semestar))
        {
            
            System.Console.WriteLine($"Uneli ste: {semestar}");
        }
        else
        {
            
            System.Console.WriteLine("Uneli ste neispravnu vrstu finansiranja.");
        }

        System.Console.WriteLine("Unesite ESPB predmeta: ");
        int godina = ConsoleViewUtils.SafeInputInt();

        System.Console.WriteLine("Unesite predmetnog profesora predmeta: ");
        string predprof = System.Console.ReadLine() ?? string.Empty;

        System.Console.WriteLine("Unesite ESPB predmeta: ");
        int espb = ConsoleViewUtils.SafeInputInt();

        System.Console.WriteLine("Unesite studente koji su polozili predmet: ");
        string inputp = System.Console.ReadLine() ?? string.Empty;
        List<string> stpolo = inputp.Split(' ').ToList();

        System.Console.WriteLine("Unesite studente koji nisu polozili predmet: ");
        string inputnp = System.Console.ReadLine() ?? string.Empty;
        List<string> stnepolo = inputnp.Split(' ').ToList();


        return new Predmet(sifra, naziv, semestar, godina, predprof, espb, stpolo, stnepolo);
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
                ShowAllPredmeti();
                break;
            case "2":
                AddPredmet();
                break;
            case "3":
                UpdatePredmet();
                break;
            case "4":
                RemovePredmet();
                break;
            case "5":
                ShowAndSortPredmet();
                break;
        }
    }

    private void ShowAllPredmeti()
    {
        PrintPredmeti(_predmetDao.GetAllPredmeti());
    }

    private void RemovePredmet()
    {
        int sifra = InputId();
        Predmet? removedPredmet = _predmetDao.RemovePredmet(sifra);
        if (removedPredmet is null)
        {
            System.Console.WriteLine("Predmet not found");
            return;
        }

        System.Console.WriteLine("Predmet removed");
    }

    private void UpdatePredmet()
    {
        int sifra = InputId();
        Predmet predmet = InputPredmet();
        predmet.Id = sifra;
        Predmet? updatedPredmet = _predmetDao.UpdatePredmet(predmet);
        if (updatedPredmet == null)
        {
            System.Console.WriteLine("Predmet not found");
            return;
        }

        System.Console.WriteLine("Predmet updated");
    }

    private void AddPredmet()
    {
        Predmet predmet = InputPredmet();
        _predmetDao.AddPredmet(predmet);
        System.Console.WriteLine("Predmet added");
    }

    private void ShowAndSortPredmet()
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

        PrintPredmeti(_predmetDao.GetAllPredmeti(page, pageSize, sortCriteria, sortDirection));
    }

    private void ShowMenu()
    {
        System.Console.WriteLine("\nChoose an option: ");
        System.Console.WriteLine("1: Show All predmeti");
        System.Console.WriteLine("2: Add predmet");
        System.Console.WriteLine("3: Update predmet");
        System.Console.WriteLine("4: Remove predmet");
        System.Console.WriteLine("5: Show and sort predmet");
        System.Console.WriteLine("0: Close");
    }

}
*/