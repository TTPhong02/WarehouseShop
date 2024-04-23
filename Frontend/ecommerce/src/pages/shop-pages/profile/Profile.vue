<template>
    <div class="row container p-profile">
        <div class="col-lg-3 col-md-3 col-sm-3 col-3 p-profile-sidebar">
            <Sidebar></Sidebar>
        </div>
        <div class="col-lg-9 col-md-9 col-sm-9 col-9 p-profile-content">
            <ContentProfile></ContentProfile>
        </div>
    </div>
</template>

<script>
import ContentProfile from "./Content.vue"
import Sidebar from "./Sidebar.vue"
export default {
    name:"UserProfile",
    components:{
        Sidebar,ContentProfile
    }
}
</script>

<style scoped>
.p-profile{
    padding: 20px 0px;
}
.p-profile-content{
    box-shadow: 0px 2px 46.41px 4.59px rgba(2,38,113,0.1);
}
</style>