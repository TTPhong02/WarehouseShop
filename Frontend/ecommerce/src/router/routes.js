import {createRouter,createWebHistory} from "vue-router";
import AdminLayout from "../layout/LayoutAdmin/AdminLayout.vue";
import ProductList from "../view/admin/ProductList.vue";
import CategoryList from "../view/admin/CategoryList.vue";
import MHomePage from "../view/admin/HomePage.vue";
import UserList from "../view/admin/UserList.vue";
import OrderList from "../view/admin/OrderList.vue";
import AdminLogin from "../view/admin/AdminLogin.vue";
import UserLogin from "../view/user/UserLogin.vue";
import MMainLayout from "../layout/main/MainLayout.vue";
import UserLayout from "../layout/LayoutUser/UserLayout.vue";
import ProductNew from "../pages/shop-pages/products/ProductNew.vue"
import CartPage from "../pages/shop-pages/carts/CartPage.vue";
import CheckoutPage from "../pages/shop-pages/checkout/CheckoutPage.vue"
import UserRegister from "../view/user/UserRegister.vue";
import UserForgetPassword from "../view/user/UserForgetPassword.vue";
const routes = [
    {
        path:'',
        components: {
            default : UserLayout,
            LayoutRouter: UserLayout
        },
        meta:{ requiresAuth: true },
        children:[
            {
                path:"/san-pham-moi",
                components:{
                    ShopRouterView: ProductNew
                }
            }
        ]
    },
    {
        path:'/cart',
        components: {
            default : UserLayout,
            LayoutRouter: CartPage
        },
        
    },
    {
        path:'/checkout',
        components: {
            default : UserLayout,
            LayoutRouter: CheckoutPage
        },
        
    },
    {
        path:'/admin',
        components: {
            default : AdminLayout,
            LayoutRouter: AdminLayout
        },
        meta:{ requiresAuth: true },
        children:[
            {
                path:"home",
                components:{
                    // default:DefaultLayout,
                    ContentRouterView: MHomePage,
                },              
            },
            {
                path:"orders",
                components:{
                    // default:DefaultLayout,
                    ContentRouterView: OrderList,
                },              
            },
            {
                path:"products",
                components:{
                    // default:DefaultLayout,
                    ContentRouterView: ProductList,
                },              
            },
            {
                path:"categories",
                components:{
                    // default:DefaultLayout,
                    ContentRouterView: CategoryList,
                },              
            },
            {
                path:"users",
                components:{
                    // default:DefaultLayout,
                    ContentRouterView: UserList,
                },              
            },
        ]
    },
    {
        path:'/login',
        components:{
            LayoutRouter:UserLogin
        }
        
    },
    {
        path:'/register',
        components:{
            LayoutRouter:UserRegister
        }
        
    },
    {
        path:'/login-admin',
        components:{
            default:MMainLayout,
            LayoutRouter:AdminLogin
        }
    }
    ,
    {
        path:'/forget',
        components:{
            default:MMainLayout,
            LayoutRouter:UserForgetPassword
        }
    }
    
];

const router = createRouter({
    history: createWebHistory(),
    routes,
});

router.beforeEach((to, from, next) => {
    // const token = JSON.parse(localStorage.getItem("Token"));
    const roles = "" ;
    if (to.path ==="/admin" && roles !== "Admin") {
      next("/login"); // Chuyển hướng đến trang user nếu admin cố gắng truy cập vào trang user
    } else if (to.path === "/login" && roles === "Admin") {
      next("/admin");
    }else{
        next();
    }
});
export default router