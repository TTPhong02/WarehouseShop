<template>
  <div class="s-address">
    <div class="s-address-header">
      Thông tin địa chỉ
      <div class="button-address">
        <button v-if="!isShowForm" @click="showFormAdd()">Thêm địa chỉ mới</button>
      </div>
    </div>
    <div v-if="isShowForm" class="s-address-form">
      <div class="address-user-location-update">Địa chỉ của bạn : {{this.address.HomeNumber}}, {{this.address.Ward}}, {{this.address.District}}, {{this.address.Province}}</div>
      <div class="address-form">
        <div class="address-form-item">
          <label for="">Họ tên:</label>
          <input v-model="this.address.ReminiscentName" type="text">
        </div>
        <div class="address-form-item">
          <label for="">Số điện thoại:</label>
          <input v-model="this.address.PhoneNumber" type="text">
        </div>
        <div class="address-form-item">
          <label for="">Số nhà/đường:</label>
          <input v-model="this.address.HomeNumber" type="text">
        </div>
      </div>
      <div class="address-form">
        <div class="address-form-item">
          <label for="">Tỉnh/Thành Phố:</label>
          <select v-model="provinceSelected"  name="" id="">
                <option v-for="item in province" 
                :key="item.province_id" 
                :value="{
                    province_id: item.province_id,
                    province_name: item.province_name,
                    province_type: item.province_type,
                }">
                {{item.province_name}}
                </option>
            </select>
        </div>
        <div class="address-form-item">
          <label for="">Quận/Huyện:</label>
          <select v-model="districtSelected"  name="" id="">
            <option v-for="item in district" 
            :key="item.district_id" 
            :value="{
                district_id: item.district_id,
                district_name: item.district_name,
                district_type: item.district_type,
            }">
            {{item.district_name}}
            </option>
          </select>
        </div>
        <div class="address-form-item">
          <label for="">Xã/Phường:</label>
          <select v-model="wardSelected" name="" id="">
                    <option v-for="item  in ward" 
                    :key="item.ward_id" 
                    :value="{
                        ward_id: item.ward_id,
                        ward_name: item.ward_name,
                        ward_type: item.ward_type,
                    }">
                    {{item.ward_name}}
                    </option>
                </select>
        </div>
      </div>
      <div class="red">
        {{this.errorAddress}}
      </div>
      <div class="button-address-add">
        <button v-if="this.formMode == this.Enum.FormMode.ADD" @click="addAddress()">Thêm </button>
        <button v-if="this.formMode == this.Enum.FormMode.UPDATE" @click="addAddress()">Cập nhật </button>
        <button @click="isShowForm = false" class="button-cancel">Hủy</button>
      </div>
    </div>
    <div v-if="!isShowForm" class="s-address-list">
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
            <button v-if="item.AddressDefault == 0" @click="setDefaultAddress(item.AddressId)" class="action-address-default">Đặt làm mặc định</button>
            <i @click="showFormUpdateAddress(item.AddressId)" class="fa-solid fa-user-pen"></i>
            <i @click="deleteAddress(item.AddressId)" class="fa-regular fa-trash-can"></i>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import localStorageService from '../../../js/storage/LocalStorageService';
