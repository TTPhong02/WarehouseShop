using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Entities
{
    public class OrderData
    {
        public List<OrderDetails> OrderDetails { get; set; }
        public List<Guid> CartItemsId { get; set; }
        public Orders Orders { get; set; }
    }
}
