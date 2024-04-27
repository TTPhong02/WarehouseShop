<template>
  <div>
    <div class="row s-overview">
      <div class="col-lg-8 col-md-8 dol-sm-8 col-8 s-overview-left">
        <div class="overview-left-main">
          <div class="overview-header">
            <p>Phân tích doanh thu</p>           
            <div class="date-picker-overview">
              <p>Năm</p>
              <div class="date-picker-input flex justify-content-center">
                <Calendar v-model="this.date" view="year" dateFormat="yy" />
              </div>
            </div>
          </div>
          <div class="overview-revenue">
              <div class="overview-revenue-item">
                <i class="fa-solid fa-sack-dollar"></i>
                <div class="overview-revenue-text">
                  <strong>Tổng doanh thu</strong>
                  <div>{{this.helper.formatMoney(500000000)}}</div>
                </div>
              </div>
              <div class="overview-revenue-item">
                <i class="fa-solid fa-sack-dollar"></i>
                <div class="overview-revenue-text">
                  <strong>Doanh thu năm {{date}}</strong>
                  <div>{{this.helper.formatMoney(500000000)}}</div>
                </div>
              </div>
              <div class="overview-revenue-item">
                <i class="fa-solid fa-sack-dollar"></i>
                <div class="overview-revenue-text">
                  <strong>Doanh thu theo quý</strong>
                  <div>{{this.helper.formatMoney(500000000)}}</div>
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
            <div class="under-user"> 
              <div class="under-user-infor">
                <img src="../../../assets/img/cart.png" alt="">  
                <div>Trần thanh phong</div>   
              </div> 
              <div>
                Tích lũy: {{this.helper.formatMoney(50000000)}}
              </div>      
            </div>
            <div class="under-user"> 
              <div class="under-user-infor">
                <img src="../../../assets/img/cart.png" alt="">  
                <div>Trần thanh phong</div>   
              </div> 
              <div>
                Tích lũy: {{this.helper.formatMoney(50000000)}}
              </div>      
            </div>
            <div class="under-user"> 
              <div class="under-user-infor">
                <img src="../../../assets/img/cart.png" alt="">  
                <div>Trần thanh phong</div>   
              </div> 
              <div>
                Tích lũy: {{this.helper.formatMoney(50000000)}}
              </div>      
            </div>
          </div>
          <div class="col-lg-6 col-md-6 col-sm-6 col-6 left-under-order">
            <div class="under-order-item">
              <i class="fa-solid fa-cart-flatbed-suitcase"></i>
              <div class="under-order-text">
                <p>Tổng số đơn hàng</p>
                <div>500</div>
              </div>
            </div>
            <div class="under-order-item">
              <i class="fa-solid fa-cart-flatbed-suitcase"></i>
              <div class="under-order-text">
                <p>Đơn hàng đang chờ xác nhận</p>
                <div>500</div>
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
          <div class="overview-noti">
            <i class="fa-solid fa-bag-shopping"></i>
            <div>Bạn vừa có đơn hàng mới</div> 
            <router-link to="/admin/orders-management">Xem</router-link>        
          </div>
          <div class="overview-noti">
            <i class="fa-solid fa-bag-shopping"></i>
            <div>Bạn vừa có đơn hàng mới</div> 
            <router-link to="/admin/orders-management">Xem</router-link>        
          </div>
          <div class="overview-noti">
            <i class="fa-solid fa-bag-shopping"></i>
            <div>Bạn vừa có đơn hàng mới</div> 
            <router-link to="/admin/orders-management">Xem</router-link>       
          </div>
        </div>
      </div>

    </div>
    
  </div>
</template>

<script>
import Calendar from 'primevue/calendar';
import Chart from 'primevue/chart';
export default {
  components:{Calendar,Chart  },
  data() {
    return {
      date:null,
      chartDataRevenue:null,
      chartOptionsRevenue:null,
      chartDataProduct:null,
      chartOptionsProduct:null,
      chartDataUser:null,
      chartOptionsUser:null,
      chartDataCategories:null,
      chartOptionsCategories:null,
    }
  },
  mounted() {
    this.chartDataRevenue = this.setChartDataRevenue();
    this.chartOptionsRevenue = this.setChartOptionsRevenue();
    this.chartDataProduct = this.setChartDataProduct();
    this.chartOptionsProduct = this.setChartOptionsProduct();
    this.chartDataCategories = this.setChartDataCategories();
    this.chartOptionsCategories = this.setChartOptionsCategories();
  },
  methods: {
        setChartDataCategories() {
            const documentStyle = getComputedStyle(document.body);

            return {
                labels: ['Đồ điện tử', 'Đồ hộp đựng ', 'C'],
                datasets: [
                    {
                        data: [540, 325, 702],
                        backgroundColor: [documentStyle.getPropertyValue('--cyan-500'), documentStyle.getPropertyValue('--orange-500'), documentStyle.getPropertyValue('--gray-500')],
                        hoverBackgroundColor: [documentStyle.getPropertyValue('--cyan-400'), documentStyle.getPropertyValue('--orange-400'), documentStyle.getPropertyValue('--gray-400')]
                    }
                ]
            };
        },
        setChartOptionsCategories() {
            const documentStyle = getComputedStyle(document.documentElement);
            const textColor = documentStyle.getPropertyValue('--text-color');

            return {
                plugins: {
                    legend: {
                      position:'right',
                        labels: {
                            usePointStyle: true,
                            color: textColor
                        }
                    }
                }
            };
        },
        setChartDataProduct() {
            const documentStyle = getComputedStyle(document.body);

            return {
                labels: ['Sản phẩm', 'Sản hẩm 2', 'C'],
                datasets: [
                    {
                        data: [100, 325, 702],
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
        setChartDataRevenue() {
            const documentStyle = getComputedStyle(document.documentElement);
            
            return {
                labels: ['1', '2', '3', '4', '5', '6', '7','8','9','10','11','12'],
                datasets: [
                    {
                        type: 'line',
                        fill:true,
                        label: 'Doanh thu',
                        backgroundColor: documentStyle.getPropertyValue('--cyan-600'),
                        data: [4100000, 5100000, 2100000, 6100000, 9100000, 6700000, 2100000,12100000,8100000,3100000,4100000,1100000]
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
.under-user-infor{
  font-size: 14px !important;
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
.overvie-right-header{
  font-size: 16px;
  font-weight: bold;
  color: #0096cc;
}
.overview-right-under{
  max-height: 200px;
  overflow-y: hidden;

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
</style>