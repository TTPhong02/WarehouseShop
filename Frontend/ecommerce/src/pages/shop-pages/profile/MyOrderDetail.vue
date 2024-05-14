<template>
  <div class="s-order-detail">
    <div class="order-detail">
        <div class="order-detail-header">
            Chi tiết đơn hàng
        </div>
        <div class="row order-detail-infor">
            <div class="col-lg-8 col-md-8 col-sm-8 col-8 order-detail-left">
                <span v-if="Orders.OrdersStatus == 0" class="status-warning">
                    Chờ xác nhận
                </span>
                <span v-if="Orders.OrdersStatus == 1" class="status-done">
                    Đã xác nhận
                </span>
                <span v-if="Orders.OrdersStatus == 2" class="status-warning">
                    Đang xử lý
                </span>
                <span v-if="Orders.OrdersStatus == 3" class="status-done">
                    Thành công
                </span>
                <span v-if="Orders.OrdersStatus == 4" class="status-dangerous">
                    Bị hủy
                </span>
                <div class="order-detail-content">
                    <div>Mã đơn hàng:   <strong>{{Orders.OrdersCode}}</strong></div>
                    <div>Ngày mua :    <strong>{{this.helper.formatDate(Orders.CreatedDate)}}</strong></div>
                    <div>Tổng tiền:    <strong>{{this.helper.formatMoney(Orders.TotalAmount)}}</strong></div>
                    <div>Ghi chú:    <strong>{{Orders.Note}}</strong></div>
                </div>
            </div>
            <div v-if="false" class="col-lg-4 col-md-4 col-sm-4 col-4 order-detail-right">
                <button>Đặt hàng lại </button>
            </div>
        </div>
    </div>
    <div class="order-delivery">
        <div class="order-delivery-infor">
            <div class="order-delivery-header">
                THÔNG TIN NGƯỜI NHẬN
            </div>
            <div class="order-delivery-content">
                <div>{{Orders.ReminiscentName}}</div>
                <div>{{Orders.DeliveryAddress}}</div>
                <div>SĐT: {{Orders.PhoneNumber}}</div>
            </div>
        </div>
        <div class="order-delivery-infor">
            <div class="order-delivery-header">
                PHƯƠNG THỨC VẬN CHUYỂN
            </div>
            <div class="order-delivery-content">
                <div>Giao hàng tận nơi</div>
                <div>Ngày giao hàng: {{this.helper.formatDate(Orders.DeliveryDate)}}</div>
            </div>
        </div>
        <div class="order-delivery-infor">
            <div class="order-delivery-header">
                PHƯƠNG THỨC THANH TOÁN
            </div>
            <div class="order-delivery-content">
                <div>{{this.helper.formatPaymentMethod(Orders.PaymentMethod)}}</div>
            </div>
        </div>
    </div>
    <div class="order-detail-product">
        <div class="order-product-header">
            DANH SÁCH SẢN PHẨM ({{OrdersDetails.length}})
        </div>
        <div class="order-product-infor">
            <table>
                <tr>
                    <th>Hình ảnh</th>
                    <th>Tên sản phẩm</th>
                    <th>Giá bán</th>
                    <th>Số lượng</th>
                    <th>Thành tiền</th>
                </tr>
                <tr v-for="item in OrdersDetails" :key="item">
                    <td><img :src="this.helper.checkImagePath(item.ImagesPath)" alt=""></td>
                    <td>{{item.ProductName}}</td>
                    <td>{{this.helper.formatMoney(item.ProductPrice)}}</td>
                    <td>{{item.Quantity}}</td>
                    <td>{{this.helper.formatMoney(item.Quantity * item.ProductPrice)}}</td>
                </tr>
            </table>
        </div>
        <div class="order-product-overview">
            <div>Thành tiền: <strong>{{this.helper.formatMoney(Orders.TotalAmount - Orders.FeeShipping)}}</strong></div>
            <div>Phí vận chuyển: <strong>{{this.helper.formatMoney(Orders.FeeShipping)}}</strong></div>
            <div>Tổng số tiền: <strong>{{this.helper.formatMoney(Orders.TotalAmount)}}</strong></div>
        </div>
    </div>
    <div class="order-detail-action">
        <button @click="cacelOrder()" class="cancel-button" v-if="Orders.OrdersStatus == 2 || Orders.OrdersStatus == 1 || Orders.OrdersStatus == 0">Hủy đơn hàng</button>
        <button class="warning-button" v-if="Orders.OrdersStatus == 3">Đánh giá sản phẩm</button>
    </div>
  </div>
</template>

