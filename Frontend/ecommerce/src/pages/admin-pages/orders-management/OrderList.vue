<template>
    <div>
        <div class="p-home__main--header">
            <div class="p-home__main--header--title">
                Quản lý đơn hàng
            </div>
            <div class="p-home__main--header--button">
                <button  @click="btnShowForm()" id="showAddForm" class="p-button1-plus show-popup-sign ">
                    <i class="fa-solid fa-plus"></i>
                        Thêm mới
                </button>
            </div>
        </div>
        <div class="p-home__main--action">
            <div  class="left p-home__main--action--button">
                <div v-if="listChecked.length > 0"   class="p-table__number">Đã chọn {{listChecked.length}} <strong id="selected-count"></strong></div>
                <div v-if="listChecked.length > 0"  @click="this.emitter.emit('deselectAll')" class="p-table__unselect">Bỏ chọn</div>
                <button v-if="listChecked.length > 0" @click="deleteManyProduct()"   class="p-table__deleteall p-button2 show-home-dialog">
                    <i class="fa-solid fa-trash-can"></i>
                    Xóa tất cả
                </button>
                <div class="combobox-status-order">
                    <MCombobox textPlaceHolder="Trạng thái đơn hàng" v-model="this.dataCombobox.statusOrder.selected" :dataCombobox="this.dataCombobox.statusOrder.data"></MCombobox>
                    <MCombobox textPlaceHolder="Trạng thái thanh toán" v-model="this.dataCombobox.statusPayment.selected" :dataCombobox="this.dataCombobox.statusPayment.data"></MCombobox>
                    <MCombobox textPlaceHolder="Trạng thái vận chuyển" v-model="this.dataCombobox.statusDelivery.selected" :dataCombobox="this.dataCombobox.statusDelivery.data"></MCombobox>
                </div>
            </div>
            <div class="p-home__main--action--button">
                <MTextSearch :searchMethod="loadDataPagingOrders" v-model="filter.paging.searchString"></MTextSearch>
                <button @click="btnShowFormImport" class="p-button3 "> 
                    <i class="fa-solid fa-file-import"></i>
                </button>
                <button @click="exportEmployee()" class=" p-button3 ">
                    <i class="fa-solid fa-file-excel"></i>
                </button>
                
                <button   id="p-btnLoadData" @click="this.emitter.emit('loadDataPagingOrders')"  class="p-button3 ">
                    <i class="fa-solid fa-arrow-rotate-right"></i>
                </button>
            </div>
        </div>
        <div class="p-home__main--table">
            <div class="p-grid">
                <div>
                    <table id="p-tblCustomer" class="p-table small-first-col">
                        <thead>
                            <tr class="p-table__row ">
                                <th>
                                    <input type="checkbox"  @click="selectAll" v-model="allSelected" class="checkAll">
                                </th>
                                <th>Mã đơn hàng</th>
                                <th>Trạng thái</th>
                                <th>Thanh toán</th>
                                <th>Vận chuyển</th>
                                <th>Khách hàng</th>
                                <th>Tổng tiền</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="item in Orders" :key="item.OrdersId" class="p-table__row">
                                <td ><input v-model="checkedIDs" @click="select" :value="item.OrdersId" type="checkbox"></td>
                                <td>{{item.OrdersCode}}</td>
                                <td>{{this.helper.formatOrderStatus(item.OrdersStatus)}}</td>
                                <td>{{this.helper.formatPaymentStatus(item.PaymentStatus)}}</td>
                                <td>{{this.helper.formatDeliveryStatus(item.DeliveryStatus)}}</td>
                                <td>{{item.ReminiscentName}}</td>
                                <td>{{this.helper.formatMoney(item.TotalAmount)}}</td>
                                <div class="p-tbl-option-fixed">
                                    <div class="p-tbl-option-fixed-position">
                                        <router-link :to="'/admin/orders-management/' + item.OrdersId" class="p-button-table-option ">   
                                            <i class="fa-solid fa-pencil"></i>                   
                                        </router-link>
                                    </div>
                                </div>
                            </tr>
                        </tbody>     
                    </table> 
                </div>
            </div>
            <div class="p-footertable">
                <div class="left p-footertable__total">
                    Tổng: <span id="numberrow"> {{totalRecord}}</span> bản ghi
                </div>
                <div class="right p-footertable__action">
                    <div class="p-footertable__action--page">Số bản ghi/Trang</div>
                    <div class="p-footertable__action--numbercombobox"> 
                        <MCombobox positionList="above" typeInput="number" :dataCombobox="dataCombobox.paging.pageSize" v-model="filter.paging.pageSize"></MCombobox>
                    </div>
                    <div class="p-footertable__action--numberrecord"><strong>1 - {{filter.paging.pageSize}}</strong> bản ghi</div>
                    <div class="p-footertable__action--button">
                        <span @click="previousPageOrders()" class="icon-previous"></span>
                        <span @click="nextPageOrders()" class="icon-next"></span>
                    </div>
                </div>
            </div>            
        </div>
        <OrdersForm :IdEntity="this.idEntity" :btnShowForm="btnShowForm"  v-if="showForm" :FormMode="this.FormMode" :closeForm="btnCloseForm" ></OrdersForm>
        <MDialog  v-if="dialog.showDialog" :dialogResFalse="btnSetFalseResponseDialog" :dialogResTrue="btnSetTrueResponseDialog" :text="dialog.text" :title="dialog.title" :errors="dialog.errors" :iconDialog="dialog.icon"></MDialog>
        <MImport :closeFormImport="btnCloseFormImport" v-if="isImport"></MImport>
    </div>
