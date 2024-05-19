<template> 
<div>
    <div>
        <table id="p-tblCustomer" class="p-table small-first-col">
            <thead>
                <tr class="p-table__row ">
                    <th>
                        <input type="checkbox"  @click="selectAll" v-model="allSelected" class="checkAll">
                    </th>
                    <th>Hình ảnh</th>
                    <th>Mã sản phẩm</th>
                    <th>Tên sản phẩm</th>
                    <th>Tên hãng</th>
                    <th>Giá tiền</th>
                    <th>Số lượng trong kho</th>
                    <th>Số lượng đã bán</th>
                    <th>Trọng lượng</th>
                    <th>Chất liệu</th>
                    <th>Công dụng</th>
                    <th>Năm sản xuất</th>
                    <th>Mô tả</th>
                    <th>Mã danh mục</th>
                    <th>Tên danh mục</th>
                    <th>Slug sản phẩm</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="item in products" :key="item.ProductId" class="p-table__row">
                    <td ><input v-model="checkedIDs" @click="select" :value="item.ProductId" type="checkbox"></td>
                    <td><img :src="checkImagePath(item.ProductId)" alt=""></td>
                    <td>{{item.ProductCode}}</td>
                    <td>{{item.ProductName}}</td>
                    <td>{{item.ProductBrandName}}</td>
                    <td>{{this.helper.formatMoney(item.ProductPrice)}}</td>
                    <td>{{item.ProductStock}}</td>
                    <td>{{item.ProductSold}}</td>
                    <td>{{item.ProductWeight}} kg</td>
                    <td>{{item.Material}}</td>
                    <td>{{item.ProductUses}}</td>
                    <td>{{item.YearOfProduce}}</td>
                    <td>{{item.ProductDescription}}</td>
                    <td>{{item.CategoriesCode}}</td>
                    <td>{{item.CategoriesName}}</td>
                    <td>{{item.ProductSlug}}</td>
                    <div class="p-tbl-option-fixed">
                        <div class="p-tbl-option-fixed-position">
                            <span @click="onShowForm(item.ProductId)" class="p-button-table-option ">   
                                <i class="fa-solid fa-pencil"></i>                   
                            </span>
                            <span @click="deleteProduct(item.ProductId)"  class="p-button-table-option">   
                                <i class="fa-regular fa-trash-can"></i>                   
                            </span>
                        </div>
                    </div>
                </tr>
            </tbody>
            
        </table> 
        <!-- <MDialog v-if="dialog.showDialog" :text="button.delete.text" :dialogResFalse="btnSetFalseResponseDialog" :dialogResTrue="btnSetTrueResponseDialog"  :iconDialog="button.delete.icon" :title="button.delete.title" ></MDialog> -->
        <!-- <MToastDone v-model="toast.showToast" :color="toast.colorText" :icon="toast.icon" :title="toast.title" :text="toast.text"></MToastDone> -->
    </div>
</div>
</template>

<script>
import imagesService from '../../../utils/ImagesService';
import productService from '../../../utils/ProductService';

