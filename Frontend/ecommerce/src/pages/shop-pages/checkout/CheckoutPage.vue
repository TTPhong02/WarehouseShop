<template>
  <div class="row s-checkout">
    <div class="col-lg-8 col-md-8 col-sm-8 col-8 checkout-left">
      <div class="row checkout-header">
        <div class="col-lg-8 col-md-8 col-sm-8 col-8 ">
          <div class="checkout-header-title">Thông tin giao hàng</div>
          <div class="header-address-user">
            <div class="address-select">
                <input v-model="typeAddress"  value="default" name="address-option" type="radio">
            </div>
            <div class="address-user">
                <img src="../../../assets/img/logo-home-address.png" alt="">
                <div class="address-user-profile">
                    <div class="address-user-name">
                        {{addressDefaultUser.ReminiscentName}}
                        <div class="address-default">Mặc định</div>
                    </div>
                    <div class="address-user-location">{{addressDefaultUser.HomeNumber}}, {{addressDefaultUser.Ward}}, {{addressDefaultUser.District}}, {{addressDefaultUser.Province}}</div>
                </div>
                <div class="address-action">
                    <router-link to="/profile/address">
                        <i class="fa-solid fa-user-pen"></i>
                    </router-link>
                </div>
            </div>
          </div>
          <div class="header-address-other">
            <div class="address-select">
                <input v-model="typeAddress" value="other" name="address-option" type="radio">
                <label for="">Giao hàng đến địa chỉ khác</label>
            </div>
          </div>
        </div>
        <div class="col-lg-4 col-md-4 col-sm-4 col-4 image-logo">
          <img src="../../../assets/img/logo.png" alt="" />
        </div>
      </div>
      <div class="row checkout-content">
        <div class="col-lg-7 col-md-7 col-sm-7 col-7 checkout-content-address">
          <div class="checkout-address-form">
            <div class="row address-form-input">
                <label class="col-lg-4 col-md-4 col-sm-4 col-4 " for="fullname">Họ và tên <span class="red">*</span></label>
                <input v-model="order.ReminiscentName" class="col-lg-8 col-md-8 col-sm-8 col-8 " type="text" placeholder="Họ và tên" />   
            </div>
            <span class="error-input red">{{listErrorMessage.fullname}}</span>
            <div class="row address-form-input">
                <label class="col-lg-4 col-md-4 col-sm-4 col-4 " for="phonenumber">Số điện thoại <span class="red">*</span> </label>
                <input v-model="order.PhoneNumber" class="col-lg-8 col-md-8 col-sm-8 col-8 " type="text" placeholder="Số điện thoại" />   
            </div>
            <span class="error-input red">{{listErrorMessage.phonenumber}}</span>
            <div class="row address-form-select">
                <label class="col-lg-4 col-md-4 col-sm-4 col-4 " for="">Chọn tỉnh/thành phố <span class="red">*</span></label>
                <select v-model="provinceSelected" class="col-lg-8 col-md-8 col-sm-8 col-8 " name="" id="">
                    <option v-for="item in province" 
                    :key="item.province_id" 
                    :value="{
                        province_id: item.province_id,
                        province_name: item.province_name,
                        province_type: item.province_type,
                    }">
                    {{item.province_name}}
                    </option>
                </select>
            </div>
            <span class="error-input red">{{listErrorMessage.province}}</span>
            <div class="row address-form-select">
                <label class="col-lg-4 col-md-4 col-sm-4 col-4 " for="">Chọn quận/huyện <span class="red">*</span></label>
                <select v-model="districtSelected" class="col-lg-8 col-md-8 col-sm-8 col-8 " name="" id="">
                    <option v-for="item in district" 
                    :key="item.district_id" 
                    :value="{
                        district_id: item.district_id,
                        district_name: item.district_name,
                        district_type: item.district_type,
                    }">
                    {{item.district_name}}
                    </option>
                </select>
            </div>
            <div class="row address-form-select">
                <label class="col-lg-4 col-md-4 col-sm-4 col-4 " for="">Chọn xã/phường <span class="red">*</span></label>
                <select v-model="wardSelected" class="col-lg-8 col-md-8 col-sm-8 col-8 " name="" id="">
                    <option v-for="item  in ward" 
                    :key="item.ward_id" 
                    :value="{
                        ward_id: item.ward_id,
                        ward_name: item.ward_name,
                        ward_type: item.ward_type,
                    }">
                    {{item.ward_name}}
                    </option>
                </select>
            </div>
            <span class="error-input red">{{listErrorMessage.district}}</span>
            <div class="row address-form-input">
                <label class="col-lg-4 col-md-4 col-sm-4 col-4 " for="address">Số nhà/đường <span class="red">*</span></label>
                <input v-model="order.HomeNumber" class="col-lg-8 col-md-8 col-sm-8 col-8 " type="text" placeholder="Số nhà/đường" />   
            </div>
            <span class="error-input red">{{listErrorMessage.home}}</span>
            <div class="row address-form-input">
                <label class="col-lg-4 col-md-4 col-sm-4 col-4 " for="address">Ghi chú</label>
                <textarea v-model="order.Note" class="col-lg-8 col-md-8 col-sm-8 col-8 " placeholder="Ghi chú"></textarea>
            </div>
          </div>
        </div>
        <div class="col-lg-5 col-md-5 col-sm-5 col-5 checkout-content-right">
          <div class="checkout-delivery">
            <div class="checkout-payment-header">
                Vận chuyển
            </div>
            <div class="checkout-label">
                <input type="radio" v-model="this.deliveryMethod" :value="this.Enum.DeliveryMethod.Delivery">
                <span for="">Giao hàng tận nơi : {{this.helper.formatMoney(deliveryFee)}}</span>
            </div>
          </div>
          <div class="checkout-payment-method">
            <div class="checkout-payment-header">
                Thanh toán
            </div>
            <div class="checkout-label">
                <input name="payment-method" v-model="this.paymentMethod" type="radio" :value="this.Enum.PaymentMethod.COD">
                <span for="">Thanh toán khi giao hàng (COD)</span>
            </div>
            <div class="checkout-label">
                <input name="payment-method" v-model="this.paymentMethod" type="radio" :value="this.Enum.PaymentMethod.MOMO">
                <span for="">Momo </span>
            </div>
            <div class="checkout-label">
                <input name="payment-method" v-model="this.paymentMethod" type="radio" :value="this.Enum.PaymentMethod.VNPAY">
                <span for="">VNPay</span>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="col-lg-4 col-md-4 col-sm-4 col-4 checkout-right">
        <div class="checkout-cart-header">
            Đơn hàng ({{this.cartSelected.length}} sản phẩm)
        </div>
        <div class="checkout-cart-content">
            <div class="checkout-cart-list">
                <div v-for="item in cartSelected" :key="item.ProductId" class="row checkout-cart-item">
                    <div class="col-lg-2 col-md-2 col-sm-2 col-2 checkout-item-image">
                        <img :src="this.helper.checkImagePath(item.ImagesPath)" alt="">
                    </div>
                    <div class="col-lg-6 col-md-6 col-sm-6 col-6 checkout-item-infor ">
                        <div class="checkout-item-name">{{item.ProductName}}</div>
                        <div class="checkout-item-description">Giá: {{this.helper.formatMoney(item.ProductPrice)}}</div>
                        <div class="checkout-item-description">Số lượng: {{item.Quantity}}</div>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-4 col-4 checkout-item-money">
                        <div class="checkout-item-total">{{this.helper.formatMoney(item.ProductPrice*item.Quantity)}}</div>
                    </div>
                </div>
            </div>
            <div class="checkout-coupon">
                <input type="text" placeholder="Nhập mã giảm giá tại đây">
                <button>Áp dụng</button>
            </div>
            <div class="checkout-total-payment">
                <div class="checkout-payment-item">
                    <div class="payment-item-title">Tạm tính</div>
                    <div class="payment-item-content">{{this.helper.formatMoney(this.totalToPay - (this.totalToPay * discount))}}</div>
                </div>
                <div class="checkout-payment-item">
                    <div class="payment-item-title">Phí vận chuyển</div>
                    <div class="payment-item-content">{{this.helper.formatMoney(this.deliveryFee)}}</div>
                </div>
            </div>
            <div class="checkout-total-total">
                <div class="checkout-payment-item">
                    <div class="payment-item-title">Tổng cộng</div>
                    <div class="payment-item-content">{{this.helper.formatMoney(this.totalToPay + this.deliveryFee)}}</div>
                </div>
            </div>
            <div class="checkout-action-other">
                <router-link to="/cart">
                <i class="fa-solid fa-chevron-left"></i>    
                Quay về giỏ hàng
                </router-link>
                <button @click="checkout()">Đặt hàng</button>
            </div>
        </div>
    </div>
  </div>
