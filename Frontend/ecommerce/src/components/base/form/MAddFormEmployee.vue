<template>
<div>

    <div id="popup-sign"  pdialog class="p-popup-sign">
        <div class="p-popup__header">Thông tin sản phẩm
            <button @click="closeForm" class="p-dialog__button--close icon-button-close"></button>  
        </div>
        <form >
            <div class="p-popup__form">
                <div class="item-image">
                    <MTextfield  ref="image" typeInput="file" textPlaceHolder="Hình ảnh" name="Hình ảnh" textLabel="Hình ảnh" v-model="image" :message="dialog.errors.image" :required ="true"> </MTextfield>
                </div>
                <div class="item-code">
                    <MTextfield  ref="ProductCode" typeInput="text" textPlaceHolder="Mã sản phẩm   " name="Mã sản phẩm" textLabel="Mã sản phẩm" v-model="product.ProductCode" :message="dialog.errors.ProductCode" :required ="true"> </MTextfield>
                </div>
                <div class="item-price">
                    <MTextfield  ref="ProductPrice" :message="dialog.errors.ProductPrice"  typeInput="number" textPlaceHolder="Giá sản phẩm" textLabel="Giá sản phẩm" v-model="product.ProductPrice" :required ="true"> </MTextfield>                
                </div>
                <div class="item-brand-name">
                    <MTextfield  ref="ProductBrandName" :message="dialog.errors.ProductBrandName"  typeInput="text" textPlaceHolder="Tên hãng" textLabel="Tên hãng" v-model="product.ProductBrandName" :required ="true"> </MTextfield>
                </div>
                
                <div class="item-name">
                    <MTextfield  ref="ProductName" typeInput="text" textPlaceHolder="Tên sản phẩm" name="Tên sản phẩm"  textLabel="Tên sản phẩm" v-model="product.ProductName" :message="dialog.errors.ProductName" :required ="true"> </MTextfield>
                </div>
                <div class="item-categories">
                    <MCombobox ref="Categories" textLabel="Danh mục" :messageInvalid="dialog.errors.Categories" :required="true"  :dataCombobox="dataCombobox.Categories.CategoriesName"  @selected="onComboboxSelected" v-model="dataCombobox.Categories.selectedItem"></MCombobox>
                </div> 
                <div class="item-slug">
                    <MTextfield  ref="ProductSlug" typeInput="text" textPlaceHolder="Slug sản phẩm" name="Slug sản phẩm"  textLabel="Slug sản phẩm" v-model="product.ProductSlug" :message="dialog.errors.ProductSlug" :required ="true"> </MTextfield>
                </div>
                  
                
                
                <div class="p-popup-sign__button">
                    <div class="p-popup__button--left" >
                        <MButton className="p-button2" text="Hủy" @click="closeForm"></MButton>          
                    </div>
                    <div class="p-popup__button--right" >
                        <MButton className="p-button1" :text="this.formMode === this.Enum.FormMode.ADD? 'Thêm mới': 'Sửa thông tin'
              " @click="checkInvalid(true)"></MButton> 
                        <MButton v-if="this.formMode == this.Enum.FormMode.ADD" className="p-button1" text="Cất và thêm" @click="checkInvalid(false)" ></MButton> 
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
import productService from '../../../utils/ProductService';
import MCombobox from '../input/MCombobox.vue';
import MTextfield from '../input/MTextfield.vue'; 
export default {
    name:"MAddFormEmployee",
    props:[       
        "closeAddForm"
    ],
    components:{
        MTextfield,MCombobox
    },
    created() {
        this.takeDataCombobox();
        this.takeNewCode();
    },   
    data() {
        return {
            formMode:null,
            imageFile:null,
            product:{},
            isChangeForm:false,
            inputInValid:[],  
            dialog:{
                showDialog : false,
                response:"",
                title: "",
                icon: "",
                text: "",
                errors:{}
            },
            toast:{
                showToast: false,
                title:'',
                text:'',
                icon:'',
                textColor:''
            },
            dataCombobox : {
                Categories:{                   
                    CategoriesId:[],
                    CategoriesName:[],
                    selectedItem:null,
                }
            }
        }
    },
    methods: {
        async takeDataCombobox(){
           var res = await categoriesService.getAll();
           const list = res.data;
           list.forEach(element => {
               this.dataCombobox.Categories.CategoriesName.push(element.CategoriesName);
               this.dataCombobox.Categories.CategoriesId.push(element.CategoriesId);
           });
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
            var res = await productService.getNewCode();
            this.product.ProductCode = res.data;
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
            if(Object.keys(this.dialog.errors).length === 0 && this.isChangeForm === true){
                this.checkInvalid(true);
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
            this.closeAddForm();
            // this.resetDataDialog();
        },
        /**
         * Hàm set dữ liệu combobox Department
         *  Author: TTPhong(22/01/2024)
         */
        onComboboxSelected(selectedCombobox) {
            for (let i = 0; i < this.dataCombobox.Categories.CategoriesName.length; i++) {
                if(selectedCombobox === this.dataCombobox.Categories.CategoriesName[i]){
                    this.dataCombobox.Categories.selectedItem = selectedCombobox;
                    this.product.CategoriesId = this.dataCombobox.Categories.CategoriesId[i];
                } 
            }
        },
        /**
         * Hàm thực hiện hiển thị toast
         * Author: TTPhong(06/12/2023)
         */
        btnShowToast(){
            this.toast.showToast = true;
        },
        /**
         * Hàm thực hiện đóng Form thêm mới
         * Author: TTPhong (06/12/2023)
         */
        closeForm(){
            if(this.isChangeForm === true ){
                this.dialog.title = this.MISAResource["VN"].DataChanged;
                this.dialog.text = this.MISAResource["VN"].QuestionTextChangedFormData;
                this.dialog.icon = this.MISAResource["VN"].IconWarning;
                this.btnShowDialog();
            }else{

                this.closeAddForm();
            }

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
        checkInvalid(closeForm){
            try {
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
                //1.Check tên
                if(this.Product.ProductName === "" ||this.Product.ProductName === null || this.Product.ProductName === undefined){
                    this.dialog.errors.ProductName = this.MISAResource["VN"].ProductNameNotEmpty;
                     if(!this.checkElementFromArray(this.inputInValid, "ProductName")){
                        this.inputInValid.push("ProductName");
                    }
                }else{
                    delete(this.dialog.errors.ProductName);
                    this.removeElementFromArray(this.inputInValid, "ProductName") ;
                }
                //2.Check BrandName
                if(this.Product.ProductBrandName === "" ||this.Product.ProductBrandName === null || this.Product.ProductBrandName === undefined){
                    this.dialog.errors.ProductBrandName = this.MISAResource["VN"].ProductBrandNameNotEmpty;
                     if(!this.checkElementFromArray(this.inputInValid, "ProductBrandName")){
                        this.inputInValid.push("ProductBrandName");
                    }
                }else{
                    delete(this.dialog.errors.ProductBrandName);
                    this.removeElementFromArray(this.inputInValid, "ProductBrandName") ;
                }
                //3.CheckPrice
                if(this.Product.ProductPrice === "" ||this.Product.ProductPrice === null || this.Product.ProductPrice === undefined){
                    this.dialog.errors.ProductPrice = this.MISAResource["VN"].ProductPriceNotEmpty;
                    if(!this.checkElementFromArray(this.inputInValid, "ProductPrice")){
                        this.inputInValid.push("ProductPrice");
                    }
                }else{
                   delete(this.dialog.errors.ProductPrice);
                   this.inputInValid=[];
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

                //5.Check Categories
                if(this.dataCombobox.Categories.selectedItem === "" ||this.dataCombobox.Categories.selectedItem === null ||this.dataCombobox.Categories.selectedItem === undefined){
                    this.dialog.errors.Categories = this.MISAResource["VN"].CategoriesNotEmpty;
                     if(!this.checkElementFromArray(this.inputInValid, "Categories")){
                        this.inputInValid.push("Categories");
                    }
                }else{
                    delete( this.dialog.errors.Categories);
                    this.removeElementFromArray(this.inputInValid, "Categories") ;
                }

                if(this.dialog.errors.ProductCode || this.dialog.errors.ProductName || this.dialog.errors.Categories|| this.dialog.errors.ProductBrandName|| this.dialog.errors.ProductPrice|| this.dialog.errors.ProductSlug){
                    this.dialog.title= this.MISAResource["VN"].InvalidData;
                    this.dialog.icon = this.MISAResource["VN"].IconWarning;
                    this.btnShowDialog();
                }else{
                    if(this.formMode == this.Enum.FormMode.ADD){
                        this.onAdd(closeForm);
                    }
                    if(this.formMode == this.Enmu.FormMode.UPDATE){
                        this.onUpdate(closeForm)
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
        async onAdd(closeForm){
            try{
                var formData = new FormData();
                formData.append("imageFile", this.imageFile);
                formData.append("dataJson", JSON.stringify(this.product));
                var res = await productService.post(formData);
                if(res.data.Data === 1){
                    this.toast.title = this.MISAResource["VN"].Success;
                    this.toast.text = this.MISAResource["VN"].AddSuccess;
                    this.toast.icon = this.MISAResource["VN"].IconSuccessSmall;
                    this.emitter.emit("onShowToastMessage", this.toast.title, this.toast.text,this.toast.icon,this.toast.textColor)
                    setTimeout(() => {
                        this.emitter.emit('loadDataPagingEmployee');
                        if(closeForm === true){
                            this.closeAddForm();     
                        }else{
                            this.product={};
                            this.dataCombobox.Categories.selectedItem="";
                        }
                        this.takeNewEmployeeCode();
                    }, 2000);
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
        async onUpdate(closeForm){
            try{
                var formData = new FormData();
                formData.append("dataJson", JSON.stringify(this.product));
                var res = await productService.put(formData);
                if(res.data.Data === 1){
                    this.toast.title = this.MISAResource["VN"].Success;
                    this.toast.text = this.MISAResource["VN"].AddSuccess;
                    this.toast.icon = this.MISAResource["VN"].IconSuccessSmall;
                    this.emitter.emit("onShowToastMessage", this.toast.title, this.toast.text,this.toast.icon,this.toast.textColor)
                    setTimeout(() => {
                        this.emitter.emit('loadDataPagingEmployee');
                        if(closeForm === true){
                            this.closeAddForm();     
                        }else{
                            this.product={};
                            this.dataCombobox.Categories.selectedItem="";
                        }
                        this.takeNewEmployeeCode();
                    }, 2000);
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
    grid-template-rows: 100px 100px 100px 100px ;
}

.item-name,.item-slug{
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