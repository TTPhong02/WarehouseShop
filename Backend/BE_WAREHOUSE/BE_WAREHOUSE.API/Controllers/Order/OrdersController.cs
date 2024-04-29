using BE_WAREHOUSE.API.Controllers.Base;
using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.Base;
using BE_WAREHOUSE.Core.Interfaces.Order;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace BE_WAREHOUSE.API.Controllers.Order
{
    [ApiController]
    public class OrdersController : BaseController<Orders>
    {
        IOrdersService _ordersService;
        IOrdersRepository _ordersRepository;
        public OrdersController(IBaseRepository<Orders> baseRepository, IBaseService<Orders> baseService, IOrdersService ordersService, IOrdersRepository ordersRepository) : base(baseRepository, baseService)
        {
            _ordersService = ordersService;
            _ordersRepository = ordersRepository;
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
        [HttpPost("RevenueYear")]
        public async Task<IActionResult> GetRevenueByYear(List<DateTime> Date)
        {
            var res = await _ordersRepository.GetRevenueDataByYear(Date);
            return Ok(res);
        }
        [HttpPost("RevenueDate")]
        public async Task<IActionResult> GetRevenueByDate(List<DateTime> Date)
        {
            var res = await _ordersRepository.GetRevenueDataByDate(Date);
            return Ok(res);
        }
        [HttpPost("RevenueMonth")]
        public async Task<IActionResult> GetRevenueByMonth(List<DateTime> Date)
        {
            var res = await _ordersRepository.GetRevenueDataByMonth(Date);
            return Ok(res);
        }
        [HttpPost("OrdersTime")]
        public async Task<IActionResult> GetOrderByTime(List<DateTime> Date)
        {
            var res = await _ordersRepository.GetOrderDataByTime(Date);
            return Ok(res);
        }
        [HttpGet("Revenue")]
        public async Task<IActionResult> GetTotalRevenue() {
            var res = await _ordersRepository.GetSumRevenue();
            return Ok(res);
        }

    }
}
