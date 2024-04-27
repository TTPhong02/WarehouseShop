<template>
    <div class="p-textfield3">
        <label for="name">{{textLabel}}</label>
        <input class="p-textfield3__input" :id="idInput" type="date" name="name"  v-model="inputChild">
    </div>
</template>
<script>
export default {
    name:"MTextfieldDate",
    props:{
        textLabel :String,
        idInput: String ,
        modelValue:String
    },
    data() {
        return {
            inputChild:null,
        }
    },
    created() {
        this.inputChild = this.modelValue;
    },
    watch: {
        /**
     * Hàm lắng nghe sự kiện khi biến input child thây đổi
     *  Author: TTPhong(22/01/2024)
     */
        inputChild(newValue){
            this.$emit("update:modelValue",newValue );
        },
        /**
     * Hàm lắng nghe sự kiện khi modelvalue thay đổi và gán lại cho inputchild
     *  Author: TTPhong(22/01/2024)
     */
        modelValue(newValue){
            this.inputChild = this.formatDate(newValue);
        }
    },
    methods: {
        /**
     * Hàm thực hiện format lại giá trị với định dạng date
     *  Author: TTPhong(22/01/2024)
     */
        formatDate(date){
            date = String(date).substring(0,10);
            return date;    
        }
    },
}
</script>
<style scoped>
label {
  font-weight: 500;
  font-size: 14px;
}
.required {
  color: red;
}
.p-textfield2__box {
  position: relative;
  width: 300px;
  height: 36px;
  border: 1px solid #e0e0e0;
  border-radius: 4px;
}
.p-textfield2__box input:focus {
  width: 254px;
  border-radius: 4px 0px 0px 4px;
  outline: unset;
}
.p-textfield2__box:hover input {
  background-color: #e0e0e0;
}
.p-textfield2__box--input {
  height: 100%;
  background-color: #fff;
  border-radius: 4px;
  width: 288px;
  border: unset;
  padding-left: 12px;
}

.p-textfield2__box--img {
  position: absolute;
  top: 8px;
  right: 8px;
  width: 20px;
  height: 20px;
}
.p-textfield2__label {
  font-weight: 700;
  font-family: Notosans;
}
.p-textfield1 {
  display: flex;
  flex-direction: column;
}
.p-textfield1 input {
  height: 36px;
  width: 276px;
  border: 1px solid #e6e6e6;
  padding: 0 12px;
}
.p-textfield1 input:focus {
  height: 36px;
  width: 276px;
  outline: unset;
}
.p-textfield1 input::placeholder {
  color: #999999;
  font-size: 14px;
}
input::-webkit-datetime-edit,
input::-webkit-calendar-picker-indicator {
  color: #999999;
  font-family: Notosans;
}
.p-textfield1 label,
.p-textfield3 label,
.p-radiobutton > label,
.p-radiobutton2 label {
  font-size: 14px;
  font-weight: 700;
  font-family: Notosans;
}
.p-radiobutton__option {
  display: flex;
  flex-direction: column;
}
.p-radiobutton__option label {
  margin-top: 12px;
  font-size: 14px;
  display: flex;
}
.p-radiobutton__option input {
  width: 15px;
  height: 15px;
  margin-right: 12px;
}
.p-radiobutton2 {
  height: 100%;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
}
.p-radiobutton2 input {
  width: 20px;
  height: 20px;
}
.p-radiobutton2__option {
  display: flex;
  align-items: center;
  justify-content: space-around;
  margin-bottom: 10px;
}
.p-textfield3 {
  display: flex;
  flex-direction: column;
  width: 100%;
}
.p-textfield3 input {
  height: 36px;
  border: 1px solid #e6e6e6;
  padding: 0 12px;
}
.p-textfield3 input:focus {
  height: 36px;
  outline: unset;
}
.p-textfield3 input::placeholder {
  color: #999999;
  font-size: 14px;
}
.p-input__validate--error {
  color: red;
  font-size: 12px;
  font-style: italic;
  margin-bottom: 20px;
}
.p-input__invalid {
  border: 1px solid red !important;
}
</style>