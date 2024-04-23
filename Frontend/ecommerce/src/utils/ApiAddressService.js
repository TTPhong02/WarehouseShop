import axios from "axios";

const httpRequest = axios.create({
  baseURL: "https://vapi.vnappmob.com/api",
  headers: { Accept: "application/json", "Content-Type": "application/json" },
});

class AddressService {
  constructor() {
    this.baseAxios = httpRequest;
  }
  getUrl(url) {
    return this.baseUrl + url;
  }
  async getProvince() {
    const res = await this.baseAxios.get("province");
    return res.data.results;
  }
  async getDistrict(id) {
    const res = await this.baseAxios.get(`/province/district/${id}`);
    return res.data.results;
  }
  async getWard(id) {
    const res = await this.baseAxios.get(`/province/ward/${id}`);
    return res.data.results;
  }
  
}
const addressApiService = new AddressService();

export default addressApiService;