import addressService from '../../../utils/AddressService';
import addressApiService from '../../../utils/ApiAddressService';
export default {
  data() {
    return {
      addressUpdate:{},
      addressList:[],
      user:{},
      formMode:null,
      isShowForm:false,
      address:{},
      provinceSelected:{},
      districtSelected:{},
      wardSelected:{},
      listErrorMessage:{},
      province:[],
      district:[],
      ward:[],
      errorAddress:"",
    }
  },
  watch:{
    
    provinceSelected: async function (newValue){
        if(newValue == null){
            this.listErrorMessage.province= "Vui lòng chọn tỉnh / thành phố"
        }
        await this.takeAddressDistrict();
    },
    districtSelected: async function(newValue){
        if(newValue == null){
            this.listErrorMessage.district= "Vui lòng chọn quận / huyện "
        }
        await this.takeAddressWard();
    },
    wardSelected:  function(newValue){
        if(newValue == null){
            this.listErrorMessage.ward= "Vui lòng chọn xã / phường"
        }
    }
  },
  mounted() {
    this.takeAddressDistrict();
    this.takeAddressProvince();
    this.takeAddressWard();
    this.takeDataAddress();
  },
  methods: {
    showFormAdd(){
      this.isShowForm = true;
      this.formMode = this.Enum.FormMode.ADD
    },
    async showFormUpdateAddress(id){
      this.formMode = this.Enum.FormMode.UPDATE
      this.isShowForm = true;
      await this.takeAddressById(id);
    },
    async setDefaultAddress(id){
      try{
        var res = await addressService.setAddressDefault(id,this.user.UsersId);
        if(res.status == 200){
          this.emitter.emit("showToast",this.Enum.ToastType.SUCCESS,"Đặt mặc định thành công !")
        }
      }catch(error){
        console.log(error);
      }
    },
    async takeAddressById(id){
      var res = await addressService.getById(id);
      this.address = res.data;
    },
    async deleteAddress(id){
      try{
        var res = await addressService.delete(id);
        if(res.status == 200){
          this.emitter.emit("showToast",this.Enum.ToastType.SUCCESS,"Xóa thành công !")
        }
        await this.takeDataAddress();
      }catch(error){
        console.log(error);
      }
    },
    async addAddress(){
        if(this.provinceSelected == null || this.districtSelected == null || this.wardSelected == null || this.address.ReminiscentName == null || this.address.HomeNumber == null ||this.address.PhoneNumber == null || this.address.ReminiscentName == "" || this.address.HomeNumber == "" ||this.address.PhoneNumber == ""){
          this.errorAddress = "Vui lòng điền đẩy đủ thông tin địa chỉ";
                      console.log(this.address);
        }else{
          this.errorAddress = "";
          if(this.formMode == this.Enum.FormMode.ADD){
            try{
              var address = {};
              address.UsersId = this.user.UsersId;
              address.Province = this.provinceSelected.province_name;
              address.Ward = this.wardSelected.ward_name;
              address.District = this.districtSelected.district_name;
              address.HomeNumber = this.address.HomeNumber;
              address.ReminiscentName = this.address.ReminiscentName;
              address.PhoneNumber = this.address.PhoneNumber;
              address.AddressDefault = 0;
              const formData = new FormData();
              formData.append("dataJson",JSON.stringify(address));
              var resAdd = await addressService.post(formData);
              if(resAdd.status == 201){
                this.emitter.emit("showToast",this.Enum.ToastType.SUCCESS,"Thêm địa chỉ thành công !");
              }
              this.address = {};
              this.isShowForm = false;
              this.takeDataAddress();

            }catch(error){
              console.log(error);
            }
          }
          else if(this.formMode == this.Enum.FormMode.UPDATE){

            try{
              var addressUpdate = {};
              const id  = this.address.AddressId
              addressUpdate.UsersId = this.user.UsersId;
              addressUpdate.Province = this.provinceSelected.province_name;
              addressUpdate.Ward = this.wardSelected.ward_name;
              addressUpdate.District = this.districtSelected.district_name;
              addressUpdate.HomeNumber = this.address.HomeNumber;
              addressUpdate.ReminiscentName = this.address.ReminiscentName;
              addressUpdate.PhoneNumber = this.address.PhoneNumber;
              addressUpdate.AddressDefault = 0;
              const formData = new FormData();
              formData.append("dataJson",JSON.stringify(addressUpdate));
              var resUpdate = await addressService.put(id,formData);
              if(resUpdate.status == 200){
                this.emitter.emit("showToast",this.Enum.ToastType.SUCCESS,"Cập nhật địa chỉ thành công !")
              }
              this.isShowForm = false;
              this.takeDataAddress();
              this.address = {};
            }catch(error){
              console.log(error);
            }
            
          }
        }
    },
    async takeAddressProvince(){
        try{
        var res = await addressApiService.getProvince();
        this.province = res;
        }catch(error){
            console.log(error);
        }
    },
    async takeAddressDistrict(){
        try{
        var res = await addressApiService.getDistrict(this.provinceSelected.province_id);
        this.district = res;
        }catch(error){
            console.log(error);
        }
    },
    async takeAddressWard(){
        try{
        var res = await addressApiService.getWard(this.districtSelected.district_id);
        this.ward = res;
        }catch(error){
            console.log(error);
        }
    },
    async takeDataAddress(){
      this.user = await localStorageService.getItemFromLocalStorage("User");
      this.addressList = await addressService.getAddressById(this.user.UsersId);
    },
  },
}
</script>

<style scoped>
.button-address-add{
  margin-top: 20px;
  display: flex;
  justify-content: center;
}
.button-cancel:hover{
  background-color: #6d6d6d !important;
}
.button-cancel{
  margin-left: 20px;
  cursor: pointer;
  padding: 5px 10px;
  font-size: 15px;
  background-color: #cfcfcf !important;
  color: #fff;
  border-radius: 8px;
  outline: unset;
}
.button-address-add button:hover{
  background-color: #025c7d;
}
.button-address-add button{
  cursor: pointer;
  padding: 5px 10px;
  font-size: 15px;
  background-color: #a2c5d2;
  color: #fff;
  border-radius: 8px;
  outline: unset;
}
.s-address-form{
  margin: 20px 0px;
}
.address-form-item {
  width: 200px;
}
.address-form-item label{
  font-size: 16px;
  font-weight: bold;
}
.address-form-item select,
.address-form-item input{
  padding: 5px 8px;
}
.address-form-item{
  padding: 5px 0px;
  display: flex;
  flex-direction: column;
}
.address-form{
  justify-content: space-between;
    display: flex;
}
.button-address button:hover{
  background-color: #025c7d;
}
.button-address button{
  cursor: pointer;
  padding: 5px 10px;
  font-size: 15px;
  background-color: #a2c5d2;
  color: #fff;
  border-radius: 8px;
  outline: unset;
}
.s-address-header{
  justify-content: space-between;
  display: flex;
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
.address-action i{
  margin: 0px 5px;
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
.address-user-location-update{
  font-size: 15px;
  font-weight: bold;
  color:#025c7d;
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