export default {
    name: 'MProductDetail',
    props:[
        "btnShowForm","showFormDuplicate","dialogData","filterParent","nextPageNumber","previousPageNumber"
    ]
    ,
    data() {
        return{
            token:null,
            images:[],
            products:[],
            selected: [],
            allSelected: false,
            checkedIDs:[],
            department:{},
            positions:{},
            idEmployee:null,
            showUpdate: false,
            showOptionAction : false,
            dialog: {
                showDialog: false,
                response: false,
                errors:[],
                title: "",
                icon: "",
                text: ""
            },
            
            totalPage:0,
            totalRecord:0,
            filter:{
                paging:{
                    pageSize:0,
                    pageNumber: 0,
                    searchString:""
                }
            },
        }
    },
    watch:{
            /**
     * Hàm lắng nghe biến pageSize thay đổi và thực hiện gọi lại hàm phân trang
     *  Author: TTPhong(22/01/2024)
     */
        'filter.paging.pageSize':function(){
            this.loadDataPagingProduct();
        },
               /**
     * Hàm lắng nghe biến searchString thay đổi và thực hiện gọi lại hàm phân trang
     *  Author: TTPhong(22/01/2024)
     */
        'filter.paging.searchString':function(newValue){
            if(newValue === ""){
                this.loadDataPagingProduct();
            }
        },
            /**
     * Hàm gửi lại đến cha khi biến totalRecord thay đổi
     *  Author: TTPhong(22/01/2024)
     */
        totalRecord(newValue){
            this.$emit('totalRecord',newValue)
        },
            /**
     * Hàm gửi lại lên cha khi biến TotalPage thay đổi
     *  Author: TTPhong(22/01/2024)
     */
        totalPage(newValue){
            this.$emit('totalPage',newValue)
        },
               /**
     * Hàm gửi lại lên cha khi biến checkedIDs thay đổi
     *  Author: TTPhong(22/01/2024)
     */
        checkedIDs(newValue){
            this.$emit('checkedIDs',newValue)
        },
               /**
     * Hàm gửi lại lên cha khi biến employees thay đổi
     *  Author: TTPhong(22/01/2024)
     */
        employees(newValue){
            this.$emit('employees',newValue)
        },
    },
    created() {
        this.takeImages();
        this.dialog = this.dialogData;
        this.filter = this.filterParent; 
        this.emitter.on("loadDataPagingProduct",this.loadDataPagingProduct);
        this.emitter.on("deselectAll",this.deselectAll);  
        this.emitter.on("nextPageProduct",this.nextPageProduct);
        this.emitter.on("previousPageProduct",this.previousPageProduct);  
    },
    methods: {
        async deleteProduct(id){
            try{
                var res = await productService.delete(id);
                if(res.data > 0){
                    this.emitter.emit("showToast",this.Enum.ToastType.SUCCESS,"Xóa thành công !");
                    this.loadDataPagingProduct();
                }
            }catch(error){
                console.log(error);
            }
        },
        async takeImages(){
            var res = await imagesService.getAll();
            this.images = res.data;
        },
        checkImagePath(valueId) {
            let imageItem = this.images.filter(
                (item) => item.ProductId === valueId
            );
            if (imageItem.length > 0) {
                return "https://localhost:7242/" + imageItem[0].ImagesPath;

            } else {
                return "https://localhost:7242/images/no-image.jpg";
            }
        },
            /**
     * Hàm thực hiện khi nhấn sang trang và gọi hàm hàm phân trang 
     *  Author: TTPhong(22/01/2024)
     */
        nextPageProduct(){
            this.filter.paging.pageNumber +=1
            if(this.filter.paging.pageNumber > this.totalPage){
                this.filter.paging.pageNumber = this.totalPage;
            }else{
                this.loadDataPagingProduct();
            }
        },
        /**
         * Hàm thực hiện khi nhấn quay lại trang trước và gọi hàm hàm phân trang 
         *  Author: TTPhong(22/01/2024)
         */
        previousPageProduct(){
            this.filter.paging.pageNumber -= 1
            if(this.filter.paging.pageNumber == 0){
                this.filter.paging.pageNumber = 1;
            }
            else{
                this.loadDataPagingProduct();
            }
        },
        /**
         * Hàm thực hiện loadData
         * Author:TTPhong(23/01/2024)
         */
        async loadDataPagingProduct(){
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
                var res = await productService.getFilterPaging({params})
                if(res.data){
                    this.emitter.emit("hiddenLoading");
                    this.totalPage = res.data.TotalPage 
                    this.totalRecord = res.data.TotalRecord 
                    this.products = res.data.Data;
                } 
            }catch(error){
                console.log(error);
                this.emitter.emit("handleApiError",error);
                setTimeout(() => {
                    this.emitter.emit("hiddenLoading")
                }, 1000);
            }
        },
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
                for (let i = 0 ; i <  this.products.length; i++) {
                    this.checkedIDs.push(this.products[i].ProductId)
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
         * Hàm set respone dialog
         *  Author: TTPhong(22/01/2024)
         */
        btnSetTrueResponseDialog(){
            this.dialog.response = true;
            this.dialog.showDialog = false;
        },
        btnSetFalseResponseDialog(){
            this.dialog.response = false;
            this.dialog.showDialog = false;
        },
        /**
         * Hàm show form update
         *  Author: TTPhong(22/01/2024)
         */
        onShowForm(id){
            this.btnShowForm(id);
        },
    }
}
</script>
<style scoped>
.p-table__row img{
    object-fit: cover;
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
    .p-table__row td{
        max-width: 300px;
        overflow: hidden;
        text-overflow: ellipsis;
    }
    .p-tbl-option-fixed-position{
        top:24px;
        position: absolute;
        right: 0;
        display: flex;
        align-items: center;
    }
    .rotate {
        rotate: 90deg;
    }
</style>

