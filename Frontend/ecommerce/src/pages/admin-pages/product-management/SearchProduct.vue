<template>
    <div class="search-father">
        <MTextSearch v-model="searchString" @focus="handleFocus()"  ></MTextSearch>
        <div v-if="this.isFocused == true " class="search-product">
            <div v-for="item in listProduct" :key="item" class="search-product-item">
                <div class="search-product-left">
                    <div class="search-product-image">
                        <img :src="checkImagePath(item.ProductId)" alt="">
                    </div>
                    <div class="search-product-infor">
                        <div class="search-product-name">{{item.ProductName}}</div>
                        <div class="search-product-giá">{{this.helper.formatMoney(item.ProductPrice) }}</div>
                    </div>
                </div>
                <div class="search-product-action">
                    <router-link @click="redirectToProduct(item.ProductSlug)" :to="'/product/' + item.ProductSlug">Chi tiết</router-link>
                </div>
            </div>
            <div v-if="this.listProduct.length == 0" class="none-product">Không có sản phẩm nào phù hợp </div>
        </div>
    </div>
</template>

<script>
import MTextSearch from '../../../components/base/input/MTextSearch.vue'
import imagesService from '../../../utils/ImagesService'
import productService from '../../../utils/ProductService'
export default {
    components:{
        MTextSearch
    },
    data() {
        return {
            listProduct:[],
            searchString:'',
            images:[],
            isFocused:false,
        }
    },
    watch:{
        'searchString':function(){
            this.takeImages();
            this.searchProduct();

        }
    },
    methods: {
        redirectToProduct(slug){
            this.$router.push('/product/'+ slug);
            this.handleBlur();
        },
        handleFocus() {
          this.isFocused = true;
        },
        handleBlur() {
          this.isFocused = false;
        },
        async searchProduct(){
            try{
                var search = this.searchString
                var res = await productService.SearchProduct(search);
                this.listProduct = res.data;
            }catch(error){
                console.log(error);
            }
        },
        async takeImages(){
            var res = await imagesService.getAll();
            this.images = res.data;
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
    },
}
</script>

<style>
.search-product-name{
    white-space: nowrap; 
    overflow: hidden;
    text-overflow: ellipsis; 
    font-weight: bold;
    text-align: center;
}
.search-product-infor{
    margin-left: 10px;
}
.search-product-image img{
    border: 1px solid #ccc;
    width: 70px;
    height: 70px;
    object-fit: cover;
}
.search-product-item{
    margin:10px 0px;
    display: flex;
    align-items: center;
    justify-content: space-between;
}
.search-product-left{
        display: flex;
    align-items: center;
}
.search-father{
    position: relative;
}
.search-product{
    max-height: 400px;
    overflow-y: scroll;
    box-shadow: 0px 2px 46.41px 4.59px rgba(2,38,113,0.1);
    background-color: #fff;
    width: 100%;
    z-index: 10000;
    padding: 5px 10px;
    position: absolute;
    border-radius: 8px;
}
</style>