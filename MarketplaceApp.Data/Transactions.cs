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
        public string buyerEmail { get; private set; }
        public string sellerEmail { get; private set; }
        public DateTime TransactionDate { get; private set; }

        public Transaction(Guid productId, Buyer buyer, Seller seller)
        {
            ProductId = productId;
            buyerEmail = buyer.Email;
            buyerEmail = seller.Email;
            TransactionDate = DateTime.Now;
        }
    }
}
