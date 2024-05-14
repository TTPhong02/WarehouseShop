using BE_WAREHOUSE.Core.DTOs;
using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.Order;
using BE_WAREHOUSE.Infrastructure.Interfaces;
using BE_WAREHOUSE.Core.Enum;
using Dapper;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using OfficeOpenXml.Table.PivotTable;

namespace BE_WAREHOUSE.Infrastructure.Repository
{
    public class OrdersRepository : BaseRepository<Orders>, IOrdersRepository
    {
        public OrdersRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Orders>> GetOrderByUsersId(Guid UsersId)
        {
            var sql = $"SELECT * FROM Orders WHERE UsersId  = '{UsersId}' ORDER BY CreatedDate DESC ";
            var res = await _dbContext.Connection.QueryAsync<Orders>(sql);
            return res;
        }

        public async Task<IEnumerable<Orders>> GetOrderDataByTime(List<DateTime> Date)
        {
            DynamicParameters parameters = new DynamicParameters();

            if (Date.Count > 1 && Date[1] != null)
            {
                parameters.Add("@StartDate", Date[0]);
                parameters.Add("@EndDate", Date[1]);
            }
            else if(Date.Count > 1 && Date[1] == null)
            {
                DateTime startDate = Date[0].AddDays(1);
                DateTime endDate = new DateTime(startDate.Year, 12, 31);
                parameters.Add("@StartDate", Date[0]);
                parameters.Add("@EndDate", endDate);
            }
            else
            {
                DateTime startDate = Date[0].AddDays(1);
                DateTime endDate = new DateTime(startDate.Year, 12, 31);
                parameters.Add("@StartDate", Date[0]);
                parameters.Add("@EndDate", endDate);
            }
            var sql = $"Proc_Get_Order_By_Time";

            var res = await _dbContext.Connection.QueryAsync<Orders>(sql, parameters);

            return res;
        }

        public async Task<IEnumerable<RevenueData>> GetRevenueDataByDate(List<DateTime> Date)
        {
            DynamicParameters parameters = new DynamicParameters();

            if (Date.Count > 1)
            {
                DateTime startYear = Date[0].AddDays(1);
                DateTime endYear = Date[1].AddDays(1);
                parameters.Add("@StartDate", startYear);
                parameters.Add("@EndDate", endYear);
            }
            else
            {
                DateTime startYear = Date[0].AddYears(-9);
                parameters.Add("@StartDate", startYear);
                parameters.Add("@EndDate", Date[0]);
            }
            var sql = $"Proc_Revenue_Date";

            var res = await _dbContext.Connection.QueryAsync<RevenueData>(sql, parameters);

            return res;
        }

        public async Task<IEnumerable<RevenueData>> GetRevenueDataByMonth(List<DateTime> Date)
        {
            DynamicParameters parameters = new DynamicParameters();

            if (Date.Count > 1)
            {
                DateTime startYear = Date[0].AddDays(1);
                DateTime endYear = Date[1].AddDays(1);
                parameters.Add("@StartMonth", startYear);
                parameters.Add("@EndMonth", endYear);
            }
            else if(Date.Count > 1  || Date[1] == null )
            {
                DateTime startMonth = Date[0].AddDays(1);
                DateTime endMonth = new DateTime(startMonth.Year, 12, 1); 
                parameters.Add("@StartMonth", startMonth);
                parameters.Add("@EndMonth", endMonth);
            }
            else
            {
                DateTime startMonth = Date[0].AddDays(1);
                DateTime endMonth = new DateTime(startMonth.Year, 12, 1);
                parameters.Add("@StartMonth", startMonth);
                parameters.Add("@EndMonth", endMonth);
            }
            var sql = $"Proc_Revenue_Month";

            var res = await _dbContext.Connection.QueryAsync<RevenueData>(sql, parameters);

            return res;
        }

        public async Task<IEnumerable<RevenueData>> GetRevenueDataByMonthOfYear(DateTime Date)
        {
            DynamicParameters parameters = new DynamicParameters();
            DateTime startMonth = new DateTime(Date.Year,1,1);
            DateTime endMonth = new DateTime(Date.Year, 12, 1);
            parameters.Add("@StartMonth", startMonth);
            parameters.Add("@EndMonth", endMonth);
            var sql = $"Proc_Revenue_Month";

            var res = await _dbContext.Connection.QueryAsync<RevenueData>(sql, parameters);

            return res;
        }

