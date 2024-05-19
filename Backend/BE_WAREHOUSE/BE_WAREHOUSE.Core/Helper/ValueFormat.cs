using BE_WAREHOUSE.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Helper
{
    public static class ValueFormat
    {
        public static string GetOrderStatusFormat(OrderStatus orderSatus)
        {
            switch (orderSatus)
            {
                case OrderStatus.PENDING:
                    return "Chờ xác nhận";
                case OrderStatus.CONFIRMED:
                    return "Đã xác nhận";
                case OrderStatus.SHIPPING:
                    return "Đang giao hàng";
                case OrderStatus.DELIVERED:
                    return "Đã giao hàng";
                case OrderStatus.FINISHED:
                    return "Đã hoàn thành";
                case OrderStatus.CANCELLED:
                    return "Đã hủy";
                default:
                    return "";
            }
        }
        public static string GetDeliveryStatusFormat(DeliveryStatus deliveryStatus)
        {
            switch (deliveryStatus)
            {
                case DeliveryStatus.NOT_DELIVERY:
                    return "Chưa giao hàng";
                case DeliveryStatus.WAITTING_FOR_DELIVERY:
                    return "Đang vận chuyển";
                case DeliveryStatus.DELIVERIED:
                    return "Đã giao";
                default:
                    return "";
            }
        }

        public static string GetPaymentStatusFormat(PaymentStatus paymentStatus)
        {
            switch (paymentStatus)
            {
                case PaymentStatus.UNPAID:
                    return "Chưa thanh toán";
                case PaymentStatus.PAID:
                    return "Đã thanh toán";
                default:
                    return "";
            }
        }

        public static string GetDeliveryMethodFormat(DeliveryMethod deliveryMethod)
        {
            switch (deliveryMethod)
            {
                case DeliveryMethod.LOCAL_DELIVERY:
                    return "Giao hàng tận nơi";
                default:
                    return "";
            }
        }
        public static string GetPaymentMethodFormat(PaymentMethod paymentMethod)
        {
            switch (paymentMethod)
            {
                case PaymentMethod.COD:
                    return "COD";
                case PaymentMethod.VNPAY:
                    return "Thanh toán bằng VNPAY";
                default:
                    return "";
            }
        }
    }
}
