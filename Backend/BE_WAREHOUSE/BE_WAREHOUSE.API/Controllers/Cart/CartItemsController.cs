using BE_WAREHOUSE.API.Controllers.Base;
using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE_WAREHOUSE.API.Controllers.Cart
{
    [ApiController]
    public class CartItemsController : BaseController<CartItems>
    {
        public CartItemsController(IBaseRepository<CartItems> baseRepository, IBaseService<CartItems> baseService) : base(baseRepository, baseService)
        {
        }
    }
}
