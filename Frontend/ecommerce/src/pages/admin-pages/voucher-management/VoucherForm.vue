<template>
<div>

    <div id="popup-sign"  pdialog class="p-popup-sign">
        <div class="p-popup__header">Thông tin khuyến mãi
            <button @click="btncloseForm" class="p-dialog__button--close icon-button-close"></button>  
        </div>
        <form >
            <div class="p-popup__form">
                <div class="item-code">
                    <MTextfield  ref="VoucherCode" typeInput="text" textPlaceHolder="Mã khuyến mãi   " name="Mã khuyến mãi" textLabel="Mã khuyến mãi" v-model="Voucher.VoucherCode" :message="dialog.errors.VoucherCode" :required ="true"> </MTextfield>
                </div>
                <div class="item-order-status">
                    <MCombobox ref="VoucherType" textLabel="Loại khuyến mãi" :messageInvalid="dialog.errors.VoucherType" :required="true"  :dataCombobox="dataCombobox.VoucherType.Data"  @selected="onComboboxSelected" v-model="dataCombobox.VoucherType.selectedItem"></MCombobox>
                </div>
                <div class="item-note">
                    <MTextfield  ref="ExpiredDate" typeInput="date" textPlaceHolder="Ngày hết hạn" name="Ngày hết hạn"  textLabel="Ngày hết hạn" v-model="Voucher.ExpiredDate" :message="dialog.errors.ExpiredDate" :required ="true"> </MTextfield>
                </div>
                <div class="item-address">
                    <MTextfield  ref="AmountDiscount" :message="dialog.errors.AmountDiscount"  typeInput="number" textPlaceHolder="% Giảm giá" textLabel="% Giảm giá" v-model="Voucher.AmountDiscount" :required ="true"> </MTextfield>
                </div>
                <div class="item-name">
                    <MTextfield  ref="AmountCondition" typeInput="number" textPlaceHolder="Điều kiện (nếu có)" name="Điều kiện (nếu có)"  textLabel="Điều kiện (nếu có)" v-model="Voucher.AmountCondition" :message="dialog.errors.AmountCondition" > </MTextfield>
                </div>
                <div class="item-address">
                    <MTextfield  ref="AmountDiscount" :message="dialog.errors.Quantity"  typeInput="number" textPlaceHolder="Số lượt sử dụng" textLabel="Số lượt sử dụng" v-model="Voucher.Quantity" :required ="true"> </MTextfield>
                </div>
                <div class="item-amount">
                    <MTextfield  ref="Description" :message="dialog.errors.Description"  typeInput="text" textPlaceHolder="Mô tả" textLabel="Mô tả" v-model="Voucher.Description"> </MTextfield>                
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
import MCombobox from '../../../components/base/input/MCombobox.vue';
import MTextfield from '../../../components/base/input/MTextfield.vue'; 
import ordersService from '../../../utils/OrdersService';
import voucherService from '../../../utils/VoucherService';
// import MTextfieldDate from '../../../components/base/input/MTextfieldDate.vue';

