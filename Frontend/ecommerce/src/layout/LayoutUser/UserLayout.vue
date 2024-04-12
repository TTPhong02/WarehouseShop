<template>
  <div class="layout">
    <HeadingShop></HeadingShop>
    <MainShop></MainShop>
    <FooterShop></FooterShop>
  </div>
</template>

<script>
import HeadingShop from "./HeadingShop.vue";
import MainShop from './MainShop.vue';
import FooterShop from './FooterShop.vue';
export default {
  name:'UserLayout',
  components:{
    HeadingShop,MainShop,FooterShop
  }
}
</script>

<style scoped>
  .layout{
    background-color:#fff;
  }

 
</style>