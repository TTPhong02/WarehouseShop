import BaseService from "./BaseService";
class ProductService extends BaseService{
    constructor(){
        super("Product")
    }
    async getProductNew() {
        const res = await this.baseAxios.get(`${this.baseUrl}/ProductNew`);
        return res;
    }
}
const productService = new ProductService();
export default productService;