</template>

<script>
import localStorageService from "../../../js/storage/LocalStorageService"
import addressService from '../../../utils/AddressService';
import addressApiService from '../../../utils/ApiAddressService.js';
import ordersService from '../../../utils/OrdersService';
import cartItemsService from '../../../utils/CartItemsService';
import vnPayService from '../../../utils/VnPayService'

export default {
    data() {
        return {
            paymentMethod:null,
            deliveryMethod:null,
            order:{},
            typeAddress:null,
            cartSelected:[],
            user:{},
            addressDelivery:{},
            province:[],
            district:[],
            ward:[],
            provinceSelected:{},
            districtSelected:{},
            wardSelected:{},
            errorNotEmpty:null,
            discount:0,
            deliveryFee:30000,
            totalToPay:null,
            listErrorMessage:{},
            addressDefaultUser:{}
        }
    },
    watch:{
        typeAddress(newValue){
           if(newValue == "default"){
                this.order.ReminiscentName = this.addressDefaultUser.ReminiscentName;
                this.order.PhoneNumber = this.addressDefaultUser.PhoneNumber;
                this.addressDelivery.HomeNumber = this.addressDefaultUser.HomeNumber;
                this.addressDelivery.Province = this.addressDefaultUser.Province;
                this.addressDelivery.District = this.addressDefaultUser.District;
                this.addressDelivery.Ward = this.addressDefaultUser.Ward;
           }else{
                this.order={};
           }
        },
        provinceSelected: async function (newValue){
            if(newValue == null){
                this.listErrorMessage.province= "Vui lòng chọn tỉnh / thành phố"
            }
            console.log("Chọn r");
            await this.takeAddressDistrict();
        },
        districtSelected: async function(newValue){
            if(newValue == null){
                this.listErrorMessage.district= "Vui lòng chọn quận / huyện "
            }
            await this.takeAddressWard();
        },
        wardSelected:  function(newValue){
            if(newValue == null){
                this.listErrorMessage.ward= "Vui lòng chọn xã / phường"
            }
        }
    },
    created() {
        this.takeAddressProvince();
        this.takeDataFromLocalStorage();
    },
    methods: {
        async checkout(){
            try{
                if(this.typeAddress == "other"){
                    var address = {};
                    address.UsersId = this.user.UsersId;
                    address.Province = this.provinceSelected.province_name;
                    address.Ward = this.wardSelected.ward_name;
                    address.District = this.districtSelected.district_name;
                    address.HomeNumber = this.order.HomeNumber;
                    address.ReminiscentName = this.order.ReminiscentName;
                    address.PhoneNumber = this.order.PhoneNumber;
                    address.AddressDefault = 0;
                    const formData = new FormData();
                    formData.append("dataJson",JSON.stringify(address));
                    await addressService.post(formData);
                    
                }
                var orderData = {}
                orderData.OrderDetails = this.cartSelected;
                var listIdProduct = await localStorageService.getItemFromLocalStorage("CartSelected");
                orderData.CartItemsId = listIdProduct;
                this.order.DeliveryMethod =  this.deliveryMethod;
                this.order.PaymentMethod = this.paymentMethod;
                this.order.DeliveryAddress = this.addressDelivery.HomeNumber + "," +this.addressDelivery.Ward +"," + this.addressDelivery.District +"," + this.addressDelivery.Province   ;
                this.order.TotalAmount = this.totalToPay + this.deliveryFee;
                this.order.FeeShipping = this.deliveryFee;
                this.order.UsersId = this.user.UsersId;
                orderData.Orders = this.order;
                var res = await ordersService.checkout(orderData);
                if(res.data){
                    
                    if(this.order.PaymentMethod == this.Enum.PaymentMethod.VNPAY){
                        var dataOrder = {}
                        dataOrder.OrderType = "Online";
                        dataOrder.Amount = this.order.TotalAmount;
                        dataOrder.OrderDescription = this.order.Note
                        dataOrder.OrderId = res.data.OrdersId;
                        var urlDirect = await vnPayService.createPaymentUrl(dataOrder);
                        location.href = urlDirect.data;
                        return ;
                    }else{
                        this.emitter.emit("showToast",this.Enum.ToastType.SUCCESS,"Đặt hàng thành công !")
                        setTimeout(() => {
                            this.$router.push("/profile/order");
                        }, 3000);
                    }
                    this.cartSelected = [];
                    localStorageService.removeItemLocalStorage("CartSelected")
                    var cartItems = await cartItemsService.getByUserId(this.user.UsersId);
                    await localStorage.setItem("CartItems",JSON.stringify(cartItems.data)); 
                    await this.emitter.emit("takeNumberOfCart");
                }
            }catch(error){
                console.log(error);
            }
            
        },
        totalMoneyToPay(){
            if(this.cartSelected.length > 0){
                 this.cartSelected.forEach(element => {
                    this.totalToPay +=  element.ProductPrice * element.Quantity;
                });
            }else{
                this.totalToPay = 0;
            }
        },
        async takeDataFromLocalStorage(){
            this.user = await localStorageService.getItemFromLocalStorage("User");
            var listIdCartItemsId = await localStorageService.getItemFromLocalStorage("CartSelected");
            var listCartItems = await localStorageService.getItemFromLocalStorage("CartItems");
            this.cartSelected = await  listCartItems.filter(item => listIdCartItemsId.includes(item.CartItemsId));
            await this.totalMoneyToPay();
            this.takeDataAddressDefault();
        },
        async takeAddressProvince(){
           try{
            var res = await addressApiService.getProvince();
            this.province = res;
           }catch(error){
                console.log(error);
           }
        },
        async takeAddressDistrict(){
           try{
            var res = await addressApiService.getDistrict(this.provinceSelected.province_id);
            this.district = res;
           }catch(error){
                console.log(error);
           }
        },
        async takeAddressWard(){
           try{
            var res = await addressApiService.getWard(this.districtSelected.district_id);
            this.ward = res;
           }catch(error){
                console.log(error);
           }
        },
        async takeDataAddressDefault(){
            try{
                var res = await addressService.getAddressDefaultById(this.user.UsersId);
                this.addressDefaultUser = res;
            }
            catch(error){
                console.log(error);
            }
        }
    },
};
</script>

