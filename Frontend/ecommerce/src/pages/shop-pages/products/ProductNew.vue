<template>
  <div class="s-product-new">
    <div class="s-wrap-text">
        <div class="container">
            <h1>Sản Phẩm Mới</h1>
        </div>
    </div>
    <div class="s-product-main">
        <div class="container">
            <div class="row s-title-top">
                <div class="col-lg-10 col-md-10 col-sm-12 col-12 s-title">
                    Sản phẩm mới
                </div>
                <div class="col-lg-2 col-md-2 col-sm-12 col-12 s-option">
                    <div class="s-option-title">
                        Sắp xếp theo : 
                        <MCombobox></MCombobox>
                    </div>
                </div>
            </div>
            <div class="s-product">
                <div class="row s-product-list">
                    <div v-for="item in products" :key="item.ProductId" class="s-product-item">
                        <div class="s-product-image">
                            <img :src="checkImagePath(item.ProductId)" alt="">
                        </div>
                        <div class="s-product-infor">
                            <div class="product-name">{{item.ProductName}}</div>
                            <div class="product-price">{{formatMoney(item.ProductPrice)}}</div>                           
                        </div>
                        <div class="product-select">
                            <div class="product-detail">
                                <i class="fa-regular fa-eye"></i>
                                Xem 
                            </div>
                            <div v-on:click="addToCart(item.ProductId)" class="product-cart">
                                <i class="fa-solid fa-cart-plus"></i>
                                Thêm
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
  </div>
</template>

<script>
import cartItemsService from "../../../utils/CartItemsService"; 
import producService from "../../../utils/ProductService"; 
import imagesService from "../../../utils/ImagesService";
export default {
    name:"ProductNew",
    data() {
        return {
            user:{},
            products:[],
            images:[],
            cartItems:[]
        }
    },
    watch:{
        localStorage(){
            this.getCartItemsFormLocal();
        }
    },
    created() {
        this.takedataProduct();
        this.takeDataImages();
        this.getCartItemsFormLocal();
        this.takeDataUsers();
    },
    methods: {
        async takeDataUsers(){
            this.user = await JSON.parse(localStorage.getItem("User"));
        },
        getCartItemsFormLocal(){
            var item = localStorage.getItem("CartItems");
            this.cartItems = JSON.parse(item);
        },
        async addToCart(id){
            if(!this.user){
                location.assign("http://localhost:8080/login")
            }else{
                var data = {};
                data.ProductId = id;
                data.Quantity = 1;
                data.CartsId = this.user.CartsId;
                console.log(data);
                await cartItemsService.insertCartItems(data);
                var res = await cartItemsService.getByUserId(this.user.UsersId);
                localStorage.setItem("CartItems",JSON.stringify(res.data));
                this.emitter.emit("takeNumberOfCart");
            }
        },
        async takedataProduct(){
            try{
                var res = await producService.getProductNew();
                this.products = res.data;
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
        formatMoney(amount) {
            return amount.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".")+ " ₫";
        }

    },
}
</script>

<style>
.s-product-list{
    flex-wrap: wrap;
}
.s-product-new{
    margin-bottom: 20px;
}
.product-select{
    cursor: pointer;
    width: 100%;
    background-color: #DFF4F3;
    display: flex;
    justify-content: space-around;
    align-items: center;
}

.product-cart:hover,
.product-detail:hover{
    background-color: #a2c5d2;
}
.product-cart,
.product-detail{
    font-size: 17px;
    width: 50%;
    text-align: center;
    /* border-top: 1px  solid #a2c5d2; */
    padding: 10px 0px;
}
.s-product-infor{
    display: flex;
    flex-direction: column;
    align-items: center;
}
.product-name{
    font-size: 17px;
}
.product-name:hover{
    color: #a2c5d2;
}
.product-price{
    color: #a2c5d2;
    font-size: 20px;
    font-weight: bold;  
}
.product-price,
.product-name{
    padding: 5px 0px;
}
.s-product-item {
    margin: 10px 17px;
    background-color: #ffffff;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    align-items: center;
    width: 200px;
    height: 330px;
    box-shadow: 0px 2px 46.41px 4.59px rgba(2,38,113,0.1);
}
.s-product-image img{
    width: 200px;
    height: 210px;
}
.s-title-top{
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin-bottom: 20px;
}
.s-title{
    font-size: 26px;
    font-weight: bold;
}
.s-wrap-text{
    background-color: #a2c5d2;
    text-align: center;
    color: #fff;
    padding: 30px;
    margin: 30px 0px;
}
</style>