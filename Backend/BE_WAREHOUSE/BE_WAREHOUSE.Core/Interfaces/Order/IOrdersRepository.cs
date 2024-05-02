using BE_WAREHOUSE.Core.DTOs;
using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Enum;
using BE_WAREHOUSE.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Interfaces.Order
{
    public interface IOrdersRepository:IBaseRepository<Orders>
    {
        public Task<IEnumerable<Orders>> GetOrderByUsersId(Guid UsersId);
        public Task<IEnumerable<RevenueData>> GetRevenueDataByYear(List<DateTime> Date);
        public Task<IEnumerable<RevenueData>> GetRevenueDataByDate(List<DateTime> Date);
        public Task<IEnumerable<RevenueData>> GetRevenueDataByMonth(List<DateTime> Date);
        public Task<IEnumerable<RevenueData>> GetRevenueDataByMonthOfYear(DateTime Date);
        public Task<IEnumerable<Orders>> GetOrderDataByTime(List<DateTime> Date);
        public Task<Double> GetSumRevenue();
        public Task<Double> GetSumRevenueByMonth(DateTime Date);
        public Task<Double> GetSumRevenueByDate(DateTime Date);
        public Task<Double> GetSumRevenueByYear(DateTime Date);
        public Task<IEnumerable<UserOrders>> GetUserMostOrders();

        public Task<int> GetNumberTotalOrder(string type);
        public Task<IEnumerable<Orders>> GetOrdersByStatus(OrderStatus status);

    }
}
