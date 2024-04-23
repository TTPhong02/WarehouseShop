using BE_WAREHOUSE.API.Controllers.Base;
using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.Base;
using BE_WAREHOUSE.Core.Interfaces.IAddress;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE_WAREHOUSE.API.Controllers.Addresss
{
    [ApiController]
    public class AddressController : BaseController<Address>
    {
        IAddressRepository _addressRepository;

        public AddressController(IBaseRepository<Address> baseRepository, IBaseService<Address> baseService, IAddressRepository addressRepository) : base(baseRepository, baseService)
        {
            _addressRepository = addressRepository;
        }
        [HttpGet("UserId/{id}")]
        public async Task<IActionResult>  GetAddressByUsersId(Guid id) 
        {
            var res = await _addressRepository.GetAddresByUsersId(id);
            return Ok(res);
        }
        [HttpGet("Default/{id}")]
        public async Task<IActionResult> GetAddressDefautByUsersId(Guid id)
        {
            var res = await _addressRepository.GetAddresDefaultByUsersId(id);
            return Ok(res);
        }
    }
}
