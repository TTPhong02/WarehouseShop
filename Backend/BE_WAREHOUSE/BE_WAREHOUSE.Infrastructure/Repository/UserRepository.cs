using BE_WAREHOUSE.Core.DTOs;
using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.User;
using BE_WAREHOUSE.Infrastructure.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Infrastructure.Repository
{
    public class UserRepository : BaseRepository<Users>, IUsersRepository
    {
        public UserRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> CheckUserIsExist(string email)
        {
            var procedure = "Proc_CheckExist_Users";
            var res = await _dbContext.Connection.QueryFirstOrDefaultAsync<Users>(procedure,new { Email = email });
            if (res == null)
            {
                return false;
            }
            return true;
        }
        
        public async Task<Users?> FindByEmailAsync(string email)
        {
            var procedure = "Proc_CheckExist_Users";
            var res = await _dbContext.Connection.QueryFirstOrDefaultAsync<Users>(procedure, new { Email = email });
            return res;
        }

        public async Task<Users?> FindUserByEmailAndPassword(UserLogin userLogin)
        {
            var procedure = "Proc_Find_Users_Email_Password";
            var res = await _dbContext.Connection.QueryFirstOrDefaultAsync<Users>(procedure, userLogin);
            return res;
        }

        public async Task<Users?> GetUserByToken(string token)
        {
            var sql = "Select * from Users where RefreshToken = @RefreshToken";
            var param = new DynamicParameters();    
            param.Add("@RefreshToken", token);
            var res = await _dbContext.Connection.QueryFirstOrDefaultAsync<Users>(sql, param);
            return res;
        }

        public async Task<int> UpdateToken(Users user, Guid id)
        {
            var procedure = $"Proc_Update_Users_Token";
            DynamicParameters param = new DynamicParameters();
            param.Add("@UsersId", id);
            param.Add("@RefreshToken", user.RefreshToken);
            param.Add("@RefreshTokenExpiryTime", user.RefreshTokenExpiryTime);
            var res = await _dbContext.Connection.ExecuteAsync(procedure,param);
            return res;
        }
    }
}
