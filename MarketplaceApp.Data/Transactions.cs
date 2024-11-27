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
        public decimal Amount { get; private set; }
        public DateTime TransactionDate { get; private set; }

        public Transaction(Guid productId, string buyerEmail)
        {
            ProductId = productId;
            BuyerEmail = buyerEmail;
            TransactionDate = DateTime.Now;
        }
    }
}
