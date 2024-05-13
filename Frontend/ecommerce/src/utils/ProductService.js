import BaseService from "./BaseService";
class ProductService extends BaseService{
    constructor(){
        super("Product")
    }
    async getProductNew() {
        const res = await this.baseAxios.get(`${this.baseUrl}/ProductNew`);
        return res;
    }
    async getProductHot() {
        const res = await this.baseAxios.get(`${this.baseUrl}/ProductHot`);
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
    async getProductByCategories(id) {
        const res = await this.baseAxios.get(`${this.baseUrl}/Categories/${id}`);
        return res;
    }
    async SortAndFilterProductByCategories(data) {
        const res = await this.baseAxios.get(`${this.baseUrl}/SortByCategories`,data);
        console.log(data);
        return res;
    }
}
const productService = new ProductService();
export default productService;