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
                    <div class="detail-infor-title">Số lượng trong kho: <span class="detail-infor-content">{{this.product.ProductStock}}</span></div>
                    <div class="detail-infor-title">Đã bán: <span class="detail-infor-content">{{this.product.ProductSold}}</span></div>
                    <div class="detail-infor-title">Nhà sản xuất: <span class="detail-infor-content">{{this.product.ProductBrandName}}</span></div>
                </div>
            </div>
            <div class="product-detail-delivery">
                <div>Thời gian giao hàng:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </div>
                <div>
                    <div class="product-delivery-address">
                        - Giao tới: <em>{{this.address.HomeNumber}},{{this.address.Ward}},{{this.address.District}},{{this.address.Province}}</em>
                    </div>
                    <div class="product-delivery-date">
                        - Dự kiến giao:  <em>{{this.helper.formatDateJS(this.dateDelivery)}}</em>
                    </div>
                </div>
            </div>
            <div class="product-detail-warranty">
                <div>Chính sách bảo hành:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
                <div>
                    <div class="product-warranty-date">
                        Đổi trả trong vòng 30 ngày
                    </div>
                </div>
            </div>
            <div class="product-detail-price">
                <div v-if="this.discount > 0" class="detail-price">{{this.helper.formatMoney(this.product.ProductPrice * this.discount)}}</div>
                <div v-if="this.discount == 0" class="detail-price">{{this.helper.formatMoney(this.product.ProductPrice)}}</div>
                <div v-if="this.discount > 0" class="price-after-discount">{{this.helper.formatMoney(this.product.ProductPrice)}}
                </div>
                <div class="amount-discount">-{{this.discount}}%</div>
            </div>
            <div class="product-detail-quantity">
                <div class="product-quantity-title">Số lượng: </div>
                <div class="product-quantity-button">
                    <i @click="decreaseQuantity()" class="fa-solid fa-minus"></i>
                    <input type="number" v-model="this.quantity">
                    <i @click="increaseQuantity()" class="fa-solid fa-plus"></i>
                </div>
                <span v-if="this.errorQuanity?.length > 0" class="error">{{this.errorQuanity}}</span>
            </div>
            <div class="product-detail-action">
                <button @click="addToCart(this.product.ProductId)" class="add-to-cart">
                    <i class="fa-solid fa-cart-plus"></i> 
                     &nbsp; Thêm vào giỏ hàng
                </button>
                <button @click="buyNow(this.product.ProductId)" class="buy-now">Mua ngay</button>
            </div>
        </div>
    </div>
    <div class="s-product-related">
        <div class="product-related-header">Sản phẩm liên quan</div>
        <swiper :slides-per-view="7"
        :space-between="35" 
        :pagination="{ clickable: true }"
        :modules="modules"
        navigation
         class="row s-productRelated-list"
         >
            <swiper-slide v-for="item in productRelated" :key="item.ProductId" class="s-product-item">
                <router-link 
                    :to="'/product/' + item.ProductSlug"
                >
                    <div class="s-productRelated-image">
                        <img :src="checkImagePath(item.ProductId)" alt="">
                    </div>
                    <div class="s-productRelated-infor">
                        <div class="productRelated-name">{{item.ProductName}}</div>
                        <div class="productRelated-price">{{this.helper.formatMoney(item.ProductPrice)}}</div>                           
                    </div>
                </router-link>
            </swiper-slide>
        </swiper>
    </div>
    <div class="product-detail-more">
        <div class="product-detail-header">Thông tin sản phẩm</div>
        <table>
            <tr>
                <th>Mã sản phẩm: </th>
                <td>{{this.product.ProductCode}}</td>
            </tr>
            <tr>
                <th>Nhà sản xuất: </th>
                <td>{{this.product.ProductBrandName}}</td>
            </tr>
            <tr>
                <th>Chất liệu: </th>
                <td>{{this.product.Material}}</td>
            </tr>
            <tr>
                <th>Trọng lượng: </th>
                <td>{{this.product.ProductWeight}} kg</td>
            </tr>
            <tr>
                <th>Năm sản xuất: </th>
                <td>{{this.product.YearOfProduce}}</td>
            </tr>
            <tr>
                <th>Công dụng: </th>
                <td>{{this.product.ProductUses}}</td>
            </tr>
            <tr>
                <th>Mô tả: </th>
                <td>{{this.product.ProductDescription}}</td>
            </tr>
            <tr>
                <th>Chính sách bảo hành: </th>
                <td>Bảo hành 1 đổi 1 trong vòng 30 ngày</td>
            </tr>

        </table>
    </div>

  </div>
</template>

