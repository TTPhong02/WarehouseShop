import httpRequest from "./request";
class BaseService {
  constructor(baseUrl) {
    this.baseUrl = baseUrl;
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
  async getNewCode() {
    const res = await this.baseAxios.get(`${this.baseUrl}/NewCode`);
    return res;
  }
  async getFilterPaging(data) {
    const res = await this.baseAxios.get(`${this.baseUrl}/Filter`, data);
    return res;
  }
  async post(data) {
    const res = await this.baseAxios.post(`${this.baseUrl}`, data, {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    });
    return res;
  }
  async put(id, dataJson) {
    const res = await this.baseAxios.put(`${this.baseUrl}/${id}`, dataJson, {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    });
    return res;
  }
  async delete(id) {
    const res = await this.baseAxios.delete(`${this.baseUrl}/${id}`);
    return res;
  }
  async deleteMany(listId) {
    const res = await this.baseAxios.delete(
      `${this.baseUrl}/ids`,
      listId
    );
    return res;
  }
}
export default BaseService;