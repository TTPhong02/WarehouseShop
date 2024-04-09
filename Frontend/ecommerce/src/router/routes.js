import {createRouter,createWebHistory} from "vue-router";
import DefaultLayout from "../layout/default/DefaultLayout.vue";
import MEmployeeList from "../view/EmployeeList.vue";
import MCustomerList from "../view/CustomerList.vue";
import MHomePage from "../view/HomePage.vue";
import MReportPage from "../view/ReportPage.vue";
import MSettingPage from "../view/SettingPage.vue";
import MLogin from "../view/Login.vue";
import MMainLayout from "../layout/main/MainLayout.vue"
const routes = [
    {
        path:'/',
        components: {
            default : DefaultLayout,
            LayoutRouter: DefaultLayout
        },
        meta:{ requiresAuth: true },
        children:[
            {
                path:"home",
                components:{
                    default:DefaultLayout,
                    ContentRouterView: MHomePage,
                },              
            },
            {
                path:"customer",
                components:{
                    default:DefaultLayout,
                    ContentRouterView: MCustomerList,
                },              
            },
            {
                path:"employee",
                components:{
                    default:DefaultLayout,
                    ContentRouterView: MEmployeeList,
                },              
            },
            {
                path:"report",
                components:{
                    default:DefaultLayout,
                    ContentRouterView: MReportPage,
                },              
            },
            {
                path:"setting",
                components:{
                    default:DefaultLayout,
                    ContentRouterView: MSettingPage,
                },              
            },
        ]
    },
    {
        path:'/login',
        components:{
            default:MMainLayout,
            LayoutRouter:MLogin
        }
    }
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

router.beforeEach((to, from, next) => {
    // const token = JSON.parse(localStorage.getItem("Token"));
    const roles =JSON.parse(localStorage.getItem("Roles")) ;
    if (to.path ==="/admin" && !roles) {
      next("/login"); // Chuyển hướng đến trang user nếu admin cố gắng truy cập vào trang user
    } else if (to.path === "/login" && roles === "Admin") {
      next("/admin");
    } else if(to.path === "/login" && roles !== "User") {
      next("/");
    }else{
        next();
    }
});
export default router