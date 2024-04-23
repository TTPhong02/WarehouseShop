import BaseService from "./BaseService";
class UsersService extends BaseService{
    constructor(){
        super("Users")
    }
    async login(user) {
        var res = await this.baseAxios.post(`${this.baseUrl}/Login`,user);
        return res;
    }
    async logout(email) {
        var res = await this.baseAxios.post(`${this.baseUrl}/Logout`,email);
        return res;
    }
    async register(user) {
        var res = await this.baseAxios.post(`${this.baseUrl}/Register`,user);
        return res;
    }
    async update(id,user) {
        var res = await this.baseAxios.put(`${this.baseUrl}/Update/${id}`,user,{
            headers: {
                "Content-Type": "multipart/form-data",
              },
        });
        return res;
    }
}
const usersService = new UsersService();
export default usersService;