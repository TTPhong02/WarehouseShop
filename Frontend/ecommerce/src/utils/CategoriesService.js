import BaseService from "./BaseService";
class CategoriesService extends BaseService{
    constructor(){
        super("Categories")
    }
}
const categoriesService = new CategoriesService();
export default categoriesService;