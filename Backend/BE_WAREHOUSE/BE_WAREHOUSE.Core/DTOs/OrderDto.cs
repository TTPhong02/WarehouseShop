using BE_WAREHOUSE.Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.DTOs
{
    public class OrderDto
    {
        public Guid OrdersId { get; set; }
        public Guid UsersId { get; set; }

        [DisplayName("Mã đơn hàng")]
        public string OrdersCode { get; set; }

        [DisplayName("Tên khách hàng")]
        public string ReminiscentName { get; set; }

        [DisplayName("Số điện thoại")]
        public string PhoneNumber { get; set; }

        [DisplayName("Địa chỉ")]
        public string DeliveryAddress { get; set; }

        [DisplayName("Tổng hóa đơn")]
        public double TotalAmount { get; set; }
        [DisplayName("Tiền ship")]
        public double? FeeShipping { get; set; }
        [DisplayName("Trạng thái")]
        public OrderStatus OrdersStatus { get; set; }

        [DisplayName("Vận chuyển")]
        public DeliveryStatus DeliveryStatus { get; set; }

        [DisplayName("Phương thức vận chuyển")]
        public DeliveryMethod DeliveryMethod { get; set; }

        [DisplayName("Phương thức thanh toán")]
        public PaymentMethod PaymentMethod { get; set; }

        [DisplayName("Thanh toán")]
        public PaymentStatus PaymentStatus { get; set; }

        [DisplayName("Ngày đặt")]
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Note { get; set; }
    }
}
