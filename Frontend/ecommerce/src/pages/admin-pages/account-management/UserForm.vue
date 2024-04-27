<template>
<div>

    <div id="popup-sign"  pdialog class="p-popup-sign">
        <div class="p-popup__header">Thông tin tài khoản
            <button @click="btncloseForm" class="p-dialog__button--close icon-button-close"></button>  
        </div>
        <form >
            <div class="p-popup__form">
                <div class="item-image">
                    <MTextfieldFile  ref="image" textPlaceHolder="Hình ảnh" name="Hình ảnh" textLabel="Hình ảnh" @file-selected="handleFileSelected" :message="dialog.errors.image" :required ="true"> </MTextfieldFile>
                </div>
                <div class="item-role">
                    <MCombobox ref="Role" textLabel="Phân quyền" :messageInvalid="dialog.errors.Role" :required="true"  :dataCombobox="dataCombobox.Role.Roles"  @selected="onComboboxRoleSelected" v-model="dataCombobox.Role.selectedItem"></MCombobox>
                </div> 
                <div class="item-gender">
                    <MRadioGender textLabel="Giới tính" v-model="Users.Gender" > </MRadioGender>
                </div>
                <div class="item-name">
                    <MTextfield  ref="Fullname" typeInput="text" textPlaceHolder="Họ tên" name="Họ tên"  textLabel="Họ tên" v-model="Users.Fullname" :message="dialog.errors.Fullname" :required ="true"> </MTextfield>
                </div>
                <div class="item-email">
                    <MTextfield  ref="Email" typeInput="text" textPlaceHolder="Email" name="Email" textLabel="Email" v-model="Users.Email" :message="dialog.errors.Email" :required ="true"> </MTextfield>
                </div>
                <div class="item-password">
                    <MTextfield  ref="Password" :message="dialog.errors.Password"  typeInput="password" textPlaceHolder="Mật khẩu" textLabel="Mật khẩu" v-model="Users.Password" :required ="true"> </MTextfield>                
                </div>
                <div class="item-phonenumber">
                    <MTextfield  ref="PhoneNumber" typeInput="text" textPlaceHolder="Số điện thoại" name="Số điện thoại"  textLabel="Số điện thoại" v-model="Users.PhoneNumber" :message="dialog.errors.PhoneNumber" :required ="true"> </MTextfield>
                </div>
                <div class="item-status">
                    <MCombobox ref="Status" textLabel="Trạng thái" :messageInvalid="dialog.errors.Status" :required="true"  :dataCombobox="dataCombobox.Status.StatusData"  @selected="onComboboxStatusSelected" v-model="dataCombobox.Status.selectedItem"></MCombobox>
                </div> 
                <div class="item-date">
                    <MTextfieldDate  ref="DateOfBirth" textPlaceHolder="Ngày sinh" name="Ngày sinh"  textLabel="Ngày sinh" v-model="Users.DateOfBirth" :message="dialog.errors.DateOfBirth" :required ="true"> </MTextfieldDate>
                </div>
                <div class="p-popup-sign__button">
                    <div class="p-popup__button--left" >
                        <MButton className="p-button2" text="Hủy" @click="btncloseForm"></MButton>          
                    </div>
                    <div class="p-popup__button--right" >
                        <MButton className="p-button1" :text="this.FormMode == this.Enum.FormMode.ADD ? 'Thêm mới': 'Sửa thông tin'
              " @click="checkInvalid()"></MButton> 
                    </div>
                </div>

            </div>
            
        </form>
    

    </div>
    <MDialog :text="dialog.text" :iconDialog="dialog.icon" v-if="dialog.showDialog" :dialogResFalse="btnSetFalseResponseDialog" :dialogResTrue="btnSetTrueResponseDialog" :title="dialog.title" :errors="dialog.errors" ></MDialog>

