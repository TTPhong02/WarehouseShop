<template>
    <div>
        <MHeadingAdmin></MHeadingAdmin>
        <MSidebarAdmin></MSidebarAdmin>
        <MMainAdmin ></MMainAdmin>
        <MToastMessage></MToastMessage>
    </div>
    
</template>

<script>
 import MHeadingAdmin from './HeadingAdmin.vue';
 import MSidebarAdmin from './SidebarAdmin.vue';
 import MMainAdmin from './MainAdmin.vue';
import MToastMessage from '../../components/base/toast/MToastMessage.vue';
export default {
    name:"AdminLayout",
    components:{
        MHeadingAdmin,MSidebarAdmin,MMainAdmin,
        MToastMessage
    }
}
</script>

<style>

</style>