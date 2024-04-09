using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.Base;
using BE_WAREHOUSE.Core.Interfaces.Products;
using BE_WAREHOUSE.Core.Resources;
using BE_WAREHOUSE.Core.Services.Base;
using MISA.AMISDemo.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Services.Products
{
    public class ProductService : BaseService<Product>,IProductService
    {
        public ProductService(IBaseRepository<Product> repository) : base(repository)
        {
        }
        public override async Task ValidateBeforeInsert(Product product)
        {
            if (product.ProductName == "" || product.ProductName == null)
            {
                throw new MISAValidateException(ProductResource.ProductNameNotEmpty);
            }
            if (
                product.ProductCode == "" || product.ProductCode == null)
            {
                throw new MISAValidateException(ProductResource.ProductCodeNotEmpty);
            }
            if (
                product.ProductPrice == null )
            {
                throw new MISAValidateException(ProductResource.ProductPriceNotEmpty);
            }
            if (
                product.ProductPrice < 0)
            {
                throw new MISAValidateException(ProductResource.ProductPriceNotSmallerThanZero);
            }

            await Task.CompletedTask;
        }
    }
}
