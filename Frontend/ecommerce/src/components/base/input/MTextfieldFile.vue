<template>
  <div class="p-textfield3">
    <label v-if="textLabel" :for="name">{{ textLabel }} <span v-if="required" class="required">*</span>
    </label>
    <input
      :class="classInvalid"
      :id="name"
      type="file"
      :placeholder="textPlaceHolder"
      :name="name"
      @change="handleFileChange"
    />
    <span v-if="required">
      <span  class="p-input__validate--error" v-if="isInputValid"
        >{{ message }}</span
      >
    </span>
    
  </div>
</template>
<script>
export default {
  name: "MTextfieldFile",
  props: {
    textLabel: String,
    textPlaceHolder: String,
    typeInput: String,
    name: String,
    required: Boolean,
    message: String,
  },
  data() {
    return {
      isInputValid: false,
      messageInvalid: null,
      classInvalid: "p-textfield3__input  ",
    };
  },
  methods: {
        handleFileChange(event) {
        const selectedFile = event.target.files[0];
        this.$emit('file-selected', selectedFile);
        },
  },
  watch: {
    /**
     * Hàm lắng nghe sự kiện khi có thông tin sai
     *  Author: TTPhong(22/01/2024)
     */
    isInputValid(newValue){
      if(newValue === true && this.required === true){
        this.classInvalid =" p-textfield3__input p-input__invalid";
      }else{
        this.classInvalid =" p-textfield3__input";
      }
    },
    /**
     * Hàm lắng nghe sự kiện khi biến message được thay đổi
     *  Author: TTPhong(22/01/2024)
     */
    message(newValue){
      if (newValue) {
        this.isInputValid = true;
      } else {
        this.isInputValid = false;
      }
    },
  },
};
</script>
<style scoped>
label {
  font-weight: 500;
  font-size: 14px;
}
.required {
  color: red;
}

input::-webkit-datetime-edit,
input::-webkit-calendar-picker-indicator {
  color: #999999;
  font-family: Notosans;
}
.p-textfield3 label
 {
  font-size: 14px;
  font-weight: 700;
  font-family: Notosans;
}
.p-textfield3 {
  display: flex;
  flex-direction: column;
  width: 100%;
}
.p-textfield3 input {
  height: 36px;
  padding: 4px 12px;
  margin-top: 8px;
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
