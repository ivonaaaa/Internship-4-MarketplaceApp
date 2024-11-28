using MarketplaceApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceApp.Presentation.AllMenus
{
    public class RegisterMenu
    {
        private readonly Marketplace _marketplace;
        public RegisterMenu(Marketplace marketplace)
        {
            _marketplace = marketplace;
        }

        public void ShowRegisterMenu()
        {
            Console.WriteLine("Želite li se registrirati kao kupac ili prodavač?");
            Console.WriteLine("1 - Kupac");
            Console.WriteLine("2 - Prodavač");
            Console.Write("\nOdaberite opciju: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    RegisterBuyer();
                    break;
                case "2":
                    RegisterSeller();
                    break;
                default:
                    Console.WriteLine("Nevažeća opcija. Pokušajte ponovo.");
                    ShowRegisterMenu();
                    break;
            }
        }

        private void RegisterBuyer()
        {
            Console.Write("Unesite ime: ");
            var name = Console.ReadLine();
            Console.Write("Unesite e-mail: ");
            var email = Console.ReadLine();
            Console.Write("Unesite početni balans: ");
            var balance = Convert.ToDecimal(Console.ReadLine());
            _marketplace.RegisterBuyer(name, email, balance);
        }

        private void RegisterSeller()
        {
            Console.Write("Unesite ime: ");
            var name = Console.ReadLine();
            Console.Write("Unesite e-mail: ");
            var email = Console.ReadLine();
            _marketplace.RegisterSeller(name, email);
        }
    }
}
