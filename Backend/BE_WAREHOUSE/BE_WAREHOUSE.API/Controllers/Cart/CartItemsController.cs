using BE_WAREHOUSE.API.Controllers.Base;
using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.Base;
using BE_WAREHOUSE.Core.Interfaces.Cart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE_WAREHOUSE.API.Controllers.Cart
{
    [ApiController]
    public class CartItemsController : BaseController<CartItems>
    {
        ICartItemsRepository _cartItemsRepository;
        public CartItemsController(ICartItemsRepository cartItemsRepository,IBaseRepository<CartItems> baseRepository, IBaseService<CartItems> baseService) : base(baseRepository, baseService)
        {
            _cartItemsRepository = cartItemsRepository;
        }
        [HttpGet("UserId/{id}")]
        public async Task<IActionResult> GetCartItemsByUserId(Guid id)
        {
            var res = await _cartItemsRepository.GetCartItemsByUserId(id);
            return Ok(res);
        }
        [HttpGet("CartId/{id}")]
        public async Task<IActionResult> GetCartItemsByCartId(Guid id)
        {
            var res = await _cartItemsRepository.GetCartItemsByCartId(id);
            return Ok(res);
        }
        [HttpPost("Insert")]
        public async Task<IActionResult> InsertCartItems(CartItems cartItems)
        {
            var res = await _cartItemsRepository.InsertCartItemsAsync(cartItems);
            return StatusCode(201,res);
        }
    }
}
