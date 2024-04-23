import BaseService from "./BaseService";
class ProductService extends BaseService{
    constructor(){
        super("Product")
    }
    async getProductNew() {
        const res = await this.baseAxios.get(`${this.baseUrl}/ProductNew`);
        return res;
    }
    async getProductByListId(data) {
        const res = await this.baseAxios.post(`${this.baseUrl}/ListId`,data);
        return res;
    }
    async getProductBySlug(slug) {
        const res = await this.baseAxios.get(`${this.baseUrl}/Slug/${slug}`);
        return res;
    }
}
const productService = new ProductService();
export default productService;