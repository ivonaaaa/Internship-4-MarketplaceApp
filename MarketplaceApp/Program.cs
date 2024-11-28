using MarketplaceApp.Presentation.AllMenus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketplaceApp.Domain;
using MarketplaceApp.Domain.Dtos;

namespace MarketplaceApp.Presentation
{
    internal class Program
    {
        private static Marketplace marketplace = new Marketplace();

        static void Main(string[] args)
        {
            Console.WriteLine("--- MARKETPLACE APP ---");
            Console.WriteLine("1 - Registracija");
            Console.WriteLine("2 - Prijava");
            Console.WriteLine("3 - Izlazak");
            Console.Write("\nOdaberite opciju: ");

            bool exit = false;
            while (!exit)
            {
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        var registerMenu = new RegisterMenu(marketplace);
                        registerMenu.ShowRegisterMenu();
                        break;
                    case "2":
                        var loginMenu = new LoginMenu(marketplace);
                        var loggedInUser = loginMenu.ShowLoginMenu();

                        if (loggedInUser != null)
                        {
                            if (loggedInUser is BuyerDto buyer)
                            {
                                var buyerMenu = new BuyerMenu(marketplace, buyer);
                                buyerMenu.ShowBuyerMenu();
                            }
                            else if (loggedInUser is SellerDto seller)
                            {
                                var sellerMenu = new SellerMenu(marketplace, seller);
                                sellerMenu.ShowSellerMenu();
                            }
                        }
                        break;
                    case "3":
                        Console.WriteLine("\nHvala što ste koristili Marketplace aplikaciju!");
                        Console.WriteLine("Izlazim iz aplikacije...");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Nevažeća opcija. Pokušajte ponovo.\n");
                        break;
                }
            }
        }
    }
}
