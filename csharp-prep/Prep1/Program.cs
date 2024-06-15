using System;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("What is your first name? ");
        string first = Console.ReadLine();

        Console.Write("What is you Last name? ");
        string last = Console.ReadLine();

        Console.WriteLine($"Your Name is {last}, {first} {last}");


    }
}