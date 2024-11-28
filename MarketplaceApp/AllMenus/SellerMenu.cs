using MarketplaceApp.Domain;
using MarketplaceApp.Domain.Dtos;
using System;
using System.Collections.Generic;
using MarketplaceApp.Helpers;

namespace MarketplaceApp.Presentation.AllMenus
{
    public class SellerMenu
    {
        private readonly Marketplace _marketplace;
        private readonly SellerDto _seller;

        public SellerMenu(Marketplace marketplace, SellerDto seller)
        {
            _marketplace = marketplace;
            _seller = seller;
        }

        public void ShowSellerMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n--- SELLER MENU ---");
                Console.WriteLine("1 - Dodaj proizvod");
                Console.WriteLine("2 - Pregledaj svoje proizvode");
                Console.WriteLine("3 - Pregledaj ukupnu zaradu");
                Console.WriteLine("4 - Pregledaj prodane proizvode po kategoriji");
                Console.WriteLine("5 - Pregledaj zaradu u određenom vremenskom razdoblju");
                Console.WriteLine("6 - Povratak");
                Console.Write("\nOdaberite opciju: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddProduct();
                        break;
                    case "2":
                        ViewYourProducts();
                        break;
                    case "3":
                        ViewTotalEarnings();
                        break;
                    case "4":
                        ViewSoldProductsByCategory();
                        break;
                    case "5":
                        ViewEarningsForTimePeriod();
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Nevažeća opcija. Pokušajte ponovo.");
                        break;
                }
            }
        }

        private void AddProduct()
        {
            Console.WriteLine("Unesite naziv proizvoda:");
            string name = Console.ReadLine();
            Console.WriteLine("Unesite opis proizvoda:");
            string description = Console.ReadLine();
            Console.WriteLine("Unesite cijenu proizvoda:");
            decimal price = decimal.Parse(Console.ReadLine());

            string category = Helper.SelectCategory();
            if (category != null)
            {
                _marketplace.AddProduct(name, description, price, _seller, category);
                Console.WriteLine($"Proizvod '{name}' uspješno dodan!");
            }
            else Console.WriteLine("Nevažeći odabir kategorije. Pokušajte ponovo.");
        }


        private void ViewYourProducts()
        {
            Console.WriteLine("Pregled proizvoda...");
            _marketplace.ViewProducts(_seller);
        }

        private void ViewTotalEarnings()
        {
            Console.WriteLine("Pregled ukupne zarade...");
            _marketplace.ViewTotalEarnings(_seller);
        }

        private void ViewSoldProductsByCategory()
        {
            string category = Helper.SelectCategory();
            if (category != null)
            {
                Console.WriteLine($"Pregled prodanih proizvoda u kategoriji: {category}...");
            }
        }

        private void ViewEarningsForTimePeriod()
        {
            Console.WriteLine("Pregled zarade u vremenskom razdoblju...");
            Console.WriteLine("Unesite datum početka (yyyy-MM-dd):");
            DateTime startDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Unesite datum završetka (yyyy-MM-dd):");
            DateTime endDate = DateTime.Parse(Console.ReadLine());

            _marketplace.ViewEarningsInTimePeriod(_seller, startDate, endDate);
        }
    }
}
