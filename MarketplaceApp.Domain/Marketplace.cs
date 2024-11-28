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

        public List<ProductDto> GetAvailableProducts()
        {
            return products
                .Where(p => p.Status == ProductStatus.ForSale)
                .Select(p => UserMappingService.MapToProductDto(p))
                .ToList();
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

        public bool BuyProduct(string buyerEmail, Guid productId)
        {
            var buyer = users.OfType<Buyer>().FirstOrDefault(b => b.Email.Equals(buyerEmail, StringComparison.OrdinalIgnoreCase));
            var product = products.FirstOrDefault(p => p.Id == productId);

            if (buyer == null || product == null || product.Status != ProductStatus.ForSale)
                return false;

            if (buyer.Balance < product.Price)
                return false;

            buyer.Balance -= product.Price;
            product.Status = ProductStatus.Sold;
            buyer.PurchasedProducts.Add(product);

            return true;
        }

        public bool ReturnProduct(BuyerDto buyerDto, Guid productId)
        {
            var buyer = users.OfType<Buyer>().FirstOrDefault(b => b.Email.Equals(buyerDto.Email, StringComparison.OrdinalIgnoreCase));
            var product = products.FirstOrDefault(p => p.Id == productId);

            if (buyer == null || product == null || !buyer.PurchasedProducts.Contains(product))
            {
                Console.WriteLine("Povrat nije moguć jer proizvod nije pronađen ili nije kupljen.");
                return false;
            }

            decimal refundAmount = product.Price * 0.8m;
            buyer.Balance += refundAmount;
            product.Status = ProductStatus.ForSale;
            buyer.PurchasedProducts.Remove(product);

            Console.WriteLine($"Povrat proizvoda '{product.Name}' uspješno obavljen. Vraćeno: {refundAmount:C}.");
            return true;
        }


        public bool AddToFavourites(BuyerDto buyerDto, Guid productId)
        {
            var buyer = users.OfType<Buyer>().FirstOrDefault(b => b.Email.Equals(buyerDto.Email, StringComparison.OrdinalIgnoreCase));
            var product = products.FirstOrDefault(p => p.Id == productId);

            if (buyer == null || product == null)
                return false;

            if (!buyer.FavoriteProducts.Contains(product))
            {
                buyer.FavoriteProducts.Add(product);
                return true;
            }

            return false;
        }

        public List<ProductDto> ViewFavoriteProducts(BuyerDto buyerDto)
        {
            var buyer = users.OfType<Buyer>().FirstOrDefault(b => b.Email.Equals(buyerDto.Email, StringComparison.OrdinalIgnoreCase));
            if (buyer == null || buyer.FavoriteProducts.Count == 0)
            {
                Console.WriteLine("Nemate omiljenih proizvoda.");
                return new List<ProductDto>();
            }
            Console.WriteLine("Vaša lista omiljenih proizvoda:");
            var favoriteProducts = buyer.FavoriteProducts.Select(UserMappingService.MapToProductDto).ToList();

            foreach (var product in favoriteProducts)
                Console.WriteLine($"- {product.Name}, Cijena: {product.Price:C}, Opis: {product.Description}");
            return favoriteProducts;
        }


        public List<ProductDto> ViewPurchaseHistory(BuyerDto buyerDto)
        {
            var buyer = users.OfType<Buyer>().FirstOrDefault(b => b.Email.Equals(buyerDto.Email, StringComparison.OrdinalIgnoreCase));
            if (buyer == null)
                return new List<ProductDto>();

            return buyer.PurchasedProducts.Select(UserMappingService.MapToProductDto).ToList();
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
