<template>
  <div>
    <div class="row s-overview">
      <div class="col-lg-8 col-md-8 dol-sm-8 col-8 s-overview-left">
        <div class="overview-left-main">
          <div class="overview-header">
            <p>Biểu đồ doanh thu</p>           
            <div class="date-picker-overview">
              <p>Năm {{this.dateFilter.getFullYear()}}</p>
            </div>
          </div>
          <div class="overview-revenue">
              <div class="overview-revenue-item">
                <i class="fa-solid fa-sack-dollar"></i>
                <div class="overview-revenue-text">
                  <strong>Tổng doanh thu</strong>
                  <div>{{this.helper.formatMoney(this.totalRevenue)}}</div>
                </div>
              </div>
              <div class="overview-revenue-item">
                <i class="fa-solid fa-sack-dollar"></i>
                <div class="overview-revenue-text">
                  <strong>Doanh thu năm nay</strong>
                  <div>{{this.helper.formatMoney(this.totalRevenueOfYear)}}</div>
                </div>
              </div>
              <div class="overview-revenue-item">
                <i class="fa-solid fa-sack-dollar"></i>
                <div class="overview-revenue-text">
                  <strong>Doanh thu hôm nay</strong>
                  <div>{{this.helper.formatMoney(this.totalRevenueOfDate)}}</div>
                </div>
              </div>
            </div>
          <div class="overview-left-main-chart">
              <Chart type="line" height="300" :data="chartDataRevenue" :options="chartOptionsRevenue" class="h-30rem" />
          </div>
        </div>
        <div class=" overview-left-under">
          <div class="col-lg-6 col-md-6 col-sm-6 col-6 left-under-user">
            <p class="under-user-title">Khách hàng thân thiết</p>
            <div v-for="item in listUserMostOrder" :key="item.UsersId" class="under-user"> 
              <div class="under-user-infor">
                <i class="fa-solid fa-crown"></i>
                <div>{{item.Fullname}}</div>   
              </div> 
              <div>
                Tích lũy: {{this.helper.formatMoney(item.TotalPurchaseAmount)}}
              </div>      
            </div>
          </div>
          <div class="col-lg-6 col-md-6 col-sm-6 col-6 left-under-order">
            <div class="under-order-item">
              <i class="fa-solid fa-cart-flatbed-suitcase"></i>
              <div class="under-order-text">
                <p>Tổng số đơn hàng</p>
                <div>{{this.totalOrder}}</div>
              </div>
            </div>
            <div class="under-order-item">
              <i class="fa-solid fa-cart-flatbed-suitcase"></i>
              <div class="under-order-text">
                <p>Đơn hàng ngày hôm nay</p>
                <div>{{this.totalOrderNow}}</div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="col-lg-4 col-md-4 dol-sm-4 col-4 s-overvie-right">
        <div class="overview-right-first">
              <div class="overvie-right-header">
                Top sản phẩm bán chạy
              </div>
              <div class="overvie-right-chart">
                <Chart type="doughnut"  :data="chartDataProduct" :options="chartOptionsProduct" class="w-full md:w-10rem" />
              </div>
        </div>
        <div class="overview-right-under">
          <div class="right-under-header">
            Thông báo đơn hàng
          </div>
          <div class="right-under-main">
            <div v-for="item in listOrder" :key="item.OrdersId"  class="overview-noti">
              <i class="fa-solid fa-bag-shopping"></i>
              <div>Bạn có đơn hàng mới chờ xác nhận</div> 
              <router-link to="/admin/orders-management">Xem</router-link>        
            </div>
          </div>
        </div>
      </div>

    </div>
    
  </div>
</template>

