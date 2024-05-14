<template>
  <div class="s-orders-detail">
    <div class="s-orders-detail-header">
        <div class="orders-header-image">
            <i class="fa-brands fa-shopify"></i>
        </div>
        <div class="orders-header-info">
            <div class="order-code">Đơn hàng:  {{Orders.OrdersCode}}</div>
            <div class="orders-created-date">Ngày đặt hàng: {{this.helper.formatDate(Orders.CreatedDate)}}</div>
        </div>
        <div class="order-status">
            <p>Trạng thái đơn hàng</p>
            <div v-if="Orders.OrdersStatus == 0" class="status-warning">
                Chờ xác nhận
            </div>
            <div v-if="Orders.OrdersStatus == 1" class="status-done">
                Đã xác nhận
            </div>
            <div v-if="Orders.OrdersStatus == 2" class="status-warning">
                Đang xử lý
            </div>
            <div v-if="Orders.OrdersStatus == 3" class="status-done">
                Thành công
            </div>
            <div v-if="Orders.OrdersStatus == 4" class="status-dangerous">
                Bị hủy
            </div>

        </div>
        <div class="order-payment-status">
             <p>Trạng thái thanh toán</p>
            <div v-if="Orders.PaymentStatus == 1" class="status-done">
                Đã thanh toán
            </div>
            <div v-if="Orders.PaymentStatus == 0" class="status-warning">
                Chờ thanh toán
            </div>
        </div>
        <div class="order-delivery-status">
             <p>Trạng thái vận chuyển</p>
            <div v-if="Orders.DeliveryStatus == 0" class="status-dangerous">
                Chờ vận chuyển
            </div>
            <div v-if="Orders.DeliveryStatus == 1" class="status-warning">
                Đang vận chuyển
            </div>
            <div v-if="Orders.DeliveryStatus == 2" class="status-done">
                Đã vận chuyển
            </div>
        </div>
    </div>
    <div class="row orders-detail-main">
        <div class="col-lg-9 col-md-9 col-sm-9 col-9 orders-detail-left">
            <div class="left-header">Danh sách sản phẩm</div>
            <div class="list-product">
                <table>
                    <tr>
                        <th>Sản phẩm</th>  
                        <th>Số lượng</th>
                        <th>Giá</th>
                        <th>Thành tiền</th>
                    </tr>
                    <tr v-for="item in OrderDetails" :key="item.OrderDetailsId" class="infor-product-detail">
                        <td class="product-infor-image">
                            <img :src="this.helper.checkImagePath(item.ImagesPath)" alt="">
                            <div class="product-infor">
                                <div class="product-name-detail">{{item.ProductName}}</div>
                                <div class="product-des-detail">{{item.ProductDescription}}</div>
                            </div>
                        </td>
                        <td>{{item.Quantity}}</td>
                        <td>{{this.helper.formatMoney(item.ProductPrice)}}</td>
                        <td>{{this.helper.formatMoney(item.ProductPrice * item.Quantity)}}</td>
                    </tr>
                    <tr>
                        <td>Tổng cộng</td>
                        <td></td>
                        <td></td>
                        <td>{{this.helper.formatMoney(this.TotalAmountOrderDetail)}}</td>
                    </tr>
                </table>
            </div>
            <div class="left-header">Chi tiết đơn hàng</div>
            <div class="orders-detail-content">
                <div class="detail-note">
                    <label for="">Ghi chú đơn hàng</label>  
                    <textarea v-model="Orders.Note" cols="20" rows="4"></textarea>
                    <MButton @click="updateOrder()" className="p-button1" text="Cập nhật"></MButton>
                </div>
                <div class="detail-order-money">
                    <div class="infor-item">
                        <div class="infor-item-title">Số lượng sản phẩm</div>
                        <div class="infor-item-text">{{this.OrderDetails.length}}</div>
                    </div>
                    <div class="infor-item">
                        <div class="infor-item-title">Tổng tiền hàng</div>
                        <div class="infor-item-text">{{this.helper.formatMoney(this.TotalAmountOrderDetail)}}</div>
                    </div>
                    <div class="infor-item">
                        <div class="infor-item-title">Giảm giá</div>
                        <div class="infor-item-text">{{this.helper.formatMoney(0)}}</div>
                    </div>
                    <div class="infor-item">
                        <div class="infor-item-title">Phí vận chuyển</div>
                        <div class="infor-item-text">{{this.helper.formatMoney(this.Orders.FeeShipping)}}</div>
                    </div>
                    <div class="infor-item">
                        <div class="infor-item-title "><strong>Tổng giá trị đơn hàng</strong></div>
                        <div class="infor-item-text"><strong>{{this.helper.formatMoney(this.Orders.TotalAmount)}}</strong></div>
                    </div>
                    <div class="infor-item">
                        <div class="infor-item-title">Số tiền đã thanh toán</div>
                        <div class="infor-item-text" v-if="this.Orders.PaymentStatus == this.Enum.PaymentStatus.PAID">{{this.helper.formatMoney(this.Orders.TotalAmount) }}</div>
                        <div class="infor-item-text" v-if="this.Orders.PaymentStatus == this.Enum.PaymentStatus.PENDING_PAID">{{this.helper.formatMoney(0) }}</div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-3 col-3  orders-detail-right">
            <div class="orders-users-info">
                <p>THÔNG TIN THANH TOÁN</p>
                <div class="infor-item">
                    <div class="infor-item-title">Phương thức:</div>
                    <div class="infor-item-text">{{this.helper.formatPaymentMethod(this.Orders.PaymentMethod)}}</div>
                </div>
                <hr>
                <p>NGƯỜI NHẬN HÀNG</p>
                <div class="infor-item">
                    <div class="infor-item-title">Tên:</div>
                    <div class="infor-item-text">{{this.Orders.ReminiscentName}}</div>
                </div>
                <div class="infor-item">
                    <div class="infor-item-title">Số điện thoại:</div>
                    <div class="infor-item-text">{{this.Orders.PhoneNumber}}</div>
                </div>
                <hr>
                <p>ĐỊA CHỈ GIAO HÀNG</p>
                <div class="infor-item-title">{{this.Orders.DeliveryAddress}}</div>
            </div>
            <div class="confirm-action">
                <p>XÁC NHẬN ĐƠN HÀNG</p>
                <div class="action-button">
                    <MButton @click="finishOrder()" v-if="Orders.OrdersStatus == 2" className="p-button1" text="Hoàn tất đơn"></MButton>
                    <MButton @click="processingOrder()" v-if="Orders.OrdersStatus == 1" className="processing" text="Xử lý đơn "></MButton>
                    <MButton @click="confirmOrder()" v-if="Orders.OrdersStatus == 0" className="p-button1" text="Xác nhận đơn "></MButton>
                    <MButton @click="cancelOrder()" v-if="Orders.OrdersStatus !== 4" className="cancel" text="Hủy đơn hàng"></MButton>
                    <MButton @click="unConfirmOrder()" v-if="Orders.OrdersStatus == 4" className="processing" text="Đặt lại đơn hàng"></MButton>
                    <MButton @click="unConfirmOrder()" v-if="Orders.OrdersStatus == 1" className=" cancel " text="Hủy xác nhận "></MButton>
                </div>
            </div>
            <router-link to="/admin/orders-management">
                <i class="fa-solid fa-angle-left"></i>
                Quay lại danh sách đơn hàng
            </router-link>
        </div>
    </div>
  </div>
