using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Interfaces.IAddress
{
    public interface IAddressRepository:IBaseRepository<Address>
    {
        Task<IEnumerable<Address>> GetAddresByUsersId(Guid id);
        Task<Address> GetAddresDefaultByUsersId(Guid id);
        Task<int> SetAddressDefault(Guid idAddress, Guid idUser);
    }
}
