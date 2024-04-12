using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.Base;
using BE_WAREHOUSE.Core.Interfaces.Cart;
using BE_WAREHOUSE.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Infrastructure.Repository
{
    public class CartsRepository : BaseRepository<Carts>, ICartsRepository
    {
        public CartsRepository(IDbContext dbContext) : base(dbContext)
        {
        }
    }
}