<style scoped>
.checkout-item-money{
    padding:0px
}
.checkout-cart-item{
    padding: 5px 0px !important;
}
input[type='radio'] {
    accent-color:#025c7d;
}
select {
  -webkit-appearance: none;
  -moz-appearance: none;
  appearance: none;
  background: url("http://cdn1.iconfinder.com/data/icons/cc_mono_icon_set/blacks/16x16/br_down.png") white no-repeat calc(100% - 10px) !important;
}
.address-form-input{
    display: flex;
    align-items: center ;
}
.address-select label{
    font-size: 14px;
    margin:0;
    padding-left: 20px;
}
.header-address-other{
    margin: 10px 0px;
}
.address-select
{
    display: flex;
    align-items: center;
}
.address-action a:hover{
    color: #025c7d;
} 
.address-action  a {
    cursor: pointer;
    font-size: 20px;
    color: #0f7ca4;
}
.address-user-location{
    font-size: 13px;
    font-style: italic;
}
.address-default{
    margin-left: 10px;
    border-radius: 4px;
    padding: 2px 4px;
    background-color: rgba(3, 140, 203, 0.2);
}
.address-user-name{
    align-items: center;
    display: flex;
    font-size: 14px;
    font-weight: bold;
}
.address-user-profile{
    padding: 0px 10px;
}
.address-user img{
    width: 40px !important;
    height: 40px !important;
    border-radius: 50%;
    border: 1px solid #a2c5d2;
}
.address-user{
    padding: 0px 20px;
    display: flex;
    align-items: center;
}
.address-select input{
    width: 19px;
    height: 19px;
}

