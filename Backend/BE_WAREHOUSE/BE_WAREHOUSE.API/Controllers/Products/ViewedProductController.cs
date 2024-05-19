using BE_WAREHOUSE.Core.Interfaces.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE_WAREHOUSE.API.Controllers.Products
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ViewedProductController : ControllerBase
    {
        IViewedProductRepository _viewedProductRepository;

        public ViewedProductController(IViewedProductRepository viewedProductRepository)
        {
            _viewedProductRepository = viewedProductRepository;
        }

        [HttpGet("ViewProduct")]
        public async Task<IActionResult> ViewProductAsync(Guid ProductId,Guid UsersId)
        {
            var res = await _viewedProductRepository.ViewProduct(ProductId, UsersId);
            return Ok(res);
        }
        [HttpGet("ViewProductByUser/{id}")]
        public async Task<IActionResult> ViewProductByUserAsync(Guid id)
        {
            var res = await _viewedProductRepository.GetViewedProductsByUsersId(id);
            return Ok(res);
        }
    }
}
