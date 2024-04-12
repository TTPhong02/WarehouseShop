<template>
    <div :class="{'p-home__sidebar':true , 'small-sidebar':isHidden} ">
            <div class="p-sidebar">
                <div class="p-sidebar__list">
                    <router-link to="/admin/home" class="p-sidebar__list--item">
                        <div class="p-sidebar__list--item--icon icon-sidebar-home"></div>
                        <!-- <img src="../assets/img/add.png" alt="addicon"> -->
                        <div v-if="!isHidden" class="p-sidebar__list--item--title">Tổng quan</div>                      
                    </router-link>
                    <router-link to="/admin/orders" class="p-sidebar__list--item">
                        <div class="p-sidebar__list--item--icon icon-sidebar-chart"></div>
                        <!-- <img src="../assets/img/add.png" alt="addicon"> -->
                        <div v-if="!isHidden" class="p-sidebar__list--item--title">Quản lý Đơn hàng</div>
                    </router-link>
                    <router-link to="/admin/products" class="p-sidebar__list--item">
                        <div class="p-sidebar__list--item--icon icon-sidebar-customer"></div>
                        <!-- <img src="../assets/img/add.png" alt="addicon"> -->
                        <div v-if="!isHidden" class="p-sidebar__list--item--title">Quản lý sản phẩm</div>
                    </router-link>
                    <router-link to="/admin/categories" class="p-sidebar__list--item">
                        <div class="p-sidebar__list--item--icon icon-sidebar-setting"></div>
                        <!-- <img src="../assets/img/add.png" alt="addicon"> -->
                        <div v-if="!isHidden" class="p-sidebar__list--item--title">Quản lý danh mục sản phẩm</div>
                    </router-link>
                    <router-link to="/admin/users" class="p-sidebar__list--item">
                        <div class="p-sidebar__list--item--icon icon-sidebar-setting"></div>
                        <!-- <img src="../assets/img/add.png" alt="addicon"> -->
                        <div v-if="!isHidden" class="p-sidebar__list--item--title">Quản lý tài khoản</div>
                    </router-link>
                </div>
                <div id="shorter" class="p-sidebar__shorter ">
                    <div @click="btnSmallSidebar"  class="p-sidebar__shorter--item">
                        <div class="p-sidebar__shorter--item--icon icon-prev"></div>
                        <div v-if="!isHidden"  class="p-sidebar__list--item--title">Thu gọn</div>
                    </div>
                </div>
            </div>  
        </div>
</template>
<script>
export default {
    name: "MSidebarAdmin",
    data() {
        return {
            isHidden:false,
        }
    },
    methods: {
        btnSmallSidebar(){
            if(this.isHidden === false){
                this.isHidden = true;
            }else{

                this.isHidden = false;
            }
        }
    },
}
</script>
<style  scoped>
a{
    text-decoration: none;
}
.small-sidebar{
    width: 65px !important;
    transition: 0.5s;
}
.router-link-active{
    background-color: #3f4b5d;
    border-radius: 4px;
}
.p-sidebar{
    height: 100%;
    display: flex;
    flex-direction: column;
    justify-content: space-between;

}

.p-sidebar__list{
    margin-top: 24px;
}
.p-sidebar__shorter--item,
.p-sidebar__list--item{
    display: flex;
    font-size: 14px;
    font-weight: 700;
    color: #869AB8;
    margin: 0px 12px 4px 12px;
    height: 44px;
    justify-content: left;
    align-items: center;
    column-gap: 12px;
}
.p-sidebar__list--item:hover{
    background-color: #27395E;
    color: #fff;
    border-radius: 4px;
    cursor: pointer;
}
.p-sidebar__shorter{
    cursor: pointer;
    border-top: 1px solid #869AB8;
    float: bottom;
}
.p-sidebar__shorter--item--icon{
    margin-left: 18px;
}
.p-sidebar__list--item--icon{
    margin-left: 12px;
}
</style>