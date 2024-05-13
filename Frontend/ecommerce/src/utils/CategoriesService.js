import BaseService from "./BaseService";
class CategoriesService extends BaseService{
    constructor(){
        super("Categories")
    }
    async getCategoriesBySlug(slug) {
        const res = await this.baseAxios.get(`${this.baseUrl}/Slug/${slug}`);
        return res;
    }
}
const categoriesService = new CategoriesService();
export default categoriesService;