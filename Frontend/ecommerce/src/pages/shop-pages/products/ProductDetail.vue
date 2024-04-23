<template>
  <div class="container s-product-detail">
    <div class="row product-detail-content">
        <div class="col-lg-5 col-md-5 col-sm-5 col-5 product-detail-left">
            <div class="product-detail-image">
                <div class="product-list-image">
                    <img :src="checkImagePath(this.product.ProductId)" alt="">
                </div>
                <div class="product-image-main">
                    <img :src="checkImagePath(this.product.ProductId)" alt="">
                </div>
            </div>
        </div>
        <div class="col-lg-7 col-md-7 col-sm-7 col-7 product-detail-right">
            <div class="product-detail-name">
                {{this.product.ProductName}}
            </div>
            <div class="product-detail-infor">
                <div>
                    <div class="detail-infor-title">Mô tả: <span class="detail-infor-content">{{this.product.ProductDescription}}</span></div>
                    <div class="detail-infor-title">Số lượng tỏng kho: <span class="detail-infor-content">{{this.product.ProductStock}}</span></div>
                    <div class="detail-infor-title">Đã bán: <span class="detail-infor-content">{{this.product.ProductSold}}</span></div>
                    <div class="detail-infor-title">Nhà sản xuất: <span class="detail-infor-content">{{this.product.ProductBrandName}}</span></div>
                </div>
            </div>
            <div class="product-detail-delivery">
                <div>Thời gian giao hàng:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </div>
                <div>
                    <div class="product-delivery-address">
                        Giao đến..........
                    </div>
                    <div class="product-delivery-date">
                        Dự kiến giao ......
                    </div>
                </div>
            </div>
            <div class="product-detail-warranty">
                <div>Chính sách bảo hành:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                <div>
                    <div class="product-warranty-date">
                        Doi tra trong vong 30 ngay
                    </div>
                </div>
            </div>
            <div class="product-detail-price">
                <div class="detail-price">999999đ</div>
                <div class="price-after-discount">9999999đ
                </div>
                    <div class="amount-discount">-99%</div>
            </div>
            <div class="product-detail-quantity">
                <div class="product-quantity-title">Số lượng: </div>
                <div class="product-quantity-button">
                    <i @click="decreaseQuantity()" class="fa-solid fa-minus"></i>
                    <input type="number" v-model="this.quantity">
                    <i @click="increaseQuantity()" class="fa-solid fa-plus"></i>
                </div>
            </div>
            <div class="product-detail-action">
                <button @click="addToCart(this.product.ProductId)" class="add-to-cart">
                    <i class="fa-solid fa-cart-plus"></i> 
                     &nbsp; Thêm vào giỏ hàng
                </button>
                <button class="buy-now">Mua ngay</button>
            </div>
        </div>
    </div>
    <div class="product-detail-more">
        <table>
            <tr>
                <th>Mã sản phẩm: </th>
                <td>ABC</td>
            </tr>
            <tr>
                <th>Nhà sản xuất: </th>
                <td>ABC</td>
            </tr>
            <tr>
                <th>Chất liệu: </th>
                <td>ABC</td>
            </tr>
            <tr>
                <th>Trọng lượng: </th>
                <td>ABC</td>
            </tr>
            <tr>
                <th>Năm sản xuất: </th>
                <td>ABC</td>
            </tr>
            <tr>
                <th>Mô tả: </th>
                <td>ABC</td>
            </tr>
            <tr>
                <th>Chính sách bảo hành: </th>
                <td>ABC</td>
            </tr>

        </table>
    </div>
  </div>
</template>

<script>
import localStorageService from '../../../js/storage/LocalStorageService';
import imagesService from "../../../utils/ImagesService";
import cartItemsService from "../../../utils/CartItemsService"
import productService from '../../../utils/ProductService';
export default {
    props:['slug'],
    data() {
        return {
            quantity:1,
            product:{},
            images:[],
        }
    },
    created() {
        this.takeDataProduct();
        this.takeDataImages();
    },
    methods: {
        async takeDataProduct(){
            var res = await productService.getProductBySlug(this.slug);
            this.product = res.data;
        },
        async takeDataImages(){
            try{
                var res = await imagesService.getAll();
                this.images = res.data;
            }catch(error){
                console.log(error);
            }
        },
        checkImagePath(valueId) {
            let imageItem = this.images.filter(
                (item) => item.ProductId === valueId
            );
            if (imageItem.length > 0) {
                return "https://localhost:7242/" + imageItem[0].ImagesPath;

            } else {
                return "https://localhost:7242/images/no-image.jpg";
            }
        },
        decreaseQuantity(){
            if(this.quantity <= 1){
                this.quantity = 1;
            }else{
                this.quantity -= 1;

                
            }
        },
        increaseQuantity(){
            if(this.quantity >= this.product.ProductStock){
                this.quantity = this.product.ProductStock;
            }
            else{
                this.quantity = this.quantity + 1;
            }
        },
        async addToCart(id){
            var user = localStorageService.getItemFromLocalStorage("User")
            if(!user){
                location.assign("http://localhost:8080/login")
            }else{
                var data = {};
                data.ProductId = id;
                data.Quantity = this.quantity;
                data.CartsId = user.CartsId;
                await cartItemsService.insertCartItems(data);
                var res = await cartItemsService.getByUserId(user.UsersId);
                localStorageService.setItemToLocalStorage("CartItems",res.data);
                this.emitter.emit("takeNumberOfCart");
            }
        },
    },
}
</script>

