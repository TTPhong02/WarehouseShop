const Enum = {
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
    }
}

export default Enum;