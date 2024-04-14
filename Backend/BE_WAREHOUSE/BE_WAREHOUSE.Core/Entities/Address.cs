using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Entities
{
    public class Address
    {
        public Guid AddressId { get; set; }
        public Guid UsersId { get; set; }
        public string Ward { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
        public string? HomeNumber { get; set; }
        public Boolean? AddressDefault { get; set; }
        public string PhoneNumber { get; set; }
        public string Fullname { get; set; }
    }
}
