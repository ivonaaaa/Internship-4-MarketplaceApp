using MarketplaceApp.Data.UserTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceApp.Data
{
    public class Transaction
    {
        public Guid ProductId { get; set; }
        public string BuyerEmail { get; set; }
        public string SellerEmail { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }

        public Transaction(Guid productId, Buyer buyer, Seller seller, decimal amount)
        {
            ProductId = productId;
            BuyerEmail = buyer.Email;
            SellerEmail = seller.Email;
            TransactionDate = DateTime.Now;
            Amount = amount;
        }
    }
}
