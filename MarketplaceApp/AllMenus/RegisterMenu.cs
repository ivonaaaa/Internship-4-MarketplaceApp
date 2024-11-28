using MarketplaceApp.Domain;
using MarketplaceApp.Helpers;
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
            string choice = Helper.GetValidInputFromUser("Želite li se registrirati kao kupac ili prodavač?", new[] { "1", "2" });

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
            string name = Console.ReadLine();
            string email = Helper.GetValidEmailInput("Unesite e-mail: ");
            decimal balance = Helper.GetValidDecimalInput("Unesite početni balans: ");
            _marketplace.RegisterBuyer(name, email, balance);
        }

        private void RegisterSeller()
        {
            string name = Console.ReadLine();
            string email = Helper.GetValidEmailInput("Unesite e-mail: ");
            _marketplace.RegisterSeller(name, email);
        }
    }
}
