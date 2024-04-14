using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.Base;
using BE_WAREHOUSE.Core.Interfaces.IAddress;
using BE_WAREHOUSE.Core.Interfaces.Image;
using BE_WAREHOUSE.Core.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Services.Addresss
{
    public class AddressService : BaseService<Address>, IAddressService
    {
        public AddressService(IBaseRepository<Address> repository, IImagesService imagesService) : base(repository, imagesService)
        {
        }
    }
}
