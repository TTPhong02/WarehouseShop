<template>
    <div :class="{'p-home__sidebar':true , 'small-sidebar':isHidden} ">
        <div class="p-sidebar-admin">
            <div class="p-sidebar__list">
                <router-link to="/admin" class="p-sidebar__list--item">
                    <div class="p-sidebar__list--item--icon">
                        <i class="fa-solid fa-chart-pie"></i>
                    </div>
                    <!-- <img src="../assets/img/add.png" alt="addicon"> -->
                    <div v-if="!isHidden" class="p-sidebar__list--item--title">Tổng quan</div>                      
                </router-link>
                <router-link to="/admin/analysis-management" class="p-sidebar__list--item">
                    <div class="p-sidebar__list--item--icon ">
                        <i class="fa-solid fa-chart-simple"></i>
                    </div>
                    <!-- <img src="../assets/img/add.png" alt="addicon"> -->
                    <div v-if="!isHidden" class="p-sidebar__list--item--title">Thống kê </div>
                </router-link>
                <router-link to="/admin/orders-management" class="p-sidebar__list--item">
                    <div class="p-sidebar__list--item--icon ">
                        <i class="fa-solid fa-bag-shopping"></i>
                    </div>
                    <!-- <img src="../assets/img/add.png" alt="addicon"> -->
                    <div v-if="!isHidden" class="p-sidebar__list--item--title">Quản lý đơn hàng</div>
                </router-link>
                <router-link to="/admin/products-management" class="p-sidebar__list--item">
                    <div class="p-sidebar__list--item--icon ">
                        <i class="fa-solid fa-database"></i>
                    </div>
                    <!-- <img src="../assets/img/add.png" alt="addicon"> -->
                    <div v-if="!isHidden" class="p-sidebar__list--item--title">Quản lý sản phẩm</div>
                </router-link>
                <router-link to="/admin/categories-management" class="p-sidebar__list--item">
                    <div class="p-sidebar__list--item--icon ">
                        <i class="fa-solid fa-outdent"></i>
                    </div>
                    <!-- <img src="../assets/img/add.png" alt="addicon"> -->
                    <div v-if="!isHidden" class="p-sidebar__list--item--title">Quản lý danh mục</div>
                </router-link>
                <router-link to="/admin/account-management" class="p-sidebar__list--item">
                    <div class="p-sidebar__list--item--icon ">
                        <i class="fa-solid fa-user"></i>
                    </div>
                    <!-- <img src="../assets/img/add.png" alt="addicon"> -->
                    <div v-if="!isHidden" class="p-sidebar__list--item--title">Quản lý tài khoản</div>
                </router-link>
                <router-link to="/admin/voucher-management" class="p-sidebar__list--item">
                    <div class="p-sidebar__list--item--icon ">
                        <i class="fa-solid fa-user"></i>
                    </div>
                    <!-- <img src="../assets/img/add.png" alt="addicon"> -->
                    <div v-if="!isHidden" class="p-sidebar__list--item--title">Quản lý khuyến mãi</div>
                </router-link>
                <router-link to="/admin/feeback-management" class="p-sidebar__list--item">
                    <div class="p-sidebar__list--item--icon ">
                        <i class="fa-solid fa-user"></i>
                    </div>
                    <!-- <img src="../assets/img/add.png" alt="addicon"> -->
                    <div v-if="!isHidden" class="p-sidebar__list--item--title">Quản lý đánh giá</div>
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

.p-sidebar-admin{
    background-color: #041434;
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
.p-sidebar__list--item--icon i{
    font-size: 20px;
}
.p-sidebar__list--item--icon{
}
</style>