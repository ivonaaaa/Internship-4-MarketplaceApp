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
    public static class MappingService
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
                Status = product.Status
            };
        }

        public static BuyerDto MapToBuyerDto(Buyer buyer)
        {
            return new BuyerDto(buyer.Name, buyer.Email, buyer.Balance);
        }

        public static SellerDto MapToSellerDto(Seller seller)
        {
            return new SellerDto(seller.Name, seller.Email, seller.TotalEarnings);
        }

        public static Seller MapToSeller(SellerDto sellerDto)
        {
            return new Seller(sellerDto.Name, sellerDto.Email);
        }

    }
}
