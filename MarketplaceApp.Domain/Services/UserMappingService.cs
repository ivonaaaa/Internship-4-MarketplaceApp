using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketplaceApp.Data;
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
    }
}
