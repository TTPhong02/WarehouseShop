﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Entities
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public Guid CategoriesId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string ProductBrandName { get; set; }
        public Double ProductPrice { get; set; }
        public int ProductStock { get; set; }
        public string ProductDescription { get; set; }
        public string ProductSlug { get; set; }
        public Boolean ProductNewFlag { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string? CategoriesName { get; set; }
        public string? CategoriesSlug { get; set; }
        public string? CategoriesDescription { get; set; }
        public string? CategoriesCode { get; set; }

    }
}