using BE_WAREHOUSE.API.Controllers.Base;
using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.Base;
using BE_WAREHOUSE.Core.Interfaces.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.FormulaParsing.Excel.Functions;

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
        [HttpGet("Categories/{id}")]
        public async Task<IActionResult> GetProductByCategories(Guid id)
        {
            var res = await _repository.GetProductByCategories(id);
            return Ok(res);
        }
        [HttpGet("Slug/{slug}")]
        public async Task<IActionResult> GetProductBySlug(string slug)
        {
            var res = await _repository.GetProductBySlug(slug);
            return Ok(res);
        }
        [HttpGet("ProductHot")]
        public async Task<IActionResult> GetProductHot()
        {
            var res = await _repository.GetProductHot();
            return Ok(res);
        }
        [HttpPost("ListId")]
        public async Task<IActionResult> GetProductByListId([FromBody]List<Guid> ids) {
            var res = await _repository.GetProductByListId(ids);
            return Ok(res);
        }
        [HttpGet("SortByCategories")]
        public async Task<IActionResult> SortAndSearchProductByCategories(int pageSize, int pageNumber,int sortType,double? minPrice,double? maxPrice, string? searchString,string? slug)
        {
            var res = await _repository.FilterProductByCategories(pageSize, pageNumber,minPrice,maxPrice, sortType, searchString,slug);
            return Ok(res);
        }
    }
}
