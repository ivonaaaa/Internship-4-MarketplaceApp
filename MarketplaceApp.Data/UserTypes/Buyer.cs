using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceApp.Data.UserTypes
{
    public class Buyer : Users
    {
        public decimal Balance { get; set; }
        public List<Product> PurchasedProducts { get; private set; } = new List<Product>();
        public List<Product> FavoriteProducts { get; private set; } = new List<Product>();

        public Buyer(string name, string email, decimal balance) : base(name, email)
        {
            Balance = balance;
        }
    }
}
