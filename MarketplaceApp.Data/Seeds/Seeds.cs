using MarketplaceApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceApp.Data.Seeds
{
    public class Seeds
    {
        public static List<PromoCode> promoCodes = new List<PromoCode>
        {
            new PromoCode("ELEKTRONIKA10", ProductCategory.Electronics, 0.10m, new DateTime(2024, 12, 31)),
            new PromoCode("KNJIGA20", ProductCategory.Books, 0.20m, new DateTime(2024, 11, 30)),
            new PromoCode("ODJECA90", ProductCategory.Clothing, 0.90m, new DateTime(2026, 02, 04)),
            new PromoCode("NAMJESTAJ20", ProductCategory.Furniture, 0.20m, new DateTime(2024, 09, 13)),
            new PromoCode("HRANA5", ProductCategory.Food, 0.05m, new DateTime(2024, 12, 15))
        };
    }
}