<script>
import orderDetailsService from '../../../utils/OrdersDetailsServices';
import ordersService from '../../../utils/OrdersService';
import productService from '../../../utils/ProductService';
export default {
    props:['id'],
    data() {
        return {
            Orders:{},
            OrdersDetails:[],
        }
    },
    mounted() {
        this.takeDataOrder();
    },
    methods: {
        async cacelOrder(){
            try{
                var order = this.Orders;
                order.OrdersStatus = this.Enum.OrderStatus.Cancelled;
                for (const element of this.OrdersDetails) {
                    var product = {};
                    var res = await productService.getById(element.ProductId);
                    product = res.data;
                    product.ProductStock += element.Quantity;
                    product.ProductSold -= element.Quantity;
                    var formData = new FormData();
                    formData.append("dataJson", JSON.stringify(product));
                    await productService.put(element.ProductId, formData);
                }
                var formDataOrder = new FormData();
                formDataOrder.append("dataJson", JSON.stringify(order));
                var res1 = await ordersService.put(order.OrdersId,formDataOrder);
                if(res1.data.Data > 0) {
                    this.emitter.emit("showToast",this.Enum.ToastType.SUCCESS,"Đã hủy đơn hàng!");
                    this.takeDataOrders();
                }
            }catch(error){
                console.log(error);
            }
        },
        async takeDataOrderDetail(){
            var res = await orderDetailsService.getByOrdersId(this.Orders.OrdersId);
            this.OrdersDetails = res.data;
            console.log(this.OrdersDetails);
        },
        async takeDataOrder(){
            try{
                var res = await ordersService.getById(this.id);
                this.Orders = res.data;
                await this.takeDataOrderDetail();
            }catch(error){
                console.log(error);
            }
        }
    },
}
</script>

<style scoped>
.order-detail-action .warning-button:hover{
    background-color: rgb(102, 114, 9);
}
.order-detail-action .warning-button{
    cursor: pointer;
    outline: unset;
    background-color: rgb(219, 219, 19);
    color: #fff;
    padding: 5px 10px;
    border-radius: 10px;
}
.order-detail-action .cancel-button:hover{
    background-color: rgb(86, 0, 0);
}
.order-detail-action .cancel-button{
    cursor: pointer;
    outline: unset;
    background-color: rgb(208, 0, 0);
    color: #fff;
    padding: 5px 10px;
    border-radius: 10px;
}
.order-detail-action {
    margin-bottom: 20px;
    display: flex;
    justify-content: center;
}
.order-product-overview div{
    padding:3px 0px;
}
.order-product-overview{
    margin: 10px 5px;
    text-align: right;
}
.order-product-infor td{
    padding: 8px 3px;
}
.order-product-infor th{
    padding: 0px 2px 10px 2px;
    
}
.order-product-infor tr{
    padding: 5px 5px;
    border-bottom: 1px solid #ccc;
}
.order-product-infor td img{
    width: 90px;
    height: 90px;
}
.order-product-infor table{
    width: 100%;
}
.order-product-header{
    padding-bottom: 10px;
    font-size: 16px;
    color: #0097ce;
    font-weight: bold;
}
.order-detail-product{
    margin: 20px 0px;

}
.order-delivery-header{
    margin-bottom: 5px;
    padding-bottom: 7px;
    color: #0097ce;
    font-weight: bold;
    border-bottom: 1px solid #ccc;
}
.order-delivery-infor{
    padding: 5px 9px;
    margin: 0px 5px;
    border:1px solid #ccc;
    flex: 1;
}
.order-delivery{
    display: flex;
    justify-content: space-between;
}
.order-detail-right button:hover{
    background-color: #a2c5d2;
}
.order-detail-right button{
    cursor: pointer;
    outline: unset;
    padding: 5px 20px;
    font-size: 16px;
    background-color: #0097ce;
    color: #fff;
    font-weight: bold;
    border-radius: 10px;
}
.order-detail-content div{
    padding: 3px 0px;
}

.order-detail-content{
    margin-top: 10px;
}

.order-detail-header{
    margin-bottom: 10px;
    font-size: 20px;
    font-weight: bold;
    color: #0097ce;
}
.order-detail{
    padding: 10px 10px;
}
.status-dangerous{
    font-weight: bold;
    padding: 2px 10px;
    text-align: center;
    border-radius: 8px;
    background-color: rgb(228, 128, 85);
    color: #fff;
}
.status-warning{
    font-weight: bold;
    padding: 2px 10px;
    text-align: center;
    border-radius: 8px;
    background-color: rgb(206, 218, 104);
    color: #fff;
}
.status-done{
    font-weight: bold;
    padding: 2px 10px;
    text-align: center;
    border-radius: 8px;
    background-color: rgb(135, 228, 135);
    color: #2c0000;
}
</style>