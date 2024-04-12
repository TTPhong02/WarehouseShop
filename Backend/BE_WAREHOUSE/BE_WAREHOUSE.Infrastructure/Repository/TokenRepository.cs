using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.Token;
using BE_WAREHOUSE.Infrastructure.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Infrastructure.Repository
{
    public class TokenRepository : ITokenRepository
    {
        IDbContext _dbContext;

        public TokenRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Users> GetUserByEmail(string phoneNumber)
        {
            var sql = "Proc_CheckExist_Users";
            var user = await _dbContext.Connection.QueryFirstOrDefaultAsync<Users>(sql, phoneNumber, commandType: System.Data.CommandType.StoredProcedure);
            return user;
        }

        public async Task<Users> GetUserByRefreshToken(string refreshToken)
        {
            var sql = "Proc_Find_Users_RefreshToken";
            var user = await _dbContext.Connection.QueryFirstOrDefaultAsync<Users>(sql, refreshToken, commandType: System.Data.CommandType.StoredProcedure);
            return user;
        }

        public async Task<int> UpdateUser(Users user)
        {
            var sql = "Proc_Update_Users";
            var res = await _dbContext.Connection.ExecuteAsync(sql, user);
            return res;
        }
    }
}
