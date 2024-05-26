<template>
    <div class="s-home-productnew">
        <div class="s-productnew-wrap">
            <div class="productnew-wrap-title">
                SẢN PHẨM MỚI NHẤT
            </div>
            <div class="productnew-wrap-text">
                Các dòng sản phẩm mới nhất với cực kì nhiều ưu đãi
            </div>
        </div>
        <swiper :slides-per-view="5"
        :space-between="35" 
        :pagination="{ clickable: true }"
        :modules="modules"
        navigation
         class="row s-productnew-list"
         >
            <swiper-slide v-for="item in productNew" :key="item.ProductId" class="s-product-item">
                <div class="s-product-image">
                    <img :src="checkImagePath(item.ProductId)" alt="">
                    <div class="tage-new">
                        Mới nhất
                    </div>
                    <div v-if="item.ProductStock == 0" class="tage-sold">
                        Hết
                    </div>
                
                </div>
                <div class="s-product-infor">
                    <div class="producthot-name">{{item.ProductName}}</div>
                    <div class="product-price">{{this.helper.formatMoney(item.ProductPrice)}}</div>                           
                </div>
                <div class="product-select">
                    <router-link @click="viewProduct(item.ProductId)" :to="'/product/' + item.ProductSlug" class="product-detail">
                        <i class="fa-regular fa-eye"></i>
                        Xem 
                    </router-link>
                    <div v-on:click="addToCart(item.ProductId)" class="product-cart">
                        <i class="fa-solid fa-cart-plus"></i>
                        Thêm
                    </div>
                </div>
            </swiper-slide>
        </swiper>
    </div>
</template>

<script>
  import { Navigation} from 'swiper/modules';
import { Swiper, SwiperSlide } from 'swiper/vue';
import 'swiper/css';
import 'swiper/css/navigation';
import productService from '../../../utils/ProductService';
import imagesService from '../../../utils/ImagesService';
import cartItemsService from '../../../utils/CartItemsService';
import viewedProductService from '../../../utils/ViewedProduct';
export default {
    components: {
      Swiper,
      SwiperSlide,
    },
    data() {
        return {
            productNew:[],
            images:[],
            user:{},
            modules: [Navigation],
        }
    },
    created() {
        this.takeDataUsers();
        this.takedataProduct();
        this.takeDataImages();
    },
    methods: {
        async viewProduct(id){
            try{
                var params ={
                    ProductId : id,
                    UsersId : this.user.UsersId
                }
                await viewedProductService.viewProduct({params});
            }catch(error){
                console.log(error);
            }
        },
        async takeDataUsers(){
            this.user = await JSON.parse(localStorage.getItem("User"));
        },
        async takedataProduct(){
            try{
                var res = await productService.getProductNew();
                this.productNew = res.data;
            }catch(error){
                console.log(error);
            }
        },
        async takeDataImages(){
            try{
                var res = await imagesService.getAll();
                this.images = res.data;
            }catch(error){
                console.log(error);
            }
        },
        async addToCart(id){
            this.viewProduct(id);
            if(!this.user){
                location.assign("http://localhost:8080/login")
            }else{
                var data = {};
                data.ProductId = id;
                var product  = await productService.getById(id);
                if(product.data.ProductStock > 0){
                    data.Quantity = 1;
                    data.CartsId = this.user.CartsId;
                    await cartItemsService.insertCartItems(data);
                    this.emitter.emit("showToast",this.Enum.ToastType.SUCCESS,"Đã thêm vào giỏ hàng !")
                    var res = await cartItemsService.getByUserId(this.user.UsersId);
                    localStorage.setItem("CartItems",JSON.stringify(res.data));
                    this.emitter.emit("takeNumberOfCart");
                }
                else{
                    this.emitter.emit("showToast",this.Enum.ToastType.FAILED,"Sản phẩm đã hết !")
                }
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
        }
    },
}
</script>

<style scoped>
.producthot-name{
    width: 170px;
    white-space: nowrap; 
    overflow: hidden;
    text-overflow: ellipsis; 
    font-weight: bold;
    text-align: center;
}
.s-productnew-list{
    padding: 20px 0px;
    margin: 20px 0px;
    box-shadow: 0px 0px 30px 0px #ebecec;
}

.swiper-button-prev{
    color: #a2c5d2 !important;
}
.s-product-infor{
    padding: 10px 0px;
}
.productnew-wrap-text{
    font-size: 16px;
    color: #fff;
}
.s-product-image img{
    object-fit: cover;
}
.s-product-image{
    position: relative;
}
.tage-sold{
    padding: 30px 30px;
    border-radius: 50%;
    background-color: rgba(56, 55, 55, 0.5);
    position: absolute;
    font-size: 20px;
    font-weight: bold;
    color: #ccc;
    top:80px;
    right: 55px;
}
.tage-new{
    background-color: #00afef;
    position: absolute;
    font-size: 14px;
    font-weight: bold;
    padding: 3px 10px;
    color: #fff;
    top: 0px;
}
@-webkit-keyframes my {
    0% { color: #f8790a; } 
    50% { color: #fff;  } 
    100% { color: #f8790a;} 
}
@-moz-keyframes my { 
    0% { color: #f8790a;  } 
    50% { color: #fff;  }
    100% { color: #f8790a;  } 
}
@-o-keyframes my { 
    0% { color: #f8790a; } 
    50% { color: #fff; } 
    100% { color: #f8790a;  } 
}
@keyframes my { 
    0% { color: #f8790a;  } 
    50% { color: #fff;  }
    100% { color: #f8790a;  } 
} 
.productnew-wrap-title{
    font-size: 30px;
    font-weight: bold;
    color: azure;
    -webkit-animation: my 1000ms infinite;
    -moz-animation: my 1000ms infinite; 
    -o-animation: my 1000ms infinite; 
    animation: my 1000ms infinite;
}
.s-productnew-wrap{
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    height: 120px;
    text-align: center;
    width: 100%;
    background-color: #a2c5d2;
}
</style>