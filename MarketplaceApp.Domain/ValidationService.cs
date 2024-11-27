using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketplaceApp.Data;

namespace MarketplaceApp.Domain
{
    public class ValidationService
    {
        public static class Validation
        {
            public static bool IsValidEmail(string email)
            {
                return !string.IsNullOrEmpty(email) && email.Contains("@") && email.Contains(".");
            }
        }
    }
}
