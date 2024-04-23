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
}
const ordersService = new OrdersService();
export default ordersService;