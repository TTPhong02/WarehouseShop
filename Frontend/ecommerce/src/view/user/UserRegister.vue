<template>
  <div class="s-container">
    <div class="p-login">
        <div class="p-login-header">
            <img src="@/assets/img/logo.png" alt="">
        </div>
        <div class="p-login-form">
            <div class="p-login-form__input">
                <input
                @focus='handleFocus("fullname")'
                ref="fullname"
                class="input"
                :class="{'invalid': errors.Fullname  } " 
                type="text" 
                v-model="users.Fullname" 
                placeholder="Họ tên"
                @keydown.enter="validateForm()"
                >
                <span  class="message">{{this.errors.Fullname}}</span>
            </div>
            <div class="p-login-form__input">
                <input
                @focus='handleFocus("username")'
                ref="username"
                class="input"
                :class="{'invalid': errors.Email  } " 
                type="text" 
                v-model="users.Email" 
                placeholder="Email"
                @keydown.enter="validateForm()"
                >
                <span  class="message">{{this.errors.Email}}</span>
            </div>
            <div class="p-login-form__input">
                <div class="box-password">
                    <input 
                    @focus='handleFocus("password")'
                    ref="password"
                    class="input "
                    :class="{'invalid': errors.password} "  
                    :type="showPass ? 'text' : 'password'" 
                    v-model="users.password" 
                    placeholder="Mật khẩu"
                    @keydown.enter="validateForm()"
                    >
                    <div @click="this.showPass = !this.showPass" :class="showPass ? 'icon-show-pass' : 'icon-hide-pass'"></div>
                </div>
                <span class="message">{{this.errors.password}}</span>
            </div>
            <span class="message">{{this.errorMessageLogin}}</span>
            <!-- <MTextfield v-model="account.phoneNumber"  :required="true" textPlaceHolder="Số điện thoại/email"></MTextfield>
            <MTextfield v-model="account.password" :required="true" typeInput="password" textPlaceHolder="Mật khẩu"></MTextfield> -->
            <div class="p-login-action">
                <router-link to="/login" class="p-login-form-forget">Đã có tài khoản?</router-link>
            </div>
            <div class="p-login-form-button">
                <MButton  @click="validateForm()" text="Đăng ký" className="p-button5"></MButton>
            </div>
            <div class="p-login-form-title">
                <div class="p-login-form-title-line"><hr></div>
                <div class="p-login-form-title-text">Hoặc đăng nhập với</div>
                <div class="p-login-form-title-line"><hr></div>
            </div>     
            <div class="p-login-form-methods">
                <div class="icon-google item ">
                </div>
                <div class="icon-apple item">
                </div>
                <div class="icon-microsoft item">
                </div>
            </div>       
        </div>
    </div>
  </div>
</template>

<script>
import usersService from "../../utils/UserService.js";
export default {
    name:'UserLogin',
    components:{
        // MTextfield
    },
    data() {
        return {
            users:{},
            showPass: false,
            errors:{},
            errorMessageLogin:null,
            menuLanguage:false
        }
    },
    methods: {

        handleFocus(input) {
            // Chỉ thực hiện hành động nếu input chưa được focus trước đó
            if (input === 'username') {
                delete(this.errors.Email);
                this.errorMessageLogin="";
            }
            else if(input === 'fullname'){
                delete(this.errors.Fullname);
                this.errorMessageLogin="";
            }else{
                delete(this.errors.password);
                this.errorMessageLogin="";
            }
        },
        /**
         * hàm thực hiện kiểm tra dữ liệu form đăng nhập 
         * Author by : TTPhong(22/02/2024)
         */
        validateForm(){
            if(this.users.Fullname === null || this.users.Fullname === undefined || this.users.Fullname === ""){
                this.errors.Fullname = this.MISAResource["VN"].UsernameNotEmpty;
            }else{
                delete(this.errors.Fullname);
            }
            if(this.users.Email === null || this.users.Email === undefined || this.users.Email === ""){
                this.errors.Email = this.MISAResource["VN"].UsernameNotEmpty;
            }else{
                delete(this.errors.Email);
            }
            if(this.users.password === null || this.users.password === undefined || this.users.password === ""){
                this.errors.password = this.MISAResource["VN"].PasswordNotEmpty;
            }else{
                delete(this.errors.password);
            }
            if(!this.errors.password && !this.errors.Email&& !this.errors.Fullname){
                this.register();
            }
        },
        /**
         * Hàm thực hiện đăng nhập vào hệ thống
         * Author by: TTPhong(22/02/2024)
         */
        async register(){
            try{
                var res = await usersService.register(this.users);
                switch (res.status) {
                    case 201:
                        this.$router.push("/login");  
                        break;
                }
            }catch(error){
                this.emitter.emit("handleApiError",error);
            }
        }
    },
}
</script>

