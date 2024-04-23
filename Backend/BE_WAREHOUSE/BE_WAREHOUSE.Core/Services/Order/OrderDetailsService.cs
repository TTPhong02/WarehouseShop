using BE_WAREHOUSE.Core.Interfaces.Base;
using BE_WAREHOUSE.Core.Interfaces.Image;
using BE_WAREHOUSE.Core.Interfaces.Order;
using BE_WAREHOUSE.Core.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Services.Order
{
    public class OrderDetailsService : BaseService<OrderDetailsService>, IOrderDetailsService
    {
        public OrderDetailsService(IBaseRepository<OrderDetailsService> repository, IImagesService imagesService) : base(repository, imagesService)
        {
        }
    }
}
