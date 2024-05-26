<template>
<div>
    <div class="s-cart">
        <div class="container">
            <div class="s-cart-title">
                <h1>Giỏ hàng của bạn </h1>
            </div>
            <div class="s-cart-content">
                <div class="row cart-header">
                    <div class="col-lg-6 col-md-6 col-sm-6 cart-header-item header-item-checkbox text-left">
                        <div class="cart-input-checkall">
                            <input  v-model="selectAll" type="checkbox">
                        </div>
                        Sản phẩm
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-2 cart-header-item">Giá </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 cart-header-item">Số lượng</div>
                    <div class="col-lg-2 col-md-2 col-sm-2 cart-header-item">Thành tiền</div>
                    <div class="col-lg-1 col-md-1 col-sm-1 cart-header-item">Thao tác</div>
                </div>
                <div class="row cart-list-item">
                    <div v-if="cartItems.length ==0" class="cart-list-noitem">
                        Chưa có sản phẩm nào , hãy tiếp tục mua hàng nhé ! 
                    </div>
                    <div v-for="item in cartItems" :key="item.CartItemsId" class="row cart-item">
                        <div class="col-lg-6 col-md-6 col-sm-6 col-6 cart-item-product text-left">
                            <div class="cart-input-check">
                                <input v-model="cartItemSelected" :value="item.CartItemsId" type="checkbox">
                            </div>
                            <div class="cart-product-image">
                                <img :src="pathImage(item.ImagesPath)" alt="">
                            </div>
                            <div class="cart-product-infor ">
                                <div class="product-name">{{item.ProductName}}</div>
                                <div class="product-description">Số lượng còn lại : {{item.ProductStock}}</div>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2  cart-item-price">
                            <div class="product-price">{{formatMoney(item.ProductPrice)}}</div>
                        </div>  
                        <div class="col-lg-1 col-md-1 col-sm-1 col-1 row cart-item-quantity">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-12 product-manage-quantity">
                                <button v-on:click="decreaseQuantity(item.CartItemsId)">-</button>
                                <input :value="item.Quantity" type="text" readonly>
                                <button v-on:click="increaseQuantity(item.CartItemsId)">+</button>
                            </div>
                            <span v-if="this.errorQuantity?.length > 0" class="red">
                                {{this.errorQuantity}}
                            </span>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2 col-2 cart-item-sum">
                            <div class="product-sum product-price">{{formatMoney(item.ProductPrice * item.Quantity)}}</div>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-1 col-1  cart-item-action">
                            <div v-on:click="deleteCartItems(item.CartItemsId)" class="product-action">
                                <i class="fa-regular fa-trash-can"></i>
                            </div>
                        </div>
                    </div>                                        
                </div>
                <div class="row cart-total">
                    <div class="col-lg-2 col-md-2 col-sm-2 col-2  cart-total-title">
                        Tổng tiền: 
                    </div>
                    <div class="col-lg-2 col-md-2 col-sm-2 col-2  cart-total-money produc-price">
                        {{formatMoney(this.totalMoney)}}
                    </div>
                </div>
                <div class="row cart-action">
                    <router-link to="/" class="cart-back-home" >
                        Tiếp tục mua hàng
                    </router-link>
                    <router-link v-if="this.cartItemSelected.length > 0" to="/checkout" class="cart-checkout">
                        Tiến hành thanh toán
                    </router-link>

                    <!-- <button class="col-lg-3 col-md-3 col-sm-3 col-3 cart-checkout">
                        
                    </button> -->
                </div>
            </div>
        </div>
    </div>
</div>
</template>

