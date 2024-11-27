using MarketplaceApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceApp.Data
{
    public class PromoCode
    {
        public string Code { get; set; }
        public ProductCategory Category { get; set; }
        public decimal Discount { get; set; }
        public DateTime ExpiryDate { get; set; }

        public PromoCode(string code, ProductCategory category, decimal discount, DateTime expiryDate)
        {
            Code = code;
            Category = category;
            Discount = discount;
            ExpiryDate = expiryDate;
        }
    }
}
