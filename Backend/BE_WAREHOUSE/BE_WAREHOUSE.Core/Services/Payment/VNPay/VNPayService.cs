using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.Order;
using BE_WAREHOUSE.Core.Interfaces.Payment.VNPay;
using BE_WAREHOUSE.Core.Library;
using BE_WAREHOUSE.Core.Model.Payment;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using BE_WAREHOUSE.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Services.Payment.VNPay
{
    public class VnPayService : IVnPayServices
    {
        IOrdersRepository _ordersRepository;

        IConfiguration _configuration;
        public VnPayService(IConfiguration configuration, IOrdersRepository ordersRepository)
        {
            _configuration = configuration;
            _ordersRepository = ordersRepository;
        }
        public string CreatePaymentUrl(PaymentInfomationModel model, HttpContext context)
        {
            var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_configuration["TimeZoneId"]);
            var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);
            var tick = model.OrderId.ToString();
            var pay = new VnPayLibrary();
            var urlCallBack = _configuration["PaymentCallBack:ReturnUrl"];

            pay.AddRequestData("vnp_Version", _configuration["Vnpay:Version"]);
            pay.AddRequestData("vnp_Command", _configuration["Vnpay:Command"]);
            pay.AddRequestData("vnp_TmnCode", _configuration["Vnpay:TmnCode"]);
            pay.AddRequestData("vnp_Amount", ((int)model.Amount * 100).ToString());
            pay.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
            pay.AddRequestData("vnp_CurrCode", _configuration["Vnpay:CurrCode"]);
            pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress(context));
            pay.AddRequestData("vnp_Locale", _configuration["Vnpay:Locale"]);
            pay.AddRequestData("vnp_OrderInfo", model.OrderDescription);
            pay.AddRequestData("vnp_OrderType", model.OrderType);
            pay.AddRequestData("vnp_ReturnUrl", urlCallBack);
            pay.AddRequestData("vnp_TxnRef", tick);

            var paymentUrl =
                pay.CreateRequestUrl(_configuration["Vnpay:BaseUrl"], _configuration["Vnpay:HashSecret"]);

            return paymentUrl;
        }

        public async Task<PaymentResponseModel> PaymentExecuteAsync(IQueryCollection collections)
        {
            var pay = new VnPayLibrary();
            var response = pay.GetFullResponseData(collections, _configuration["Vnpay:HashSecret"]);
            if(response.Success == true)
            {
                var orderId = new Guid(response.OrdersId);
                var order = await _ordersRepository.GetByIdAsync(orderId);
                order.PaymentStatus = Enum.PaymentStatus.PAID;
                order.OrdersStatus = Enum.OrderStatus.PENDING;
                await _ordersRepository.UpdateAsync(order,orderId);
                return response;
            }
            return response;
        }

    }
}
