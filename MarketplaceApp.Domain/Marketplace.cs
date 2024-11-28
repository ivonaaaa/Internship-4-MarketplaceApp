using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketplaceApp.Data;
using MarketplaceApp.Data.UserTypes;
using MarketplaceApp.Data.Enums;

namespace MarketplaceApp.Domain
{
    public class Marketplace
    {
        public List<Users> users = new List<Users>();
        private List<Product> products = new List<Product>();
        private List<Transaction> transactions = new List<Transaction>();

        public Marketplace() { }

        public void RegisterBuyer(string name, string email, decimal balance)
        {
            if (Service.IsValidEmail(email) && !UserExists(email))
            {
                var buyer = new Buyer(name, email, balance);
                users.Add(buyer);
                Console.WriteLine("Kupac registriran uspješno!");
            }
            else Console.WriteLine("Krivi email ili kupac s tim emailom već postoji!");
        }

        public void RegisterSeller(string name, string email)
        {
            if (Service.IsValidEmail(email) && !UserExists(email))
            {
                var seller = new Seller(name, email);
                users.Add(seller);
                Console.WriteLine("Prodavač registriran uspješno!");
            }
            else Console.WriteLine("Krivi email ili prodavač s tim emailom već postoji!");
        }

        private bool UserExists(string email)
        {
            return users.Any(users => users.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public Users Login(string email)
        {
            var user = users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                Console.WriteLine("Korisnik nije pronađen.");
                return null;
            }
            Console.WriteLine($"Dobrodošli, {user.Name}");
            return user;
        }

        public List<Product> GetAvailableProducts()
        {
            return products.Where(p => p.Status == ProductStatus.ForSale).ToList();
        }

        public List<Product> GetAvailableProductsByCategory(ProductCategory category)
        {
            return products.Where(p => p.Category == category && p.Status == ProductStatus.ForSale).ToList();
        }
    }
}
