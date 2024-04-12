using BE_WAREHOUSE.Core.DTOs;
using BE_WAREHOUSE.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Interfaces.Token
{
    public interface ITokenService
    {
        string GenerateRefreshToken();
        Task<TokenModel> LoginTakeToken(UserLogin user);

        Task<TokenModel> RefreshToken(TokenModel tokenModel);
        Task<int> UpdateAccount(Users user);
        Task LogoutAsync(string email);
    }
}