</div>
</template>
<script>
import usersService from '../../../utils/UserService';
import MCombobox from '../../../components/base/input/MCombobox.vue';
import MTextfield from '../../../components/base/input/MTextfield.vue'; 
import MRadioGender from '../../../components/base/input/MRadioGender.vue'; 
import MTextfieldFile from '../../../components/base/input/MTextfieldFile.vue'; 
import MTextfieldDate from '../../../components/base/input/MTextfieldDate.vue'; 
export default {
    props:[       
        "closeForm","FormMode","IdEntity"
    ],
    components:{
        MTextfield,MCombobox,MTextfieldFile,MRadioGender,MTextfieldDate
    },
    created() {
        this.checkFormMode();
    },
    data() {
        return {
            ImageFile:null,
            Users:{},
            inputInValid:[],  
            dialog:{
                showDialog : false,
                response:"",
                title: "",
                icon: "",
                text: "",
                errors:{}
            },
            dataCombobox : {
                Role:{                   
                    Roles:["User","Admin"],
                    selectedItem:null,
                },
                Status:{
                    StatusData:["Hoạt động","Dừng hoạt động"],
                    selectedItem:null,
                }
            }
        }
    },
    methods: {
        async checkFormMode(){
            if(this.FormMode === this.Enum.FormMode.UPDATE){
                var res = await usersService.getById(this.IdEntity);
                this.Users = res.data;
                this.dataCombobox.Role.selectedItem = this.Users.Role;
                this.dataCombobox.Status.selectedItem = this.helper.formatAccountStatus(this.Users.Status);
            }else{
                this.dataCombobox.Role.selectedItem = null;
                this.dataCombobox.Status.selectedItem = null;
                this.Users={}
            }
        },
        handleFileSelected(file) {
            this.ImageFile = file;
        },

        /**
         * Hàm thực hiện focus vào ô đầu tiên
         * Author : TTphong(22/01/2024)
         */
        focusInputInvalid(inputFocus){
            this.$refs[inputFocus].focusToInput();
        },
        /**
         * Hàm thực hiện xóa phần tử khỏi mảng
         * Author: TTPhong (06/12/2023)
         */
        removeElementFromArray(arr, elementToRemove) {
            const indexToRemove = arr.indexOf(elementToRemove);
            if (indexToRemove !== -1) {
                arr.splice(indexToRemove, 1);
            }
        },
        /**
         * Hàm thực hiện xóa hêt dữ liệu dialog
         * Author: TTPhong (26/01/2024)
         */
        resetDataDialog(){
            this.dialog.title=""
            this.dialog.icon=""
            this.dialog.text=""
            this.dialog.errors={};
        },
        /**
         * Hàm set respone dialog
         *  Author: TTPhong(22/01/2024)
         */
        btnSetTrueResponseDialog(){
            this.dialog.response = true;
            this.dialog.showDialog = false;
            if(Object.keys(this.dialog.errors).length > 0){
                this.focusInputInvalid(this.inputInValid[0]);
            }
            // this.resetDataDialog();
        },
        /**
         * Hàm set respone dialog
         *  Author: TTPhong(22/01/2024)
         */
        btnSetFalseResponseDialog(){
            this.dialog.response = false;
            this.dialog.showDialog = false;

            if(Object.keys(this.dialog.errors).length > 0){
                this.focusInputInvalid(this.inputInValid[0]);
            }
        },
        /**
         * Hàm set dữ liệu combobox Department
         *  Author: TTPhong(22/01/2024)
         */
        onComboboxRoleSelected(selectedCombobox) {    
            this.Users.Role = selectedCombobox
        },
        onComboboxStatusSelected(selectedCombobox) {    
            if(selectedCombobox === "Đang hoạt động"){
                this.Users.Status = this.Enum.AccountStatus.Active
            }
            if(selectedCombobox === "Dừng hoạt động"){
                this.Users.Status = this.Enum.AccountStatus.Unactive
            }
        },
        /**
         * Hàm thực hiện đóng Form thêm mới
         * Author: TTPhong (06/12/2023)
         */
        btncloseForm(){
                this.closeForm();

        },
        /**
         * Hàm thực hiện hiển thị dialog cảnh báo
         * Author: TTPhong(06/12/2023)
         */
        btnShowDialog(){
            this.dialog.showDialog = true;
        },
        /**
         * Hàm thực hiện ẩn dialog cảnh báo
         * Author: TTPhong(06/12/2023)
         */
        btnCloseDialog(){
            this.dialog.showDialog = false;
        },
        /**
         * Hàm thực hiện kiểm tra phần tử có trong mảng hay không
         *  Author: TTPhong(22/01/2024)
         */
        checkElementFromArray(arr, elementToCheck){
            for (let i = 0; i < arr.length; i++) {
                if(elementToCheck === arr[i]){
                    return true;
                }
                return false;
                
            }
        },
        /**
         * Hàm kiểm tra dữ liệu khi nhập vào
         *  Author: TTPhong(22/01/2024)
         */
        checkInvalid(){
            try {
                //Check ảnh 
                if(this.ImageFile === "" && this.FormMode === this.Enum.FormMode.ADD || this.ImageFile === null && this.FormMode === this.Enum.FormMode.ADD || this.ImageFile === undefined && this.FormMode === this.Enum.FormMode.ADD){
                    this.dialog.errors.ImageFile = this.MISAResource["VN"].ImageFileNotEmpty;
                }else{
                   delete(this.dialog.errors.ImageFile);
                }

                //Check mã 
                if(this.Product.ProductCode === "" ||this.Product.ProductCode === null || this.Product.ProductCode === undefined){
                    this.dialog.errors.ProductCode = this.MISAResource["VN"].ProductCodeNotEmpty;
                    if(!this.checkElementFromArray(this.inputInValid, "ProductCode")){
                        this.inputInValid.push("ProductCode");
                    }
                }else{
                   delete(this.dialog.errors.ProductCode);
                   this.inputInValid=[];
                }

                //Check danh mục
                if(this.dataCombobox.Categories.selectedItem === "" ||this.dataCombobox.Categories.selectedItem === null ||this.dataCombobox.Categories.selectedItem === undefined){
                    this.dialog.errors.Categories = this.MISAResource["VN"].CategoriesNotEmpty;
                     if(!this.checkElementFromArray(this.inputInValid, "Categories")){
                        this.inputInValid.push("Categories");
                    }
                }else{
                    delete( this.dialog.errors.Categories);
                    this.removeElementFromArray(this.inputInValid, "Categories") ;
                }

                //Check tên
                if(this.Product.ProductName === "" ||this.Product.ProductName === null || this.Product.ProductName === undefined){
                    this.dialog.errors.ProductName = this.MISAResource["VN"].ProductNameNotEmpty;
                     if(!this.checkElementFromArray(this.inputInValid, "ProductName")){
                        this.inputInValid.push("ProductName");
                    }
                }else{
                    delete(this.dialog.errors.ProductName);
                    this.removeElementFromArray(this.inputInValid, "ProductName") ;
                }

                //CheckPrice
                if(this.Product.ProductPrice === "" ||this.Product.ProductPrice === null || this.Product.ProductPrice === undefined){
                    this.dialog.errors.ProductPrice = this.MISAResource["VN"].ProductPriceNotEmpty;
                    if(!this.checkElementFromArray(this.inputInValid, "ProductPrice")){
                        this.inputInValid.push("ProductPrice");
                    }
                }else if(this.Product.ProductPrice < 0){
                    this.dialog.errors.ProductPrice = this.MISAResource["VN"].ProductPriceNotSmallThanZero;
                    if(!this.checkElementFromArray(this.inputInValid, "ProductPrice")){
                        this.inputInValid.push("ProductPrice");
                    }
                }else{
                   delete(this.dialog.errors.ProductPrice);
                   this.inputInValid=[];
                }

                //Check BrandName
                if(this.Product.ProductBrandName === "" ||this.Product.ProductBrandName === null || this.Product.ProductBrandName === undefined){
                    this.dialog.errors.ProductBrandName = this.MISAResource["VN"].ProductBrandNameNotEmpty;
                     if(!this.checkElementFromArray(this.inputInValid, "ProductBrandName")){
                        this.inputInValid.push("ProductBrandName");
                    }
                }else{
                    delete(this.dialog.errors.ProductBrandName);
                    this.removeElementFromArray(this.inputInValid, "ProductBrandName") ;
                }

                //4.Check Slug
                if(this.Product.ProductSlug === "" ||this.Product.ProductSlug === null || this.Product.ProductSlug === undefined){
                    this.dialog.errors.ProductSlug = this.MISAResource["VN"].ProductSlugNotEmpty;
                    if(!this.checkElementFromArray(this.inputInValid, "ProductSlug")){
                        this.inputInValid.push("ProductSlug");
                    }
                }else{
                   delete(this.dialog.errors.ProductSlug);
                   this.inputInValid=[];
                }

                //check số lượng kho
                if(this.Product.ProductStock === "" ||this.Product.ProductStock === null || this.Product.ProductStock === undefined){
                    this.dialog.errors.ProductStock = this.MISAResource["VN"].ProductStockNotEmpty;
                    if(!this.checkElementFromArray(this.inputInValid, "ProductStock")){
                        this.inputInValid.push("ProductStock");
                    }
                }else if(this.Product.ProductStock < 0){
                    this.dialog.errors.ProductStock = this.MISAResource["VN"].ProductStockNotSmallThanZero;
                    if(!this.checkElementFromArray(this.inputInValid, "ProductStock")){
                        this.inputInValid.push("ProductStock");
                    }
                }else{
                   delete(this.dialog.errors.ProductStock);
                   this.inputInValid=[];
                }

                //Check số lượng bán
                if(this.Product.ProductSold < 0){
                    this.dialog.errors.ProductSold = this.MISAResource["VN"].ProductSoldNotSmallThanZero;
                    if(!this.checkElementFromArray(this.inputInValid, "ProductSold")){
                        this.inputInValid.push("ProductSold");
                    }
                }

                //Check mô tả
                if(this.Product.ProductDescription === "" ||this.Product.ProductDescription === null || this.Product.ProductDescription === undefined){
                    this.dialog.errors.ProductDescription = this.MISAResource["VN"].ProductDescriptionNotEmpty;
                    if(!this.checkElementFromArray(this.inputInValid, "ProductDescription")){
                        this.inputInValid.push("ProductDescription");
                    }
                }else{
                   delete(this.dialog.errors.ProductDescription);
                   this.inputInValid=[];
                }

                if(this.dialog.errors.ProductStock ||this.dialog.errors.ProductSold || this.dialog.errors.ImageFile || this.dialog.errors.ProductCode || this.dialog.errors.ProductName || this.dialog.errors.Categories|| this.dialog.errors.ProductBrandName|| this.dialog.errors.ProductPrice|| this.dialog.errors.ProductSlug|| this.dialog.errors.ProductDescription){
                    this.dialog.title= this.MISAResource["VN"].InvalidData;
                    this.dialog.icon = this.MISAResource["VN"].IconWarning;
                    this.btnShowDialog();
                }else{
                    if(this.FormMode == this.Enum.FormMode.ADD){
                        this.onAdd();
                    }
                    if(this.FormMode == this.Enum.FormMode.UPDATE){
                        this.onUpdate()
                    }
                }
            } catch (error) {
                console.error(error);
            }
        },
        /**
         * Hàm thực hiện thêm 
         *  Author: TTPhong(22/01/2024)
         */
        async onAdd(){
            try{
                var formData = new FormData();
                formData.append("imageFile", this.ImageFile);
                formData.append("dataJson", JSON.stringify(this.Product));
                var res = await usersService.post(formData);
                if(res.data.Data === 1){
                    this.emitter.emit("showToast",this.Enum.ToastType.SUCCESS,"Thêm mới thành công !")
                    this.emitter.emit('loadDataPagingEmployee');
                    this.Product={};
                    this.dataCombobox.Categories.selectedItem="";
                    this.closeForm();     

                }
            }catch(error ){
                const response = error.response;
                const status = response.status;
                this.emitter.emit("handleApiError",error);
                switch(status){
                    case 400:
                        this.dialog.title= this.MISAResource["VN"].InvalidData;
                        this.inputInValid = [];
                        this.inputInValid.push("employeeCode");
                        this.dialog.text = response.data.Errors ;
                        this.dialog.icon = this.MISAResource["VN"].IconDangerous;    
                        break;
                    default:
                        break;
                }
                this.btnShowDialog();
            }

        },
        /**
         * Hàm thực hiện thêm 
         *  Author: TTPhong(22/01/2024)
         */
        async onUpdate(){
            try{
                var formData = new FormData();
                formData.append("dataJson", JSON.stringify(this.Product));
                var res = await usersService.put(this.IdEntity,formData);
                if(res.data.Data === 1){
                    this.emitter.emit("showToast",this.Enum.ToastType.SUCCESS,"Cập nhật thành công !")
                    this.emitter.emit('loadDataPagingEmployee');
                    this.closeForm();     
                    this.Product={};
                    this.dataCombobox.Categories.selectedItem="";


                }
            }catch(error){
                console.log(error);
                const response = error.response;
                const status = response.status;
                this.emitter.emit("handleApiError",error);
                switch(status){
                    case 400:
                        this.dialog.title= this.MISAResource["VN"].InvalidData;
                        this.inputInValid = [];
                        this.inputInValid.push("employeeCode");
                        this.dialog.text = response.data.Errors ;
                        this.dialog.icon = this.MISAResource["VN"].IconDangerous;    
                        break;
                    default:
                        break;
                }
                this.btnShowDialog();
            }

        },
    },
}
</script>
<style scoped>
#popup-sign {
    z-index: 10;
}
.p-popup{
    background-color: #fff;
    width: 348px;
    box-shadow:  0px 1px 30px 2px #888888;
}
.p-popup__header{
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 24px 24px;
    font-size: 24px;
    font-weight: 700;
}
.p-popup__header img{
    cursor: pointer;
    width: 24px;
    height: 24px;
    vertical-align: middle;
}
.p-popup__text{
    font-size: 14px;
    text-align: left;
    padding-left: 24px;
}
.p-popup__textfield{
    padding: 16px 24px 16px 24px;
}
.p-popup__combobox{
    padding: 0px 24px 24px 24px;
}
.p-popup-sign__button{
    grid-column-start: 1;
    grid-column-end: 5;
    border-top: 1px solid #E0E0E0;
    display: flex;
    justify-content: space-between;
    border-radius: 0px 0px 4px 4px;
}
.p-popup__button--left{
    padding: 12px 24px ;
}
.p-popup__button--right{
    padding: 12px 24px;
    text-align: right;
    display: flex;
    column-gap: 12px;
}
.p-popup-sign{
    /* display: none; */
    position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
    width: 1212px;
    border-radius: 4px;
    background-color: #fff;
    box-shadow:  0px 1px 30px 2px #888888;
}
.p-popup__form{
    padding: 0px 24px;
    display: grid;
    column-gap: 20px;
    row-gap: 18px;
    grid-template-columns: 250px 350px 250px 250px;
    grid-template-rows: 80px 80px 80px 80px ;
}
.item-description,
.item-slug{
    grid-column-start: 1;
    grid-column-end: 3;
}
.item-categories{
    grid-column-start: 3;
    grid-column-end: 5;
}

.p-radiobutton2__option--item{
    display: flex;
    align-items: center;
    column-gap: 8px;
}

</style>