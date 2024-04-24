<template>
    <div class="row s-header-admin">
        <div class="col-lg-1 col-md-1 col-sm-1 col-1 s-header-grid">
            <i class="fa-solid fa-bars"></i>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-3 col-3 s-header-logo">
            <img src="../../assets/img/logo.png" alt="">
        </div>
        <div class="col-lg-5 col-md-5 col-sm-5 col-5 s-header-name">
            THOWIE GIA DỤNG CHO MỌI NHÀ
        </div>
        <div class="col-lg-1 col-md-1 col-sm-1 col-1 s-header-noti">
            <i class="fa-regular fa-bell"></i>
        </div>
        <div class="col-lg-2 col-md-2 col-sm-2 col-2 s-header-user ">
            Xin chào, {{this.user.Fullname}}
            <div class="s-user-nav">
                <div @click="backtoshop()" class="user-nav-item">
                    Quay lại trang bán hàng 
                </div>
                <div @click="logout()" class="user-nav-item">
                    Đăng xuất 
                </div>
            </div>
        </div>
    </div>                      
</template>
<script>
export default {
    name: "MHeadingAdmin",
    mounted() {
        this.user = JSON.parse(localStorage.getItem("User"));
    },
    data() {
        return {
            user:{},
        }
    },
    methods: {
        backtoshop(){
            localStorage.clear();
            this.$router.push("/");
        },
        /**
         * Hàm thực hiện đăng xuất thông tin người dùng
         * Author by : TTPhong(22/02/2024)
         */
        logout(){
            localStorage.clear();
            this.$router.push("/login-admin")
        }
    },
}
</script>
<style scoped>
.user-nav-item:hover{
    color: #a2c5d2;
}
.user-nav-item{
    color: black;
    padding: 10px 0px;
}
.s-user-nav{
    padding: 10px 10px;
    font-size: 13px;
    background-color: #fff;
    box-shadow: 0 12px 20px rgba(0,0,0,.12);
    display: none;
    position: absolute;
    top: 25px;
    border-radius: 5px;
}
.s-header-user:hover .s-user-nav{
    display: block;
        transition: 0.5s;
}
.s-header-user{
    cursor: pointer;
    color: #1a7a9c;
    font-size: 18px;
    font-weight: bold;
    position: relative;
}

.s-header-noti{
    cursor: pointer;
    text-align: right;
    font-size: 23px;
    color: #27395E;
}
.s-header-name{
    font-size: 25px;
    font-weight: bold;
    color: #27395E;
}
.s-header-logo{
    cursor: pointer;
    text-align: left;
}
.s-header-logo img{
    width: 160px;
    height: 56px;
}
.s-header-grid{
    cursor: pointer;
    text-align: center;
    font-size: 27px;
    color: #27395E;
}
.s-header-admin{

    align-items: center;
    justify-content: space-around;
    height: 56px;
    box-shadow: 0 0px 20px rgba(0,0,0,.12);
}
</style>