</template>

<script>
import orderDetailsService from '../../../utils/OrdersDetailsServices';
import MButton from '../../../components/base/button/MButton.vue';
import ordersService from '../../../utils/OrdersService';
import productService from '../../../utils/ProductService';
export default {
    props:['id'],
    components:{
        MButton
    },
    data() {
        return {
            TotalAmountOrderDetail:0,
            Orders:{},
            OrderDetails:[],
        }
    },
    created() {
        this.takeDataOrders();
        this.takeDataOrdersDetails();
    },
    methods: {
        async processingOrder(){
            this.Orders.OrdersStatus = this.Enum.OrderStatus.Processing;
            this.Orders.DeliveryStatus = this.Enum.DeliveryStatus.DELIVERING;
            await this.updateOrder();
        },
        async finishOrder(){
            this.Orders.OrdersStatus = this.Enum.OrderStatus.Finished;
            this.Orders.DeliveryStatus = this.Enum.DeliveryStatus.DELIVERED;
            this.Orders.PaymentStatus = this.Enum.PaymentStatus.PAID;
            const date = new Date();
            this.Orders.DeliveryDate = date;
            await this.updateOrder();
        },      
        async cancelOrder(){
            this.Orders.OrdersStatus = this.Enum.OrderStatus.Cancelled;
            this.Orders.DeliveryStatus = null;
            for (const element of this.OrderDetails) {
                var product = {};
                var res = await productService.getById(element.ProductId);
                product = res.data;
                product.ProductStock += element.Quantity;
                product.ProductSold -= element.Quantity;
                var formData = new FormData();
                formData.append("dataJson", JSON.stringify(product));
                await productService.put(element.ProductId, formData);
            }
            await this.updateOrder();
        },
        async unConfirmOrder(){
            this.Orders.OrdersStatus = this.Enum.OrderStatus.Pending;
            this.Orders.DeliveryStatus = null;
            await this.updateOrder();
        },
        async confirmOrder(){
            this.Orders.OrdersStatus = this.Enum.OrderStatus.Confirmed;
            this.Orders.DeliveryStatus = this.Enum.DeliveryStatus.PENDING_DELIVERY;
            await this.updateOrder();
        },
        async updateOrder(){
            try{
                var formData = new FormData();
                formData.append("dataJson", JSON.stringify(this.Orders));
                var res = await ordersService.put(this.id,formData);
                if(res.data.Data > 0) {
                    this.emitter.emit("showToast",this.Enum.ToastType.SUCCESS,"Cập nhật thành công !");
                    this.takeDataOrders();
                }
            }catch(error){
                console.log(error);
            }
        },
        async takeDataOrdersDetails(){
            var res = await orderDetailsService.getByOrdersId(this.id);
            this.OrderDetails = res.data;
            await this.OrderDetails.forEach(element => {
                 this.TotalAmountOrderDetail += element.Quantity * element.ProductPrice;
            });
        },
        async takeDataOrders(){
            var res = await ordersService.getById(this.id);
            this.Orders = res.data;
        }
    },
}
</script>

