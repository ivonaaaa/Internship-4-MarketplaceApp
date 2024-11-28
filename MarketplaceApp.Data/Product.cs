using MarketplaceApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketplaceApp.Data.UserTypes;

namespace MarketplaceApp.Data
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ProductStatus Status { get; set; }
        public ProductCategory Category { get; private set; }
        public Seller Seller { get; private set; }

        public Product(string name, string description, decimal price, Seller seller, ProductCategory category)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Price = price;
            Seller = seller;
            Category = category;
            Status = ProductStatus.ForSale;
        }
    }
}