<style scoped>
    .p-login{
        background-color: #fff  ;
        width: 350px;
        border-radius: 8px;
        padding: 40px 48px 40px 48px;
        min-height: 340px;
        position: relative;
        box-shadow: 0 12px 20px rgba(0,0,0,.12);
    }
    .p-login-header img{
        width: 250px;
        height: 100px;
    }
    .p-login-header{
        display: flex;
        justify-content: center;
        align-items: center;
    }
    .p-login-header-title{
        font-size: 27px;
        font-weight: bold;
    }
    /* .p-login-form{
        padding: 20px 0px 0px 0px ;
    } */
    .p-login-form-title{
        display: flex;
        justify-content: center;
        align-items: center;
        padding-top: 26px;
        margin-bottom: 16px;
    }
    .p-login-action{
        display: flex;
        justify-content: space-between;
    }
    .p-login-form-forget:hover{
        color: #1ea2d2;
    }
    .p-login-form-forget{
        color: #a2c5d2;
        padding: 0px 0px 10px 0px;
        cursor: pointer;
    }
    .p-login-form-button{
        display: flex;
        justify-content: center;
        align-items: center;
    }
    .p-login-form-title-line{
        /* border: 1px solid #ccc; */
        width: 100%;
        flex: 1;
    }
     .p-login-form-title-text{
        color: #9ea1a5;
        padding: 0px 10px;
    }
    .p-login-form-methods{
        display: flex;
        justify-content: center;
        align-items: center;    
    }
    .p-login-form-methods .item{
        padding:0px 4px;   
    }
    .p-login-form__input{
        margin: 12px 0px;
    }
    .input{
        font-size: 16px;
        color: #212121;
        line-height: 1.2;
        width: 100%;
        height: 48px;
        border: 1px solid #e0e5e9;
        background: 0 0;
        padding: 0px 14px;
        border-radius: 3px;
    }
    .invalid{
        border: 1px solid red;
    }
    .message{
        text-align: inherit;
        font-size: 12px;
        color: #ff1d1d;
        height: 20px;
        line-height: 20px;
    }
    .s-container{
        border-radius: 8px;
        margin: auto;
        width: 100vw;
        height: 100vh;
        background-size: cover;
        display: flex;
        justify-content: center;
        align-items: center;
        background: url("@/assets/img/anh-nen-dang-nhap.png") repeat;
        background-color: rgba(162, 197, 210, 0.25  );
        box-shadow: 0px 2px 46.41px 4.59px rgba(2,38,113,0.1);
    }
     .box{
        position: relative;
        padding: 6px 10px;
     }
     .box-item{
        display: flex;
        justify-content: center;
        align-items: center;
     }
     .p-language-menu{
        position: absolute;
        top: 110%;
        left: 0%;
        width: 100%;
        box-shadow: 0 2px 8px rgba(0,0,0,.3);
        z-index: 9999;
        background-color: #fff;
        border-radius: 3px;
        max-height: 400px;
        padding-top: 8px;
        padding-bottom: 8px;
        overflow-x: hidden;
        overflow-y: hidden;
     }
     .p-language-menu-item{
        padding: 8px 16px;
     }
     .p-language-menu-item:hover{
        background-color: #f2f2f2;
     }
    .box-password{
        position: relative;
    }
    .icon-show-pass{
        position: absolute;
        top: 37%;
        right: 3%;
        cursor: pointer;
    }
    .icon-hide-pass{
        position: absolute;
        top: 33%;
        right: 3%;
        cursor: pointer;
    }
    .item{
        margin: 0px 4px;
    }
</style>