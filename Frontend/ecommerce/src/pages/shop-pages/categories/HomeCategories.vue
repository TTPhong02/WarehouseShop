<template>
  <div class="s-main-categories">
    <div class="main-categories-list">
        <div v-for="item in categories" :key="item.CategoriesId" class="categories-item">
            <div class="categories-item-image">
                <img :src="this.helper.checkImagePath(item.ImagesPath)" alt="">
            </div>
            <div class="categories-item-name">{{item.CategoriesName}}</div>
            <div class="categories-item-action">
                <router-link to="">Xem tất cả</router-link>
            </div>
        </div>
    </div>
  </div>
</template>

<script>
import categoriesService from "../../../utils/CategoriesService"
export default {
    data() {
        return {
            categories:[],
        }
    },
    created() {
        this.takeCategories();
    },
    methods: {
        async takeCategories(){
            var res = await categoriesService.getAll();
            this.categories = res.data;
        }
    },
}
</script>

<style scoped>
.s-main-categories{
    padding: 20px 0px;
}
.main-categories-list{
    display: flex;
    width: 100%;

}
.categories-item-action a:hover{
    color: #048bbc;
    text-decoration: underline;
}
.categories-item-action a{
    font-size: 15px;
    color: #a2c5d2;
}
.categories-item-name:hover{
    color: #5a97ad;
}
.categories-item-name{
    cursor: pointer;
    padding:10px 0px 5px 0px;
    color: #048bbc;
    font-size: 16px;
    font-weight: bold;
}
.categories-item-image:hover img {
    scale: 1.05;
    transition: 0.5s;
}
.categories-item-image{
    overflow: hidden;
    cursor: pointer;
    box-shadow: 0px 0px 30px 0px #d3dcdc ;
}
.categories-item-image img {
    width: 300px;
    height: 180px;
}
</style>