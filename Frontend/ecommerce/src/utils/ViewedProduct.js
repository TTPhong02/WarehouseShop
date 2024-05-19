import httpRequest from "./request";
class ViewedProductService{
    constructor() {
        this.baseUrl = "ViewedProduct";
        this.baseAxios = httpRequest;
      }
      async viewProduct(data) {
        const res = await this.baseAxios.get(`${this.baseUrl}/ViewProduct`,data);
        return res;
      }
      async getListViewProduct(id) {
        const res = await this.baseAxios.get(`${this.baseUrl}/ViewProductByUser/${id}`);
        return res;
      }
}
const viewedProductService = new ViewedProductService();
export default viewedProductService;