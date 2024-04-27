<template>
  <header class="s-header">
    <div class="sticky-header-wrap s-header-wrap">
      <div class="container">
        <div class="row s-text-run">
          <div class="text-run col-lg-12 col-md-12 col-sm-12">Chào mừng bạn đã ghé thăm cửa hàng của chúng tôi ! Chúc bạn mua sắm vui vẻ ❤️❤️❤️ </div>
        </div>
      </div>
    </div>
    <div class="sticky-header-mid s-header-mid">
      <div class="container">
        <div class="row content-header">
          <div class="col-lg-1 col-md-1 col-sm-2 mid-header-icon">
            <i class="fa-solid fa-bars"></i>
          </div>
          <router-link
            to="/"
            class="col-lg-2 col-md-2 col-sm-8 mid-header-logo"
          >
            <img src="../../assets/img/logo.png" alt="logo" />
          </router-link>
          <div class="col-lg-6 col-md-6 col-sm-0 mid-header-search">
            <MTextSearch> </MTextSearch>
          </div>
          <router-link
            to="/cart"
            class="col-lg-1 col-md-1 col-sm-2 mid-header-cart"
          >
            <img class="img_cart" src="../../assets/img/cart.png" alt="" />
            <span v-if="this.numberCart > 0" class="number_cart">{{this.numberCart}}</span>
          </router-link>
          <div
            class="col-lg-2 col-md-2 col-sm-0 mid-header-profile menu-father"
          >
            <div v-if="this.user" class="profile-avatar">
              <img src="../../assets/img/cart.png" alt="" />
              <div class="profile-name">Xin chào, {{this.user.Fullname}}</div>
            </div>
            <div v-if="!this.user" class="profile-signin">
              <router-link to="/login">Đăng nhập</router-link>
              <router-link to="/register">Đăng kí</router-link>
            </div>
            <div v-if="this.user" class="header-profile-nav menu-child">
              <router-link to="/profile" class="profile-nav-item">Thông tin tài khoản</router-link>
              <div v-on:click="logout()" class="profile-nav-item">Đăng xuất</div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="s-header-nav">
      <div class="container">
        <div class="row nav-content">
          <div class="col-lg-1 col-md-1 col-sm-0 nav-content-first"></div>
          <div class="col-lg-10 col-md-10 col-sm-0 nav-content-list">
            <div class="list-item-nav">
              <router-link to="/"> Trang chủ </router-link>
            </div>
            <div class="list-item-nav">
              <router-link to="/gioi-thieu"> Giới thiệu </router-link>
            </div>
            <div class="list-item-nav menu-father">
              <router-link to="/">
                Sản phẩm
                <i class="fa-solid fa-caret-down"></i>
                <div class="menu-child">
                  <div class="menu-child-list">
                    <router-link v-for="item in categories" :key="item.CategoriesId" :to="item.CategoriesSlug" class="child-item">
                      {{item.CategoriesName}}
                    </router-link>
                  </div>
                </div>
              </router-link>
            </div>
            <div class="list-item-nav">
              <router-link to="/tin-tuc"> Tin tức </router-link>
            </div>
            <div class="list-item-nav">
              <router-link to="/kiem-tra-don-hang">
                Kiểm tra đơn hàng
              </router-link>
            </div>
            <div class="list-item-nav menu-father">
              <router-link to="/he-thong-cua-hang">
                Hệ thống cửa hàng
                <i class="fa-solid fa-caret-down"></i>
                <div class="menu-child">
                  <div class="menu-child-list">
                    <router-link to="danh-sach-cua-hang" class="child-item">
                      Danh sách cửa hàng
                    </router-link>
                  </div>
                </div>
              </router-link>
            </div>
          </div>
          <div class="col-lg-1 col-md-1 col-sm-0 nav-list-last"></div>
        </div>
      </div>
    </div>
    <div class="s-header-placeholder">
 </div>
    
  </header>
</template>

<script>
import cartLocalStorageService from "../../js/storage/CartLocalStorage"
import MTextSearch from "../../components/base/input/MTextSearch.vue";
import usersService from '../../utils/UserService';
import categoriesService from '../../utils/CategoriesService';

export default {
  name: "HeadingShop",
  components: {
    MTextSearch,
  },
  data() {
    return {
      user:{},
      numberCart:0,
      categories:[],
    }
  },
  created() {
    this.emitter.on("takeNumberOfCart",this.takeNumberOfCart);
    this.emitter.on("takeDataUsersHeading",this.takeDataUsers)
  },
  mounted() {
    this.takeDataUsers();
    window.addEventListener("scroll", this.handleScroll);
    this.takeNumberOfCart();
    this.takeDataCategories();
  },
  methods: {
    takeNumberOfCart(){
      var cart = cartLocalStorageService.getCartFromLocalStorage();
      if(cart){
        this.numberCart = cart.length;
      }
      else{
        this.numberCart = 0;
      }
    },
    async takeDataCategories(){
      var res = await categoriesService.getAll();
      this.categories = res.data;
    },
    async takeDataUsers(){
      this.user = await JSON.parse(localStorage.getItem("User"));
    },
    async logout(){
      var res = await usersService.logout(this.user.Email);
      switch (res.status) {
        case 201:
          localStorage.clear();
          // localStorage.removeItem("AccessToken");
          // localStorage.removeItem("RefreshToken");
          // localStorage.removeItem("User");
          // localStorage.removeItem("CartItems");
          // localStorage.removeItem("CartSelected");
          window.location.reload();
      }

    },
    handleScroll() {
      let headerNav = document.querySelector(".s-header-nav");
      let headerWrap = document.querySelector(".s-header-wrap");
      let headerMid = document.querySelector(".s-header-mid");
      let headerPlace = document.querySelector(".s-header-placeholder");

      if (window.scrollY > 0) {
        // Khi cuộn xuống, ẩn header-nav và cố định header-wrap và header-mid
        headerNav.classList.add("hidden");
        headerWrap.style.position = "fixed";
        headerMid.style.position = "fixed";
        headerPlace.style.display = "block";
      } else {
        // Khi ở đầu trang, hiển thị header-nav và loại bỏ cố định của header-wrap và header-mid
        headerNav.classList.remove("hidden");
        headerWrap.style.position = "";
        headerMid.style.position = "";
        headerPlace.style.display = "none";
      }
    },
  },
  beforeUnmount() {
    window.removeEventListener("scroll", this.handleScroll);
  },
};
</script>

