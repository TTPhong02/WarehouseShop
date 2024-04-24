<template>
      <div id="popup-sign"  pdialog class="p-popup-sign">
        <div class="p-popup__header">Thông tin nhân viên
            <button @click="CloseAddFormEmployee" class="p-dialog__button--close icon-button-close"></button>  
            <!-- <img id="btnCloseAddForm" class="close-popup-sign"  src="../assets/img/dialog_design_guideline/Layer 2.png" alt="closeicon"> -->
        </div>
        <form >
            <div class="p-popup__form">
                <div class="item-id">
                    <MTextfield  ref="employeeCode" typeInput="text" textPlaceHolder="Mã nhân viên" name="Mã nhân viên" textLabel="Mã nhân viên" v-model="employee.EmployeeCode" :message="dialog.errors.employeeCode" :required ="true"> </MTextfield>
                </div>
                <div class="item-name">
                    <MTextfield  ref="employeeFullName" typeInput="text" textPlaceHolder="Họ và tên đầy đủ" name="Họ và tên" idInput="txtEmployeeFullName" textLabel="Họ và tên" v-model="employee.FullName" :message="dialog.errors.fullName" :required ="true"> </MTextfield>
                </div>
                <div class="item-date">
                    <MTextfieldDate idInput="txtEmployeeDateOfBirth" textLabel="Ngày sinh" v-model="employee.DateOfBirth"></MTextfieldDate>
                </div>
                <div class="item-gender">
                    <MRadioGender v-model="employee.Gender"></MRadioGender>
                </div>
                <div class="item-department">
                    <MCombobox ref="employeeDepartment" textLabel="Đơn vị" :messageInvalid="dialog.errors.department" :required="true"  :dataCombobox="dataCombobox.department.departments.departmentName"  @selected="onComboboxDepartmentSelected" v-model="dataCombobox.department.selectedItem"></MCombobox>
                </div>   
                <div class="item-card">
                    <MTextfield  ref="employeeIdentityNumber" :message="dialog.errors.identityNumber"  typeInput="text" textPlaceHolder="Số CMTND"  idInput="txtEmployeeCard" textLabel="Số CMTND" v-model="employee.IdentityNumber"> </MTextfield>
                </div>
                <div class="item-date">
                    <MTextfieldDate idInput="txtEmployeeDateCard" textLabel="Ngày cấp" v-model="employee.IdentityDate"></MTextfieldDate>
                </div>
                <div class="item-position">
                    <MCombobox  textLabel="Chức danh"  :dataCombobox="dataCombobox.position.positions.positionsName"  @selected="onComboboxPositionSelected" v-model="dataCombobox.position.selectedItem"></MCombobox>
                </div>
                <div class="item-place">
                    <MTextfield typeInput="text" textPlaceHolder="Nơi cấp"  idInput="txtEmployeePlace" textLabel="Nơi cấp" v-model="employee.IdentityPlace"> </MTextfield>
                </div>
                 <div class="item-address">
                    <MTextfield typeInput="text" textPlaceHolder="Địa chỉ"  idInput="txtEmployeeAddress" textLabel="Địa chỉ" v-model="employee.Address"> </MTextfield>
                </div>
                <div class="item-phone">
                    <MTextfield typeInput="text" textPlaceHolder="Số điện thoại"  idInput="txtEmployeePhoneNumber" textLabel="Số điện thoại" v-model="employee.PhoneNumber"> </MTextfield>
                </div>  
                <div class="item-phone">
                    <MTextfield typeInput="text" textPlaceHolder="Số điện thoại cố định"  idInput="txtEmployeePhoneNumber" textLabel="Số điện thoại cố định"  v-model="employee.PhoneNumberFixed"> </MTextfield>
                </div> 
                <div class="item-email" >
                    <MTextfield  ref="employeeEmail" typeInput="email"  textPlaceHolder="example@gmail.com"  idInput="txtEmployeeEmail" textLabel="Email" v-model="employee.Email" :message="dialog.errors.email" > </MTextfield >
                </div>    
                <div class="item-bankid">
                    <MTextfield typeInput="text" textPlaceHolder="Số tài khoản"  idInput="txtEmployeeAddress" textLabel="Số tài khoản ngân hàng" v-model="employee.BankAccount"> </MTextfield>
                </div>
                <div class="item-bankname">
                    <MTextfield typeInput="text" textPlaceHolder="Tên ngân hàng"  idInput="txtEmployeeAddress" textLabel="Tên ngân hàng" v-model="employee.BankName"> </MTextfield>
                </div>
                <div class="item-bankplace">
                    <MTextfield typeInput="text" textPlaceHolder="Chi nhánh ngân hàng"  idInput="txtEmployeeAddress" textLabel="Chi nhánh ngân hàng" v-model="employee.BankAddress"> </MTextfield>
                </div>
                <div class="p-popup-sign__button">
                    <div class="p-popup__button--left" >
                        <MButton className="p-button2" text="Hủy" @click="CloseAddFormEmployee"></MButton>          
                    </div>
                    <div class="p-popup__button--right" >
                        <MButton className="p-button1" text="Cất" @click="checkInvalid(true)"></MButton> 
                        <MButton className="p-button1" text="Cất và thêm" @click="checkInvalid(false)" ></MButton> 
                    </div>
                </div>

            </div>
            
        </form>
    
    <!-- <MToastDone :title="toast.title" :text="toast.text"></MToastDone> -->
    </div>
</template>

<script>
export default {

}
</script>

<style>

</style>