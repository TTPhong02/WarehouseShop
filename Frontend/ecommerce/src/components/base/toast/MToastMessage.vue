<template >
    <span v-if="showToast" id="toast" class="p-message1">
        <div class="p-message1__icon">
            <div class="icon-toast">
                <i v-if="this.type == this.Enum.ToastType.SUCCESS" style="color:green" class=" fa-solid fa-circle-check"></i>
                <i v-if="this.type == this.Enum.ToastType.FAILED" style="color:red" class="fa-solid fa-circle-exclamation"></i>
                <i v-if="this.type == this.Enum.ToastType.WARNING" style="color:gold" class="fa-solid fa-triangle-exclamation"></i>
            </div>
        </div>
        <div class="p-message1__text">
            {{this.text}}
        </div>
        <!-- <div class="p-message1__action">Hoàn tác</div> -->
        <div class="p-message1__closeicon">
            <i @click="hiddenToast()" class="fa-solid fa-xmark"></i>
        </div>
    </span>
</template>

<script>
export default {
    name:"MToastMessage",
    created() {
        this.emitter.on("showToast",this.onShowToastMessage);
    },
    data() {
        return {
            type:null,
            showToast: false,
            isFailed:false,
            text:null, 
        }
    },
    methods: {
        onShowToastMessage(type, text) {
            this.showToast = true;
            this.type = type;
            this.text = text;
            setTimeout(() => {
                this.showToast = false;
            }, 2900);
        },
        hiddenToast(){
            this.showToast = false;
        }
    }
}
</script>

<style>
#toast1,
#toast2{
    z-index: 1000;
}
.red{
    color: red !important;
}
.p-message1{
    display: flex;
    max-width: 550px;
    align-items: center;
    justify-content: space-between;
    background-color: #fff;
    height: 56px;
    border-radius: 4px;
    box-shadow: 0px 0px 20px 1px #ccc;
}
.p-message1__icon{
    padding-left: 16px;
}
.p-message1__icon i{
    vertical-align: middle;
    font-size: 24px;
    border-radius: 50%;
    color: #50B83C;
}
.p-message2__icon i{
    vertical-align: middle;
    font-size: 24px;
    border-radius: 50%;
    color: #ff0000;
}
.p-message1__title{
    padding-left: 8px;
    color: #50B83C;
    font-weight: 700;
}
.p-message1__text{
    font-weight: bold;
   padding:0px 20px;
}
.p-message1__action{

    font-weight: 700;
    text-decoration:underline;
    cursor: pointer;
    
}
.p-message1__closeicon{
    padding: 0 16px;
    cursor: pointer;
}
.p-message1__closeicon:hover i{
    font-weight: bold;
    color: cornflowerblue;
}
.p-message1__closeicon i{
    font-size: 18px;
    vertical-align: middle;
}
.p-message1__left, .p-message1__right{
    display: flex;
}
#toast{
    z-index: 1000000;
    position: fixed;
    top:10%;
    right: 30px;
    -webkit-animation: fadein 0.5s , fadeout 0.5s 2.5s;
    animation: fadein 0.5s , fadeout 0.5s 2.5s ; 
}


@-webkit-keyframes fadein {
    from {right: 0; opacity: 0;}
    to {right: 24px; opacity: 1;}
  }
  
  @keyframes fadein {
    from {right: 0; opacity: 0;}
    to {right: 24px; opacity: 1;}
  }
  
  @-webkit-keyframes fadeout {
    from {right: 24px; opacity: 1;}
    to {right: 0; opacity: 0;}
  }
  
  @keyframes fadeout {
    from {right: 24px; opacity: 1;}
    to {right: 0; opacity: 0;}
  }
</style>