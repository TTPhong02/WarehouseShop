using BE_WAREHOUSE.API.Controllers.Base;
using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE_WAREHOUSE.API.Controllers.Addresss
{
    [ApiController]
    public class AddressController : BaseController<Address>
    {
        public AddressController(IBaseRepository<Address> baseRepository, IBaseService<Address> baseService) : base(baseRepository, baseService)
        {
        }
    }
}
