import BaseService from "./BaseService";
class AddressService extends BaseService{
    constructor(){
        super("Address")
    }
    async getAddressById(id) {
        const res = await this.baseAxios.get(`${this.baseUrl}/UserId/${id}`);
        return res.data;
    }
    async getAddressDefaultById(id) {
        const res = await this.baseAxios.get(`${this.baseUrl}/Default/${id}`);
        return res.data;
    }

}
const addressService = new AddressService();
export default addressService;