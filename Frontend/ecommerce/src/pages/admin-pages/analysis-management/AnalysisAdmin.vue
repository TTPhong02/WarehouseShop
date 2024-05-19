<template>
  <div class="s-analysis">
    <div class="row s-analysis-header">
        <div class="analysis-header-left">
            <div class="analysis-header-title">Thống kê </div>
        </div>
        <div class="analysis-header-left">
            <MButton @click="exportRenvenueByTime()" className="p-button1" text="Xuất Excel"></MButton>
        </div>
    </div>
    <div class="row s-analysis-main">
        <div class=" analyis-main-chart">
            <div class="analysis-chart-first">
                <div class="analysis-first-option-chart">
                    <div class="select-type">
                        Lọc theo:
                        <div class="combobox-analysis">
                            <MCombobox v-model="this.typeFilter.typeFilterSelected"  :dataCombobox="typeFilter.data"></MCombobox>
                        </div>
                    </div>
                    <div class="content-select">
                        <span>Bắt đầu/kết thúc :</span>
                        <Calendar  selectionMode="range"  v-model="this.typeFilter.dateSelect" :view="this.typeFilter.typeDateCombobox" :dateFormat="this.typeFilter.dateFormat" />
                    </div>
                    <MButton @click="setChartDataBar()" className="p-button1" text="Lọc"></MButton>
                </div>
                <div class="card analyis-chart-bar">
                    <Chart type="bar" width="620" height="300" :data="chartDataBar" :options="chartOptionsBar" class="h-30rem" />
                </div>
            </div>
            <div class="analysis-list-order">
                <div class="list-order-header">
                    Danh sách đơn hàng({{this.listOrder.length}})
                </div>
                <div class="analysis-order-item">
                    <table>
                        <thead>
                            <th>Mã đơn  </th>
                            <th>Khách hàng</th>
                            <th>Tổng tiền</th>
                            <th>Trạng thái</th>
                        </thead>
                        <tr v-for="item in listOrder" :key="item.OrdersId">
                            <td>{{item.OrdersCode}}</td>
                            <td>{{item.ReminiscentName}}</td>
                            <td>{{this.helper.formatMoney(item.TotalAmount)}}</td>
                            <td>{{this.helper.formatOrderStatus(item.OrdersStatus)}}</td>
                        </tr>
                    </table>
                </div>
            </div>
            <div v-if="false" class="analysis-chart-firts">
                <div class="analysis-first-option-chart">
                    <div class="select-type">
                        Số lượng so sánh:
                        <div class="combobox-analysis-number">
                            <MCombobox :dataCombobox="numberCompare.data"></MCombobox>  
                        </div>
                    </div>
                    <div class="content-select">
                        <span>Lựa chọn: </span>
                        <Calendar selectionMode="range" v-model="this.typeFilter.dataSelect" view="year" dateFormat="yy" />
                    </div>
                    <MButton className="p-button1" text="So sánh"></MButton>
                </div>
                <div class="card analyis-chart-line">
                    <Chart type="line" width="620" height="300" :data="chartDataLine" :options="chartOptionsLine" class="h-30rem" />
                </div>
            </div>
            
        </div>
    </div>
    <div class="row s-analysis-under">
        <div class="col-lg-12 col-md-12 col-sm-12 col-12 analysis-under-left">
            <div class="analysis-under-title">Chi tiết thống kê</div>
            <div class=" analysis-under-list">
                <div class="analysis-under-item">
                    <div class="analysis-item-main">{{this.helper.formatMoney(this.totalRevenue)}}</div>
                    <span>Tổng doanh thu</span>
                </div>
                <div class=" analysis-under-item">
                    <div class="analysis-item-main">{{this.helper.formatMoney(this.totalRevenueByTime)}}</div>
                    <span>Tổng doanh thu theo thời gian thống kê </span>
                </div>

            </div>

        </div>
        
    </div>
  </div>
</template>

