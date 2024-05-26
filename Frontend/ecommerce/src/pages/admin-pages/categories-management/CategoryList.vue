<template>
    <div>
        <div class="p-home__main--header">
            <div class="p-home__main--header--title">
                Quản lý danh mục
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
                </div>
                <div class="p-home__main--action--button">
                    <MTextSearch :searchMethod="btnsearch" v-model="filter.paging.searchString"></MTextSearch>
                    <button @click="btnShowFormImport" class="p-button3 "> 
                        <i class="fa-solid fa-file-import"></i>
                    </button>
                    <button @click="exportEmployee()" class=" p-button3 ">
                        <i class="fa-solid fa-file-excel"></i>
                    </button>
                    
                    <button   id="p-btnLoadData" @click="this.emitter.emit('loadDataPagingProduct')"  class="p-button3 ">
                        <i class="fa-solid fa-arrow-rotate-right"></i>
                    </button>
                </div>
            </div>
            <div class="p-home__main--table">
                <div class="p-grid">
                    <MProductDetail :filterParent="filter" ref="nextPageRef" refsPreviousPage="previousPageRef"  @totalRecord="hanldeDataPagingTotalRecord"  @totalPage="hanldeDataPagingTotalPage" @checkedIDs="handleListChecked"  :btnShowForm="btnShowForm"  ></MProductDetail>
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
                            <span @click="btnPreviousPage()" class="icon-previous"></span>
                            <span @click="btnNextPage()" class="icon-next"></span>
                        </div>
                    </div>
                </div>            
            </div>
        <ProductForm :IdEntity="this.idEntity" :btnShowForm="btnShowForm"  v-if="showForm" :FormMode="this.FormMode" :closeForm="btnCloseForm" ></ProductForm>
        <MDialog  v-if="dialog.showDialog" :dialogResFalse="btnSetFalseResponseDialog" :dialogResTrue="btnSetTrueResponseDialog" :text="dialog.text" :title="dialog.title" :errors="dialog.errors" :iconDialog="dialog.icon"></MDialog>
        <MImport :closeFormImport="btnCloseFormImport" v-if="isImport"></MImport>
    </div>
</template>
<script>
import MProductDetail from "./MCategoryDetail.vue";
import MTextSearch  from '@/components/base/input/MTextSearch.vue';
import MDialog  from '@/components/base/dialog/MDialog.vue';
import MCombobox from '@/components/base/input/MCombobox.vue';
import MImport from '@/components/import/MImport.vue'
import ProductForm from './CategoryForm.vue';
import productService from '../../../utils/ProductService';
// import saveAs from 'file-saver';

export default {
    name: "CategoryList",
    components: {
        ProductForm,
        MProductDetail,
        MTextSearch,
        MDialog,
        MCombobox,
        MImport
    },
    data(){
        return{   
            idEntity:null,
            FormMode:null,
            isImport:false,
            fileImport: null,
            textSearch:null,
            isLoading:true,
            showForm : false,
            showToast: false,
            listChecked : [],
            dataCombobox:{
                paging:{
                    pageSize:[10,20,30,50,100]
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
            toast:{
                showToast: false,
                title:'',
                text:'',
                textColor:'',
                icon:''
            },
            listCheckedImport:[]
        }
    },
    created() {
        this.emitter.on("deleteEmployee",this.deleteEmployee);
    },
    mounted() {
        document.title="Danh mục | Admin"

    },
    beforeUnmount() {
        document.title = "Thowie"
    },
    watch:{
        FormMode(newValue){
            if(newValue === this.Enum.FormMode.UPDATE){
                this.take
            }
        },
        fileImport(){
            this.importEmployee();
        },
    },
    methods :{
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
         * Hàm thực hiện gọi hàm tìm kiếm nhân viên
         * Author: TTPhong(22/01/2024)
        */
        btnsearch(){
            this.emitter.emit("loadDataPagingCategories");
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
        async deleteEmployee(id) {
            this.dialog.title = this.MISAResource["VN"].Warning;
            this.dialog.text = this.MISAResource["VN"].QuestionDelete;
            this.dialog.icon = this.MISAResource["VN"].IconWarning;
            this.dialog.showDialog = true;
            try {
                // Chờ đợi người dùng đưa ra quyết định (có sử dụng await)
                const userDecision = await new Promise(resolve => {
                this.$watch('dialog.response', resolve);
                });

                // Kiểm tra quyết định người dùng và thực hiện xóa nếu cần
                if (userDecision === true) {
                    const response = await this.api.delete(`${this.URLRequest}Employees/${id}`,);
                    if (response.data === 1) {
                        this.toast.title = this.MISAResource["VN"].Success;
                        this.toast.text = this.MISAResource["VN"].DeleteSuccess;
                        this.toast.icon = this.MISAResource["VN"].IconSuccessSmall;
                        this.toast.showToast = true;
                        this.emitter.emit("loadDataPagingEmployee");
                    }
                }
            } catch (error) {
                this.emitter.emit("handleApiError",error);
            }
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
                    const response = await productService.deleteMany({data:this.listChecked});
                    if (response.data > 0) {
                        this.emitter.emit("showToast",this.Enum.ToastType.SUCCESS,"Xóa thành công !")
                        this.listChecked=[];
                    }
                    this.emitter.emit("loadDataPagingProduct");
                }
            } catch (error) {
                this.emitter.emit("handleApiError",error);
            }
        },
        /**
         * Hàm lấy dữ liệu totalRecord
         * Author: TTPhong(22/01/2024)
        */
        hanldeDataPagingTotalRecord(data){
            this.totalRecord = data;
        },
        /**
         * Hàm lấy dữ liệu totalPage
         * Author: TTPhong(22/01/2024)
        */
        hanldeDataPagingTotalPage(data){
            this.totalPage = data;
        },
        /**
         * Hàm lấy dữ liệu listTableEmployee
         * Author: TTPhong(22/01/2024)
        */
        handleListEmployee(data){
            this.listTableEmployee = data;
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
        btnNextPage(){
            this.emitter.emit('nextPageEmployee');
        },
        /**
         * Hàm thực hiện chuyển sang tiếp theo 
         * Author: TTPhong(22/01/2024)
         */
        btnPreviousPage(){
            this.emitter.emit('previousPageEmpolyee');
        }
    }
}
</script>
<style scoped>
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
</style>