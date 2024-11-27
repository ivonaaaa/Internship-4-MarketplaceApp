using Microsoft.Win32;
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
        //funcionalnost, funkcije

        private List<Users> users = new List<Users>();
        private List<Product> products = new List<Product>();
        private List<Transaction> transactions = new List<Transaction>();

        public Marketplace() { }

        public void RegisterBuyer(string name, string email, decimal balance)
        {
            if (ValidationService.Validation.IsValidEmail(email) && !UserExists(email))
            {
                var buyer = new Buyer(name, email, balance);
                users.Add(buyer);
                Console.WriteLine("Kupac registriran uspješno!");
            }
            else Console.WriteLine("Krivi email ili kupac s tim emailom već postoji!");
        }

        public void RegisterSeller(string name, string email)
        {
            if (ValidationService.Validation.IsValidEmail(email) && !UserExists(email))
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

        public bool BuyProduct(Buyer buyer, Guid productId, string promoCode = null)
        {
            var product = products.FirstOrDefault(p => p.Id == productId);
            if (product == null || product.Status != ProductStatus.ForSale)
            {
                Console.WriteLine("Proizvod nije dostupan.");
                return false;
            }

            decimal finalPrice = product.Price;
            if (!string.IsNullOrEmpty(promoCode))
            {
                var discount = ValidationService.ApplyPromoCode(promoCode, product.Category);
                if (discount > 0)
                {
                    finalPrice -= finalPrice * discount;
                    Console.WriteLine($"Promo kod primijenjen! Nova cijena: {finalPrice:C}");
                }
                else Console.WriteLine("Nevažeć promo kod.");
            }

            if (buyer.Balance < finalPrice)
            {
                Console.WriteLine("Nemate dovoljno sredstava.");
                return false;
            }

            buyer.Balance -= finalPrice;
            product.Seller.TotalEarnings += finalPrice * 0.95m;
            product.Status = ProductStatus.Sold;
            transactions.Add(new Transaction(product.Id, buyer.Email));

            Console.WriteLine($"Kupnja proizvoda "{product.Name}" je uspješna!");
            return true;
        }

        public bool ReturnProduct(Buyer buyer, Guid productId)
        {
            var transaction = transactions.FirstOrDefault(t => t.ProductId == productId && t.BuyerEmail == buyer.Email);
            if (transaction == null)
            {
                Console.WriteLine("Povrat nije moguć jer transakcija nije pronađena.");
                return false;
            }
            var product = products.FirstOrDefault(p => p.Id == productId);
            decimal refundAmount = product.Price * 0.8m;
            buyer.Balance += refundAmount;
            product.Status = ProductStatus.ForSale;

            Console.WriteLine($"Povrat proizvoda "{product.Name}" je uspješan!");
            return true;
        }




    }
}
