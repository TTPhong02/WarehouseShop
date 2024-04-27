import BaseService from "./BaseService";
class OrderDetailsService extends BaseService{
    constructor(){
        super("OrderDetails")
    }
    async getByOrdersId(id) {
        const res = await this.baseAxios.get(`${this.baseUrl}/OrdersId/${id}`);
        return res;
    }
}
const orderDetailsService = new OrderDetailsService();
export default orderDetailsService;