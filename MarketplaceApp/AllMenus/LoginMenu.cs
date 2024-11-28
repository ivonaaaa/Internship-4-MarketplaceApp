using MarketplaceApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketplaceApp.Domain.Dtos;
using MarketplaceApp.Helpers;

namespace MarketplaceApp.Presentation.AllMenus
{
    public class LoginMenu
    {
        private readonly Marketplace _marketplace;
        public LoginMenu(Marketplace marketplace)
        {
            _marketplace = marketplace;
        }

        public UserDto ShowLoginMenu()
        {
            string email = Helper.GetValidEmailInput("Unesite e-mail: ");
            var userDto = _marketplace.Login(email);

            if (userDto != null)
                return (UserDto)userDto;
            else
            {
                Console.WriteLine("Neispravan e-mail.");
                return null;
            }
        }
    }
}
