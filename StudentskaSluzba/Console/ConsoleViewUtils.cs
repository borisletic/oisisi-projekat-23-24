using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentskaSluzba.Console;

static class ConsoleViewUtils
{
    public static int SafeInputInt()
    {
        int input;

        string rawInput = System.Console.ReadLine() ?? string.Empty;

        while (!int.TryParse(rawInput, out input))
        {
            System.Console.WriteLine("Not a valid number, try again: ");

            rawInput = System.Console.ReadLine() ?? string.Empty;
        }

        return input;
    }
}