<script>
import cartLocalStorageService from "../../../js/storage/CartLocalStorage";
import cartItemsService from "../../../utils/CartItemsService";
import localStorageService from '../../../js/storage/LocalStorageService';
export default {
    components:{
        
    },
    data() {
        return {
            users:{} ,
            cartItems:[],
            totalMoney: 0,
            cartItemSelected: [],
            allSelected:false,
            errorQuantity:"",
        }
    },
    mounted() {
        this.takeDataUsers();
        this.getCartItemsFormLocal();
    },
    watch:{
        cartItemSelected:{
            deep:true,
            handler:"setLocalStorageCartSelect"
        },
        cartItems:{
            deep:true,
            handler: 'totalMoneyMethod'
        },
    },
    computed: {
        selectAll: {
            get() {
                if (this.cartItems && this.cartItems.length > 0) { // A users array exists with at least one item
                let allChecked = true;
                for (const item of this.cartItems) {
                    if (!this.cartItemSelected.includes(item.CartItemsId)) {
                        allChecked = false; // If even one is not included in array
                        }
                        
                        // Break out of loop if mismatch already found
                        if(!allChecked) break;
                    }

                    return allChecked;
                }
                return false;
            },
            set(value) {
                const checked = [];

                if (value) {
                    this.cartItems.forEach((item) => {
                        checked.push(item.CartItemsId);
                    });
                }
                this.cartItemSelected = checked;
                localStorage.setItem("CartSelected",this.cartItemSelected);
            }
        }
    },
    methods: {
        async checkCartItem(){
            var arrayCartItem = this.cartItems;
            for (const item of arrayCartItem) {
                console.log(item);
                if (item.ProductStock == 0) {
                    console.log("abc");
                    await this.deleteCartItems(item.CartItemsId);
                }
            }
        },
        setLocalStorageCartSelect(){
            localStorageService.setItemToLocalStorage("CartSelected",this.cartItemSelected);
        },

        totalMoneyMethod(){
            this.totalMoney = 0;
            this.cartItems.forEach(item=>{
                if(item){
                    this.totalMoney += item.Quantity * item.ProductPrice;
                }
                else{
                    this.totalMoney = 0;
                }
            })
        },
        async deleteCartItems(id){
             await cartItemsService.delete(id);
             cartLocalStorageService.removeItemGetOutCart(id);
             this.emitter.emit("takeNumberOfCart");
            this.getCartItemsFormLocal();
        },
         decreaseQuantity(id){
            var data = {};
            const formData = new FormData();
            var arrCartItems = this.cartItems;
            arrCartItems.forEach(item => {
                if(item.CartItemsId == id ){
                    item.Quantity = item.Quantity - 1;
                    if(item.Quantity < 1){
                        item.Quantity = 1;
                        this.emitter.emit("showToast",this.Enum.ToastType.FAILED,"Số lượng không nhỏ hơn 0 !")
                    }else{
                        data.ProductId = item.ProductId;
                        data.CartsId = item.CartsId;
                        data.CartItemsId = id;
                        data.Quantity = item.Quantity;
                        formData.append("dataJson",JSON.stringify(data));
                        cartItemsService.put(id,formData);
                        localStorage.setItem("CartItems",JSON.stringify(arrCartItems)); 
                    }
                }
            });
            
                     
        },
         increaseQuantity(id){
            var data = {};
            const formData = new FormData();
            var arrCartItems = this.cartItems;
            arrCartItems.forEach(item => {
                if(item.CartItemsId == id){
                    item.Quantity = item.Quantity + 1
                    if(item.Quantity > item.ProductStock){
                        item.Quantity = item.ProductStock
                        this.emitter.emit("showToast",this.Enum.ToastType.FAILED,"Số lượng đã vượt quá số hàng còn lại !")
                    }else{
                        this.errorQuantity = "";
                        data.ProductId = item.ProductId;
                        data.CartsId = item.CartsId;
                        data.CartItemsId = id;
                        data.Quantity = item.Quantity;
                        formData.append("dataJson",JSON.stringify(data));
                        cartItemsService.put(id,formData);
                    }
                    
                }
            });
            localStorage.setItem("CartItems",JSON.stringify(arrCartItems));          
        },
        takeDataUsers(){
            this.users = JSON.parse(localStorage.getItem("User"));
        },
        async getCartItemsFormLocal(){
            var item = localStorage.getItem("CartItems");
            if(item){
                 this.cartItems = await JSON.parse(item);
            }
            else{
                this.cartItems = [];
            }
            await this.checkCartItem();
        },
        
        pathImage(valueId){
             return "https://localhost:7242/" + valueId;
        },
        formatMoney(amount) {
            return amount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".")+ " ₫";
        }
    },
}
</script>

