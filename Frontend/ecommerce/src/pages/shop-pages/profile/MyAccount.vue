<template>
  <div class="m-account">
    <div class="m-account-header">
        Thông tin tài khoản
    </div>
    <div class="m-account-infor">
        <div class=" row account-infor-item">
            <label class="col-lg-4 col-md-4 col-sm-4 col-4 ">Họ tên <span class="red">*</span></label>
            <div class="col-lg-8 col-md-8 col-sm-8 col-8 infor-item-input">
                <input v-model="this.user.Fullname" type="text">
                <span v-if="this.errorValidate.Fullname" class="p-input__validate--error">{{this.errorValidate.Fullname}}</span>
            </div>
        </div>
        <div class="row account-infor-item">
            <label class="col-lg-4 col-md-4 col-sm-4 col-4 ">Số điện thoại <span class="red">*</span></label>
            <div class="col-lg-8 col-md-8 col-sm-8 col-8 infor-item-input">
                <input v-model="this.user.PhoneNumber" type="text">
                <span v-if="this.errorValidate.PhoneNumber" class="p-input__validate--error">{{this.errorValidate.PhoneNumber}}</span>
            </div>
        </div>
        <div class="row account-infor-item">
            <label class="col-lg-4 col-md-4 col-sm-4 col-4 ">Email <span class="red">*</span></label>
            <div class="col-lg-8 col-md-8 col-sm-8 col-8 infor-item-input">
                <input v-model="this.user.Email" type="text">
                <span v-if="this.errorValidate.Email" class="p-input__validate--error">{{this.errorValidate.Email}}</span>
            </div>
        </div>
        <div class="row account-infor-item">
            <label class="col-lg-4 col-md-4 col-sm-4 col-4 ">Giới tính <span class="red">*</span></label>
            <div class="gender-option">
                <input v-model="this.user.Gender" class="col-lg-8 col-md-8 col-sm-8 col-8" name="gender" type="radio" value="0">
                <label for=" female">Nam</label>
                <input v-model="this.user.Gender" class="col-lg-8 col-md-8 col-sm-8 col-8" name="gender" type="radio" value="1">
                <label for="male">Nữ</label>
                <input v-model="this.user.Gender" class="col-lg-8 col-md-8 col-sm-8 col-8" name="gender" type="radio" value="2">
                <label for="male">Khác</label>
            </div>
        </div>
        <div class="row account-infor-item">
            <label class="col-lg-4 col-md-4 col-sm-4 col-4 ">Ngày sinh <span class="red">*</span></label>

            <div class="col-lg-8 col-md-8 col-sm-8 col-8 infor-item-input">
                <input v-model="formattedDate"  type="date">
                <span v-if="this.errorValidate.DateOfBirth" class="p-input__validate--error">{{this.errorValidate.DateOfBirth}}</span>
            </div>
        </div>
        <div class="row account-infor-item">
            <label class="col-lg-4 col-md-4 col-sm-4 col-4 "></label>
            <div class="col-lg-8 col-md-8 col-sm-8 col-8 change-password">
                <input @change="isChangePassword = !isChangePassword"  type="checkbox">
                <label class="col-lg-4 col-md-4 col-sm-4 col-4 ">Đổi mật khẩu</label>
            </div>
        </div>
        <div v-if="isChangePassword" class="account-change-password">
            <div class="row account-infor-item">
                <label class="col-lg-4 col-md-4 col-sm-4 col-4 ">Mật khẩu hiện tại <span class="red">*</span></label>
                <div class="col-lg-8 col-md-8 col-sm-8 col-8 infor-item-input">
                    <input v-model="this.user.CurrentPassword"  class="col-lg-8 col-md-8 col-sm-8 col-8" type="password">
                    <span v-if="this.errorValidate.CurrentPassword" class="p-input__validate--error red">{{this.errorValidate.CurrentPassword}}</span>
                </div>
            </div>
            <div class="row account-infor-item">
                <label  class="col-lg-4 col-md-4 col-sm-4 col-4 ">Mật khẩu mới <span class="red">*</span></label>
                <div class="col-lg-8 col-md-8 col-sm-8 col-8 infor-item-input">
                    <input v-model="this.user.NewPassword" class="col-lg-8 col-md-8 col-sm-8 col-8" type="password">
                    <span v-if="this.errorValidate.NewPassword" class="p-input__validate--error red">{{this.errorValidate.NewPassword}}</span>
                </div>
            </div>
            <div class="row account-infor-item">
                <label class="col-lg-4 col-md-4 col-sm-4 col-4 ">Nhập lại mật khẩu mới <span class="red">*</span></label>
                <div class="col-lg-8 col-md-8 col-sm-8 col-8 infor-item-input">
                    <input v-model="this.checkPassword" class="col-lg-8 col-md-8 col-sm-8 col-8" type="password">
                    <span v-if="this.errorValidate.checkPassword" class="p-input__validate--error red">{{this.errorValidate.checkPassword}}</span>
                </div>
            </div>
        </div> 
        <div class="account-infor-action">
            <button @click="updateUser()">Lưu thay đổi</button>
        </div>
    </div>
  </div>
</template>

