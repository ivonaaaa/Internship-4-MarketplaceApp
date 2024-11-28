using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketplaceApp.Data;
using MarketplaceApp.Data.UserTypes;
using MarketplaceApp.Data.Enums;
using MarketplaceApp.Domain.Dtos;
using MarketplaceApp.Domain.Services;

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

        public UserDto Login(string email)
        {
            var user = users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            return UserMappingService.MapToUserDto(user);
        }

        private bool UserExists(string email)
        {
            return users.Any(users => users.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public List<Product> GetAvailableProducts()
        {
            return products.Where(p => p.Status == ProductStatus.ForSale).ToList();
        }

        public List<Product> GetAvailableProductsByCategory(ProductCategory category)
        {
            return products.Where(p => p.Category == category && p.Status == ProductStatus.ForSale).ToList();
        }

        public Product FindProductById(Guid productId)
        {
            var product = products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                Console.WriteLine("Proizvod nije pronađen.");
            }
            return product;
        }

        public void BuyProduct(Buyer buyer, Guid productId, string promoCode = null)
        {
            var product = FindProductById(productId);

            decimal finalPrice = product.Price;
            if (!string.IsNullOrEmpty(promoCode))
            {
                var discount = Service.ApplyPromoCode(promoCode, product.Category);
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
                return;
            }

            buyer.Balance -= finalPrice;
            product.Seller.TotalEarnings += finalPrice * 0.95m;
            product.Status = ProductStatus.Sold;
            buyer.PurchasedProducts.Add(product);
            transactions.Add(new Transaction(product.Id, buyer, product.Seller));

            Console.WriteLine($"Kupnja je uspješna!");
        }

        public void ReturnProduct(Buyer buyer, Guid productId)
        {
            var product = FindProductById(productId);
            var transaction = transactions.FirstOrDefault(t => t.ProductId == productId && t.buyerEmail == buyer.Email);
            if (transaction == null)
            {
                Console.WriteLine("Povrat nije moguć jer transakcija nije pronađena.");
                return;
            }
            decimal refundAmount = product.Price * 0.8m;
            buyer.Balance += refundAmount;
            product.Status = ProductStatus.ForSale;

            Console.WriteLine($"Povrat je uspješan!");
        }

        public void AddToFavourites(Buyer buyer, Guid productId)
        {
            var product = FindProductById(productId);

            if (!buyer.FavoriteProducts.Contains(product))
            {
                buyer.FavoriteProducts.Add(product);
                Console.WriteLine($"{product.Name} je dodan na vašu listu omiljenih proizvoda.");
            }
            else Console.WriteLine($"{product.Name} je već na vašoj listi omiljenih proizvoda.");
        }

        public void ViewFavoriteProducts(Buyer buyer)
        {
            if (buyer.FavoriteProducts.Count == 0)
            {
                Console.WriteLine("Nemate omiljenih proizvoda.");
                return;
            }

            Console.WriteLine("Vaša lista omiljenih proizvoda:");
            foreach (var product in buyer.FavoriteProducts)
                Console.WriteLine($"- {product.Name}, Cijena: {product.Price}");
        }

        public void ViewPurchaseHistory(Buyer buyer)
        {
            if (buyer.PurchasedProducts.Count == 0)
            {
                Console.WriteLine("Nemate kupljenih proizvoda.");
                return;
            }

            Console.WriteLine("Povijest kupljenih proizvoda:");
            foreach (var product in buyer.PurchasedProducts)
                Console.WriteLine($"- {product.Name}, Cijena: {product.Price}");
        }

        public void AddProduct(string name, string description, decimal price, Seller seller, ProductCategory category)
        {
            Product newProduct = new Product(name, description, price, seller, category);
            seller.ProductsForSale.Add(newProduct);
            Console.WriteLine($"Proizvod '{name}' uspješno dodan u ponudu.");
        }

        public void ViewProducts(Seller seller)
        {
            if (seller.ProductsForSale.Count == 0)
            {
                Console.WriteLine("Nemate proizvode u ponudi.");
                return;
            }

            foreach (var product in seller.ProductsForSale)
                Console.WriteLine($"Naziv: {product.Name}, Opis: {product.Description}, Cijena: {product.Price:C}, Status: {product.Status}");
        }

        public void ViewTotalEarnings(Seller seller)
        {
            Console.WriteLine($"Ukupna zarada od prodaje: {seller.TotalEarnings}");
        }

        public void ViewSoldProductsByCategory(Seller seller, ProductCategory category)
        {
            var soldProducts = seller.ProductsForSale
                .Where(p => p.Status == ProductStatus.Sold && p.Category == category)
                .ToList();

            if (soldProducts.Count == 0)
            {
                Console.WriteLine($"Nema prodanih proizvoda u kategoriji {category}.");
                return;
            }

            foreach (var product in soldProducts)
                Console.WriteLine($"Naziv: {product.Name}, Opis: {product.Description}, Cijena: {product.Price}");
        }

        public void ViewEarningsInTimePeriod(Seller seller, DateTime startDate, DateTime endDate)
        {
            decimal totalEarnings = 0;
            foreach (var product in seller.ProductsForSale.Where(p => p.Status == ProductStatus.Sold))
            {
                var transaction = transactions.FirstOrDefault(t => t.ProductId == product.Id && t.TransactionDate >= startDate && t.TransactionDate <= endDate);
                if (transaction != null)
                    totalEarnings += product.Price * 0.95m;
            }
            Console.WriteLine($"Zarada u razdoblju od {startDate.ToShortDateString()} do {endDate.ToShortDateString()}: {totalEarnings}");
        }
    }
}
