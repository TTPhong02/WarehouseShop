﻿using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Interfaces.Order
{
    public interface IOrderDetailsRepository:IBaseRepository<OrderDetails>
    {
        Task<IEnumerable<OrderDetails>> GetByIdOrders(Guid id);
    }
}
