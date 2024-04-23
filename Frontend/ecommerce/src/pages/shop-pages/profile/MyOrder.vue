<template>
  <div class="s-order">
    <div class="s-order-header">
        <div class="order-header-title">
            Đơn hàng của tôi  
        </div>
        <div class="order-header-type">
            <div @click="showorderAll()" class="order-type-item ">
                <div class="border-left"></div>
                <div class="type-item-title">
                    Tất cả ({{this.order.length}})
                </div>
                <div class="border-right"></div>
            </div>
            <div @click="showorderPending()" class="order-type-item">
                <div class="border-none"></div>
                <div class="type-item-title">
                    Chờ xác nhận ({{this.orderPending.length}})
                </div>
                <div class="border-right"></div>
            </div>
            <div @click="showorderConfirmed()" class="order-type-item">
                <div class="border-none"></div>
                <div class="type-item-title">
                    Đã xác nhận ({{this.orderConfirmed.length}})
                </div>
                <div class="border-right"></div>
            </div>
            <div @click="showorderProcessing()" class="order-type-item">
                <div class="border-none"></div>
                <div class="type-item-title">
                    Đang xử lý ({{this.orderProcessing.length}})
                </div>
                <div class="border-right"></div>
            </div>
            <div @click="showorderFinished()" class="order-type-item">
                <div class="border-none"></div>
                <div class="type-item-title">
                    Hoàn tất ({{this.orderFinished.length}})
                </div>
                <div class="border-right"></div>
            </div>
            <div @click="showorderCancelled()" class="order-type-item">
                <div class="border-none"></div>
                <div class="type-item-title">
                    Bị hủy ({{this.orderCancelled.length}})
                </div>
                <div class="border-right"></div>
            </div>
        </div>
    </div>
    <div class="s-order-list">
        <table>
            <thead>
                <tr>
                    <th>Mã đơn hàng</th>
                    <th>Ngày mua</th>
                    <th>Người nhận</th>
                    <th>Tổng tiền</th>
                    <th>Trạng thái</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="item in orderList" :key="item.OrdersId">
                    <td>{{item.OrdersCode}}</td>
                    <td>{{this.helper.formatDate(item.CreatedDate)}}</td>
                    <td>{{item.ReminiscentName}}</td>
                    <td>{{this.helper.formatMoney(item.TotalAmount)}}</td>
                    <td>{{this.helper.formatOrderStatus(item.OrdersStatus)}}</td>
                    <td><a href="/profile/order">Xem chi tiết</a></td>
                </tr>
            </tbody>
        </table>
    </div>
  </div>
</template>

<script>
import localStorageService from '../../../js/storage/LocalStorageService'
import ordersService from "../../../utils/OrdersService"
export default {
    data() {
        return {
            orderList:[],
            order:[],
            orderPending:[],
            orderConfirmed:[],
            orderProcessing:[],
            orderFinished:[],
            orderCancelled:[],
            user:{},
        }
    },
    created() {
        this.takeDataUserFromLocalStorage();
    },
    methods: {
        async takeDataUserFromLocalStorage(){
            this.user = await localStorageService.getItemFromLocalStorage("User");
            var res = await ordersService.getByUsersId(this.user.UsersId);
            this.order = res.data;
            await this.checkDataOrder();
            this.orderList = this.order;
        },
        async checkDataOrder(){
            this.orderPending = await this.order.filter(item=>item.OrdersStatus == 0);
            this.orderConfirmed =await this.order.filter(item=>item.OrdersStatus == 1);
            this.orderProcessing =await this.order.filter(item=>item.OrdersStatus == 2);
            this.orderFinished =await this.order.filter(item=>item.OrdersStatus == 3);
            this.orderCancelled =await this.order.filter(item=>item.OrdersStatus == 4);
        },
        showorderAll(){
            this.orderList = this.order;

        },
        showorderConfirmed(){
            this.orderList = this.orderConfirmed;
        },
        showorderProcessing(){
            this.orderList = this.orderProcessing;
        },
        showorderFinished(){
            this.orderList = this.orderFinished;
        },
        showorderCancelled(){
            this.orderList = this.orderCancelled;
        },
        showorderPending(){
            this.orderList = this.orderPending;
        }
    },
}
</script>

<style scoped>
.s-order-list table{
    width: 100%;
}
.s-order-list table th{
    padding-bottom: 10px;
   font-weight: bold;
   color: #62808f;
   border-bottom: 1px solid #a2c5d2;
}
.s-order-list table td{
    border-bottom: 1px solid #a2c5d2 ;
    padding: 15px 0px;
   color: #005979;

}

.type-item-title{
    border-bottom: 2px solid #13e4b4;
    padding: 10px 0px;
}
.order-header-type{
    padding-top: 0px;
    display: flex;
    align-items: center;
    justify-content: space-between;

}
.border-right{
    border-right: 1px solid #a2c5d2;
}

.border-left{
    border-left: 1px solid #a2c5d2;
}
.order-type-item{
    cursor: pointer;
    justify-content: space-between;
    display: flex;
    font-size: 15px;
    color: #025c7d;
    text-align: center;
    width: 100%;
    flex: 1;
    padding: 10px 0px;
}
.s-order-header{
  color: #a2c5d2;
  font-size: 18px;
  font-weight: bold;

}
.action-order-default:hover{
  background-color: #a2c5d2;
}
.action-order-default{
  cursor: pointer;
  outline: none;
  background-color: #025c7d;
  color: #fff;
  margin-right: 20px;
  font-size: 13px;
  padding: 5px 10px;
  border-radius: 5px;
}
.s-order-item{
    padding: 10px 0px;
    display: flex;
    justify-content: space-between;
}
.s-order{
  padding: 20px 10px;
}
.order-action i:hover{
    color: #a2c5d2;
}
.order-action{
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding-right: 20px;
    cursor: pointer;
    font-size: 20px;
    color: #025c7d;
}
.order-user-location{
    font-size: 13px;
    font-style: italic;
}
.order-default{
    margin-left: 10px;
    border-radius: 4px;
    padding: 2px 4px;
    background-color: rgba(3, 140, 203, 0.2);
}
.order-user-name{
    align-items: center;
    display: flex;
    font-size: 14px;
    font-weight: bold;
}
.order-user-profile{
    padding: 0px 10px;
}
.order-user img{
    width: 40px !important;
    height: 40px !important;
    border-radius: 50%;
    border: 1px solid #a2c5d2;
}
.order-user{
    padding: 0px 20px;
    display: flex;
    align-items: center;

}
</style>