export default {
    props:[       
        "closeForm","FormMode","IdEntity"
    ],
    components:{
        MCombobox,MTextfield
    },
    created() {
        this.checkFormMode();
    },
    data() {
        return {
            ImageFile:null,
            Voucher:{},
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
                VoucherType:{
                    Data:["Phí ship","Tiền hàng"],
                    selectedItem:null
                }
            }
        }
    },
    watch:{
        'dataCombobox.VoucherType.selectedItem':function(newValue){
            if(newValue == "Phí ship"){
                this.Voucher.VoucherType = this.Enum.VoucherType.SHIPPING;
            }else if(newValue == "Tiền hàng"){
                this.Voucher.VoucherType = this.Enum.VoucherType.PRICE;
            }
        }
    },
    methods: {
        async checkFormMode(){
            if(this.FormMode === this.Enum.FormMode.UPDATE){
                var res = await voucherService.getById(this.IdEntity);
                this.Voucher = res.data;
            }else{
                this.Voucher={}
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
            var res = await ordersService.getNewCode();
            this.Orders.OrdersCode = res.data;
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
        onComboboxSelected(selectedCombobox) {
            this.dataCombobox.VoucherType.selectedItem = selectedCombobox;
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

                //Check mã 
                if(this.Voucher.VoucherCode === "" ||this.Voucher.VoucherCode === null || this.Voucher.VoucherCode === undefined){
                    this.dialog.errors.VoucherCode = this.MISAResource["VN"].VoucherCodeNotEmpty;
                    if(!this.checkElementFromArray(this.inputInValid, "VoucherCode")){
                        this.inputInValid.push("VoucherCode");
                    }
                }else{
                   delete(this.dialog.errors.VoucherCode);
                   this.inputInValid=[];
                }
                //Check danh mục
                if(this.dataCombobox.VoucherType.selectedItem === "" ||this.dataCombobox.VoucherType.selectedItem === null ||this.dataCombobox.VoucherType.selectedItem === undefined){
                    this.dialog.errors.VoucherType = this.MISAResource["VN"].VoucherTypeNotEmpty;
                     if(!this.checkElementFromArray(this.inputInValid, "VoucherType")){
                        this.inputInValid.push("VoucherType");
                    }
                }else{
                    delete( this.dialog.errors.VoucherType);
                    this.removeElementFromArray(this.inputInValid, "VoucherType") ;
                }

                //Check tên
                if(this.Voucher.ExpiredDate === "" ||this.Voucher.ExpiredDate === null || this.Voucher.ExpiredDate === undefined){
                    this.dialog.errors.ExpiredDate = this.MISAResource["VN"].ExpiredDateNotEmpty;
                     if(!this.checkElementFromArray(this.inputInValid, "ExpiredDate")){
                        this.inputInValid.push("ExpiredDate");
                    }
                }else{
                    delete(this.dialog.errors.ExpiredDate);
                    this.removeElementFromArray(this.inputInValid, "ExpiredDate") ;
                }

                //CheckPrice
                if(this.Voucher.AmountDiscount === "" ||this.Voucher.AmountDiscount === null || this.Voucher.AmountDiscount === undefined){
                    this.dialog.errors.AmountDiscount = this.MISAResource["VN"].AmountDiscountNotEmpty;
                    if(!this.checkElementFromArray(this.inputInValid, "AmountDiscount")){
                        this.inputInValid.push("AmountDiscount");
                    }
                }else if(this.Voucher.AmountDiscount < 0){
                    this.dialog.errors.AmountDiscount = this.MISAResource["VN"].AmountDiscountNotSmallThanZero;
                    if(!this.checkElementFromArray(this.inputInValid, "AmountDiscount")){
                        this.inputInValid.push("AmountDiscount");
                    }
                }else{
                   delete(this.dialog.errors.AmountDiscount);
                   this.inputInValid=[];
                }
                //CheckPrice
                if(this.Voucher.Quantity === "" ||this.Voucher.Quantity === null || this.Voucher.Quantity === undefined){
                    this.dialog.errors.Quantity = this.MISAResource["VN"].QuantityNotEmpty;
                    if(!this.checkElementFromArray(this.inputInValid, "Quantity")){
                        this.inputInValid.push("Quantity");
                    }
                }else if(this.Voucher.Quantity < 0){
                    this.dialog.errors.Quantity = this.MISAResource["VN"].QuantityNotSmallThanZero;
                    if(!this.checkElementFromArray(this.inputInValid, "Quantity")){
                        this.inputInValid.push("Quantity");
                    }
                }else{
                   delete(this.dialog.errors.Quantity);
                   this.inputInValid=[];
                }
            

                if(this.dialog.errors.VoucherCode ||this.dialog.errors.VoucherType || this.dialog.errors.ExpiredDate || this.dialog.errors.Quantity ||this.dialog.errors.AmountDiscount){
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
                formData.append("dataJson", JSON.stringify(this.Voucher));
                var res = await voucherService.post(formData);
                if(res.data.Data === 1){
                    this.emitter.emit("showToast",this.Enum.ToastType.SUCCESS,"Thêm mới thành công !")
                    this.Voucher={};
                    this.dataCombobox.VoucherType.selectedItem="";
                    this.closeForm();     

                }
            }catch(error ){
                console.log(error);
            }

        },
        /**
         * Hàm thực hiện thêm 
         *  Author: TTPhong(22/01/2024)
         */
        async onUpdate(){
            try{
                var formData = new FormData();
                formData.append("dataJson", JSON.stringify(this.Voucher));
                var res = await voucherService.put(this.IdEntity,formData);
                if(res.data.Data === 1){
                    this.emitter.emit("showToast",this.Enum.ToastType.SUCCESS,"Cập nhật thành công !")
                    this.emitter.emit('loadDataPagingProduct');
                    this.closeForm();     
                    this.Voucher={};
                    this.dataCombobox.Voucher.selectedItem="";


                }
            }catch(error){
                console.log(error);
                
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
    overflow-y: scroll;
    overflow-x: hidden ;
    height: 300px;
    padding: 0px 24px;
    display: grid;
    column-gap: 20px;
    row-gap: 18px;
    grid-template-columns: 250px 350px 250px 250px;
    grid-template-rows: 80px 80px 80px ;
}

.item-description,
.item-slug{
    grid-column-start: 1;
    grid-column-end: 3;
}
.item-amount,
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