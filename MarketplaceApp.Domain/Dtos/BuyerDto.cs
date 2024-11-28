using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceApp.Domain.Dtos
{
    public class BuyerDto : UserDto
    {
        public decimal Balance { get; set; }

        public BuyerDto(string name, string email, decimal balance) : base(name, email)
        {
            Balance = balance;
        }
    }
}

