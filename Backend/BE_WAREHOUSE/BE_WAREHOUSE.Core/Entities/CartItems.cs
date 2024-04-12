using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Entities
{
    public class CartItems
    {
        public Guid CartItemsId { get; set; }
        public Guid CartsId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
