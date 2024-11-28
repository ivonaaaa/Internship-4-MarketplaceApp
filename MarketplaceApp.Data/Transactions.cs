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
        public Guid ProductId { get; private set; }
        public string BuyerEmail { get; private set; }
        public string SellerEmail { get; private set; }
        public DateTime TransactionDate { get; private set; }

        public Transaction(Guid productId, Buyer buyer, Seller seller)
        {
            ProductId = productId;
            BuyerEmail = buyer.Email;
            SellerEmail = seller.Email;
            TransactionDate = DateTime.Now;
        }
    }
}