<script>
import Chart from 'primevue/chart';
import ordersService from '../../../utils/OrdersService';
import productService from '../../../utils/ProductService';
export default {
  components:{Chart},
  data() {
    return {
      listOrder:null,
      totalOrder:null,
      totalOrderNow:null,
      listUserMostOrder:null,
      totalRevenueOfDate:null,
      totalRevenueOfYear:null,
      totalRevenue:null,
      dateFilter:null,
      chartDataRevenue:null,
      chartOptionsRevenue:null,
      chartDataProduct:null,
      chartOptionsProduct:null,
      chartDataUser:null,
      chartOptionsUser:null,
    }
  },
  created() {
        const date = new Date();
        this.dateFilter = date ;
  },
  mounted() {
    this.takeOrderByStatus();
    this.takeTotalNumberOrder();
    this.takeUserMostOrder();
    this.takeTotalRevenue();
    this.setChartDataRevenue();
    this.chartOptionsRevenue = this.setChartOptionsRevenue();
    this.setChartDataProduct();
    this.chartOptionsProduct = this.setChartOptionsProduct();
  },
  methods: {
    async takeOrderByStatus(){
      var res =await ordersService.getOrderByStatus(this.Enum.OrderStatus.Pending)
      this.listOrder = res.data;
    },
    async takeTotalNumberOrder(){
      var res = await ordersService.getTotalNumberOrder("All");
      this.totalOrder = res.data;
      var resNow = await ordersService.getTotalNumberOrder("DateNow");
      this.totalOrderNow = resNow.data;
    },
    async takeUserMostOrder(){
      var res = await ordersService.getUserMostOrder();
      this.listUserMostOrder = res.data;
    },
    async takeTotalRevenue(){
      var res = await ordersService.getTotalRevenue();
      this.totalRevenue = res.data;
      let today = new Date();

      // Lấy thông tin về năm, tháng và ngày
      let year = today.getFullYear();
      let month = (today.getMonth() + 1).toString().padStart(2, '0'); // Thêm số 0 đằng trước nếu tháng < 10
      let day = today.getDate().toString().padStart(2, '0'); // Thêm số 0 đằng trước nếu ngày < 10

      // Tạo chuỗi ngày tháng trong định dạng "YYYY-MM-DD"
      let formattedDate = `${year}-${month}-${day}`;
      var resDate = await ordersService.getTotalRevenueByDate(formattedDate);
      this.totalRevenueOfDate = resDate.data;
    },
    async setChartDataProduct() {
        const documentStyle = getComputedStyle(document.body);
        const res = await productService.getProductHot();
        var dataLabel = [];
        var dataNumberSold = [];
        var listData = res.data;
        listData.forEach(element => {
          dataLabel.push(element.ProductName);
          dataNumberSold.push(element.ProductSold);
        });
        this.chartDataProduct = {
            labels: dataLabel,
            datasets: [
                {
                    data: dataNumberSold,
                    backgroundColor: [documentStyle.getPropertyValue('--cyan-500'), documentStyle.getPropertyValue('--orange-500'), documentStyle.getPropertyValue('--gray-500')],
                    hoverBackgroundColor: [documentStyle.getPropertyValue('--cyan-400'), documentStyle.getPropertyValue('--orange-400'), documentStyle.getPropertyValue('--gray-400')]
                }
            ]
        };
    },
    setChartOptionsProduct() {
        const documentStyle = getComputedStyle(document.documentElement);
        const textColor = documentStyle.getPropertyValue('--text-color');

        return {
            options:{
              responsive:false,
              maintainAspectRatio:false,
            },
            plugins: {
                
                legend: {
                    position:'right',
                    labels: {
                        cutout: '10%',
                        color: textColor
                    }
                }
            }
        };
    },
    async setChartDataRevenue() {
        const documentStyle = getComputedStyle(document.documentElement);
            var date = new Date();
            var dateFilter = `${date.getFullYear()}-01-01`;
            var res = await ordersService.getRevenueByMonthOfYear(dateFilter);
            const listData = res.data;
            var dataLabel = [];
            var dataRevenue=[];
            listData.forEach(element => {
                dataLabel.push(this.helper.formatMonth(element.DateLabel));
                dataRevenue.push(element.Revenue);
                this.totalRevenueOfYear += element.Revenue;
            });
            this.chartDataRevenue = {
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
    },
    setChartOptionsRevenue() {
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
  }
  
}
</script>

<style scoped>
.left-under-user{
  max-height: 210px;
  overflow: hidden;
}
.under-user-infor i {
  font-size: 20px;
  color: gold;
  margin-right: 20px;
}
.under-user-infor{
  font-size: 15px !important;
  font-weight: bold;
}
.under-user-infor,
.under-user{
  justify-content: space-between;
  display: flex;
  align-items: center;
  font-size: 13px;
  margin: 5px 0px;
}
.under-user img{
  margin-right: 5px;
  width: 35px;
  height: 35px;
}
.under-user{
  border-bottom:1px solid rgba(0,0,0,.12);
}
.under-user div:last-child{
  font-style: italic;
  color: #000;
  font-size: 13px;
}
.overview-noti div{
  font-size: 15px;
  font-weight: bold;
}
.overview-noti i {
  margin-right: 15px;
  color: #137699;
  font-size: 30px;
}
.overview-noti{
  justify-content: space-between;
  border-radius: 10px;
  padding: 5px 10px;
  margin: 8px;
  background-color: rgba(198, 223, 245,.3);
  display: flex;
  align-items: center;
}
.overview-revenue{
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 20px 10px 5px 10px;
}
.overview-revenue-text{
  font-size: 14px;
  padding: 0px 15px;
}
.overview-revenue-item i {
  color: #d8d100;
  font-size: 35px;
}
.overview-revenue-item{
  display: flex;
  align-items: center;
}
.under-order-text div{
  font-size: 16px;
  font-weight: bold;
}
.under-user-title,
.under-order-text p{
  font-size: 17px;
  color: #0096cc;
  font-weight: bold;
  margin: 0px;
}
.under-order-text{
  padding: 10px 20px;
}
.under-order-item{
  align-items: center;
  display: flex;
}
.under-order-item i{
  color: #0096cc;
  font-size: 40px;
}
.left-under-order{
  padding-right: 0px;
}
.overview-left-under{
  display: flex;
}
.right-under-header,
.overvie-right-header{
  font-size: 16px;
  font-weight: bold;
  color: #0096cc;
}
.overview-right-under{


}
.overview-right-under,
.overview-right-first{
  margin-bottom: 10px;
  border-radius: 10px;
  padding: 10px 15px;
  box-shadow: 0 12px 20px rgba(0,0,0,.12);
  background-color: #fff;
}
.p-chart{
  display: flex !important;
  justify-content: center;
}
.overvie-right-chart {
}
.under-order-item,
.left-under-user,
.overview-left-main{
  margin-bottom: 10px;
  border-radius: 10px;
  padding:10px 15px ;
  background-color: #fff;
    box-shadow: 0 12px 20px rgba(0,0,0,.12);
}
.date-picker-input{
  width: 160px !important;
    text-align: center !important;
}
.date-picker-overview p {
  font-weight: bold;
  margin-right: 10px;
}
.date-picker-overview{
  align-items: center;
  display: flex;
}
.overview-header > p{
    font-weight: bold;
  color: #0294c9;
    font-size: 18px;
}
.overview-header p {
  color: #0096cc;
  margin-bottom: 0px;
}
.overview-header{
  display: flex;
  justify-content: space-between;
}
.right-under-main{
  max-height: 170px;
  overflow-y:  scroll;
      scrollbar-width: thin;
    scroll-behavior: smooth;
}
</style>