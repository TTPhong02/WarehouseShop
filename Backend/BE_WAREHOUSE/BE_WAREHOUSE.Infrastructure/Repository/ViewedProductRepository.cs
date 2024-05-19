using BE_WAREHOUSE.Core.DTOs;
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
    public class ViewedProductRepository : IViewedProductRepository
    {
        IDbContext _dbContext;

        public ViewedProductRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ProductViewed>> GetViewedProductsByUsersId(Guid usersId)
        {
            var sql = "Proc_Get_ViewedProduct_By_User";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("UsersId", usersId);
            var res = await _dbContext.Connection.QueryAsync<ProductViewed>(sql, dynamicParameters);
            return res;
        }

        public async Task<int> ViewProduct(Guid productId, Guid userId)
        {
            var sqlCheckProduct = $"SELECT * FROM ViewedProduct Where UsersId = '{userId}' AND ProductId = '{productId}'";
            var productCheck  = await _dbContext.Connection.QueryFirstOrDefaultAsync<ViewedProduct>(sqlCheckProduct);
            if(productCheck != null)
            {
                var sqlUpdate = $"Proc_Update_ViewedProduct";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("UsersId", userId);
                parameters.Add("ProductId", productId);
                var resUpdate = await _dbContext.Connection.ExecuteAsync(sqlUpdate,parameters);
                return resUpdate;
            }
            else
            {
                var sqlInsert = $"Proc_Insert_ViewedProduct";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("UsersId", userId);
                parameters.Add("ProductId", productId);
                var resInsert = await _dbContext.Connection.ExecuteAsync(sqlInsert,parameters);
                return resInsert;
            }
        }
    }
}
