using BE_WAREHOUSE.API.Controllers.Base;
using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.Base;
using BE_WAREHOUSE.Core.Interfaces.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE_WAREHOUSE.API.Controllers.Products
{
    [ApiController]
    public class ProductController : BaseController<Product>
    {
        IProductRepository _repository;
        public ProductController(IBaseRepository<Product> baseRepository, IBaseService<Product> baseService, IProductRepository repository) : base(baseRepository, baseService)
        {        
            _repository = repository;
        }
        [HttpGet("ProductNew")]
        public async Task<IActionResult> GetProductNew()
        {
            var res = await _repository.GetProductNew();
            return Ok(res);
        }
    }
}
