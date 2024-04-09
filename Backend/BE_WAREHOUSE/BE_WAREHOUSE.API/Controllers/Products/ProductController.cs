using BE_WAREHOUSE.API.Controllers.Base;
using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE_WAREHOUSE.API.Controllers.Products
{
    [ApiController]
    public class ProductController : BaseController<Product>
    {
        public ProductController(IBaseRepository<Product> baseRepository, IBaseService<Product> baseService) : base(baseRepository, baseService)
        {
        }
    }
}
