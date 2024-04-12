<template>
  <div class="row s-checkout">
    <div class="col-lg-8 col-md-8 col-sm-8 col-8 checkout-left">
      <div class="row checkout-header">
        <div class="iamge-logo">
          <img src="../../../assets/img/logo.png" alt="" />
        </div>
      </div>
      <div class="row checkout-content">
        <div class="col-lg-6 col-md-6 col-sm-6 col-6 checkout-content-address">
          <div class="checkout-address-header">Thông tin nhận hàng</div>
          <div class="checkout-address-form">
            <input type="email" placeholder="Email" />
            <input type="text" placeholder="Họ và tên" />
            <input type="text" placeholder="Số diện thoại" />
            <input type="text" placeholder="Địa chỉ" />
            <input type="text" placeholder="Tỉnh thành" />
            <input type="text" placeholder="Quận huyện" />
            <input type="text" placeholder="Phường Xã" />
            <textarea placeholder="Ghi chú"></textarea>
          </div>
        </div>
        <div class="col-lg-6 col-md-6 col-sm-6 col-6 checkout-content-right">
          <div class="checkout-delivery">
            <div class="checkout-payment-header">
                Vận chuyển
            </div>
            <div class="checkout-label">
                <input type="radio" value="COD">
                <span for="">Giao hàng tận nơi</span>
            </div>
          </div>
          <div class="checkout-payment-method">
            <div class="checkout-payment-header">
                Thanh toán
            </div>
            <div class="checkout-label">
                <input type="radio" value="COD">
                <span for="">Thanh toán khi giao hàng (COD)</span>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="col-lg-4 col-md-4 col-sm-4 col-4 checkout-right">
        <div class="checkout-cart-header">
            Đơn hàng (10 sản phẩm)
        </div>
        <div class="checkout-cart-content">
            <div class="checkout-cart-list">
                <div class="row checkout-cart-item">
                    <div class="col-lg-2 col-md-2 col-sm-2 col-2 checkout-item-image">
                        <img src="../../../assets/img/sanpham1.jpg" alt="">
                    </div>
                    <div class="col-lg-7 col-md-7 col-sm-7 col-7 checkout-item-infor ">
                        <div class="checkout-item-name">Tên sản phẩm</div>
                        <div class="checkout-item-description">Mô tả</div>
                    </div>
                    <div class="col-lg-3 col-md-3 col-sm-3 col-3 checkout-item-money">
                        <div class="checkout-item-total">999999đ</div>
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
                    <div class="payment-item-content">999999đ</div>
                </div>
                <div class="checkout-payment-item">
                    <div class="payment-item-title">Phí vận chuyển</div>
                    <div class="payment-item-content">999đ</div>
                </div>
            </div>
            <div class="checkout-total-total">
                <div class="checkout-payment-item">
                    <div class="payment-item-title">Tổng cộng</div>
                    <div class="payment-item-content">10000000đ</div>
                </div>
            </div>
            <div class="checkout-action-other">
                <router-link to="/cart">
                <i class="fa-solid fa-chevron-left"></i>    
                Quay về giỏ hàng
                </router-link>
                <button>Đặt hàng</button>
            </div>
        </div>
    </div>
  </div>
</template>

<script>
export default {};
</script>

<style scoped>
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
    height: 50px;
    display: flex;
    justify-content: flex-start;
    align-items: center;
    border: 1px solid #ccc;
    background-color: #fff;
}
.checkout-address-form textarea,
.checkout-address-form input {
    outline: none;
    border-radius: 4px;
    border: 1px solid #ccc;
    padding: 5px 8px;
    margin: 5px 0px;
    width: 100%;
    height: 50px;
}
.checkout-payment-header,
.checkout-address-header{
    font-size: 18px;
    font-weight: bold;
}
.iamge-logo img{
    width: 350px;
    height: 150px;
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

</style>
