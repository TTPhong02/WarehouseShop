import BaseService from "./BaseService";
class OrdersService extends BaseService{
    constructor(){
        super("Orders")
    }
    async checkout(data) {
        const res = await this.baseAxios.post(`${this.baseUrl}/Checkout`,data);
        return res;
    }
    async getByUsersId(id) {
        const res = await this.baseAxios.get(`${this.baseUrl}/UsersId/${id}`);
        return res;
    }
    async getRevenueByYear(data){
        const res = await this.baseAxios.post(`${this.baseUrl}/RevenueYear`,data);
        return res;
    }
    async getRevenueByMonth(data){
        const res = await this.baseAxios.post(`${this.baseUrl}/RevenueMonth`,data);
        return res;
    }
    async getRevenueByMonthOfYear(date){
        console.log(date);
        const res = await this.baseAxios.get(`${this.baseUrl}/RevenueMonthOfYear/${date}`);
        return res;
    }
    async getRevenueByDate(data){
        const res = await this.baseAxios.post(`${this.baseUrl}/RevenueDate`,data);
        return res;
    }
    async getOrderbyTime(data){
        const res = await this.baseAxios.post(`${this.baseUrl}/OrdersTime`,data);
        return res;
    }
    async getTotalRevenue(){
        const res = await this.baseAxios.get(`${this.baseUrl}/Revenue`);
        return res;
    }
    async getTotalRevenueByDate(date){
        const res = await this.baseAxios.get(`${this.baseUrl}/RevenueTotalDate/${date}`);
        return res;
    }
    async getTotalNumberOrder(type){
        const res = await this.baseAxios.get(`${this.baseUrl}/TotalOrder/${type}`);
        return res;
    }
    async getUserMostOrder(){
        const res = await this.baseAxios.get(`${this.baseUrl}/UserMostOrder`);
        return res;
    }
    async getOrderByStatus(status){
        const res = await this.baseAxios.get(`${this.baseUrl}/OrderStatus/${status}`);
        return res;
    }
    async filterOrderByStatus(data){
        const res = await this.baseAxios.get(`${this.baseUrl}/FilterOrderByStatus`,data);
        return res;
    }
}
const ordersService = new OrdersService();
export default ordersService;