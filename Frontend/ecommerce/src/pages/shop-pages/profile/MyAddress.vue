<template>
  <div class="s-address">
    <div class="s-address-header">
      Danh sách địa chỉ
    </div>
    <div class="s-address-list">
      <div v-for="item in addressList" :key="item.AddressId" class=" s-address-item">
        <div class="address-user">
          <img src="../../../assets/img/logo-home-address.png" alt="">
          <div class="address-user-profile">
              <div class="address-user-name">
                  {{item.ReminiscentName}}
                  <div v-if="item.AddressDefault == 1" class="address-default">Mặc định</div>
              </div>
              <div class="address-user-location">{{item.HomeNumber}}, {{item.Ward}}, {{item.District}}, {{item.Province}}</div>
          </div>
        </div>
        <div class="address-action">
            <button v-if="item.ReminiscentName == 0" class="action-address-default">Đặt làm mặc định</button>
            <i class="fa-solid fa-user-pen"></i>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import localStorageService from '../../../js/storage/LocalStorageService';
import addressService from '../../../utils/AddressService';
export default {
  data() {
    return {
      addressList:[],
      user:{}
    }
  },
  created() {
    this.takeDataAddress();
  },
  methods: {
    async takeDataAddress(){
      this.user = await localStorageService.getItemFromLocalStorage("User");
      this.addressList = await addressService.getAddressById(this.user.UsersId);
    }
  },
}
</script>

<style scoped>
.s-address-header{
  color: #a2c5d2;
  font-size: 17px;
  font-weight: bold;

}
.action-address-default:hover{
  background-color: #a2c5d2;
}
.action-address-default{
  cursor: pointer;
  outline: none;
  background-color: #025c7d;
  color: #fff;
  margin-right: 20px;
  font-size: 13px;
  padding: 5px 10px;
  border-radius: 5px;
}
.s-address-item{
    padding: 10px 0px;
    display: flex;
    justify-content: space-between;
}
.s-address{
  padding: 20px 10px;
}
.address-action i:hover{
    color: #a2c5d2;
}
.address-action{
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding-right: 20px;
    cursor: pointer;
    font-size: 20px;
    color: #025c7d;
}
.address-user-location{
    font-size: 13px;
    font-style: italic;
}
.address-default{
    margin-left: 10px;
    border-radius: 4px;
    padding: 2px 4px;
    background-color: rgba(3, 140, 203, 0.2);
}
.address-user-name{
    align-items: center;
    display: flex;
    font-size: 14px;
    font-weight: bold;
}
.address-user-profile{
    padding: 0px 10px;
}
.address-user img{
    width: 40px !important;
    height: 40px !important;
    border-radius: 50%;
    border: 1px solid #a2c5d2;
}
.address-user{
    padding: 0px 20px;
    display: flex;
    align-items: center;

}
</style>