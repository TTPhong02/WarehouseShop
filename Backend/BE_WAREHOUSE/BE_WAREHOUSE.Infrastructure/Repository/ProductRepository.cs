using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.Products;
using BE_WAREHOUSE.Infrastructure.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Infrastructure.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Product>> GetProductNew()
        {
            DateTime currentDate = DateTime.Now;

            // Lấy thời gian 1 tháng trước
            string oneMonthAgo = currentDate.AddMonths(-1).ToString("yyyy-MM-dd HH:mm:ss");
            var sql = $"SELECT * FROM view_product WHERE CreatedDate > '{oneMonthAgo}' ";
            var res = await _dbContext.Connection.QueryAsync<Product>(sql) ;
            return res;
        }
    }
}
