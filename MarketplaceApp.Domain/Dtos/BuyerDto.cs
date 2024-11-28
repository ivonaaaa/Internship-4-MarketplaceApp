using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceApp.Domain.Dtos
{
    public class BuyerDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal Balance { get; set; }
    }
}

