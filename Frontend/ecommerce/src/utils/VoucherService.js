import BaseService from "./BaseService";
class VoucherService extends BaseService{
    constructor(){
        super("Voucher")
    }
    async useVoucher(code) {
        const res = await this.baseAxios.get(`${this.baseUrl}/UseVoucher/${code}`);
        return res;
    }
    async filterVoucher(params) {
        const res = await this.baseAxios.get(`${this.baseUrl}/FilterVoucher`,params);
        return res;
    }
}
const voucherService = new VoucherService();
export default voucherService;