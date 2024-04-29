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
}
const ordersService = new OrdersService();
export default ordersService;