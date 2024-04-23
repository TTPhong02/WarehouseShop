<template>
    <div>
        <HomeSlide></HomeSlide>
        <div class="container">
            <HomeCategories></HomeCategories>
            <HomeProductNew></HomeProductNew>
        </div>
    </div>
</template>

<script>
import HomeCategories from "../categories/HomeCategories.vue";
import HomeSlide from "../../../layout/LayoutUser/HomeSlide.vue";
import HomeProductNew from './HomeProductNew.vue';
export default {
    components:{
        HomeCategories,HomeSlide,HomeProductNew
    }

}
</script>

<style>
</style>