import localStorageService from "./LocalStorageService";
class UserLocalStorageService {
  /**
   * Thực hiện lấy giá trị của user từ localStorage
   * @returns
   * @author TTPhong(09/04/2024)
   */
  getUsertFromLocalStorage() {
    try {
      return localStorageService.getItemFromLocalStorage("User") ;
    } catch (error) {
      console.log("Error getting data from Local Storage:", error);
      return undefined;
    }
  }
  /**
   * Thực hiện tạo mới hoặc sửa user từ localStorage
   * @param {array} value
   * @author TTPhong(09/04/2024)
   */
  setUserToLocalStorage(value) {
    try {
      localStorageService.setItemToLocalStorage("User", value);
    } catch (error) {
      console.log("Error getting data from Local Storage:", error);
    }
  }
  removeCartLocalStorage() {
    try {
      localStorageService.removeItemLocalStorage("User");
    } catch (error) {
      console.log("Error remove data from Local Storage:", error);
    }
  }
}
const userLocalStorageService = new UserLocalStorageService();
export default userLocalStorageService;
