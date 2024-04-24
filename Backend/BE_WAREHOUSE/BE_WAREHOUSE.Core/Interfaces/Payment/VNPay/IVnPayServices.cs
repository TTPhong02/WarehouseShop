using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Model.Payment;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Interfaces.Payment.VNPay
{
    public interface IVnPayServices
    {
        string CreatePaymentUrl(PaymentInfomationModel model, HttpContext context);
        Task<PaymentResponseModel> PaymentExecuteAsync(IQueryCollection collections);
    }
}
