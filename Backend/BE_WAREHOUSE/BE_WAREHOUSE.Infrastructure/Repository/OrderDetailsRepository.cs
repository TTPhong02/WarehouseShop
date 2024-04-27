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
    public class OrderDetailsRepository : BaseRepository<OrderDetails>, IOrderDetailsRepository
    {
        public OrderDetailsRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<OrderDetails>> GetByIdOrders(Guid id)
        {
            var sql = $"SELECT * FROM view_orderdetails Where OrdersId = '{id}'";
            var res = await _dbContext.Connection.QueryAsync<OrderDetails>(sql);
            return res;
        }
    }
}
