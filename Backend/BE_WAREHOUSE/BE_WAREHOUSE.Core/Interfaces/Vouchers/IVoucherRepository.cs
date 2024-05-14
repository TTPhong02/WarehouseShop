using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Interfaces.Vouchers
{
    public interface IVoucherRepository:IBaseRepository<Voucher>
    {
        public Task<Voucher> UseVoucher(string voucherCode);
        public Task<PagingEntity<Voucher>> FilterVoucher(int pageSize,int pageNumber,int? voucherType,string? searchString);
    }
}