</template>
<script>
// import MOrdersDetail from "./MOrdersDetail.vue";
import MTextSearch  from '@/components/base/input/MTextSearch.vue';
import MDialog  from '@/components/base/dialog/MDialog.vue';
import MCombobox from '@/components/base/input/MCombobox.vue';
import MImport from '@/components/import/MImport.vue'
import OrdersForm from './OrdersForm.vue';
import ordersService from '../../../utils/OrdersService';
// import saveAs from 'file-saver';

export default {
    components: {
        OrdersForm, 
        MTextSearch,
        MDialog,
        MCombobox,
        MImport
    },
    data(){
        return{   
            checkedIDs:[],
            allSelected:false,
            Orders:{},
            OrdersDto:{},
            OrdersAll:{},
            idEntity:null,
            FormMode:null,
            isImport:false,
            fileImport: null,
            textSearch:null,
            isLoading:true,
            showForm : false,
            listChecked : [],
            dataCombobox:{
                paging:{
                    pageSize:[10,20,30,50,100]
                },
                statusOrder:{
                    data:["Chờ xác nhận","Đã xác nhận","Đang xử lý","Hoàn thành","Bị hủy"],
                    selected:null
                },
                statusPayment:{
                    data:["Chờ thanh toán","Đã thanh toán"],
                    selected:null
                },
                statusDelivery:{
                    data:["Chờ vận chuyển","Đang vận chuyển","Đã vận chuyển"],
                    selected:null
                }
            },
            totalRecord:null,
            totalPage:null,
            filter:{
                paging:{
                    pageSize:50,
                    pageNumber: 1,
                    searchString:""
                }
            },
            listTableEmployee:[],
            dialog:{
                showDialog:false,
                response:false,
                errors:[],
                title: "",
                icon: "",
                text: ""  
            },
            listCheckedImport:[]
        }
    },
    created() {
        this.loadDataPagingOrders();
    },
    mounted() {
        document.title="Đơn hàng | Admin"

    },
    beforeUnmount() {
        document.title = "Admin"
    },
    watch:{
        'dataCombobox.statusPayment.selected': function (newvalue) {
            if (newvalue == "Chờ thanh toán") {
                this.OrdersDto = this.OrdersAll.filter(item=> item.PaymentStatus == this.Enum.PaymentStatus.PENDING_PAID);
                this.Orders = this.OrdersDto;
            }else if(newvalue == "Đã thanh toán"){
                this.OrdersDto = this.OrdersAll.filter(item=> item.PaymentStatus == this.Enum.PaymentStatus.PAID);
                this.Orders = this.OrdersDto;
            }
        },
        'dataCombobox.statusDelivery.selected': function (newvalue) {
            if (newvalue == "Chờ vận chuyển") {
                this.OrdersDto = this.OrdersAll.filter(item=> item.DeliveryStatus == this.Enum.DeliveryStatus.PENDING_DELIVERY);
                this.Orders = this.OrdersDto;
            }else if(newvalue == "Đã vận chuyển"){
                this.OrdersDto = this.OrdersAll.filter(item=> item.DeliveryStatus == this.Enum.DeliveryStatus.DELIVERED);
                this.Orders = this.OrdersDto;
            }else if(newvalue == "Đang vận chuyển"){
                this.OrdersDto = this.OrdersAll.filter(item=> item.DeliveryStatus == this.Enum.DeliveryStatus.DELIVERING);
                this.Orders = this.OrdersDto;
            }
        },
        'dataCombobox.statusOrder.selected': function (newvalue) {
            if (newvalue == "Chờ xác nhận") {
                this.OrdersDto = this.OrdersAll.filter(item=> item.OrdersStatus == this.Enum.OrderStatus.Pending);
                this.Orders = this.OrdersDto;
            }else if(newvalue == "Đã xác nhận"){
                this.OrdersDto = this.OrdersAll.filter(item=> item.OrdersStatus == this.Enum.OrderStatus.Confirmed);
                this.Orders = this.OrdersDto;
            }else if(newvalue == "Đang xử lý"){
                this.OrdersDto = this.OrdersAll.filter(item=> item.OrdersStatus == this.Enum.OrderStatus.Processing);
                this.Orders = this.OrdersDto;
            }else if(newvalue == "Hoàn thành"){
                this.OrdersDto = this.OrdersAll.filter(item=> item.OrdersStatus == this.Enum.OrderStatus.Finished);
                this.Orders = this.OrdersDto;
            }else if(newvalue == "Bị hủy"){
                this.OrdersDto = this.OrdersAll.filter(item=> item.OrdersStatus == this.Enum.OrderStatus.Cancelled);
                this.Orders = this.OrdersDto;
            }
        },
        fileImport(){
            this.importEmployee();
        },
    },
    methods :{
        /**
         * Hàm xóa CheckAll 
         * Author: TTPhong(22/01/2024)
         */
        deselectAll(){
            this.checkedIDs = [];
            this.allSelected = false;
        },
        /**
         * Hàm CheckAll 
         * Author: TTPhong(22/01/2024)
         */
        selectAll(){
            this.checkedIDs = [];
            if (!this.allSelected) {
                // this.employees.forEach(element => {
                //     this.checkedIDs.push(element.EmployeeId);
                // });
                for (let i = 0 ; i <  this.Orders.length; i++) {
                    this.checkedIDs.push(this.Orders[i].OrdersId)
                }
            }
        },
        /**
         * Hàm thưc hiện khi chọn 1 trong data combobox
         * Author: TTPhong(22/01/2024)
         */
        select(){
            this.allSelected = false;
            // console.log(this.checkedIDs);
        },
        /**
         * Hàm thực hiện đóng form import
         * Author: TTPhong (29/01/2024)
         */
        btnCloseFormImport(){
            this.isImport = false;
        },
        /**
         * Hàm thực hiện mở form import
         * Author: TTPhong (29/01/2024)
         */
        btnShowFormImport(){
            this.isImport = true;
        },
        /**
         * Hàm thực hiện export tất cả employee
         * Author: TTPhong(29/01/2024)
         */
        async exportEmployee(){
            var url = `${this.URLRequest}Employees/Export/Excel`;
            await this.api.get(
                url
                  
                           
            )
            .then(res=>{
                console.log(res);
                window.open(url);
                this.toast.title = this.MISAResource["VN"].Success;
                this.toast.icon = this.MISAResource["VN"].IconSuccessSmall;
                this.toast.text = this.MISAResource["VN"].ExportSuccess;
                this.toast.showToast = true;
            })
            .catch(error=>{
                this.emitter.emit("handleApiError",error);
            })
        },
        /**
         * Hàm set respone dialog
         *  Author: TTPhong(22/01/2024)
         */
        btnSetTrueResponseDialog(){
            this.dialog.response = true;
            this.dialog.showDialog = false;
        },
        /**
         * Hàm set respone dialog
         *  Author: TTPhong(22/01/2024)
         */
        btnSetFalseResponseDialog(){
            this.dialog.response = false;
            this.dialog.showDialog = false;
        },
        /**
         * hàm thực hiện xóa một nhân viên
         * Author: TTPhong(23/01/2024)
         */
        async deleteManyProduct() {
            this.dialog.icon = this.MISAResource["VN"].IconWarning;
            this.dialog.title = this.MISAResource["VN"].Warning;
            this.dialog.text = this.MISAResource["VN"].QuestionDelete;
            this.dialog.showDialog = true;
            try {
                // Chờ đợi người dùng đưa ra quyết định (có sử dụng await)
                const userDecision = await new Promise(resolve => {
                this.$watch('dialog.response', resolve);
                });

                // Kiểm tra quyết định người dùng và thực hiện xóa nếu cần
                if (userDecision === true) {
                    const response = await ordersService.deleteMany({data:this.listChecked});
                    if (response.data > 0) {
                        this.emitter.emit("showToast",this.Enum.ToastType.SUCCESS,"Xóa thành công !")
                        this.listChecked=[];
                    }
                    this.emitter.emit("loadDataPagingOrders");
                }
            } catch (error) {
                this.emitter.emit("handleApiError",error);
            }
        },
        /**
         * Hàm lấy dữ liệu listChecked
         * Author: TTPhong(22/01/2024)
        */
        handleListChecked(data){
            this.listChecked = data;
        },
        /**
         * Hàm thực hiện hiển thị form 
         * Author: TTPhong (06/12/2023) 
         */
        btnShowForm(id){
            if(id){
                this.idEntity = id;
                this.showForm = true;
                this.FormMode = this.Enum.FormMode.UPDATE;
            }else{
                this.showForm = true;
                this.FormMode = this.Enum.FormMode.ADD;
            }
        },
        /**
         * Hàm thực hiện ẩn form thêm mới
         * Author: TTPhong (06/12/2023) 
         */
        btnCloseForm(){
            this.showForm = false;
        },
        /**
         * Hàm thực hiện chuyển sang tiếp theo 
         * Author: TTPhong(22/01/2024)
         */
        nextPageOrders(){
            this.filter.paging.pageNumber +=1
            if(this.filter.paging.pageNumber > this.totalPage){
                this.filter.paging.pageNumber = this.totalPage;
            }else{
                this.loadDataPagingOrders();
            }
        },
        /**
         * Hàm thực hiện khi nhấn quay lại trang trước và gọi hàm hàm phân trang 
         *  Author: TTPhong(22/01/2024)
         */
        previousPageOrders(){
            this.filter.paging.pageNumber -= 1
            if(this.filter.paging.pageNumber == 0){
                this.filter.paging.pageNumber = 1;
            }
            else{
                this.loadDataPagingOrders();
            }
        },
        /**
         * Hàm thực hiện loadData
         * Author:TTPhong(23/01/2024)
         */
        async loadDataPagingOrders(){
            try{
                this.emitter.emit("showLoading");
                let params = !this.filter.paging.searchString
                ? {
                    pageSize: this.filter.paging.pageSize,
                    pageNumber: this.filter.paging.pageNumber,
                }
                : {
                    searchString: this.filter.paging.searchString,
                    pageSize: this.filter.paging.pageSize,
                    pageNumber: this.filter.paging.pageNumber,
                };
                var res = await ordersService.getFilterPaging({params})
                if(res.data){
                    this.emitter.emit("hiddenLoading");
                    this.totalPage = res.data.TotalPage 
                    this.totalRecord = res.data.TotalRecord 
                    this.Orders = res.data.Data;
                    this.OrdersAll  = res.data.Data;
                } 
            }catch(error){
                console.log(error);
                this.emitter.emit("handleApiError",error);
                setTimeout(() => {
                    this.emitter.emit("hiddenLoading")
                }, 1000);
            }
        },
    }
}
</script>
<style scoped>
.combobox-status-order input{
    margin: 0px 5px;
}
.combobox-status-order {
    display: flex;
    align-items: center;
    justify-content: space-between;
}
.p-home__main--header--title{
    display: flex !important; 
    align-items: center !important;
    column-gap: 12px !important;
    font-size: 24px !important;
    font-weight: 700 !important;
}
.p-button3 i {
    color: #fff !important;
}
.p-button3{
    border: 1px solid #a2c5d2 !important;
    background-color: #a2c5d2 !important;
    padding: 0 !important;
    background-color: rgba(255, 255, 255, 0.1);
}
.p-button3:active{
    background-color: rgba(28, 74, 18, 0.1);
}
.custom-file-input::-webkit-file-upload-button {
  visibility: hidden;
}
.custom-file-input::before {
    height: 36px;
    width: 36px;
    border: 1px solid #39AC66;
    border-radius: 4px;

    display: inline-block;
    border-radius: 3px;
    outline: none;
    /* -webkit-user-select: none; */
    cursor: pointer;
}
    @import url('@/css/layout/home.css');
    ::-webkit-scrollbar {
        width: 8px;
        height: 8px;
    }
    ::-webkit-scrollbar-thumb {
        background: #888; 
        border-radius: 6px;
    }
        .p-button-table-option:hover{
        color: #a2c5d2;
    }
    .p-button-table-option{
        font-size: 18px;
        padding: 5px 10px;
        border-radius: 50%;
        background-color: #fff;
        margin: 5px;
        box-shadow: #ccc 0px 0px 8px 1px;
    }
    .p-tbl-option-fixed{
        display: none;
        position: sticky;
        right: 0;
        height:100%;
    }
    .p-table__row:hover .p-tbl-option-fixed{
        display: block;
    }
    .p-table__row td img{
        width: 70px;
        height: 70px;
    }
    .p-tbl-option-fixed-position{
        position: absolute;
        right: 0;
        display: flex;
        align-items: center;
    }
    .rotate {
        rotate: 90deg;
    }
</style>