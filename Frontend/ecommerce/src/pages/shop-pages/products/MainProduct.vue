<template>
    <div>
        <HomeSlide></HomeSlide>
        <div class="container">
            <HomeCategories></HomeCategories>
            <HomeProductNew></HomeProductNew>
            <HomeProductViewed></HomeProductViewed>
            <HomeProuctByCategories></HomeProuctByCategories>
        </div>
    </div>
</template>

<script>
import HomeCategories from "../categories/HomeCategories.vue";
import HomeSlide from "../../../layout/LayoutUser/HomeSlide.vue";
import HomeProductNew from './HomeProductNew.vue';
import HomeProuctByCategories from './HomeProuctByCategories.vue';
import HomeProductViewed from './HomeProductViewed.vue';
export default {
    components:{
        HomeCategories,HomeSlide,HomeProductNew,HomeProuctByCategories,HomeProductViewed
    }

}
</script>

<style>
</style>