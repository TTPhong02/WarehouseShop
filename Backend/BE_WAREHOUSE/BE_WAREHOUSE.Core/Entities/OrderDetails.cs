using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Entities
{
    public class OrderDetails
    {
        public Guid OrderDetailsId { get; set; }
        public Guid OrdersId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string ProductBrandName { get; set; }
        public Double ProductPrice { get; set; }
        public string ProductDescription { get; set; }
        public Double Discount { get; set; }
        public int Quantity { get; set; }
        public string ImagesPath { get; set; }
    }
}
