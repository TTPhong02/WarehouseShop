import httpRequest from "./request";
class ImagesService {
  constructor() {
    this.baseUrl = "Images";
    this.baseAxios = httpRequest;
  }
  getUrl(url) {
    return this.baseUrl + url;
  }
  async getAll() {
    const res = await this.baseAxios.get(this.baseUrl);
    return res;
  }
  async getById(id) {
    const res = await this.baseAxios.get(`${this.baseUrl}/${id}`);
    return res;
  }
  
}
const imagesService = new ImagesService();

export default imagesService;