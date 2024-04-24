using BE_WAREHOUSE.Core.Interfaces.Payment.VNPay;
using BE_WAREHOUSE.Core.Model.Payment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE_WAREHOUSE.API.Controllers.Payment
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class VnPayController : ControllerBase
    {
        IVnPayServices _vnPayService;

        public VnPayController(IVnPayServices vnPayServices)
        {
            _vnPayService = vnPayServices;
        }

        [HttpPost("CreatePaymentUrl")]
        public IActionResult CreatePaymentUrl(PaymentInfomationModel model)
        {
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);

            return StatusCode(201, url);
        }

        [HttpGet("PaymentCallback")]
        public  async Task<IActionResult> PaymentCallback()
        {
            var response = await _vnPayService.PaymentExecuteAsync(Request.Query);
            if (response != null)
            {
                return Redirect("http://localhost:8080/profile/order");
            }
            return StatusCode(200, response);
        }
    }
}
