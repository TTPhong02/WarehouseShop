<template>
    <button type="button" v-bind:class="className">{{text}}</button>
</template> 
<script>
export default {
    name:"MButton",
    props:[
        "text",
        "className",
    ],
}
</script>
<style >
.processing{
    background-color: #7bd15c !important;
       border-radius: 4px ;
    min-width: 80px;
    height: 36px;
    font-size: 14px;
    color:#fff;
    border: unset;
    padding: 0px 16px 0px 12px;
    cursor: pointer;    
    outline: unset;
}
.processing:hover{
    background-color: #179506 !important;
}
.cancel{
    background-color: #d04444 !important;
       border-radius: 4px ;
    min-width: 80px;
    height: 36px;
    font-size: 14px;
    color:#fff;
    border: unset;
    padding: 0px 16px 0px 12px;
    cursor: pointer;    
    outline: unset;
}
.cancel:hover{
    background-color: #c80202 !important;
}
.p-button1-plus{
    background-color: #a2c5d2 !important;
    border-radius: 4px ;
    min-width: 80px;
    height: 36px;
    font-size: 14px;
    color:#fff;
    border: unset;
    padding: 0px 16px 0px 12px;
    cursor: pointer;
}
.p-button1{
    background-color: #a2c5d2 !important;
    border-radius: 4px ;
    min-width: 80px;
    height: 36px;
    font-size: 14px;
    color:#fff;
    border: unset;
    padding: 0px 16px;
    cursor: pointer;
}
.p-button1-plus:hover,
.p-button1:hover{
    background-color:#1ea2d2!important ;
}
.p-button1-plus:active,
.p-button1:active{
    background-color: #1ea2d2 !important;
}
.p-button1-plus{
    display: flex;
    align-items: center;
    column-gap: 4px;
}
.p-button1-plus i{
    font-weight: 500;
    font-size: 17px;
}
.p-button2{
    background-color: #fff !important;
    color: #1F1F1F;
    border-radius: 3px;
    min-width: 80px;
    height: 36px;
    padding: 0px 16px;
    border: 1px solid #E6E6E6 !important;
    cursor: pointer;
}
.p-button2:hover{
    background-color:#F6F6F6 !important ;
}
.p-button2:active{
    background-color: #E6E6E6 !important;
}
.p-button3{
    height: 36px;
    width: 36px;
    padding: 8px;
    display: flex;
    align-items: center;
    justify-content: center;
    cursor: pointer;
    border: 1px solid #39AC66 !important;
    border-radius: 4px;
    background-color: rgba(109, 189, 93, 0.1) !important;
}
.p-button3 i{
    font-size: 20px;
    color: #39AC66 !important;
}
.p-button3:active , .p-button3:active i {
    background-color: #1ea2d2 !important;
    color:#fff !important;
}
.p-button3 img{
    width: 20px;
    height: 20px;
    color:#50B83C !important;
}
.plus{
    font-size: 20px;
}
.p-button4{
    border-radius: 50%;
    box-shadow: 0px 0px 10px 2px #ccc!important;
    padding:10px;
    border: unset;
    cursor: pointer;
}
.p-button4 i{
    font-size: 20px;
    width: 20px;
    height: 20px;
    color: #666666!important;
}

.p-button5{
    background-color: #a2c5d2!important;
    border-radius: 4px ;
    width: 100%;
    height: 36px;
    font-size: 14px;
    color:#fff!important;
    border: unset;
    padding: 0px 16px;
    cursor: pointer;
}
.p-button5:hover{
    background-color: #1ea2d2!important;
    transition: all 0.6s;
}
</style>