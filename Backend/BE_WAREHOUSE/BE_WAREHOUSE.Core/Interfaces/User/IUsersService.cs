using BE_WAREHOUSE.Core.DTOs;
using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.Base;
using MISA.AMISDemo.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Interfaces.User
{
    public interface IUsersService:IBaseService<Users>
    {
        Task<MISAServiceResult> RegisterServiceAsync(Users user);
        Task<MISAServiceResult> LoginServiceAsync(UserLogin userLogin);
    }
}
