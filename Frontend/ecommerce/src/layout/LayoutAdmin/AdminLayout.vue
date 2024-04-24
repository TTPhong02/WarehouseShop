<template>
    <div>
        <MHeadingAdmin></MHeadingAdmin>
        <MSidebarAdmin></MSidebarAdmin>
        <MMainAdmin ></MMainAdmin>
    </div>
</template>

<script>
 import MHeadingAdmin from './HeadingAdmin.vue';
 import MSidebarAdmin from './SidebarAdmin.vue';
 import MMainAdmin from './MainAdmin.vue';
export default {
    name:"AdminLayout",
    components:{
        MHeadingAdmin,MSidebarAdmin,MMainAdmin
    }
}
</script>

<style>

</style>