<style scoped>
input[type="number"]::-webkit-outer-spin-button,
input[type="number"]::-webkit-inner-spin-button {
    -webkit-appearance: none;
    margin: 0;
}
.product-quantity-button i:last-child{
    cursor: pointer;
    border-radius: 0px 5px 5px 0px;
    display: flex;
    align-items: center;
    width: 50px;
    height: 35px;
    padding: 0px 20px;
    outline: none;
    background-color: #a2c5d2;
    color: #fff;
}
.product-quantity-button i:first-child{
        cursor: pointer;
    border-radius: 5px 0px 0px 5px;
    display: flex;
    align-items: center;
    width: 50px;
    height: 35px;
    padding: 0px 20px;
    outline: none;
    background-color: #a2c5d2;
    color: #fff;
}
.product-quantity-button input{
    color: #fff;
    text-align: center;
    font-size: 20px;
    font-weight: bold;
    border: unset;
    width: 45px;
    height: 35px;
    outline: none;
    background-color: #a2c5d2;
}
.product-quantity-button{
    display: flex;
    align-items: center;
    padding: 0px 40px;
}
.product-warranty-date,
.product-delivery-date,
.product-delivery-address{
    font-weight: bold;
}
.product-detail-quantity{
    font-weight: bold;
    font-size: 19px;
    display: flex;
    padding: 15px 0px;
}

.product-detail-warranty,
.product-detail-delivery{
    display: flex;
    padding: 5px 0px;
}
.amount-discount{
    font-weight: bold;
    border-radius: 8px;
    background: #a2c5d2;
    color: #fff;
    font-size: 15px;
    padding: 2px 5px;
}
.price-after-discount{
    padding-right: 10px;
    color: #a2c5d2;
    font-size: 19px ;
    text-decoration: line-through;
}
.detail-price{
    padding-right: 15px;
    color: #46a9cc;
    font-size: 33px;
    font-weight: bold;
}
.product-detail-price{
    display: flex;
    align-items: center;
}
.detail-infor-content{
    padding: 0px 10px;
    color: #a2c5d2;
    font-weight: bold;
}
.detail-infor-title{
    padding: 5px 0px;
}
.product-detail-name{
    font-weight: bold;
    color: #46a9cc;
    font-size: 25px;
}
.add-to-cart i{
    font-size: 20px;
}
.product-detail-action .buy-now{
    border: 3px solid #a2c5d2 !important;
    font-size: 16px;
    font-weight: bold;
    color: #FFF;
    border-radius: 8px;
    cursor: pointer;
    padding: 0px 20px;
    outline: none;
    width: 250px;
    height: 50px;
    background-color: #a2c5d2;
    margin-left: 20px;
}
.product-detail-action .buy-now:hover{
    background-color: #FFF;
    color:#a2c5d2;
    transition: 0.3s;
}
.product-detail-action .add-to-cart:hover{
    background-color: #a2c5d2;
    color:#fff;
        transition: 0.3s;
}
.product-detail-action .add-to-cart{
    padding: 0px 10px;
    display: flex;
    justify-content: space-between;
    align-items: center;
    font-size: 16px;
    font-weight: bold;
    color: #a2c5d2;
    border-radius: 8px;
    cursor: pointer;
    width: 230px;
    height: 50px;
    outline: none;
    border: 3px solid #a2c5d2 !important;
    background-color: #fff;
}
.product-detail-action{
    display: flex;
    justify-content: space-around;
    align-items: center;
}
.product-image-main img{
    width: 360px;
    height: 400px;
}
.product-list-image img:hover{
    border: 1px solid #a2c5d2;
    border-radius: 8px;
}
.product-list-image img {
    width: 70px;
    height: 70px;
}
 .product-detail-image{
    display: flex;
    justify-content: space-between;
    margin-bottom: 40px;
 }
.product-detail-content{
    padding: 15px 15px;
    border-radius: 10px;
    box-shadow: 0px 2px 46.41px 4.59px rgba(2,38,113,0.1);
}
</style>