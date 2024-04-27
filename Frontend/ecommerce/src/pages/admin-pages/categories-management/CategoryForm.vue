<template>
<div>

    <div id="popup-sign"  pdialog class="p-popup-sign">
        <div class="p-popup__header">Thông tin sản phẩm
            <button @click="btncloseForm" class="p-dialog__button--close icon-button-close"></button>  
        </div>
        <form >
            <div class="p-popup__form">
                <div class="item-image">
                    <MTextfieldFile  ref="image" textPlaceHolder="Hình ảnh" name="Hình ảnh" textLabel="Hình ảnh" @file-selected="handleFileSelected" :message="dialog.errors.image" :required ="true"> </MTextfieldFile>
                </div>
                <div class="item-code">
                    <MTextfield  ref="CategoriesCode" typeInput="text" textPlaceHolder="Mã danh mục   " name="Mã danh mục" textLabel="Mã danh mục" v-model="Categories.CategoriesCode" :message="dialog.errors.CategoriesCode" :required ="true"> </MTextfield>
                </div>
                <div class="item-name">
                    <MTextfield  ref="CategoriesName" typeInput="text" textPlaceHolder="Tên danh mục" name="Tên danh mục"  textLabel="Tên danh mục" v-model="Categories.CategoriesName" :message="dialog.errors.CategoriesName" :required ="true"> </MTextfield>
                </div>
                <div class="item-slug">
                    <MTextfield  ref="CategoriesSlug" typeInput="text" textPlaceHolder="Slug danh mục" name="Slug danh mục"  textLabel="Slug danh mục" v-model="Categories.CategoriesSlug" :message="dialog.errors.CategoriesSlug" :required ="true"> </MTextfield>
                </div>
                <div class="item-description">
                    <MTextfield  ref="CategoriesDescription" typeInput="text" textPlaceHolder="Mô tả danh mục" name="Mô tả danh mục"  textLabel="Mô tả sản phẩm" v-model="Categories.CategoriesDescription" :message="dialog.errors.CategoriesDescription" :required ="true"> </MTextfield>
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
import categoriesService from '../../../utils/CategoriesService';
import MTextfield from '../../../components/base/input/MTextfield.vue'; 
import MTextfieldFile from '../../../components/base/input/MTextfieldFile.vue'; 
export default {
    name:"ProductForm",
    props:[       
        "closeForm","FormMode","IdEntity"
    ],
    components:{
        MTextfield,MTextfieldFile
    },
    created() {
        this.checkFormMode();
    },
    data() {
        return {
            ImageFile:null,
            Categories:{},
            inputInValid:[],  
            dialog:{
                showDialog : false,
                response:"",
                title: "",
                icon: "",
                text: "",
                errors:{}
            }
        }
    },
    methods: {
        async checkFormMode(){
            if(this.FormMode === this.Enum.FormMode.UPDATE){
                var res = await categoriesService.getById(this.IdEntity);
                this.Categories = res.data;
            }else{
                this.Categories={}
                await this.takeNewCode();
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
         * Hàm thực hiện lấy mã nhân viên mới 
         * Author: TTPhong(22/01/2024)
         */
        async takeNewCode(){
            var res = await categoriesService.getNewCode();
            this.Categories.CategoriesCode = res.data;
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
                if(this.Categories.CategoriesCode === "" ||this.Categories.CategoriesCode === null || this.Categories.CategoriesCode === undefined){
                    this.dialog.errors.CategoriesCode = this.MISAResource["VN"].CategoriesCodeNotEmpty;
                    if(!this.checkElementFromArray(this.inputInValid, "CategoriesCode")){
                        this.inputInValid.push("CategoriesCode");
                    }
                }else{
                   delete(this.dialog.errors.CategoriesCode);
                   this.inputInValid=[];
                }

                //Check tên
                if(this.Categories.CategoriesName === "" ||this.Categories.CategoriesName === null || this.Categories.CategoriesName === undefined){
                    this.dialog.errors.CategoriesName = this.MISAResource["VN"].CategoriesNameNotEmpty;
                     if(!this.checkElementFromArray(this.inputInValid, "CategoriesName")){
                        this.inputInValid.push("CategoriesName");
                    }
                }else{
                    delete(this.dialog.errors.CategoriesName);
                    this.removeElementFromArray(this.inputInValid, "CategoriesName") ;
                }

                //4.Check Slug
                if(this.Categories.CategoriesSlug === "" ||this.Categories.CategoriesSlug === null || this.Categories.CategoriesSlug === undefined){
                    this.dialog.errors.CategoriesSlug = this.MISAResource["VN"].CategoriesSlugNotEmpty;
                    if(!this.checkElementFromArray(this.inputInValid, "CategoriesSlug")){
                        this.inputInValid.push("CategoriesSlug");
                    }
                }else{
                   delete(this.dialog.errors.CategoriesSlug);
                   this.inputInValid=[];
                }

                //Check mô tả
                if(this.Categories.CategoriesDescription === "" ||this.Categories.CategoriesDescription === null || this.Categories.CategoriesDescription === undefined){
                    this.dialog.errors.CategoriesDescription = this.MISAResource["VN"].CategoriesDescriptionNotEmpty;
                    if(!this.checkElementFromArray(this.inputInValid, "CategoriesDescription")){
                        this.inputInValid.push("CategoriesDescription");
                    }
                }else{
                   delete(this.dialog.errors.CategoriesDescription);
                   this.inputInValid=[];
                }

                if( this.dialog.errors.ImageFile || this.dialog.errors.CategoriesCode || this.dialog.errors.CategoriesName || this.dialog.errors.CategoriesSlug|| this.dialog.errors.CategoriesDescription){
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
                formData.append("dataJson", JSON.stringify(this.Categories));
                var res = await categoriesService.post(formData);
                if(res.data.Data === 1){
                    this.emitter.emit("showToast",this.Enum.ToastType.SUCCESS,"Thêm mới thành công !")
                    this.emitter.emit('loadDataPagingCategories');
                    this.Categories={};
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
                formData.append("dataJson", JSON.stringify(this.Categories));
                var res = await categoriesService.put(this.IdEntity,formData);
                if(res.data.Data === 1){
                    this.emitter.emit("showToast",this.Enum.ToastType.SUCCESS,"Cập nhật thành công !")
                    this.emitter.emit('loadDataPagingCategories');
                    this.closeForm();     
                    this.Categories={};
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
    grid-template-rows: 80px 80px 80px;
}
.item-slug,

.item-name{
    grid-column-start: 1;
    grid-column-end: 3;
}
.item-description,
.item-name{
    grid-column-start: 3;
    grid-column-end: 5;
}

.p-radiobutton2__option--item{
    display: flex;
    align-items: center;
    column-gap: 8px;
}

</style>