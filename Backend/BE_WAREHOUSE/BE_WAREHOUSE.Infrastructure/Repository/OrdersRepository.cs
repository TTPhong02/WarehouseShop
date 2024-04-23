using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.Order;
using BE_WAREHOUSE.Infrastructure.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Infrastructure.Repository
{
    public class OrdersRepository : BaseRepository<Orders>, IOrdersRepository
    {
        public OrdersRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Orders>> GetOrderByUsersId(Guid UsersId)
        {
            var sql = $"SELECT * FROM Orders WHERE UsersId = '{UsersId}'";
            var res = await _dbContext.Connection.QueryAsync<Orders>(sql);
            return res;
        }
    }
}
