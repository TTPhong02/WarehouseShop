using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Interfaces.Order
{
    public interface IOrdersRepository:IBaseRepository<Orders>
    {
        public Task<IEnumerable<Orders>> GetOrderByUsersId(Guid UsersId);
    }
}