<style scope>
.cart-list-noitem{
    padding-bottom: 20px;
    width: 100%;
    text-align: center;
    color: #ccc;
    font-style: italic;
    font-size: 20px;
    font-weight: bold;
}
.cart-action{
    margin: 20px 50px;
    justify-content: flex-end;
}
.cart-action a:hover{
    color: #1ea2d2;
}

.cart-checkout{
    display: flex;
    justify-content: center;
    align-items: center;
    cursor: pointer;
    color: #fff;
    font-size: 17px;
    background-color: #a2c5d2;
    width: 200px;
    height: 50px;
}

.cart-back-home{
    display: flex;
    justify-content: center;
    align-items: center;
    font-size: 17px;
    cursor: pointer;
    margin: 0px 10px;
    width: 200px;
    height: 50px;
    color: #000;
    background-color: #e6e5e5;
}
.cart-total-money{
    margin-right: 20px;
    font-size: 20px;
    font-weight: bold;
        color: #a2c5d2;
}
.cart-total-title{
    justify-content: end;
    font-size: 20px;
}
.product-manage-quantity{
    
    display: flex;
    justify-content: center;
    align-items: center;
}
.product-manage-quantity button:active{
    background-color: #1ea2d2;
}
.product-manage-quantity button{
    background-color: #a2c5d2;
    cursor: pointer;
    font-size: 20px;
    border: 1px solid #000;
    outline: none;
    min-width: 30px;
    min-height: 20px;
    font-weight: bold;
}
.product-manage-quantity input{
    text-align: center;
    font-weight: bold;
    outline: none;
    border: 1px solid #a2c5d2;
    width: 30px;
    height: 30px;
    border-color: #a2c5d2;
    box-shadow: none;
}
.product-description{
    padding: 0px !important;
    font-style: italic;
    font-size: 14px;
    width: 100%;
    white-space: nowrap; 
    overflow: hidden;
    text-overflow: ellipsis; 
}
.product-price{
        padding: 0px !important;
    font-size: 15px;
}
.product-name{

    font-weight: bold;
    padding: 0px !important;
    font-size: 17px;
}
.product-action{
    padding: 0px !important;
    font-size: 30px;
    cursor: pointer;
}
.header-item-checkbox,
.cart-item-product{
    display: flex;
    align-items: center;
    justify-content: left;
}
.cart-item-quantity,
.product-manage-quantity,
.cart-item-product{
    
    padding: 0px !important;
}
.cart-item
.cart-item-price,
.cart-item-quantity,
.cart-item-sum,
.cart-item-action,
.cart-item-product{

    text-align: center;
    width: 100%;
    flex: 1;
}
.cart-input-checkall{
    padding-right: 20px;
}
.cart-product-image{
    padding: 0px 20px;
}
.cart-product-image img{
    object-fit: cover;
    width: 100px;
    height: 110px;
}
.text-left{
    text-align: left !important;
}
.cart-header-item{
    text-align: center;
    width: 100%;
    padding: 20px 0px ;
}
.cart-product-infor {
    width: 100%;
    overflow: hidden;

}
.cart-list-item{
    flex-direction: column;
    border-bottom: 1px solid #ccc;
    margin: 20px 40px;
}
.cart-total{
    display: flex;
    justify-content: flex-end;
    margin: 20px 50px;
}
.cart-header{
    justify-content: space-between;
    font-weight: bold;
    font-size: 16px;
    margin: 20px 40px;
    border-bottom:1px solid #a2c5d2;
}
.s-cart{
    margin-bottom: 30px;
    background-color: #fff;
}
.s-cart-title h1{

    font-size: 25px;
    padding: 10px 10px;
}
.s-cart-content{
    border: 1px solid #a2c5d2;
    box-shadow: 0px 2px 46.41px 4.59px rgba(2,38,113,0.1);
}
.cart-item{
    margin: 0px;
    padding: 10px 0px 10px 0px;
    justify-content: space-between;
    align-items: center;
}
</style>