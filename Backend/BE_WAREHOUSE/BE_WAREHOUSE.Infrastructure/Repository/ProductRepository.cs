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

        public async Task<IEnumerable<Product>> GetProductByListId(List<Guid> ids)
        {
            var sql = $"SELECT * FROM view_product WHERE ProductId IN @ListId ";
            var res = await _dbContext.Connection.QueryAsync<Product>(sql, new { ListId = ids });
            return res;
        }

        public async Task<Product> GetProductBySlug(string slug)
        {
            var sql = $"SELECT * FROM view_product WHERE ProductSlug = '{slug}'";
            var res = await _dbContext.Connection.QueryFirstOrDefaultAsync<Product>(sql);
            return res; 
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
