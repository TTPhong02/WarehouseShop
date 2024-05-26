using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.DTOs
{
    public class ProductByTime
    {
        public List<DateTime> Date { get; set; }
        public Guid? CategoriesId { get; set; }
    }
}
