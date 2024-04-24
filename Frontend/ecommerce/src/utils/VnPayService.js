import httpRequest from "./request";
class VnPayService {
  constructor() {
    this.baseUrl = "VnPay";
    this.baseAxios = httpRequest;
  }
  getUrl(url) {
    return this.baseUrl + url;
  }
  async createPaymentUrl(data) {
    const res = await this.baseAxios.post(`${this.baseUrl}/CreatePaymentUrl`,data);
    return res;
  }
  
}
const vnPayService = new VnPayService();

export default vnPayService;