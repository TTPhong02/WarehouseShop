using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Enum;
using BE_WAREHOUSE.Core.Interfaces.Base;
using BE_WAREHOUSE.Core.Interfaces.Vouchers;
using BE_WAREHOUSE.Infrastructure.Interfaces;
using Dapper;
using MISA.AMISDemo.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Infrastructure.Repository
{
    public class VoucherRepository : BaseRepository<Voucher>, IVoucherRepository
    {
        public VoucherRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<PagingEntity<Voucher>> FilterVoucher(int pageSize, int pageNumber, int? voucherType, string? searchString)
        {
            var procedure = $"Proc_Filter_Voucher";

            PagingEntity<Voucher> pagingEntity = new PagingEntity<Voucher>();
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("@pageSize", pageSize);
            parameters.Add("@pageNumber", pageNumber);
            parameters.Add("@voucherType", voucherType);
            parameters.Add("@searchString", searchString);

            parameters.Add("@totalRecord", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
            parameters.Add("@totalPage", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

            var res = await _dbContext.Connection.QueryAsync<Voucher>(procedure, parameters, commandType: System.Data.CommandType.StoredProcedure);

            pagingEntity.Data = res;
            pagingEntity.TotalRecord = parameters.Get<int>("@totalRecord");
            pagingEntity.TotalPage = parameters.Get<int>("@totalPage");

            return pagingEntity;
        }

        public async Task<Voucher> UseVoucher(string voucherCode)
        {
            var date = DateTime.Now;
            string dateNow = date.ToString("yyyy-MM-dd HH:mm:ss");
            var sql = $"SELECT * FROM view_voucher where VoucherCode = '{voucherCode}' AND ExpiredDate > '{dateNow}' AND Quantity > 0";
            var res = await _dbContext.Connection.QueryFirstOrDefaultAsync<Voucher>(sql);
            if (res != null) { 
                return res;
            }
            else
            {
                throw new MISAValidateException(System.Net.HttpStatusCode.BadRequest, "Mã giảm giá không đúng hoặc hết lượt sử dụng");
            }
        }
    }
}
