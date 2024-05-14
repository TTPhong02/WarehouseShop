using BE_WAREHOUSE.API.Controllers.Base;
using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.Base;
using BE_WAREHOUSE.Core.Interfaces.Vouchers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE_WAREHOUSE.API.Controllers.Vouchers
{
    [ApiController]
    public class VoucherController : BaseController<Voucher>
    {
        IVoucherRepository _voucherRepository;
        public VoucherController(IBaseRepository<Voucher> baseRepository, IBaseService<Voucher> baseService, IVoucherRepository voucherRepository) : base(baseRepository, baseService)
        {
            _voucherRepository = voucherRepository;
        }
        [HttpGet("UseVoucher/{voucherCode}")]
        public async Task<IActionResult> UseVoucher(string voucherCode)
        {
            var res = await _voucherRepository.UseVoucher(voucherCode);
            return Ok(res);
        }
        [HttpGet("FilterVoucher")]
        public async Task<IActionResult> FilterVoucher(int pageSize, int pageNumber, int? voucherType, string? searchString)
        {
            var res = await _voucherRepository.FilterVoucher(pageSize, pageNumber, voucherType, searchString);
            return Ok(res);
        }

    }
}
