using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketplaceApp.Data;
using MarketplaceApp.Data.UserTypes;
using MarketplaceApp.Domain.Dtos;

namespace MarketplaceApp.Domain.Services
{
    public static class UserMappingService
    {
        public static UserDto MapToUserDto(Users user)
        {
            if (user == null) return null;

            return new UserDto(user.Name, user.Email);
        }

        public static ProductDto MapToProductDto(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                //Status = product.Status
            };
        }

        public static BuyerDto MapToBuyerDto(Buyer buyer)
        {
            return new BuyerDto
            {
                Name = buyer.Name,
                Email = buyer.Email,
                Balance = buyer.Balance
            };
        }

    }
}
