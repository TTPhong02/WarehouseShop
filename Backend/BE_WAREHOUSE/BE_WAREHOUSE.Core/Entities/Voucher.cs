using BE_WAREHOUSE.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Entities
{
    public class Voucher
    {
        public Guid VoucherId { get; set; }
        public string VoucherCode { get; set; }
        public VoucherType VoucherType { get; set; }
        public int AmountDiscount { get; set; }
        public int? AmountCondition { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public DateTime ExpiredDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
