<template>
<div class="container">
  <div class="row s-categories-main">
    <div class="col-lg-3 col-md-3 col-sm-3 col-3 s-categories-sidebar">
        <div class="categoreis-sidebar-header">
            KHOẢNG GIÁ
        </div>
        <div class="categories-sidebar-select">
            <div v-for="item in dataFilter" :key="item.label" class="categories-sidebar-item">
                <input type="checkbox" name="range-price" v-model="this.rangePrice" :value="{
                    minPrice:item.minValue,
                    maxPrice:item.maxValue,
                }">
                <label for="">{{this.helper.formatMoney(item.minValue)}} - {{this.helper.formatMoney(item.maxValue)}}</label>
            </div>
        </div>
    </div>
    <div class="col-lg-9 col-md-9 col-sm-9 col-9 categories-content">
        <div class="categories-content-header">
            <div class="categories-content-name">
                NHÓM {{this.categories.CategoriesName}}
            </div>
            <div class="categories-content-sort">
                <label for="">Sắp xếp theo :  </label>
                <select v-model="valueSort" name="sort" id="">
                    <option value="0">Mặc định</option>
                    <option value="0">A → Z</option>
                    <option value="1">Z → A</option>
                    <option value="2">Giá thấp nhất</option>
                    <option value="3">Giá cao nhất</option>
                </select>
            </div>
        </div>
        <div class="categories-content-product">
            <div v-if="productbyCategories.length == 0" class="categories-product-noitem">
                Không có sản phẩm !
            </div>
            <div v-for="item in productbyCategories" :key="item.ProductId" class="categories-product-item">
                <div class="categories-product-image">
                    <img :src="checkImagePath(item.ProductId)" alt="">
                </div>
                <div class="categories-product-infor">
                    <div class="product-infor-name">
                        {{item.ProductName}}
                    </div>
                    <div class="product-infor-price">
                        {{this.helper.formatMoney(item.ProductPrice)}}
                    </div>
                    <div class="categories-product-action">
                        <router-link :to="'/product/' + item.ProductSlug">
                            <i class="fa-regular fa-eye"></i>
                        Xem
                        </router-link>
                        <div v-on:click="addToCart(item.ProductId)" class="product-action-cart">
                            <i class="fa-solid fa-cart-plus"></i>
                            Thêm
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div v-if="numberPage.length > 0" class="categories-content-footer">
            <div class="categories-footer-paging">
                    <div v-if="this.filterData.pageNumber > 1 " @click="previousPage()" class="previous-page">
                        <i class="fa-solid fa-angle-left"></i>
                    </div>
                    <div class="number-page">
                        <div v-for="item in numberPage" :key="item" class="" @click="selectPage(item)" :class="{ 'page-active': this.filterData.pageNumber == item, 'number-page-item':true }">
                            {{item}}
                        </div>
                    </div>
                    <div v-if="this.filterData.pageNumber < this.paging.totalPage" @click="nextPage()" class="next-page">
                        <i class="fa-solid fa-angle-right"></i>
                    </div>
            </div>
        </div>
    </div>
</div>
  </div>
</template>