<script>
import Chart from 'primevue/chart'
import MButton from '../../../components/base/button/MButton.vue'
import MCombobox from '../../../components/base/input/MCombobox.vue'
import Calendar from 'primevue/calendar'
import ordersService from '../../../utils/OrdersService'
import { saveAs } from "file-saver";
export default {
    components:{
        MButton,Chart,MCombobox,Calendar
    },
    data() {
        return {
            totalRevenue:null,
            totalRevenueByTime:null,
            listOrder:[],
            numberCompare:{
                data:[1,2,3,4,5],
                numberCompareSelected:1
            },
            typeFilter:{
                dateFormat:"",
                typeDateCombobox:"year",
                dateSelect:[],
                data:["Ngày","Tháng","Năm"],
                typeFilterSelected:null,
            },
            chartDataBar:null,
            chartOptionsBar:null,
            chartDataLine:null,
            chartOptionsLine:null,
            dataFilter:{
                dateSelectFilter:null,
            }
        }
    },
    watch:{
        'typeFilter.typeFilterSelected':function(newValue){
            if(newValue == "Tháng"){
                this.typeFilter.typeDateCombobox = 'month';
                this.typeFilter.dateFormat="mm/yy";
            }else if(newValue == "Năm"){
                this.typeFilter.typeDateCombobox = 'year';
                this.typeFilter.dateFormat="yy";
            }else{
                 this.typeFilter.typeDateCombobox = 'date';
                 this.typeFilter.dateFormat="dd/mm/yy";
            }
        },
        'typeFilter.dataSelect':function(newValue){
            this.dataFilter.dateSelectFilter = this.helper.formatDateTime(newValue);
        },
    },
    created() {
        this.getTotalRevenue();
        const date = new Date();
        this.typeFilter.dateSelect.push(date) ;
        this.typeFilter.typeFilterSelected = "Năm";
    },
    mounted() {
        this.setChartDataBar();
        this.chartOptionsBar = this.setChartOptionsBar();
        this.chartDataLine = this.setChartDataLine();
        this.chartOptionsLine = this.setChartOptionsLine();
    },
    methods: {
        async exportRenvenueByTime() {
            try {
                this.emitter.emit("loading");
                const date = this.helper.formatDateJS(this.typeFilter.dateSelect[0]) +" - " + this.helper.formatDateJS(this.typeFilter.dateSelect[1])
                const excelRequest = {
                    TitleHeader: "Thống kê doanh thu "+ date,
                    EntityIds: [],
                    Data: this.listOrder,
                    WorksheetName: "Thống kê doanh thu",
                };
                const res = await ordersService.exportRevenueByTime(excelRequest);
                const blob = new Blob([res.data], {
                    type: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                });
                var fileName = "Thống kê doanh thu";
                if (res.status == 200) {
                    this.emitter.emit("unloading");
                    // Mở cửa sổ thoại mở thư mục và cho phép thay tên file
                    saveAs(blob, fileName, { autoBom: false });
                }
            } catch (error) {
                this.emitter.emit("handleApiError", error);
                this.emitter.emit("unoading", false);
            }
        },
        async getListOrderByTime(){
            if(this.typeFilter.typeFilterSelected == "Năm" && this.typeFilter.dateSelect.length == 1){
                var date = [];
                date[1] = this.typeFilter.dateSelect[0];
                var endDate = new Date(this.typeFilter.dateSelect[0]);
                endDate.setFullYear(endDate.getFullYear()-9);
                date[0] = endDate;     
                var res = await ordersService.getOrderbyTime(date);
                this.listOrder = res.data;
                this.totalRevenueByTime = 0;
                this.listOrder.forEach(element => {
                    this.totalRevenueByTime += element.TotalAmount;
                });
            }
            else{
                var resA = await ordersService.getOrderbyTime(this.typeFilter.dateSelect);
                this.listOrder = resA.data;
                this.totalRevenueByTime = 0;
                this.listOrder.forEach(element => {
                    this.totalRevenueByTime += element.TotalAmount;
                });
            }
        },
        async getTotalRevenue(){
            var res = await ordersService.getTotalRevenue();
            this.totalRevenue = res.data;
        },
        async setChartDataBar() {
            const documentStyle = getComputedStyle(document.documentElement);
            this.getListOrderByTime();
            if(this.typeFilter.typeFilterSelected == "Năm"){
                var res = await ordersService.getRevenueByYear(this.typeFilter.dateSelect);
                const listData = res.data;
                var dataLabel = [];
                var dataRevenue=[];
                listData.forEach(element => {
                    dataLabel.push(this.helper.formatYear(element.DateLabel));
                    dataRevenue.push(element.Revenue);
                });
                this.chartDataBar = {
                    labels: dataLabel,
                    datasets: [
                        {
                            type: 'bar',
                            fill:true,
                            label: 'Doanh thu',
                            backgroundColor: documentStyle.getPropertyValue('--cyan-600'),
                            data: dataRevenue
                        }
                    ]
                };

            }else if(this.typeFilter.typeFilterSelected == "Tháng"){
                var resMonth = await ordersService.getRevenueByMonth(this.typeFilter.dateSelect);
                console.log(this.typeFilter.dateSelect);
                const listData = resMonth.data;
                var dataLabelMonth = [];
                var dataRevenueMonth=[];
                listData.forEach(element => {
                    dataLabelMonth.push(this.helper.formatMonth(element.DateLabel));
                    dataRevenueMonth.push(element.Revenue);
                });
                this.chartDataBar = {
                    labels: dataLabelMonth,
                    datasets: [
                        {
                            type: 'bar',
                            fill:true,
                            label: 'Doanh thu',
                            backgroundColor: documentStyle.getPropertyValue('--cyan-600'),
                            data: dataRevenueMonth
                        }
                    ]
                };

            }else{
                var resDate = await ordersService.getRevenueByDate(this.typeFilter.dateSelect);
                const listData = resDate.data;
                var dataLabelDate = [];
                var dataRevenueDate=[];
                listData.forEach(element => {
                    dataLabelDate.push(this.helper.formatDate(element.DateLabel));
                    dataRevenueDate.push(element.Revenue);
                });
                this.chartDataBar = {
                    labels: dataLabelDate,
                    datasets: [
                        {
                            type: 'bar',
                            fill:true,
                            label: 'Doanh thu',
                            backgroundColor: documentStyle.getPropertyValue('--cyan-600'),
                            data: dataRevenueDate
                        }
                    ]
                };
            }
        },
        setChartOptionsBar() {
            const documentStyle = getComputedStyle(document.documentElement);
            const textColor = documentStyle.getPropertyValue('--text-color');
            const textColorSecondary = documentStyle.getPropertyValue('--text-color-secondary');
            const surfaceBorder = documentStyle.getPropertyValue('--surface-border');

            return {
                maintainAspectRatio: false,
                aspectRatio: 0.6,
                plugins: {
                    legend: {
                        labels: {
                            color: textColor
                        }
                    }
                },
                scales: {
                    x: {
                        ticks: {
                            color: textColorSecondary
                        },
                        grid: {
                            color: surfaceBorder
                        }
                    },
                    y: {
                        ticks: {
                            color: textColorSecondary
                        },
                        grid: {
                            color: surfaceBorder
                        }
                    }
                }
            };
        },
        setChartDataLine() {
            const documentStyle = getComputedStyle(document.documentElement);
            return {
                labels: ['1', '2', '3', '4', '5', '6', '7','8','9','10','11','12'],
                datasets: [
                    {
                        type: 'line',
                        fill:false,
                        label: 'Doanh thu',
                        backgroundColor: documentStyle.getPropertyValue('--cyan-600'),
                        data: [4100000, 5100000, 2100000, 6100000, 9100000, 6700000, 2100000,12100000,8100000,3100000,4100000,1100000]
                    },
                    {
                        type: 'line',
                        fill:false,
                        label: 'Doanh thu',
                        backgroundColor: documentStyle.getPropertyValue('--orange-600'),
                        data: [41000000, 51000000, 21000000, 61000000, 8100000, 670000, 210000,1210000,810000,310000,410000,110000]
                    }
                ]
            };
        },
        setChartOptionsLine() {
            const documentStyle = getComputedStyle(document.documentElement);
            const textColor = documentStyle.getPropertyValue('--text-color');
            const textColorSecondary = documentStyle.getPropertyValue('--text-color-secondary');
            const surfaceBorder = documentStyle.getPropertyValue('--surface-border');

            return {
                maintainAspectRatio: false,
                aspectRatio: 0.6,
                plugins: {
                    legend: {
                        labels: {
                            color: textColor
                        }
                    }
                },
                scales: {
                    x: {
                        ticks: {
                            color: textColorSecondary
                        },
                        grid: {
                            color: surfaceBorder
                        }
                    },
                    y: {
                        ticks: {
                            color: textColorSecondary
                        },
                        grid: {
                            color: surfaceBorder
                        }
                    }
                }
            };
        }
    },
}
</script>

