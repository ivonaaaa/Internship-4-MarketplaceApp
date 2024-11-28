using MarketplaceApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceApp.Presentation.AllMenus
{
    public class LoginMenu
    {
        private readonly Marketplace _marketplace;
        public LoginMenu(Marketplace marketplace)
        {
            _marketplace = marketplace;
        }

        public void ShowLoginMenu()
        {
            Console.Write("Unesite e-mail: ");
            var email = Console.ReadLine();

            var user = _marketplace.Login(email);

            if (user != null)
                Console.WriteLine($"Dobrodošli, {user.Name}!");
            else Console.WriteLine("Neispravan e-mail. Pokušajte ponovo.");
        }
    }
}
