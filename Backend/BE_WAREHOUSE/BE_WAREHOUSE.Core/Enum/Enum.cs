using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Enum
{
    /// <summary>
    /// Tình trạng đơn hàng
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// Chờ xác nhận
        /// </summary>
        PENDING = 0,
        /// <summary>
        /// Đã xác nhận
        /// </summary>
        CONFIRMED = 1,
        /// <summary>
        /// Đang giao hàng
        /// </summary>
        SHIPPING = 2,
        /// <summary>
        /// Đã giao hàng
        /// </summary>
        DELIVERED = 3,
        /// <summary>
        /// Đã hủy
        /// </summary>
        CANCELLED = 4,
        /// <summary>
        /// Hoàn thành
        /// </summary>
        FINISHED = 5,
        /// <summary>
        /// Chờ thanh toán
        /// </summary>
        WAIT_FOR_PAY = 6,
    }

    /// <summary>
    /// Tình trạng giao hàng
    /// </summary>
    public enum DeliveryStatus
    {
        /// <summary>
        /// Chưa giao hàng
        /// </summary>
        NOT_DELIVERY = 0,
        /// <summary>
        /// Chờ lấy hàng
        /// </summary>
        WAITTING_FOR_DELIVERY = 1,
        /// <summary>
        /// Đã lấy hàng
        /// </summary>
        RECEIVED_THE_GOODS = 2,
    }

    /// <summary>
    /// Hình thức giao hàng
    /// </summary>
    public enum DeliveryMethod
    {
        /// <summary>
        /// giao hàng tận nơi
        /// </summary>
        LOCAL_DELIVERY = 0,
    }

    /// <summary>
    /// Phương thức thanh toán
    /// </summary>
    public enum PaymentMethod
    {
        COD = 0,
        ZALOPAY_WALLTET = 1,
        VNPAY = 2,
        MONO_WALLET = 3,
        SHOPPEPAY_WALLET = 4,
    }

    /// <summary>
    /// Tình trạng thanh toán
    /// </summary>
    public enum PaymentStatus
    {
        UNPAID = 0,
        PAID = 1,
    }
    public enum SortType
    {
        /// <summary>
        /// Sắp xếp tăng dần
        /// </summary>
        ASC = 1,
        /// <summary>
        /// Sắp xếp giảm dần
        /// </summary>
        DESC = 2,
        /// <summary>
        /// Không sắp xếp
        /// </summary>
        NULL = 0,
    }
    public enum Gender
    {
        Male = 0,
        Female = 1,
        Other = 2,
    }
    public enum AccountStatus
    {
        Active = 1,
        Unactive = 0,
    }
    public enum VoucherType
    {
        Shipping = 0,
        Price = 1,
    }
}
