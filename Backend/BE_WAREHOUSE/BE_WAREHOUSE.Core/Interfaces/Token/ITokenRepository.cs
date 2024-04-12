using BE_WAREHOUSE.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Interfaces.Token
{
    public interface ITokenRepository
    {
        Task<Users> GetUserByRefreshToken(string refreshToken);
        Task<Users> GetUserByEmail(string email);
        Task<int> UpdateUser(Users user);
    }
}
