using BE_WAREHOUSE.Core.DTOs;
using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Interfaces.Products
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductNew();
        Task<IEnumerable<Product>> GetProductByListId(List<Guid> ids);
        Task<Product> GetProductBySlug(string slug);
        Task<IEnumerable<Product>> GetProductHot();
        Task<IEnumerable<Product>> GetProductByCategories(Guid id);
        Task<IEnumerable<ProductOrderByTime>> GetProductInOrderByTime(List<DateTime> Date, Guid? CategoriesId);
        Task<PagingEntity<Product>> FilterProductByCategories(int pageSize,int pageNumber,double? minPrice,double? maxPrice, int sortType, string? searchString,string? slug);
        Task<IEnumerable<Product>> SearchProduct(string searchString);
    }
}