<style scoped>
.profile-name{
  color: #a2c5d2;
  font-size: 14px;
  padding-left:10px ;
}
.profile-avatar{
  cursor: pointer;
  display: flex;
  justify-content: space-between;
  align-items: center;
}
.s-header-placeholder{
  height: 167px;
  display: none;
}
.s-header-nav.hidden {
  display: none;
}
.sticky-header-wrap{
  top: 0;
  left: 0;
  right: 0;
  background-color: #a2c5d2; /* Màu nền header khi sticky */
  z-index: 1000;
}

.sticky-header-mid{
  top: 37px; /* Adjust according to the height of the s-header-wrap */
  left: 0;
  right: 0;
  z-index: 1000;
}
.s-text-run{
  width: 100%;

}
.text-run{
  font-size: 16px;

}

@keyframes runText {
  0% {
    transform: translateX(100%); /* Di chuyển văn bản sang phải 100% */
  }
  100%  {
    transform: translateX(-100%); /* Di chuyển văn bản sang trái 100% */
  }
}
.s-header-wrap .container .row{
    padding-top: 5px;
}
.s-header-wrap .container .row .text-run {
  animation: runText 25s linear infinite; 
}
a {
  color: black;
}
.menu-father{
    position: relative;
}
.menu-father:hover .menu-child {
    overflow: visible;
    max-height: 3000px;
    max-width: 3000px;
    opacity: 1;
    transform: perspective(600px) rotateX(0deg);
    transition: transform 0.5s ease, opacity 0.2s ease, max-height 0s step-end, max-width 0s step-end, padding 0s step-end;
}

.menu-child {
    background: #fff;
    overflow: hidden;
    display: block;
    max-height: 3000px;
    max-width: 3000px;
    opacity: 0;
    transform: perspective(600px) rotateX(-90deg);
    transition: transform 0.5s ease, opacity 0.6s ease, max-height 0.6s step-end, max-width 0.6s step-end, padding 0.6s step-end;
    box-shadow: 0 1px 11px 0px rgba(0,0,0,0.2);
    transform-origin: 0% 0%;
    margin: 0;
    padding: 20px 20px 20px 15px;
    position: absolute;
    top: 120%;
    right: 0;
    z-index: 99;
    width: 100%;
    border: none;
    text-align: left;
}
.s-header-nav {
  height: 50px;
  display: flex;
  justify-content: center;
  align-items: center;
  background-color: #fff;
}
.list-item-nav {
  text-align: center;
  font-size: 16px;
  padding: 0 20px;
}

.nav-content-list a:active,
.nav-content-list a:hover {
  color: #a2c5d2;
}

.p-textfield1 input {
  width: 100% !important;
}
.s-container {
  width: 100%;
  background-color: #ccc;
  position: relative;
}
.s-header {
  left: 0;
  right: 0;
}
.content-header {
    display: flex;
  justify-content: center;
  align-items: center;
}
.s-header-wrap {
  color: #fff;
  left: 0;
  right: 0;
  background-color: #a2c5d2;
  width: 100%;
  height: 37px;
      overflow: hidden; 
}
.s-header-mid {
  height: 80px;
  background-color: #fff;
  border-top: 1px solid #f8f8f8;
  border-bottom: 1px solid #f8f8f8;
}
.mid-header-cart {
  position: relative;
}
.number_cart {
  position: absolute;
  top: -5px;
  right: 25px;
  font-size: 11px;
  background: #6baec7;
  border-radius: 50%;
  color: #fff;
  width: 20px;
  height: 20px;
  line-height: 20px;
  text-align: center;
}
.mid-header-search {
  padding: 0px 90px;
}
.mid-header-profile .avatar {
  padding-right: 10px;
}
.nav-content-list,
.mid-header-profile {
  padding: 0px;
  display: flex;
  justify-content: center;
  align-items: center;
}
.profile-signin{
  display: flex;
  justify-content: space-between;
}
.profile-signin a:hover{
  font-weight: bold;
  color:#6baec7;
}
.profile-signin a{
  padding: 0px 10px;
  text-decoration: underline !important;
  color:#a2c5d2;
  font-size: 16px;
}
.profile-nav-item{
  cursor: pointer;
  padding: 5px 0px;
}
.profile-nav-item:hover{
color: #a2c5d2;
}
  
.mid-header-icon i {
  font-size: 30px;
}
.mid-header-cart img,
.mid-header-profile img {
  width: 40px;
}
.mid-header-logo img {
  width: 180px;
}
</style>
