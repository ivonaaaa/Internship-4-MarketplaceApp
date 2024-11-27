using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceApp.Data.UserTypes
{
    public class Seller : Users
    {
        public List<Product> ProductsForSale { get; set; } = new List<Product>();
        public decimal TotalEarnings { get; set; }

        public Seller(string name, string email) : base(name, email) { }
    }
}
