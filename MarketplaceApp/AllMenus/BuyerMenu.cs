using MarketplaceApp.Domain.Dtos;
using MarketplaceApp.Domain;
using System;

namespace MarketplaceApp.Presentation.AllMenus
{
    public class BuyerMenu
    {
        private readonly Marketplace _marketplace;
        private readonly BuyerDto _buyer;

        public BuyerMenu(Marketplace marketplace, BuyerDto buyer)
        {
            _marketplace = marketplace;
            _buyer = buyer;
        }

        public void ShowBuyerMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("\n--- MENU ZA KUPCE ---");
                Console.WriteLine("1 - Pregled svih proizvoda");
                Console.WriteLine("2 - Kupnja proizvoda");
                Console.WriteLine("3 - Povrat kupljenog proizvoda");
                Console.WriteLine("4 - Dodavanje proizvoda u listu omiljenih");
                Console.WriteLine("5 - Pregled povijesti kupljenih proizvoda");
                Console.WriteLine("6 - Pregled liste omiljenih proizvoda");
                Console.WriteLine("7 - Povratak na glavni meni");
                Console.Write("\nOdaberite opciju: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ShowAvailableProducts();
                        Console.ReadKey();
                        break;
                    case "2":
                        BuyProduct();
                        Console.ReadKey();
                        break;
                    case "3":
                        ReturnProduct();
                        Console.ReadKey();
                        break;
                    case "4":
                        AddToFavourites();
                        Console.ReadKey();
                        break;
                    case "5":
                        ViewPurchaseHistory();
                        Console.ReadKey();
                        break;
                    case "6":
                        ViewFavoriteProducts();
                        Console.ReadKey();
                        break;
                    case "7":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Nevažeća opcija. Pokušajte ponovo.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void ShowAvailableProducts()
        {
            var products = _marketplace.GetAvailableProducts();
            if (products.Count == 0)
            {
                Console.WriteLine("Nema dostupnih proizvoda.");
                return;
            }

            Console.WriteLine("\nDostupni proizvodi:");
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.Id}, Naziv: {product.Name}, Cijena: {product.Price}, Opis: {product.Description}");
            }
        }

        private void BuyProduct()
        {
            Console.Write("Unesite ID proizvoda za kupnju: ");
            if (Guid.TryParse(Console.ReadLine(), out var productId))
            {
                var result = _marketplace.BuyProduct(_buyer.Email, productId);
                Console.WriteLine(result ? "Kupnja uspješna!" : "Kupnja nije uspjela. Provjerite podatke.");
            }
            else
            {
                Console.WriteLine("Neispravan ID.");
            }
        }

        private void ReturnProduct()
        {
            Console.Write("Unesite ID proizvoda za povrat: ");
            if (Guid.TryParse(Console.ReadLine(), out var productId))
            {
                var result = _marketplace.ReturnProduct(_buyer, productId);
                Console.WriteLine(result ? "Povrat uspješan!" : "Povrat nije uspio. Provjerite podatke.");
            }
            else
            {
                Console.WriteLine("Neispravan ID.");
            }
        }

        private void AddToFavourites()
        {
            Console.Write("Unesite ID proizvoda za dodavanje u omiljene: ");
            if (Guid.TryParse(Console.ReadLine(), out var productId))
            {
                var result = _marketplace.AddToFavourites(_buyer, productId);
                Console.WriteLine(result ? "Proizvod dodan u omiljene!" : "Dodavanje u omiljene nije uspjelo.");
            }
            else
            {
                Console.WriteLine("Neispravan ID.");
            }
        }

        private void ViewPurchaseHistory()
        {
            var history = _marketplace.ViewPurchaseHistory(_buyer);
            if (history.Count == 0)
            {
                Console.WriteLine("Nemate povijest kupljenih proizvoda.");
                return;
            }

            Console.WriteLine("\nPovijest kupljenih proizvoda:");
            foreach (var product in history)
            {
                Console.WriteLine($"Naziv: {product.Name}, Cijena: {product.Price}, Opis: {product.Description}");
            }
        }

        private void ViewFavoriteProducts()
        {
            var favorites = _marketplace.ViewFavoriteProducts(_buyer);
            if (favorites.Count == 0)
            {
                Console.WriteLine("Nemate omiljenih proizvoda.");
                return;
            }

            Console.WriteLine("\nVaša lista omiljenih proizvoda:");
            foreach (var product in favorites)
            {
                Console.WriteLine($"Naziv: {product.Name}, Cijena: {product.Price}, Opis: {product.Description}");
            }
        }
    }
}
