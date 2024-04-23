using BE_WAREHOUSE.API.Controllers.Base;
using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.Base;
using BE_WAREHOUSE.Core.Interfaces.Order;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE_WAREHOUSE.API.Controllers.Order
{
    [ApiController]
    public class OrdersController : BaseController<Orders>
    {
        IOrdersService _ordersService;
        public OrdersController(IBaseRepository<Orders> baseRepository, IBaseService<Orders> baseService, IOrdersService ordersService) : base(baseRepository, baseService)
        {
            _ordersService = ordersService;
        }
        [HttpPost("Checkout")]
        public async Task<IActionResult> CheckOutAsync(OrderData orderData) 
        {
            var res = await _ordersService.CheckoutService(orderData);
            return StatusCode(201,res);
        }
        [HttpGet("UsersId/{id}")]
        public async Task<IActionResult> GetOrderByUsersId(Guid id)
        {
            var res = await _ordersService.GetOrderByUsersId(id);
            return Ok(res);
        }
    }
}
