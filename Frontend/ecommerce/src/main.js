import { createApp } from 'vue'
import App from './App.vue'
import MHomePage from "./view/HomePage.vue";
import MCustomerList from "./view/CustomerList.vue";
import MEmployeeList from "./view/EmployeeList.vue";
import MReportPage from "./view/ReportPage.vue";
import MSettingPage from "./view/SettingPage.vue";
import MDialog from './components/base/dialog/MDialog.vue';
import MButton from './components/base/button/MButton.vue';
import MToastMessage from './components/base/toast/MToastMessage.vue';
import MLoading from "./components/base/loading/MLoading.vue"
import MLogin from "./view/Login.vue"
import axios from 'axios';
import MISAEnum from './helpers/enum';
import MISAResource from './helpers/resource';
// import { createRouter,createWebHistory } from 'vue-router';
import tinyEmitter from 'tiny-emitter/instance';
import MTooltip from "./components/base/tooltip/MTooltip.vue";
import router from "./router/routes.js";
import urlApi from './js/request/urlapi';


const app = createApp(App);
app.component("MHomePage",MHomePage);
app.component("MCustomerList",MCustomerList);
app.component("MEmployeeList",MEmployeeList);
app.component("MLogin",MLogin); 
app.component("MReportPage",MReportPage);
app.component("MSettingPage",MSettingPage);
app.component("MDialog",MDialog);
app.component("MToastMessage",MToastMessage);
app.component("MButton",MButton);
app.component("MLoading",MLoading);
app.component("MTooltip",MTooltip);
app.config.globalProperties.api = axios;
app.config.globalProperties.MISAEnum = MISAEnum;
app.config.globalProperties.MISAResource = MISAResource;
app.config.globalProperties.emitter = tinyEmitter;
app.config.globalProperties.token = tinyEmitter;
app.config.globalProperties.URLRequest = urlApi;
app.use(router);
app.mount('#app');