.header-address-user{
    display:flex;
    /* justify-content: space-between; */
    align-items: center;
}
.checkout-header-title{
    color: #a2c5d2;
    padding: 10px 0px;
    font-size: 25px;
    font-weight: bold;
    
}
.checkout-action-other a:hover{
    color: #0f7ca4;
}
.checkout-action-other a{
    font-size: 16px;
    color: #a2c5d2;
}
.checkout-action-other button{
    cursor: pointer;
    /* outline: none; */
    border: none;
    border-radius: 5px;
    color: #fff;
    font-size: 17px;
    width: 120px;
    height: 50px;
    background-color: #0f7ca4;
}
.checkout-action-other{
    display: flex;
    justify-content: space-between;
}
.checkout-total-total{
    padding: 8px;
}
.checkout-total-total .checkout-payment-item .payment-item-content {
    color: #a2c5d2;
    font-size: 23px;
    font-weight: bold;
}
.checkout-total-total .checkout-payment-item .payment-item-title {
    font-size: 18px;
    font-weight: bold;
}
.checkout-total-total .checkout-payment-item{
    padding:5px 0px;
    display: flex;
    justify-content: space-between;
}
.checkout-total-payment .checkout-payment-item{
    padding:5px 0px;
    display: flex;
    justify-content: space-between;
    font-size: 15px;
}
.checkout-total-payment{
    border-bottom: 1px solid #ccc;
    padding: 20px 0px;
}
.checkout-coupon{
    border-bottom: 1px solid #ccc;
    padding-bottom: 20px;
    display: flex;
    justify-content: space-around;
}
.checkout-coupon button{
    color: #fff;
    font-weight: bold;
    cursor: pointer;
    border-radius: 4px;
    margin-left: 15px;
    background-color: #a2c5d2;
    width: 150px;
    height: 40px;
}
.checkout-coupon input{
    outline: none;
    padding: 0px 10px;
    width: 100%;
    height: 40px;
    border: 1px solid #ccc;
    border-radius: 4px;
}
.checkout-cart-list{
    max-height: 300px;
    padding: 0px 0px 20px 20px;
    border-bottom: 1px solid #ccc;
        margin-bottom: 20px;
}
.checkout-cart-content{
    padding:20px 0px 20px 20px ;

}
.checkout-item-description{
    font-style: italic;
        font-size: 13px;
}
.checkout-item-name{
    font-size: 16px;
    font-weight: bold;
}
.checkout-item-image{
    display: flex;
    justify-content: center;
    align-items: center;
    padding: 0px;
    margin: 0px;
    max-width: 60px;
    height: 60px;
    border: 1px solid #ccc;
    border-radius: 4px;
}
.checkout-item-image img{
    border-radius: 4px;
    width: 60px;
    height: 60px;
}
.checkout-cart-header{
    padding: 20px 20px;
    font-size: 18px;
    font-weight: bold;
    border-bottom: 1px solid #ccc;
}
.checkout-right{
    padding:0px !important;
    border-left: 1px solid #ccc;
    
}
.checkout-label input[type="radio"]{
    transform: scale(1.5);
    margin: 0px 10px;
    outline: none;
}
.checkout-label{
    border-radius: 4px;
    margin: 5px 0px;
    padding: 5px 8px;
    width: 100%;
    height: 45px;
    display: flex;
    justify-content: flex-start;
    align-items: center;
    border: 1px solid #ccc;
    background-color: #fff;
}
.checkout-address-form textarea{
    height: 180px !important;
}
.checkout-address-form select,
.checkout-address-form textarea,
.checkout-address-form input {
    outline: none;
    border-radius: 4px;
    border: 1px solid #ccc;
    padding: 5px 8px;
    margin: 5px 0px;
    width: 100%;
    height: 45px;
}
.checkout-payment-header,
.checkout-address-header{
    font-size: 17px;
    font-weight: bold;
}
.image-logo{
    display: flex;
    align-items: center;
}
.image-logo img{
    width: 250px;
    height: 100px;
}
.checkout-left{
    padding: 0;
    padding-right: 20px;
}
.s-checkout {
  margin: 0 auto;
  width: 1280px;
  height: 100vh;
  padding: 0px 30px;
    box-shadow: 0px 2px 46.41px 4.59px rgba(2,38,113,0.1);
}
::-webkit-scrollbar {
    width: 8px;
    height: 8px;
    padding-right: 8px;
}
::-webkit-scrollbar-thumb {
    background: #888; 
    border-radius: 6px;
  }
</style>
