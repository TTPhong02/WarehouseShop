import localStorageService from "./LocalStorageService";
class CartLocalStorageService {
  /**
   * Thực hiện lấy giá trị của cart từ localStorage
   * @returns
   * @author TTPhong(09/04/2024)
   */
  getCartFromLocalStorage() {
    try {
      return localStorageService.getItemFromLocalStorage("CartItems") || [];
    } catch (error) {
      console.log("Error getting data from Local Storage:", error);
      return undefined;
    }
  }

  /**
   * Thực hiện tạo mới hoặc sửa cart từ localStorage
   * @param {array} value
   * @author TTPhong(09/04/2024)
   */
  setCartToLocalStorage(value) {
    try {
      localStorageService.setItemToLocalStorage("CartItems", value);
    } catch (error) {
      console.log("Error getting data from Local Storage:", error);
    }
  }

  /**
   * Thực hiện thêm sản phẩm vào cart
   * @param {array} value
   * @author TTPhong(09/04/2024)
   */
  addItemToCart(value) {
    try {
      const data = this.getCartFromLocalStorage();
      //Tìm kiếm xem item được thêm đã có trong cart hay chưa
      if (data.length > 0) {
        const checkItemExist = data.filter(
          (item) => item.CartItemsId === value.CartItemsId
        );
        //Nếu chưa có thì thêm vào mảng giá trị
        if (checkItemExist.length === 0) {
          data.push(value);
        }
        //Nếu đã có thì update số lượng
        else {
          const itemExistObject = checkItemExist[0];
          itemExistObject.Quantity = itemExistObject.Quantity + value.Quantity;
          itemExistObject.ProvisionalMoney =
            itemExistObject.Quantity * value.Price;
        }
      } else {
        data.push(value);
      }

      //Cập nhật lại dữ liệu vào local
      this.setCartToLocalStorage(data);
    } catch (error) {
      console.log("Error adding item to Local Storage:", error);
    }
  }

  updateQuantityItemToCart(value) {
    try {
      const data = this.getCartFromLocalStorage();
      //Tìm kiếm xem item được thêm đã có trong cart hay chưa
      if (data.length > 0) {
        const checkItemExist = data.filter(
          (item) => item.CartItemsId === value.CartItemsId
        );

        //Update số lượng gỡ bỏ cái cũ thêm cái mới
        const itemExistObject = checkItemExist[0];
        itemExistObject.Quantity = value.Quantity;
        itemExistObject.ProvisionalMoney = value.Quantity * value.Price;
      }

      //Cập nhật lại dữ liệu vào local
      this.setCartToLocalStorage(data);
    } catch (error) {
      console.log("Error adding item to Local Storage:", error);
    }
  }

  /**
   * Thực hiện gỡ bỏ item ra khỏi cart
   * @param {string} itemId
   * @author TTPhong(09/04/2024)
   */
  removeItemGetOutCart(itemId) {
    try {
      const data = this.getCartFromLocalStorage();
      const updateData = data.filter((item) => item.CartItemsId !== itemId);
      this.setCartToLocalStorage(updateData);
    } catch (error) {
      console.log("Error adding item to Local Storage:", error);
    }
  }

  removeCartLocalStorage() {
    try {
      localStorageService.removeItemLocalStorage("cart");
    } catch (error) {
      console.log("Error remove data from Local Storage:", error);
    }
  }
}
const cartLocalStorageService = new CartLocalStorageService();
export default cartLocalStorageService;
