using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketplaceApp.Data;
using MarketplaceApp.Data.Enums;
using MarketplaceApp.Data.Seeds;

namespace MarketplaceApp.Domain
{
    public class Service
    {
        public static bool IsValidEmail(string email)
        {
            return !string.IsNullOrEmpty(email) && email.Contains("@") && email.Contains(".");
        }

        public static decimal ApplyPromoCode(string promoCode, ProductCategory category)
        {
            var code = Seeds.promoCodes.FirstOrDefault(pc => pc.Code == promoCode && pc.Category == category);
            if (code == null)
            {
                Console.WriteLine("Promo kod nije pronađen za ovu kategoriju.");
                return 0m;
            }
            if (DateTime.Now > code.ExpiryDate)
            {
                Console.WriteLine("Promo kod je istekao.");
                return 0m;
            }
            return code.Discount;
        }
    }
}
