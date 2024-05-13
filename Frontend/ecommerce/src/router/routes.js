import {createRouter,createWebHistory} from "vue-router";
import AdminLayout from "../layout/LayoutAdmin/AdminLayout.vue";
import ProductList from "../pages/admin-pages/product-management/ProductList.vue";
import CategoryList from "../pages/admin-pages/categories-management//CategoryList.vue";
import UserList from "../pages/admin-pages/account-management//UserList.vue";
import OrderList from "../pages/admin-pages/orders-management/OrderList.vue";
import OverviewAdmin from "../pages/admin-pages/overview-management/OverviewAdmin.vue";
import MOrdersDetail from "../pages/admin-pages/orders-management/MOrdersDetail.vue";
import AdminLogin from "../view/admin/AdminLogin.vue";
import UserLogin from "../view/user/UserLogin.vue";
import UserLayout from "../layout/LayoutUser/UserLayout.vue";
import ProductNew from "../pages/shop-pages/products/ProductNew.vue"
import CartPage from "../pages/shop-pages/carts/CartPage.vue";
import CheckoutPage from "../pages/shop-pages/checkout/CheckoutPage.vue"
import UserRegister from "../view/user/UserRegister.vue";
import UserForgetPassword from "../view/user/UserForgetPassword.vue";
import UserProfile from "../pages/shop-pages/profile/Profile.vue";
import MyAccount from "../pages/shop-pages/profile/MyAccount.vue";
import MyAddress from "../pages/shop-pages/profile/MyAddress.vue";
import MyOrder from "../pages/shop-pages/profile/MyOrder.vue";
import MainProduct from "../pages/shop-pages/products/MainProduct.vue"
import ProductDetail from "../pages/shop-pages/products/ProductDetail.vue"
import CategoriesDetail from "../pages/shop-pages/categories/CategoriesDetail.vue";
import localStorageService from "@/js/storage/LocalStorageService";
import AnalysisAdmin from '../pages/admin-pages/analysis-management/AnalysisAdmin.vue'
const routes = [
    {
        path:"/",
        component: UserLayout,
        meta:{ requiresAuth: true },
        children:[
            {
                path:'',
                components:{
                    ShopRouterView: MainProduct
                }
            },
            {
                path:"san-pham-moi",
                components:{
                    ShopRouterView: ProductNew
                }
            }
            ,
            {
                path:'profile',
                components:{
                    ShopRouterView:UserProfile
                },
                children:[
                    {
                        path:'',
                        components:{
                            LayoutRouterProfile: MyAccount
                        }
                    },
                    {
                        path:'address',
                        components:{
                            LayoutRouterProfile: MyAddress
                        }
                    },
                    {
                        path:'order',
                        components:{
                            LayoutRouterProfile: MyOrder
                        }
                    }
                ]
            },
            {
                path:'cart',
                components:{
                    ShopRouterView : CartPage
                },
            },
            {
                path:'product/:slug',
                components:{
                    ShopRouterView : ProductDetail
                },
                props: true,
            },
            {
                path:'categories/:slug',
                components:{
                    ShopRouterView : CategoriesDetail
                },
                props: true,
            }
        ]
    },
    {
        path:'/checkout',
        component:CheckoutPage,  
        
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
                path:"",
                components:{
                    ContentRouterView: OverviewAdmin,
                },              
            },
            {
                path:"orders-management",
                components:{
                    // default:DefaultLayout,
                    ContentRouterView: OrderList,
                }          
            },
            {
                path:'orders-management/:id',
                components:{
                    ContentRouterView: MOrdersDetail
                },
                props: true,
            },
            {
                path:"products-management",
                components:{
                    // default:DefaultLayout,
                    ContentRouterView: ProductList,
                },              
            },
            {
                path:"categories-management",
                components:{
                    // default:DefaultLayout,
                    ContentRouterView: CategoryList,
                },              
            },
            {
                path:"account-management",
                components:{
                    // default:DefaultLayout,
                    ContentRouterView: UserList,
                },              
            },
            {
                path:"analysis-management",
                components:{
                    // default:DefaultLayout,
                    ContentRouterView: AnalysisAdmin,
                },              
            },
        ]
    },
    {
        path:'/login',
        component: UserLogin,  
        
    },
    {
        path:'/register',
        component: UserRegister, 
    },
    {
        path:'/login-admin',
        component:AdminLogin
        
    }
    ,
    {
        path:'/forget',
        component:UserForgetPassword,
    }
    
];
const router = createRouter({
    history: createWebHistory(),
    routes,
});

router.beforeEach((to, from, next) => {
    // const token = JSON.parse(localStorage.getItem("Token"));
    var user = localStorageService.getItemFromLocalStorage("User");
    if (to.path ==="/admin" && user?.Role !== "Admin") {
      next("/login"); // Chuyển hướng đến trang user nếu admin cố gắng truy cập vào trang user
    } else if (to.path === "/" && user?.Role === "Admin") {
      next("/admin");
    }
    else {
    next();
    }
});
export default router