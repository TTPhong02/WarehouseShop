<template>
<div>

    <router-view></router-view>
    <MLoading></MLoading>
</div>
</template>

<script>

import MLoading from './components/base/loading/MLoading.vue';
export default {
  name: 'App',
  components: {
    MLoading  
  }
}
</script>

<style>

</style>
