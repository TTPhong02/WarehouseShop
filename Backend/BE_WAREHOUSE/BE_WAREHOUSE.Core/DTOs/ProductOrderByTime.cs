using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.DTOs
{
    public class ProductOrderByTime
    {
        public Guid ProductId { get; set; }
        public Guid CategoriesId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string CategoriesName { get; set; }
        public int ProductSold { get; set; }
        public double ProductRevenue { get; set; }
    }
}
