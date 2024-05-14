const Enum = {
    ToastType:{
        FAILED:0,
        SUCCESS:1,
        WARNING:2,
    },
    Gender:{
        MALE: 0,
        FEMALE: 1,
        OTHER: 2
    },
    WorkStatus:{
        WORKING :1,
        NOT_WORKING : 0
    },
    FormMode:{
        ADD : 0,
        UPDATE:1,
    },
    DeliveryMethod:{    
        Delivery: 0,
    },
    PaymentMethod:{
        COD: 0,
        VNPAY: 1,
        MOMO:2,
    },
    OrderStatus:{
        Pending:0,
        Confirmed:1,
        Processing:2,
        Finished:3,
        Cancelled:4,
    },
    PaymentStatus:{
        PENDING_PAID:0,
        PAID:1
    },
    DeliveryStatus:{
        PENDING_DELIVERY:0,
        DELIVERING :1,
        DELIVERED:2
    },
    AccountStatus:{
        Active:1,
        Unactive:0
    },
    VoucherType:{
        SHIPPING:0,
        PRICE:1
    }
}

export default Enum;