<style scoped>
table tr:first-child{
    border-bottom: 1px solid #a2c5d2;
}
table tr:first-child th{
    padding-bottom: 3px;
}
table tr:last-child {
    border-top: 1px solid #a2c5d2;
}
table tr:last-child td{
    font-weight: bold;
    padding-top: 3px;
}
.product-des-detail{
    font-style: italic;
    font-size: 12px;
}
.product-name-detail{
    font-weight: bold;
    font-style: italic;
    font-size: 15px;
}
.product-infor{
    padding: 0px 20px;
}
.product-infor-image{
    margin: 5px 0px;
    display: flex;
    align-items: center;
}
.product-infor-image img{
    width: 70px;
    height: 70px;
}
hr{
    border-color: #a2c5d2;
}
a:hover{
    color: #0195ca;
    font-weight: bold;
}
a{
    color: #a2c5d2;
    font-size: 16px;

}
.action-button button{
    margin: 5px 5px;
}
.action-button{
    flex-wrap: wrap;
    display: flex;
    align-items: center;
    justify-content: space-between;
}
.confirm-action p,
.orders-users-info p{
    font-weight: bold;
    color: #0195ca;
    font-size: 16px;
    margin-bottom: 10px;
}
.confirm-action,
.orders-users-info{
    border-radius:8px ;
    margin-top: 10px;
    margin-bottom: 10px;
    padding: 10px 15px;
    box-shadow: 0 1px 11px 0px rgba(0,0,0,0.15);
}
.infor-item-title{
    font-size: 14px;
}
.detail-order-money{
    font-size: 16px;
    padding: 24px 20px 0px 15px;
}
.infor-item{
    padding: 2px 0px;
    display: flex;
    align-items: center;
    justify-content: space-between;
}
.detail-order-money{
    flex: 1;
}
.detail-note textarea{
    padding: 10px 10px;
    border: 1px solid #a2c5d2;
    outline: unset;
    margin-bottom: 15px;
}
.detail-note label{
    font-size: 14px;
    font-weight: bold;
}
.detail-note{
    padding: 5px 10px;
    display: flex;
    flex-direction: column;
    flex: 1;
}
.orders-detail-content{
    flex: 1;
    display: flex;
    justify-content: space-between;
    border-radius:0px 0px 8px 8px ;
    padding: 10px 10px;
    box-shadow: 0 1px 11px 0px rgba(0,0,0,0.15);
}
table{
    table-layout: auto;
    width: 100%;
}

.list-product{
    border-radius:0px 0px 8px 8px ;

    padding: 15px 10px;
    box-shadow: 0 1px 11px 0px rgba(0,0,0,0.15);
}
.left-header{
    font-size: 17px;
    font-weight: bold;
    color: #0195ca;
    border-radius: 8px 8px 0px 0px;
    margin-top: 10px;
    margin-bottom: 5px;
    padding: 5px 10px;
    box-shadow: 0 1px 11px 0px rgba(0,0,0,0.15);
}
.status-dangerous{
    text-align: center;
    border-radius: 8px;
    background-color: rgb(228, 128, 85);
    color: #fff;
}
.status-warning{
    text-align: center;
    border-radius: 8px;
    background-color: rgb(206, 218, 104);
    color: #fff;
}
.status-done{
    text-align: center;
    border-radius: 8px;
    background-color: rgb(135, 228, 135);
    color: #fff;
}
.order-delivery-status p ,
.order-payment-status p,
.order-status p{
    text-transform: uppercase;
    color: #0195ca;
    font-weight: bold;
    margin-bottom: 5px;
}
.order-delivery-status,
.order-payment-status,
.order-status{
    border-right: 2px solid #a2c5d2;
    padding: 0px 10px;
}
.orders-created-date{
    font-size: 13px;
    font-style: italic;;
}
.order-code{
    font-size: 16px;
    font-weight: bold;
}
.orders-header-info{
    border-right: 2px solid #a2c5d2;
    padding: 0px 10px;
}
.orders-header-image{
    text-align: center;
    font-size: 30px;
    color: #fff;
    padding: 2px 11px;
    background-color: #a2c5d2;
    border-radius: 10px;
    box-shadow: 0 1px 11px 0px rgba(0,0,0,0.15);
}
.s-orders-detail-header{
    padding: 10px 10px;
    display:flex;
    align-items: center;
    box-shadow: 0 1px 11px 0px rgba(0,0,0,0.15);
    border-radius: 10px;
}
.confirm-action,
.orders-users-info,
.orders-detail-content,
.left-header,
.list-product,
.s-orders-detail-header{
        background-color: #fff;
}
</style>