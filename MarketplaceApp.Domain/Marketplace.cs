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
            if (Services.Service.IsValidEmail(email) && !UserExists(email))
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

        public object Login(string email)
        {
            var user = users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            if (user == null)
                return null;
            if (user is Buyer buyer)
                return MappingService.MapToBuyerDto(buyer);
            else if (user is Seller seller)
                return MappingService.MapToSellerDto(seller);
            return null;
        }

        private bool UserExists(string email)
        {
            return users.Any(users => users.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public List<ProductDto> GetAvailableProducts()
        {
            return products
                .Where(p => p.Status == ProductStatus.ForSale)
                .Select(p => MappingService.MapToProductDto(p))
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
            var favoriteProducts = buyer.FavoriteProducts.Select(MappingService.MapToProductDto).ToList();

            foreach (var product in favoriteProducts)
                Console.WriteLine($"- {product.Name}, Cijena: {product.Price:C}, Opis: {product.Description}");
            return favoriteProducts;
        }


        public List<ProductDto> ViewPurchaseHistory(BuyerDto buyerDto)
        {
            var buyer = users.OfType<Buyer>().FirstOrDefault(b => b.Email.Equals(buyerDto.Email, StringComparison.OrdinalIgnoreCase));
            if (buyer == null)
                return new List<ProductDto>();

            return buyer.PurchasedProducts.Select(MappingService.MapToProductDto).ToList();
        }


        public void AddProduct(string name, string description, decimal price, SellerDto sellerDto, string category)
        {
            var seller = MappingService.MapToSeller(sellerDto);
            var productCategory = Services.Service.ParseCategory(category);
            var product = new Product(name, description, price, seller, productCategory);
            products.Add(product);
            seller.ProductsForSale.Add(product);
            Console.WriteLine($"Proizvod '{name}' uspješno dodan.");
        }


        public List<ProductDto> ViewProducts(SellerDto sellerDto)
        {
            var seller = users.OfType<Seller>().FirstOrDefault(s => s.Email.Equals(sellerDto.Email, StringComparison.OrdinalIgnoreCase));

            return seller.ProductsForSale
                .Select(p => MappingService.MapToProductDto(p))
                .ToList();
        }

        public decimal ViewTotalEarnings(SellerDto sellerDto)
        {
            return products
                .Where(p => p.Seller.Email == sellerDto.Email && p.Status == ProductStatus.Sold)
                .Sum(p => p.Price * 0.95m);
        }

        public List<ProductDto> ViewSoldProductsByCategory(SellerDto sellerDto, string category)
        {
            try
            {
                ProductCategory categoryEnum = Services.Service.ParseCategory(category);

                return products
                    .Where(p => p.Seller.Email == sellerDto.Email && p.Category == categoryEnum && p.Status == ProductStatus.Sold)
                    .Select(p => MappingService.MapToProductDto(p))
                    .ToList();
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Neispravna kategorija. Pokušajte ponovo.");
                return new List<ProductDto>();
            }
        }


        public decimal ViewEarningsInTimePeriod(SellerDto sellerDto, DateTime startDate, DateTime endDate)
        {
            decimal totalEarnings = 0;

            foreach (var transaction in transactions)
            {
                if (transaction.SellerEmail == sellerDto.Email && transaction.TransactionDate >= startDate && transaction.TransactionDate <= endDate)
                {
                    var product = products.FirstOrDefault(p => p.Id == transaction.ProductId);
                    if (product != null && product.Status == ProductStatus.Sold)
                    {
                        decimal earnings = product.Price * 0.95m;
                        totalEarnings += earnings;
                    }
                }
            }
            return totalEarnings;
        }
    }
}
