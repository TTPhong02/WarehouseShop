using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.Base;
using BE_WAREHOUSE.Core.Interfaces.Cart;
using BE_WAREHOUSE.Core.Interfaces.Image;
using BE_WAREHOUSE.Core.Interfaces.Order;
using BE_WAREHOUSE.Core.Interfaces.Products;
using BE_WAREHOUSE.Core.Services.Base;
using Microsoft.AspNetCore.SignalR;
using MISA.AMISDemo.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Services.Order
{
    public class OrdersService : BaseService<OrdersService>, IOrdersService
    {
        IOrdersRepository _ordersRepository;
        IOrderDetailsRepository _orderDetailRepository;
        IProductRepository _productRepository;
        ICartItemsRepository _cartItemsRepository;
        public OrdersService(IBaseRepository<OrdersService> repository, IImagesService imagesService, IOrdersRepository ordersRepository, IOrderDetailsRepository orderDetailRepository, IProductRepository productRepository, ICartItemsRepository cartItemsRepository) : base(repository, imagesService)
        {
            _ordersRepository = ordersRepository;
            _orderDetailRepository = orderDetailRepository;
            _productRepository = productRepository;
            _cartItemsRepository = cartItemsRepository;
        }

        public async Task<Orders> CheckoutService(OrderData orderData)
        {
            var orderDetail = orderData.OrderDetails;
            var cartItemId = orderData.CartItemsId;
            var orders = orderData.Orders;
            var ordersId = Guid.NewGuid();
            var ordersCode = await _ordersRepository.GetMaxCodeAsync();
            orders.OrdersId = ordersId;
            orders.OrdersStatus = 0;
            orders.OrdersCode = ordersCode;
            var res = await _ordersRepository.InsertAsync(orders);
            var count = 0;
            if (res > 0)
            {
                foreach(var item in orderDetail) {
                    item.OrdersId = ordersId;
                    var resOrderDetail = await _orderDetailRepository.InsertAsync(item);
                    //Kiểm tra và cập nhật số lượng
                    var product = await _productRepository.GetByIdAsync(item.ProductId);
                    product.ProductSold += item.Quantity;
                    if(product.ProductStock > item.Quantity)
                    {
                        product.ProductStock -= item.Quantity;
                    }
                    else
                    {
                        throw new MISAValidateException("Số lượng không đủ !");
                    }
                    var resUpdate = await _productRepository.UpdateAsync(product, item.ProductId);
                    if(resUpdate > 0)
                    {
                        count += resUpdate;
                    }
                }
                if(count == orderDetail.Count)
                {
                    await _cartItemsRepository.DeleteAnyAsync(cartItemId);
                    
                }
            }
            return orders;
        }

        public async Task<IEnumerable<Orders>> GetOrderByUsersId(Guid UsersId)
        {
            var res = await _ordersRepository.GetOrderByUsersId(UsersId);
            return res;
        }
    }
}
