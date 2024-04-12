using BE_WAREHOUSE.Core.DTOs;
using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.Cart;
using BE_WAREHOUSE.Infrastructure.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Infrastructure.Repository
{
    public class CartItemsRepository : BaseRepository<CartItems>, ICartItemsRepository
    {
        public CartItemsRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<CartItemsDTO>> GetCartItemsByCartId(Guid cartId)
        {
            var sql = $"Select * from view_cartitems where CartId = '{cartId}'";
            var res = await _dbContext.Connection.QueryAsync<CartItemsDTO>(sql);
            return res;
        }

        public async Task<IEnumerable<CartItemsDTO>> GetCartItemsByUserId(Guid userId)
        {
            var sql = $"Select * from view_cartitems where UsersId = '{userId}'";
            var res = await _dbContext.Connection.QueryAsync<CartItemsDTO>(sql);
            return res;
        }

        public async Task<int> InsertCartItemsAsync(CartItems cartItems)
        {
            var sql = $"SELECT * FROM CartItems WHERE ProductId = '{cartItems.ProductId}'";
            var product = await _dbContext.Connection.QueryFirstOrDefaultAsync<CartItems>(sql);
            if(product == null)
            {
                var procedure = $"Proc_Insert_CartItems";
                var res = await _dbContext.Connection.ExecuteAsync(procedure, cartItems);
                return res;
            }
            else
            {
                cartItems.Quantity = product.Quantity + cartItems.Quantity;
                var procedure = $"Proc_Update_CartItems";
                var res = await _dbContext.Connection.ExecuteAsync(procedure, cartItems);
                return res;
            }
        }
    }
}
