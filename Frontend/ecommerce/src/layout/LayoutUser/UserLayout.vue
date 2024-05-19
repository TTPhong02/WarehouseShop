<template>
  <div class="layout">
    <HeadingShop></HeadingShop>
    <router-view name="ShopRouterView"></router-view>
    <FooterShop></FooterShop>
    <MToastMessage></MToastMessage>
    <df-messenger
      chat-title="Hỗ trợ khách hàng"
      agent-id="3d69f23e-a3ab-4242-90d8-eda26580989b"
      language-code="en"
    ></df-messenger>
  </div>
</template>

<script>
import HeadingShop from "./HeadingShop.vue";

import FooterShop from './FooterShop.vue';
import MToastMessage from '../../components/base/toast/MToastMessage.vue';
export default {
  name:'UserLayout',
  components:{
    HeadingShop,FooterShop,
    MToastMessage
  }
}
</script>

<style scoped>
  .layout{
    background-color:#fff;
  }

 
</style>