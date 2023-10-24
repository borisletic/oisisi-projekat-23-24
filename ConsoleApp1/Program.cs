// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

string s = Console.ReadLine();
int n = Convert.ToInt32(s);


bool hasThree = false;

while (n > 0)
{
    int t = n % 10;
    if (t == 3)
    {
        Console.WriteLine("Number contains 3");
        break;
    }
    n = n / 10;
}

if(!hasThree)
{
    Console.WriteLine("Number NOT contains 3");

}