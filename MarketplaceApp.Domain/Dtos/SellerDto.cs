using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceApp.Domain.Dtos
{
    public class SellerDto : UserDto
    {
        public decimal TotalEarnings { get; set; }

        public SellerDto(string name, string email, decimal totalEarnings) : base(name, email)  // Call the UserDto constructor
        {
            TotalEarnings = totalEarnings;
        }
    }
}