<style scoped>
.list-order-header{ 
    font-size: 18px;
    color: #005979;
    font-weight: bold;
    margin-bottom: 20px;
}
.analysis-order-item{
    overflow-y: scroll;
    height: 303px;
}
table{
    overflow: scroll;
    height: 303px;
    max-height: 350px;
    width: 100%;
    border: 2px solid #005979;
    border-radius: 8px;
}
tr{
    border-bottom: 1px solid #efeeee;
    max-height: 25px !important;
}
thead{
    font-style: italic;
    border-bottom: 1px solid #64a8c1;
    border-radius: 8px;
    font-size: 13px;
}
tr,td,th{
    padding:5px 8px ;
    text-align: left;
}
.analysis-list-order{
    
    margin-left: 20px;
    height: 350px;
    flex: 1;
}
.analysis-item-main{
    font-size: 25px;
    color: #005979;
    font-weight: bold;
}
.analysis-under-item span{
    font-size: 15px;
}
.analysis-under-item:last-child{
    border: none;
}
.analysis-under-item{
    text-align: center;
    flex: 1;
    padding: 0px 30px 0px 10px;
    border-right: 1px solid #0097cd;
}
.analysis-under-list{
    display: flex;
    padding: 10px 0px;
    align-items: center;
    justify-content: space-between;
}
.analysis-under-title{
    color: #0096cc;
    font-size: 18px;
    font-weight: bold;
}
.analysis-under-right{
    box-shadow: 0 12px 20px rgba(0,0,0,.12);
    margin-top: 5px;
    padding: 0px;
    padding: 20px 20px;
    border-radius: 0px 0px 8px 0px;
    background-color: #fff;
}
.analysis-under-left{
    border-radius: 0px 0px 0px 8px;
    box-shadow: 0 12px 20px rgba(0,0,0,.12);
    margin-top: 5px;
    padding: 10px 10px;
    background-color: #fff;
}
.s-analysis-under{
    justify-content: space-between;
}
.content-select span{
    margin-right: 10px;
}
.content-select{
    font-size: 14px;
    justify-content: space-between;
}
.combobox-analysis-number{
    width: 70px;
}
.combobox-analysis{
    margin-left: 10px;
    height: 36px !important;
    width: 100px !important;
}
.analysis-first-option-chart{
    padding-bottom: 10px;
    display: flex;
    justify-content: space-between;
    align-items: center;
    flex: 1;
}
.content-select,
.select-type,
.analysis-main-option{
    display: flex;
    justify-content: space-between;
    align-items: center;
}
.card{
    border: 2px solid #64a8c1
}
.analyis-main-chart{
    display: flex;
    justify-content: space-around;
    align-items: center;
}
.s-analysis-main{
    flex-direction: column;
    box-shadow: 0 12px 20px rgba(0,0,0,.12);
    margin-top: 5px;
    padding: 10px 10px;
    background-color: #fff;
}
.analysis-header-title{
    color: #0096cc;
    font-size: 19px;
    font-weight: bold;
}
.s-analysis-header{
    align-items: center;
    border-radius: 8px 8px 0px 0px;
    padding: 10px 10px;
    background-color: #fff;
    box-shadow: 0 12px 20px rgba(0,0,0,.12);
    justify-content: space-between;
}
.p-inputtext{
    padding: 0px !important;
}
</style>