        public async Task<IEnumerable<RevenueData>> GetRevenueDataByYear(List<DateTime> Date)
        {

            DynamicParameters parameters = new DynamicParameters();

            if(Date.Count > 1)
            {
                DateTime startYear = Date[0].AddDays(1);
                DateTime endYear = Date[1].AddDays(1);
                parameters.Add("@StartYear", startYear);
                parameters.Add("@EndYear", endYear);
            }
            else
            {
                DateTime startYear = Date[0].AddYears(-9);
                parameters.Add("@StartYear", startYear);
                parameters.Add("@EndYear", Date[0]);
            }
            var sql = $"Proc_Revenue_Year";
                
            var res = await _dbContext.Connection.QueryAsync<RevenueData>(sql,parameters);

            return res;
            
        }

        public Task<double> GetSumRevenue()
        {
            var sql = $"SELECT SUM(TotalAmount) AS Revenue FROM Orders ";
            var res = _dbContext.Connection.ExecuteScalarAsync<double>(sql);
            return res;
        }
        public Task<Double> GetSumRevenueByYear(DateTime Date)
        {
            var sql = $"SELECT SUM(TotalAmount) AS Revenue FROM Orders Where 'YEAR({Date})' =  YEAR(CreatedDate)";
            var res = _dbContext.Connection.ExecuteScalarAsync<double>(sql);
            return res;
        }
        public Task<Double> GetSumRevenueByMonth(DateTime Date)
        {
            var sql = $"SELECT SUM(TotalAmount) AS Revenue FROM Orders Where 'MONTH({Date})' =  MONTH(CreatedDate) AND 'YEAR({Date})' = YEAR(CreatedDate)";
            var res = _dbContext.Connection.ExecuteScalarAsync<double>(sql);
            return res;
        }
        public Task<Double> GetSumRevenueByDate(DateTime Date)
        {
            var sql = "SELECT SUM(TotalAmount) AS Revenue FROM Orders WHERE DATE(CreatedDate) = @Date";
            var parameters = new { Date = Date.Date }; 
            var res = _dbContext.Connection.ExecuteScalarAsync<double>(sql, parameters);
            return res;
        }

        public Task<IEnumerable<UserOrders>> GetUserMostOrders()
        {
            var sql = "SELECT UsersId,Fullname, COUNT(OrdersId) AS TotalOrders, SUM(TotalAmount) AS TotalPurchaseAmount FROM view_orders_users WHERE Role = 'User' GROUP BY UsersId ORDER BY TotalOrders DESC LIMIT 3";
            var res = _dbContext.Connection.QueryAsync<UserOrders>(sql);
            return res;
        }

        public async Task<int> GetNumberTotalOrder(string type)
        {
            var total = 0;
            if (type == "All" )
            {
                var sql = "SELECT Count(OrdersId)  FROM Orders";
                var res = await _dbContext.Connection.ExecuteScalarAsync<int>(sql);
                total = res;
            }
            else if (type == "DateNow")
            {
                var date = DateTime.Now.Date; 
                var sql = $"SELECT COUNT(OrdersId) FROM Orders WHERE '{date.ToString("yyyy-MM-dd")}' = DATE(CreatedDate)";
                var res = await _dbContext.Connection.ExecuteScalarAsync<int>(sql);
                total = res;
            }
            return total;
        }

        public async Task<IEnumerable<Orders>> GetOrdersByStatus(OrderStatus status)
        {
            var sql = $"SELECT * FROM Orders WHERE OrdersStatus = '{status}'";
            var res = await _dbContext.Connection.QueryAsync<Orders>(sql);
            return res;
        }

        public async Task<PagingEntity<Orders>> FilterOrderByStatus(int pageSize, int pageNumber, int? orderStatus, int? paymentStatus, int? deliveryStatus,string searchString)
        {
            var procedure = $"Proc_Filter_Order_By_Status";

            PagingEntity<Orders> pagingEntity = new PagingEntity<Orders>();
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@pageSize", pageSize);
            parameters.Add("@pageNumber", pageNumber);
            parameters.Add("@orderStatus", orderStatus);
            parameters.Add("@paymentStatus",paymentStatus);
            parameters.Add("@deliveryStatus", deliveryStatus);
            parameters.Add("@searchString", searchString);

            parameters.Add("@totalRecord", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
            parameters.Add("@totalPage", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

            var res = await _dbContext.Connection.QueryAsync<Orders>(procedure, parameters, commandType: System.Data.CommandType.StoredProcedure);

            pagingEntity.Data = res;
            pagingEntity.TotalRecord = parameters.Get<int>("@totalRecord");
            pagingEntity.TotalPage = parameters.Get<int>("@totalPage");

            return pagingEntity;
        }
    
    }
}
