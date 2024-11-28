using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceApp.Presentation
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- MARKETPLACE APP ---");
            Console.WriteLine("1 - Registracija");
            Console.WriteLine("2 - Prijava");
            Console.WriteLine("3 - Izlazak");
            Console.Write("\nOdaberite opciju: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    //RegisterMenu.ShowRegisterMenu();
                    break;
                case "2":
                    //LoginMenu.ShowLoginMenu();
                    break;
                case "3":
                    Console.WriteLine("Izlazim iz aplikacije...");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Nevažeća opcija. Pokušajte ponovo.");
                    //Main(); ili while radije
                    break;
            }
        }
    }
}
