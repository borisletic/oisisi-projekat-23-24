//using StudentskaSluzba.Console;
using CLI.DAO;
namespace StudentskaSluzba;

class Program
{
    static void Main()
    {
        //StudentDAO students = new StudentDAO();
        //StudentConsoleView student_view = new StudentConsoleView(students);
        //student_view.RunMenu();


        ProfesorDAO professors = new ProfesorDAO();
        //ProfesorConsoleView professor_view = new ProfesorConsoleView(professors);
        //professor_view.RunMenu();

        //PredmetDAO predmeti = new PredmetDAO();
        //PredmetConsoleView predmet_view = new PredmetConsoleView(predmeti);
        //predmet_view.RunMenu();

        //OcenaDAO ocena = new OcenaDAO();
        //OcenaConsoleView ocena_view = new OcenaConsoleView(ocena);
        //ocena_view.RunMenu();

        //KatedraDAO katedra = new KatedraDAO();
        //KatedraConsoleView katedra_view = new KatedraConsoleView(katedra);
        //katedra_view.RunMenu();

        IndeksDAO indeks = new IndeksDAO();
        //IndeksConsoleView indeks_view = new IndeksConsoleView(indeks);
        //indeks_view.RunMenu();

        AdresaDAO adresa = new AdresaDAO();
        //AdresaConsoleView adresa_view = new AdresaConsoleView(adresa);
        //adresa_view.RunMenu();
    }
}