<template>
  <div class="layout">
    <HeadingShop></HeadingShop>
    <router-view name="ShopRouterView"></router-view>
    <FooterShop></FooterShop>
  </div>
</template>

<script>
import HeadingShop from "./HeadingShop.vue";
// import MainShop from './MainShop.vue';
import FooterShop from './FooterShop.vue';
export default {
  name:'UserLayout',
  components:{
    HeadingShop,FooterShop
  }
}
</script>

<style scoped>
  .layout{
    background-color:#fff;
  }

 
</style>