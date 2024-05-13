<template>
<div>
    <div v-for="item in categories" :key="item.CategoriesId" class="s-home-productCategories">
        <div class="s-productCategories-wrap">
            <div class="productCategories-wrap-title">
                Nhóm {{item.CategoriesName}}
            </div>
        </div>
        <swiper :slides-per-view="5"
        :space-between="35" 
        navigation
         class="row s-productCategories-list"
         >
            <swiper-slide v-for="product in this.productByCategories[item.CategoriesId]" :key="product.ProductId" class="s-product-product">
                <div class="s-product-image">
                    <img :src="checkImagePath(product.ProductId)" alt="">
                </div>
                <div class="s-product-infor">
                    <div class="producthot-name">{{product.ProductName}}</div>
                    <div class="product-price">{{this.helper.formatMoney(product.ProductPrice)}}</div>                           
                </div>
                <div class="product-select">
                    <router-link :to="'/product/' + product.ProductSlug" class="product-detail">
                        <i class="fa-regular fa-eye"></i>
                        Xem 
                    </router-link>
                    <div v-on:click="addToCart(product.ProductId)" class="product-cart">
                        <i class="fa-solid fa-cart-plus"></i>
                        Thêm
                    </div>
                </div>
            </swiper-slide>
        </swiper>
    </div>
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
import categoriesService from '../../../utils/CategoriesService';

import {reactive} from 'vue'
export default {
    components: {
      Swiper,
      SwiperSlide,
    },
    data() {
        return {
            categories:[],
            productByCategories:reactive({}),
            images:[],
            user:{},
            modules: [Navigation],
        }
    },
    mounted() {
        this.takeDataCategories();
        this.takeDataUsers();
        this.takeDataImages();
    },
    methods: {
        async takeDataCategories(){
            var res = await categoriesService.getAll();
            this.categories = res.data;
            await this.takedataProduct();
        },
        async takeDataUsers(){
            this.user = await JSON.parse(localStorage.getItem("User"));
        },
        async takedataProduct(){
            try{
                for (const category of this.categories) {
                    const products = await productService.getProductByCategories(category.CategoriesId);
                    this.productByCategories[category.CategoriesId] = products.data;
                }   
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
            if(!this.user){
                location.assign("http://localhost:8080/login")
            }else{
                var data = {};
                data.ProductId = id;
                data.Quantity = 1;
                data.CartsId = this.user.CartsId;
                await cartItemsService.insertCartItems(data);
                this.emitter.emit("showToast",this.Enum.ToastType.SUCCESS,"Đã thêm vào giỏ hàng !")
                var res = await cartItemsService.getByUserId(this.user.UsersId);
                localStorage.setItem("CartItems",JSON.stringify(res.data));
                this.emitter.emit("takeNumberOfCart");
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
    font-weight: bold;
}
.s-productCategories-list{
    padding: 20px 0px;
    margin: 20px 0px;
    box-shadow: 0px 0px 30px 0px #ebecec;
}
.s-product-item{
    margin: 0px 50px;
}
.swiper-button-prev{
    color: #a2c5d2 !important;
}
.s-product-infor{
    padding: 10px 0px;
}
.productCategories-wrap-text{
    font-size: 16px;
    color: #fff;
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
.productCategories-wrap-title{
    text-transform: uppercase;;
    font-size: 30px;
    font-weight: bold;
    color: azure;
    -webkit-animation: my 1000ms infinite;
    -moz-animation: my 1000ms infinite; 
    -o-animation: my 1000ms infinite; 
    animation: my 1000ms infinite;
}
.s-productCategories-wrap{
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