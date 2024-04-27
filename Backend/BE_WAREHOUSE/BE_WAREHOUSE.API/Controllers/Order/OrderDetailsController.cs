using BE_WAREHOUSE.API.Controllers.Base;
using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.Base;
using BE_WAREHOUSE.Core.Interfaces.Order;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE_WAREHOUSE.API.Controllers.Order
{
    [ApiController]
    public class OrderDetailsController : BaseController<OrderDetails>
    {
        IOrderDetailsRepository _orderDetailsRepository;
        public OrderDetailsController(IBaseRepository<OrderDetails> baseRepository, IBaseService<OrderDetails> baseService,  IOrderDetailsRepository orderDetailsRepository) : base(baseRepository, baseService)
        {
            _orderDetailsRepository = orderDetailsRepository;
        }
        [HttpGet("OrdersId/{id}")]
        public async Task<IActionResult> GetByOrdersId(Guid id)
        {
            var res = await _orderDetailsRepository.GetByIdOrders(id);
            return Ok(res);
        }

    }
}
