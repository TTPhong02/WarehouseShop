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

        public async Task<IEnumerable<Product>> GetProductHot()
        {
            var sql = "SELECT ProductSold, ProductName FROM Product ORDER BY ProductSold DESC LIMIT 5";
            var res = await _dbContext.Connection.QueryAsync<Product>(sql);
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
        public async Task<IEnumerable<Product>> GetProductByCategories(Guid id)
        {
            var sql = $"SELECT * FROM view_product WHERE CategoriesId = '{id}' ";
            var res = await _dbContext.Connection.QueryAsync<Product>(sql);
            return res;
        }

        public async Task<PagingEntity<Product>> FilterProductByCategories(int pageSize, int pageNumber, double? minPrice, double? maxPrice, int sortType, string? searchString, string slug)
        {
            var procedure = $"Proc_Sort_Product_By_Categories";

            PagingEntity<Product> pagingEntity = new PagingEntity<Product>();
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@pageSize", pageSize);
            parameters.Add("@pageNumber", pageNumber);
            parameters.Add("@minPrice", minPrice);
            parameters.Add("@maxPrice", maxPrice);
            parameters.Add("@typeSort", sortType);
            parameters.Add("@searchString", searchString);
            parameters.Add("@categoriesSlug", slug);

            parameters.Add("@totalRecord", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
            parameters.Add("@totalPage", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

            var res = await _dbContext.Connection.QueryAsync<Product>(procedure, parameters, commandType: System.Data.CommandType.StoredProcedure);

            pagingEntity.Data = res;
            pagingEntity.TotalRecord = parameters.Get<int>("@totalRecord");
            pagingEntity.TotalPage = parameters.Get<int>("@totalPage");

            return pagingEntity;
        }
    }
}
