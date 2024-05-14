using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.Base;
using BE_WAREHOUSE.Core.Interfaces.Image;
using BE_WAREHOUSE.Core.Interfaces.Vouchers;
using BE_WAREHOUSE.Core.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Services.Vouchers
{
    public class VoucherService : BaseService<Voucher>, IVoucherService
    {
        public VoucherService(IBaseRepository<Voucher> repository, IImagesService imagesService) : base(repository, imagesService)
        {
        }
    }
}
