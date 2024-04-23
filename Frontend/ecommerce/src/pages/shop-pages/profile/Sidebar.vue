<template>
    <div class="profile-sidebar">
        <div class="profile-sidebar-header">
            Tài Khoản 
        </div>
        <div class="profile-sidebar-select">
            <div class="profile-sidebar-item">
                <router-link to="/profile/">Thông tin tài khoản</router-link>

            </div>
            <div class="profile-sidebar-item">
                <router-link to="/profile/address">Địa chỉ của tôi</router-link>

            </div>
            <div class="profile-sidebar-item">
                <router-link to="/profile/order">Đơn hàng của tôi</router-link>
            </div>
            
        </div>
    </div>
</template>

<script>
export default {
    name:"SidebarProfile"
}
</script>

<style scoped>
.profile-sidebar-item{

    width: 100%;
    padding: 15px 0px 5px 0px;
    border-bottom: 1px solid #ccc;
}
.profile-sidebar-select a:hover {
    color: #1ea2d2;
}
.profile-sidebar-select a {
    color: #a2c5d2;
    font-size: 16px;
}
.profile-sidebar-select{
    
    padding: 10px 10px;
    display: flex;
    flex-direction: column;
}
.profile-sidebar-header{
    padding: 10px 20px;
    padding-top: 20px;
    border-bottom: 1px solid #ccc;
    color: #a2c5d2;
    font-size: 20px;
    font-weight: bold;
}
.profile-sidebar{
    padding: 0px;
    min-height: 400px ;
    box-shadow: 0px 2px 46.41px 4.59px rgba(2,38,113,0.1);
}
</style>