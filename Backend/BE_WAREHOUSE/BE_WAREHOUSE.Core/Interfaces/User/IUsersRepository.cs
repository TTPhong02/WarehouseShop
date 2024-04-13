using BE_WAREHOUSE.Core.DTOs;
using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.Base;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Interfaces.User
{
    public interface IUsersRepository:IBaseRepository<Users>
    {
        /// <summary>
        /// Kiểm tra tài khoản đã tồn tại trong hệ thống chưa
        /// </summary>
        /// <param name="user">thông tin tài khoản</param>
        /// <returns>
        /// true - đã tồn tại
        /// false - chưa tồn tại
        /// </returns>
        /// Created By: TTPhong(06/04/2024)
        Task<bool> CheckUserIsExist(string email);


        /// <summary>
        /// Kiểm tra thông tin người dùng có xác thực không
        /// </summary>
        /// <param name="user">thông tin tài khoản</param>
        /// <returns>
        /// true - xác thực
        /// false - không xác thực
        /// </returns>
        /// Created By: TTPhong(06/04/2024)
        Task<UserDTO?> FindUserByEmailAndPassword(UserLogin userLogin);

        Task<Users?> FindByEmailAsync(string email);

        Task<Users?> GetUserByToken(string token);
        Task<int> UpdateToken(Users user,Guid id);
    }
}
