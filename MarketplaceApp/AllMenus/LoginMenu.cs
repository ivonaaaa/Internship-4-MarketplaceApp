using MarketplaceApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketplaceApp.Domain.Dtos;

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

            var userDto = _marketplace.Login(email);

            if (userDto != null)
            {
                Console.WriteLine($"Dobrodošli, {userDto.Name}!");
            }
            else
            {
                Console.WriteLine("Neispravan e-mail. Pokušajte ponovo.");
            }
        }
    }
}