<script>
import cartItemsService from '../../../utils/CartItemsService';
import categoriesService from '../../../utils/CategoriesService'
import imagesService from '../../../utils/ImagesService';
import productService from '../../../utils/ProductService';
export default {
    props:['slug'],
    data() {
        return {
            categories:{},
            productbyCategories:[],
            user:{},
            images:[],
            valueSort:0,
            rangePrice:[],
            paging:{
                totalPage:null,
                totalRecord:null,
            },
            dataFilter:[
                {
                    label:"Price",
                    minValue:0,
                    maxValue:100000
                },
                {
                    label:"Price",
                    minValue:100000,
                    maxValue:500000
                },
                {
                    label:"Price",
                    minValue:500000,
                    maxValue:1000000
                },
                {
                    label:"Price",
                    minValue:1000000,
                    maxValue:3000000
                },
                {
                    label:"Price",
                    minValue:3000000,
                    maxValue:5000000
                },
            ],
            filterData:{
                pageSize:2,
                pageNumber:1,
                sortType:0,
                minPrice:null,
                maxPrice:null,
                searchString:"",
                slug:"",
            }
        }
    },
    mounted() {
        this.takeCategoriesBySlug();
        this.takeDataUsers();
        this.takeDataImages();
    },
    computed:{
        numberPage(){
            const totalVisiblePages = 5; // Số lượng trang hiển thị tối đa
            const pages = [];
            const startPage = Math.max(1, this.filterData.pageNumber - Math.floor(totalVisiblePages / 2));
            const endPage = Math.min(this.paging.totalPage, startPage + totalVisiblePages - 1);
            for (let page = startPage; page <= endPage; page++) {
                pages.push(page);
            }
            return pages;
        }
    },
    watch:{
        rangePrice: {
            async handler(){
                var data = this.rangePrice;
                var min = data[0]?.minPrice;
                var max = data[0]?.maxPrice;
                if(data.length == 1){
                    min = data[0].minPrice;
                    max = data[0].maxPrice;
                    this.filterData.minPrice = min;
                    this.filterData.maxPrice = max;
                }
                else if(data.length > 1){
                    data.forEach(element => {
                        if(element.minPrice < min){
                            min = element.minPrice;
                        }
                        if(element.maxPrice > max){
                            max = element.maxPrice;
                        }
                    })
                    this.filterData.minPrice = min;
                    this.filterData.maxPrice = max;
                }else if(data.length == 0){
                    this.filterData.minPrice = null;
                    this.filterData.maxPrice = null;
                }
                this.takeDataProductByCategories();
            },
            deep:true
        },
        valueSort : async function (newvalue) {
                if(newvalue){
                    this.filterData.typeSort = newvalue;
                }
                await this.takeDataProductByCategories();
        }
    },
    methods: {
        async selectPage(number){
            this.filterData.pageNumber = number;
            await this.takeDataProductByCategories();
        },
        async nextPage(){
            this.filterData.pageNumber = this.filterData.pageNumber + 1;
            await this.takeDataProductByCategories();
        },
        async previousPage(){
            this.filterData.pageNumber = this.filterData.pageNumber - 1;
            await this.takeDataProductByCategories();
        },
        async takeCategoriesBySlug(){
            try{
                var res = await categoriesService.getCategoriesBySlug(this.slug);
                this.categories = res.data;
                await this.takeDataProductByCategories(this.categories.CategoriesId);
            }catch(error){
                console.log(error);
            }
            
        },
        async takeDataProductByCategories(){
            try{
                const params = {
                    pageSize:this.filterData.pageSize,
                    pageNumber: this.filterData.pageNumber,
                    sortType:this.filterData.typeSort,
                    minPrice:this.filterData.minPrice,
                    maxPrice:this.filterData.maxPrice,
                    searchString:this.filterData.searchString,
                    slug:this.slug,
                };
                var res = await productService.SortAndFilterProductByCategories({params});
                this.productbyCategories = res.data.Data;
                this.paging.totalPage = res.data.TotalPage;
                this.paging.totalRecord = res.data.TotalRecord;

            }catch(error){
                console.log(error);
            }

        },
        async takeDataUsers(){
            this.user = await JSON.parse(localStorage.getItem("User"));
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
.categories-product-noitem{
    font-size: 20px;
    color: #ccc;
    font-style: italic  ;
}
.categories-content-footer{
    padding-top: 20px ;
    display: flex;
    justify-content: center;
}
.next-page,
.previous-page{
    cursor: pointer;
    font-size: 20px;
    margin: 0px 10px;
}
.categories-footer-paging{
    display: flex;
    align-items: center;
}
.page-active{
    background-color: #059ad0 !important;
}
.number-page{
    display: flex;
}
.number-page-item{
    margin: 0px 5px;
    cursor: pointer;
    display: flex;
    align-items: center;
    justify-content: center;
    width: 26px;
    height: 26px;
    color: #fff;
    font-size: 15px;
    text-align: center;
    font-weight: bold;
    background-color: #a2c5d2;
}
.categories-content-sort select{
    text-align: center;
    border: none;
    outline: none;
}
.categories-content-sort option{
    padding: 5px 0px;
}
.categories-sidebar-select{
    padding: 10px 10px;
    background-color: #fff;
    box-shadow: 0px 2px 46.41px 4.59px rgba(2,38,113,0.1);
}
.categories-content{
    padding-bottom: 15px;
    box-shadow: 0px 2px 46.41px 4.59px rgba(2,38,113,0.1);
    background-color: #fff;
    border-radius: 10px;
    max-height: 800px;
}
.categories-content-name{
    color: #a2c5d2;
    font-size: 25px;
    text-transform: uppercase;

}
.categories-content-header{
    background-color: #fff;
    padding: 15px 0px;
    display: flex;
    justify-content: space-between;
    align-items: center;
}
.product-action-cart{
    display: flex;
    align-items: center;
}
.product-action-cart:hover{
    color: #a2c5d2;
}
.categories-product-action a:hover{
    color:#a2c5d2;
}
.categories-product-action a{
    color:#000;
}
.categories-product-action i{
    padding-right: 2px;
 font-size: 13px;
}
.product-infor-price:hover{
color: #059ad0;
}
.product-infor-price{
    text-align: center;
    color: #a2c5d2;
}
.product-infor-name{
    white-space: nowrap; 
    font-weight: bold;
    text-align: center;
    width: 170px;
    padding: 5px 5px;
      overflow: hidden;
  text-overflow: ellipsis; 
}
.categories-product-action{
    display: flex;
    align-items: center;
    justify-content: space-evenly; 
    padding-top: 10px ;
}
.categories-product-item{
    
    margin: 10px 20px;
    cursor: pointer;
    width: 170px;
    height: 260px;
    box-shadow: 0px 2px 46.41px 4.59px rgba(2,38,113,0.1);
}
.categories-content-product{
    display: flex;
    align-items: center;
    flex-wrap: wrap ;
}
.categories-product-image img{
    width: 170px;
    height: 170px;
}
.categoreis-sidebar-header{
    background-color: #a2c5d2 ;
    padding: 5px 10px;
    font-size: 20px;
    font-weight: bold;
    color: #fff;
}
label{
    margin:0px 0px 0px 10px;
}
.categories-sidebar-item{
    margin: 8px 0px;
    display: flex;
    align-items: center;
    justify-content: left;
}
input[type="radio"]{
   appearance: none;
   border: 1px solid #d3d3d3;
   width: 20px;
   height: 20px;
   content: none;
   outline: none;
   margin: 0;
}

input[type="radio"]:checked {
  appearance: none;
  outline: none;
  padding: 0;
  content: none;
  border: none;
    width: 20px;
    height: 20px;
}

input[type="radio"]:checked::before{
    width: 20px;
   height: 20px;
outline: none;
  position: absolute;
  color: green !important;
  content: "\00A0\2713\00A0" !important;
  border: 1px solid #d3d3d3;
  font-weight: bolder;
  font-size: 14px;
}
</style>