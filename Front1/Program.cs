using RRP3.Models;
using RRP3.Services;

namespace Front1;

internal class Program
{
    static void Main(string[] args)
    {
        RRP3service.ConsoleRun();
        while (true)
        {
            Console.ReadLine();
        }
    }
}