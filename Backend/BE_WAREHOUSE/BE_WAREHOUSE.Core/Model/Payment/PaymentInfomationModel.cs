using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Model.Payment
{
    public class PaymentInfomationModel
    {
        public string OrderType { get; set; }
        public double Amount { get; set; }
        public string OrderDescription { get; set; }
        public Guid OrderId { get; set; }
    }
}
