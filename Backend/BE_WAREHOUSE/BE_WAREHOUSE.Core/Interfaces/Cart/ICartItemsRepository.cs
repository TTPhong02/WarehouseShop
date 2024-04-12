using BE_WAREHOUSE.Core.DTOs;
using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Interfaces.Cart
{
    public interface ICartItemsRepository:IBaseRepository<CartItems>
    {
        Task<IEnumerable<CartItemsDTO>> GetCartItemsByUserId(Guid userId);
        Task<IEnumerable<CartItemsDTO>> GetCartItemsByCartId(Guid userId);
        Task<int> InsertCartItemsAsync(CartItems cartItems);
    }
}
