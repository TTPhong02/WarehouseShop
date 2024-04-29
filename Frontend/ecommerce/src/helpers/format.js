import Enum from "./enum";
const helper = {
    formatMoney(amount) {
        if(amount == undefined){
            return "0"+"₫";
        }
        return amount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".")+ " ₫";
    },
    checkImagePath(valueId) {
        if (valueId) {
            return "https://localhost:7242/" + valueId;

        } else {
            return "https://localhost:7242/images/no-image.jpg";
        }
    },
    formatGender(gender){
        if(gender != null || gender != undefined){
            switch (gender) {
                case Enum.Gender.MALE:
                    return "Nam";
                case Enum.Gender.FEMALE:
                    return "Nữ";
                case Enum.Gender.OTHER:    
                    return "Khác"
                default:
                    return "Khác";
            }
        }
    },
    formatOrderStatus(status){
        if(status != null || status != undefined){
            switch (status) {
                case Enum.OrderStatus.Pending:
                    return "Chờ xác nhận";
                case Enum.OrderStatus.Confirmed:
                    return "Đã xác nhận";
                case Enum.OrderStatus.Processing:
                    return "Đang xử lý";
                case Enum.OrderStatus.Finished:
                    return "Hoàn thành";
                case Enum.OrderStatus.Cancelled:
                    return "Bị hủy";
                default:
                    return "Khác";
            }
        }
    },
    formatPaymentMethod(method){
        if(method != null || method != undefined){
            switch (method) {
                case Enum.PaymentMethod.COD:
                    return "Thanh toán khi nhận";
                case Enum.PaymentMethod.MOMO:
                    return "MOMO";
                case Enum.PaymentMethod.VNPAY:
                return "VNPay";
                default:
                    return "Khác";
            }
        }
    },
    formatPaymentStatus(status){
        if(status != null || status != undefined){
            switch (status) {
                case Enum.PaymentStatus.PAID:
                    return "Đã thanh toán";
                case Enum.PaymentStatus.PENDING_PAID:
                    return "Chờ thanh toán";
                default:
                    return "Khác";
            }
        }
    },
    formatDeliveryStatus(status){
        if(status != null || status != undefined){
            switch (status) {
                case Enum.DeliveryStatus.PENDING_DELIVERY:
                    return "Chờ vận chuyển";
                case Enum.DeliveryStatus.DELIVERING:
                    return "Đang vận chuyển";
                    case Enum.DeliveryStatus.DELIVERED:
                    return "Đã vận chuyển";
                default:
                    return "Khác";
            }
        }
    },
    formatAccountStatus(status){
        if(status != null || status != undefined){
            switch (status) {
                case Enum.AccountStatus.Active:
                    return "Đang hoạt động";
                case Enum.AccountStatus.Unctive:
                    return "Dừng hoạt động";
                default:
                    return "Dừng hoạt động";
            }
        }
    },
    formatDate(date){
        if(date){

            date = String(date).substring(0,10);
            return date;    
        }else{
            return "";
        }
    },
    formatDateTime(date, displayOnForm) {
        try {
          if (date) {
            //chuyển về dũ liệu date trong js
            let dob = new Date(date);
            let day = dob.getDate() < 10 ? `0${dob.getDate()}` : dob.getDate();
            let month =
              dob.getMonth() + 1 < 10
                ? `0${dob.getMonth() + 1}`
                : dob.getMonth() + 1;
            let year = dob.getFullYear();
            if (displayOnForm) {
              return `${year}-${month}-${day}`;
            } else {
              return `${day}/${month}/${year}`;
            }
          } else {
            return "";
          }
        } catch (error) {
          console.error(error);
        }
      },
}
export default helper ;