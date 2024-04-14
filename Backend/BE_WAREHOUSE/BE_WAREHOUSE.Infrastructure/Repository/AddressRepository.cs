using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.IAddress;
using BE_WAREHOUSE.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Infrastructure.Repository
{
    public class AddressRepository : BaseRepository<Address>, IAddressRepository
    {
        public AddressRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
