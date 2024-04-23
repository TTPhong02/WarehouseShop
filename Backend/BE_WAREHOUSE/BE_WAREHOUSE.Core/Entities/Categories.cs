using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Entities
{
    public class Categories
    {
        public Guid CategoriesId { get; set; }
        public string CategoriesName { get; set; }
        public string CategoriesCode { get; set; }
        public string CategoriesDescription { get; set; }
        public string CategoriesSlug { get; set; }
        public string CreatedBy { get; set; }
        public Guid ImagesId { get; set; }
        public string ImagesPath { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
