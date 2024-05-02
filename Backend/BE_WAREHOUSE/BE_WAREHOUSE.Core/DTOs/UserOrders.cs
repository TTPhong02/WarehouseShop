using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.DTOs
{
    public class UserOrders
    {
        public Guid UsersId { get; set; }
        public string ImagesPath { get; set; }
        public string Fullname { get; set; }
        public double TotalPurchaseAmount { get; set; }
        public int TotalOrders { get; set; }
    }
}