<script>
import localStorageService from '../../../js/storage/LocalStorageService'
import usersService from '../../../utils/UserService';
export default {
    data() {
        return {
            isChangePassword:false,
            user:{},
            checkPassword:null,
            errorValidate:{}
        }
    },
    computed: {
        formattedDate() {
            if (this.user.DateOfBirth) {
                return this.user.DateOfBirth.substring(0, 10); // Định dạng ngày tháng
            }
            return ''; // Trả về chuỗi trống nếu không có ngày tháng sinh
        }
    },
    mounted() {
        this.takeDataUser();
    },
    methods: {
        validateDataNoPassword(){
            if(this.isChangePassword == false){
                delete(this.errorValidate.NewPassword);
                delete(this.errorValidate.CurrentPassword);
                delete(this.errorValidate.checkPassword);
            }
            if(this.user.Fullname == "" || this.user.Fullname == null ||this.user.Fullname == undefined){
                this.errorValidate.Fullname = "Họ tên không được để trống";
            }else{
                delete(this.errorValidate.Fullname);
            }
            if(this.user.PhoneNumber == "" || this.user.PhoneNumber == null ||this.user.PhoneNumber == undefined){
                this.errorValidate.PhoneNumber = "Số điện thoại không được để trống";
            }else{
                delete(this.errorValidate.PhoneNumber);
            }
            if(this.user.Email == "" || this.user.Email == null ||this.user.Email == undefined){
                this.errorValidate.Email = "Email không được để trống";
            }else{
                delete(this.errorValidate.Email);
            } 
        },
        validateDataPassword(){
            if( this.user.CurrentPassword == "" || this.user.CurrentPassword == null ||this.user.CurrentPassword == undefined){
                this.errorValidate.CurrentPassword = "Mật khẩu hiện tại không được để trống";
            }else{
                delete(this.errorValidate.CurrentPassword);
            }
            if( this.user.NewPassword == "" || this.user.NewPassword == null ||this.user.NewPassword == undefined){
                this.errorValidate.NewPassword = "Mật khẩu mới không được để trống";
            }else{
                delete(this.errorValidate.NewPassword);
            }
            if( this.checkPassword == "" || this.checkPassword == null || this.checkPassword == undefined){
                this.errorValidate.checkPassword = "Mật khẩu nhập lại không được để trống";
            }else{
                delete(this.errorValidate.checkPassword);
            } 
            if(this.checkPassword != ""  && this.user.NewPassword != "" && this.checkPassword != this.user.NewPassword){
                this.errorValidate.checkPassword = "Mật khẩu nhập lại không giống";
            }
            if( this.checkPassword === this.user.NewPassword){
                delete(this.errorValidate.checkPassword);
            }
                       
        },
        takeDataUser(){
            this.user = localStorageService.getItemFromLocalStorage("User");
        },
        async updateUser(){
            try{
                const formData = new FormData();
                formData.append("dataJson",JSON.stringify(this.user));
                if(this.isChangePassword == true){
                    this.validateDataNoPassword();
                    this.validateDataPassword();
                    console.log(this.user.CurrentPassword);
                    console.log(this.user.NewPassword);
                    console.log(this.checkPassword);
                    if(Object.keys(this.errorValidate).length === 0){
                        var resPassword = await usersService.update(this.user.UsersId,formData);
                        switch (resPassword.data.StatusCode) {
                            case 200:
                                this.emitter.emit("showToast",this.Enum.ToastType.SUCCESS,"Cập nhật thành công !")
                                var user1 = await usersService.getById(this.user.UsersId);
                                localStorageService.setItemToLocalStorage("User",user1.data);
                                this.takeDataUser();
                                this.emitter.emit("takeDataUsersHeading");
                                break;
                            default:
                                break;
                        }
                    }
                }else{
                    this.validateDataNoPassword();
                    if(Object.keys(this.errorValidate).length === 0){
                        var res = await usersService.update(this.user.UsersId,formData);
                        switch (res.data.StatusCode) {
                            case 200:
                                this.emitter.emit("showToast",this.Enum.ToastType.SUCCESS,"Cập nhật thành công");
                                var user = await usersService.getById(this.user.UsersId);
                                localStorageService.setItemToLocalStorage("User",user.data);
                                this.emitter.emit("takeDataUsersHeading");
                                this.takeDataUser();
                                break;
                            default:
                                break;
                        }
                    }
                }
            }catch(error){
                console.log(error);
            }
        },
    },
}
</script>

<style scoped>
.requied{
    color:red;
}
.infor-item-input{
    padding: 0px;
    display: flex;
    flex-direction: column;
}
.account-infor-action button:hover{
        background-color: #1ea2d2;
        transition: 0.5s;
}
.account-infor-action button{
    cursor: pointer;
    outline: none;
    color: #fff;
    border-radius: 10px ;
    background-color: #a2c5d2;
    padding: 10px 30px;
}
.account-infor-action{
    text-align: center;
}
.row{
    margin: 0px !important;
}
.change-password label,
.gender-option label{
    font-size: 16px;
}
.change-password input ,
.gender-option input[type="radio"]{
    width: 20px !important;
    height: 20px !important;
}
.account-infor-item input[type="date"],
.account-infor-item input[type="text"],
.account-infor-item input[type="password"]{
    height: 35px;
    padding: 5px 8px;
}
.account-infor-item{
    padding: 8px 0px;
}
.gender-option{
    display: flex;
    justify-content: left;
}
.account-infor-item{
    display: flex;
    align-items: center;
}
.m-account-header{
    padding-bottom: 10px;
    font-weight: bold;
    color: #a2c5d2;
    font-size: 17px;
}
.m-account{
    /* box-shadow: 0px 2px 46.41px 4.59px rgba(2,38,113,0.1); */
    padding: 25px 20px;
}
</style>