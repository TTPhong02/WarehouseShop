using BE_WAREHOUSE.Core.DTOs;
using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.Order;
using BE_WAREHOUSE.Infrastructure.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Infrastructure.Repository
{
    public class OrdersRepository : BaseRepository<Orders>, IOrdersRepository
    {
        public OrdersRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Orders>> GetOrderByUsersId(Guid UsersId)
        {
            var sql = $"SELECT * FROM Orders WHERE UsersId = '{UsersId}'";
            var res = await _dbContext.Connection.QueryAsync<Orders>(sql);
            return res;
        }

        public async Task<IEnumerable<Orders>> GetOrderDataByTime(List<DateTime> Date)
        {
            DynamicParameters parameters = new DynamicParameters();

            if (Date.Count > 1)
            {
                DateTime startYear = Date[0].AddDays(1);
                DateTime endYear = Date[1].AddDays(1);
                parameters.Add("@StartDate", Date[0]);
                parameters.Add("@EndDate", Date[1]);
            }
            else
            {
                DateTime startYear = Date[0].AddYears(-9);
                parameters.Add("@StartDate", startYear);
                parameters.Add("@EndDate", Date[0]);
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
            if(Date.Count < 1  || Date[1] == null )
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
    }
}
