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
        public DateTime DeliveryDate { get; set; }
        public string DeliveryMethod { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryStatus { get; set; }
        public string Fullname { get; set; }
        public string PhoneNumber { get; set; }
        public string PaymentMethod { get; set; }
        public int OrdersDate { get; set; }

    }
}
