using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.IAddress;
using BE_WAREHOUSE.Infrastructure.Interfaces;
using Dapper;
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

        public async Task<IEnumerable<Address>> GetAddresByUsersId(Guid id)
        {
            var sql = $"SELECT * FROM Address WHERE UsersId = '{id}'";
            var res = await _dbContext.Connection.QueryAsync<Address>(sql);
            return res;
        }

        public async Task<Address> GetAddresDefaultByUsersId(Guid id)
        {
            var sql = $"SELECT * FROM Address WHERE UsersId = '{id}' AND AddressDefault = '1'";
            var res = await _dbContext.Connection.QueryFirstOrDefaultAsync<Address>(sql);
            return res;
        }
    }
}
