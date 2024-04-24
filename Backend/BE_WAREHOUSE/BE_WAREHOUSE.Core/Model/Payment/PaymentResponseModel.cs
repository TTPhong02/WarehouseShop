using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Model.Payment
{
    public class PaymentResponseModel
    {
        public bool Success { get; set; }
        public string PaymentMethod { get; set; }
        public string OrdersDescription  { get; set; }
        public string OrdersId { get; set; }
        public string PaymentId { get; set; }
        public string TransactionId { get; set; }
        public string Token { get; set; }
        public string VnPayResponseCode { get; set; }
    }
}
