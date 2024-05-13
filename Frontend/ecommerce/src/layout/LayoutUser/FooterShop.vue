<template>
 <footer class="s-footer">
    <div class="wrap-notification">
        <div class="container">
            <div class="row content">
                <div class="col-lg-6 col-md-12 col-sm-12 content-text">
                    <h3>Đăng kí nhận thông tin khuyến mãi</h3>
                </div>
                <div class="col-lg-6 col-md-12 col-sm-12  content-form">
                    <MTextfield textPlaceHolder="Nhập email của bạn"></MTextfield>
                    <MButton style="margin-top:8px;" className="p-button1"></MButton>
                </div>
            </div>  
        </div>
    </div>
    <div class="s-footer-about">
        <div class="row container">
            <div class="col-lg-4 col-md-4 col-sm-12 s-footer-item contact">
                <h5 class="item-title">Thông tin liên hệ</h5>
                <div class="item-content">
                    <div>
                        <strong>Thowie Miền Bắc - Vận hành đơn hàng toàn quốc</strong>
                    </div>
                    <div>
                        <span class="s-footer-color">Địa chỉ: </span>
                        <span>Trụ sở gia dịch : 16 Xóm 6 Lã Côi, Xã Yên Viên, H.Gia Lâm, TP.Hà Nội</span>
                    </div>
                    <div>
                        <span class="s-footer-color">Mã số thuế: </span>
                        <span>123456789</span>
                    </div>
                    <div>
                        <span class="s-footer-color">Email: </span>
                        <span>Phong762002@gmail.com</span>
                    </div>
                    <div>
                        <span class="s-footer-color">Hotline: </span>
                        <span>0359650520</span>
                    </div>
                </div>
            </div>
            <div class="col-lg-2 col-md-2 col-sm-12 s-footer-item about-me">
                <div class="row item-title">
                    <h5 class="col-lg-12 col-md-12 col-sm-10 col-10 ">Về chúng tôi</h5>
                    <div class="d-lg-none d-md-none col-sm-2 col-2 icon-show">
                        <i class="fa-solid fa-plus"></i>
                    </div>
                </div>
                <div class="item-content">
                    <router-link to="/"> Trang chủ</router-link>
                    <router-link to="/gioi-thieu"> Giới thiệu</router-link>
                    <router-link to="/san-pham"> Sản phẩm</router-link>
                    <router-link to="/tin-tuc"> Tin tức</router-link>
                    <router-link to="/lien-he"> Liên hệ</router-link>
                    <router-link to="/kiem-tra-don-hang"> Kiểm tra đơn hàng</router-link>
                    <router-link to="/he-thong-khach-hang"> Hệ thống khách hàng</router-link>
                </div>
            </div>
            <div class="col-lg-2 col-md-2 col-sm-12 s-footer-item policy">
                <div class="row item-title">
                    <h5 class="col-lg-12 col-md-12 col-sm-10 col-10 ">Chính sách</h5>
                    <div v-if="true" class="d-lg-none d-md-none col-sm-2 col-2 icon-show">
                        <i class="fa-solid fa-plus"></i>
                    </div>
                </div>
                <div class="item-content">
                    <router-link to="/chinh-sach-bao-mat">Chính sách bảo mật</router-link>
                    <router-link to="/chinh-sach-van-chuyen"> Chính sách vận chuyển</router-link>
                    <router-link to="/chinh-sach-doi-tra"> Chính sách đổi trả</router-link>
                    <router-link to="/quy-dinh-su-dung"> Quy định sử dụng</router-link>
                </div>
            </div>
            <div class="col-lg-4 col-md-4 col-sm-12 s-footer-item social-media">
                <div class="row item-title">
                    <h5 class="col-lg-12 col-md-12 col-sm-10 col-10 ">Fanpage</h5>
                    <div v-if="true" class="d-lg-none d-md-none col-sm-2 col-2 icon-show">
                        <i class="fa-solid fa-plus"></i>
                    </div>
                </div>
            </div>
        </div>
    </div>
 </footer>
</template>

<script>
import MTextfield from '../../components/base/input/MTextfield.vue'

export default {
    components:{
        MTextfield
    }
}
</script>

<style scoped>
.wrap-notification{
    background-color: #a2c5d2;
}
.wrap-notification .content{
    margin: 0 auto;
    display: flex;
    justify-content: center;
    align-items: center;
    height: 127px;
}
.content-form{
    display: flex;
    justify-content: center;
    align-items: center;
}
.s-footer-item{

}
.s-footer-about{
    padding-bottom: 40px;
}
.item-title{
    margin-top:40px;
}
.item-content{
    display: flex;
    flex-direction: column;
    align-content: space-between;
    margin-top: 30px;
}
.item-content > div,
.item-content a{
    color: black;
    padding: 5px 0px;
}
.item-content a:hover,
.s-footer-color{
    color: #a2c5d2;
}
.s-footer{
    margin-top: 30px;
}
</style>