import BaseService from "./BaseService";
class CartItemsService extends BaseService{
    constructor(){
        super("CartItems")
    }
    async getByUserId(userid){
        const res = await this.baseAxios.get(`${this.baseUrl}/UserId/${userid}`);
        return res;
    }
    async getByCartId(cartid){
        const res = await this.baseAxios.get(`${this.baseUrl}/CartId/${cartid}`);
        return res;
    }
    async insertCartItems(data){
        const res = await this.baseAxios.post(`${this.baseUrl}/Insert`,data);
        return res;
    }
}
const cartItemsService = new CartItemsService();
export default cartItemsService;