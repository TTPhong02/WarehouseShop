import Enum from "./enum";
const helper = {
    formatMoney(amount) {
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
                case Enum.Gender.Male:
                    return "Nam";
                case Enum.Gender.Female:
                    return "Nữ";
                case Enum.Gender.Other:    
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
    formatDate(date){
        date = String(date).substring(0,10);
        return date;    
    }
}
export default helper ;