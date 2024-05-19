using BE_WAREHOUSE.Core.DTOs;
using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Interfaces.Products
{
    public interface IViewedProductRepository
    {
        Task<IEnumerable<ProductViewed>> GetViewedProductsByUsersId(Guid usersId);
        Task<int> ViewProduct(Guid productId,Guid usersId);
    }
}