<script>
import { Navigation} from 'swiper/modules';
import { Swiper, SwiperSlide } from 'swiper/vue';
import 'swiper/css';
import 'swiper/css/navigation';
import localStorageService from '../../../js/storage/LocalStorageService';
import imagesService from "../../../utils/ImagesService";
import cartItemsService from "../../../utils/CartItemsService"
import productService from '../../../utils/ProductService';
import addressService from '../../../utils/AddressService';
export default {
    props:['slug'],
    components: {
      Swiper,
      SwiperSlide,
    },
    data() {
        return {
            modules: [Navigation],
            discount:0,
            dateDelivery:null,
            address:{},
            quantity:1,
            product:{},
            images:[],
            errorQuanity:null,
            productRelated:[],
        }
    },
    watch:{
        slug(newValue){
            if(newValue){
                window.location.reload();
            }
        },
        quantity(newValue){
            if(newValue > this.product.ProductStock){
                this.errorQuanity = "Số lượng sản phẩm còn lại không đủ";
                this.quantity = this.product.ProductStock;
            }else if(newValue <= 0){
                this.errorQuanity = "Số lượng không được nhỏ hơn 1";
                this.quantity = 1;
            }
        }
    },
    mounted() {
        this.takeDataProduct();
        this.takeDataImages();
        this.takeAddress();
    },
    methods: {
        async takeProductRelated(){
            var res = await productService.getProductByCategories(this.product.CategoriesId);
            this.productRelated = res.data;
            console.log(this.productRelated);
        },
        async calculateDelivery(){
            var date = new Date();
            if(this.address.Province !== null ||this.address.Province !== "" ||this.address.Province !== undefined ){
                if(this.address.Province.includes("Hà Nội")){
                    date.setDate(date.getDate() + 2);
                    this.dateDelivery = date;
                }else{
                    date.setDate(date.getDate() + 3);
                    this.dateDelivery = date;
                }
            }else{
                date.setDate(date.getDate() + 3);
                this.dateDelivery = date;
            }
        },
        async takeAddress(){
            var user =await localStorageService.getItemFromLocalStorage("User");
            if(user){
                var res = await addressService.getAddressDefaultById(user.UsersId);
                this.address = res;
                await this.calculateDelivery();
            }
            else{
                this.address = "";
            }

        },
        async takeDataProduct(){
            var res = await productService.getProductBySlug(this.slug);
            this.product = res.data;
            await this.takeProductRelated();
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
                this.errorQuanity = "Số lượng không được nhỏ hơn 1";
            }else{
                this.errorQuanity = "";
                this.quantity -= 1;
            }
        },
        increaseQuantity(){
            if(this.quantity >= this.product.ProductStock){
                this.errorQuanity = "Số lượng sản phẩm còn lại không đủ";
                this.quantity = this.product.ProductStock;
            }
            else{
                this.errorQuanity = "";
                this.quantity = this.quantity + 1;
            }
        },
        async buyNow(id){
            this.addToCart(id);
            this.$router.push("/cart");
        },
        async addToCart(id){
            var user = await localStorageService.getItemFromLocalStorage("User");
            if(!user){
                location.assign("http://localhost:8080/login")
            }else{
                var data = {};
                data.ProductId = id;
                data.Quantity = this.quantity;
                data.CartsId = user.CartsId;
                await cartItemsService.insertCartItems(data);
                this.emitter.emit("showToast",this.Enum.ToastType.SUCCESS,"Đã thêm vào giỏ hàng !")
                var res = await cartItemsService.getByUserId(user.UsersId);
                localStorage.setItem("CartItems",JSON.stringify(res.data));
                this.emitter.emit("takeNumberOfCart");
                window.location.reload();
            }
        },
    },
}
</script>

<style scoped>
td{
    padding: 8px 10px !important;
}
.product-detail-more{
    background-color: #fff;
    box-shadow: 0px 2px 46.41px 4.59px rgba(2,38,113,0.1);
    margin: 0px;
    padding: 20px;
    border-radius: 10px;
    margin: 0px -15px;
}
.product-detail-header{
    font-size: 20px;
    font-weight: bold;
}
.product-related-header{
    padding: 10px 10px;
    font-size: 20px;
    font-weight: bold;
}
.productRelated-price{
    color: #a2c5d2;
}
.productRelated-name{
    width: 130px;
    white-space: nowrap; 
    overflow: hidden;
    text-overflow: ellipsis; 
    font-weight: bold;
    padding: 5px 0px;
    color: #000;
}
.s-productRelated-image img{
    object-fit: cover;
    width: 150px;
    height: 150px;
}
.s-product-item{
    box-shadow: none;
}
.s-product-related{
    margin: 10px -15px;
    box-shadow: 0px 2px 46.41px 4.59px rgba(2,38,113,0.1);
    padding: 5px 10px;
    background-color: #fff;
    border-radius: 10px;
}
.error{
    color:red;
    font-size: 13px;
    font-style: italic;
}
em{
    color: #a2c5d2 ;
    font-weight: 100;
}
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
    object-fit: cover;
    width: 360px;
    height: 400px;
}
.product-list-image img:hover{
    border: 1px solid #a2c5d2;
    border-radius: 8px;
}
.product-list-image img {
    object-fit: cover;
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