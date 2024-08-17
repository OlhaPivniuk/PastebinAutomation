using System;
using NUnitLite;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Starting tests...");
        new AutoRun().Execute(args);
        Console.WriteLine("Tests completed.");
    }
}
