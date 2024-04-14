import { createApp } from 'vue'
import App from './App.vue'
import MDialog from './components/base/dialog/MDialog.vue';
import MButton from './components/base/button/MButton.vue';
import MToastMessage from './components/base/toast/MToastMessage.vue';
import MLoading from "./components/base/loading/MLoading.vue"
import AdminLogin from "./view/admin/AdminLogin.vue"
import axios from 'axios';
import MISAEnum from './helpers/enum';
import MISAResource from './helpers/resource';
// import { createRouter,createWebHistory } from 'vue-router';
import tinyEmitter from 'tiny-emitter/instance';
import MTooltip from "./components/base/tooltip/MTooltip.vue";
import router from "./router/routes.js";
import urlApi from './js/request/urlapi';
import helper from './helpers/format';
// // Import Bootstrap and BootstrapVue CSS files (order is important)
// import 'bootstrap/dist/css/bootstrap.css'
// import 'bootstrap-vue/dist/bootstrap-vue.css'

const app = createApp(App);
app.component("AdminLogin",AdminLogin); 
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
app.config.globalProperties.helper = helper;
app.use(router);
app.mount('#app');