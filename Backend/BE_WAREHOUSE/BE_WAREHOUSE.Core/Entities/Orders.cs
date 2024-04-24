using BE_WAREHOUSE.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Entities
{
    public class Orders
    {
        public Guid OrdersId { get; set; }
        public Guid UsersId { get; set; }
        public string? OrdersCode { get; set; }
        public OrderStatus OrdersStatus { get; set; }
        public int DeliveryMethod { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string DeliveryAddress { get; set; }
        public int? DeliveryStatus { get; set; }
        public int PaymentMethod { get; set; }
        public PaymentStatus? PaymentStatus { get; set; }  
        public string ReminiscentName { get; set; }
        public string PhoneNumber { get; set; }
        public double TotalAmount { get; set; }
        public double FeeShipping { get; set; }
        public string? Note { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }
}
