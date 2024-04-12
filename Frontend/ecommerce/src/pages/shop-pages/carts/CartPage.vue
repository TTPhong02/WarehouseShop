<template>
<div>
    <HeadingShop></HeadingShop>
    <div class="s-cart">
        <div class="container">
            <div class="s-cart-title">
                <h1>Giỏ hàng của bạn </h1>
            </div>
            <div class="s-cart-content">
                <div class="row cart-header">
                    <div class="col-lg-6 col-md-7 col-sm-7 cart-header-item">Sản phẩm</div>
                    <div class="col-lg-1 col-md-1 col-sm-1 cart-header-item">Giá </div>
                    <div class="col-lg-2 col-md-1 col-sm-1 cart-header-item">Số lượng</div>
                    <div class="col-lg-2 col-md-2 col-sm-2 cart-header-item">Thành tiền</div>
                    <div class="col-lg-1 col-md-1 col-sm-1 cart-header-item">Thao tác</div>
                </div>
                <div class="cart-list-item">
                    <div class="row cart-item">
                        <div class="row col-lg-6 col-md-7 col-sm-7 col-7 cart-item-product">
                            <div class="cart-product-image">
                                <img src="../../../assets/img/sanpham1.jpg" alt="">
                            </div>
                            <div class="cart-product-infor">
                                <div class="product-name">Tên sản phẩm</div>
                                <div class="product-description">Mô tả ở đây</div>
                            </div>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-1  cart-item-price">
                            <div class="product-price">999999đ</div>
                        </div>  
                        <div class="col-lg-2 col-md-1 col-sm-1 col-1 row cart-item-quantity">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-12 product-manage-quantity">
                                <button>-</button>
                                <input type="text" readonly>
                                <button>+</button>
                            </div>
                            <div class="d-lg-none d-md-none col-sm-12 col-12 product-action">
                                <i class="fa-solid fa-xmark"></i>
                            </div>
                        </div>
                        <div class="col-lg-2 col-md-2 col-sm-2 col-2 cart-item-sum">
                            <div class="product-sum product-price">999999đ</div>
                        </div>
                        <div class="col-lg-1 col-md-1 col-sm-1 col-1  cart-item-action">
                            <div class="product-action">
                                <i class="fa-regular fa-trash-can"></i>
                            </div>
                        </div>
                    </div>                                        
                </div>
                <div class="row cart-total">
                    <div class="col-lg-2 col-md-2 col-sm-2 col-2  cart-total-title">
                        Tổng tiền: 
                    </div>
                    <div class="col-lg-1 col-md-1 col-sm-1 col-1  cart-total-money produc-price">
                        9999999đ
                    </div>
                </div>
                <div class="row cart-action">
                    <button class="col-lg-3 col-md-3 col-sm-3 col-3 cart-back-home">Tiếp tục mua hàng
                    </button>
                    <button class="col-lg-3 col-md-3 col-sm-3 col-3 cart-checkout">
                        Tiến hành thanh toán
                    </button>
                </div>
            </div>
        </div>
    </div>
</div>
</template>

<script>

import HeadingShop from '../../../layout/LayoutUser/HeadingShop.vue'
export default {
    components:{
        HeadingShop
    }
}
</script>

<style scope>
.cart-action{
    margin: 20px 50px;
    justify-content: flex-end;
}
.cart-checkout{
    cursor: pointer;
    color: #fff;
    font-size: 16px;
    background-color: #a2c5d2;
    width: 200px;
    height: 50px;
}
.cart-back-home{
        font-size: 16px;
    cursor: pointer;
    margin: 0px 10px;
    width: 200px;
    height: 50px;
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
.product-manage-quantity button{
    cursor: pointer;
    font-size: 20px;
    border: 1px solid #000;
    outline: none;
    min-width: 30px;
    min-height: 20px;
}
.product-manage-quantity input{
    outline: none;
    /* border: none; */
    width: 30px;
    height: 30px;
}
.product-description{
    font-style: initial;
    font-size: 14px;
}
.product-price{
    font-size: 17px;
}
.product-name{
    font-size: 16px;
}
.product-action{
    font-size: 30px;
    cursor: pointer;
}
.cart-product-image{
    padding: 10px 20px;
}
.cart-product-image img{
    width: 100px;
    height: 110px;
}
.cart-header-item{
    padding: 20px 10px ;
}
.cart-list-item{
    border-bottom: 1px solid #ccc;
    margin: 20px 50px;
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
    justify-content: space-between;
    align-items: center;
}
</style>