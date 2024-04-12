using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.Base;
using BE_WAREHOUSE.Core.Interfaces.Cart;
using BE_WAREHOUSE.Core.Interfaces.Image;
using BE_WAREHOUSE.Core.Interfaces.User;
using BE_WAREHOUSE.Core.Services.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Services.User
{
    public class UsersService : BaseService<Users>, IUsersService
    {
        IUsersRepository _userRepository;
        ICartsRepository _cartRepository;
        public UsersService(IBaseRepository<Users> repository, IImagesService imagesService, IUsersRepository userRepository, ICartsRepository cartRepository) : base(repository, imagesService)
        {
            _userRepository = userRepository;
            _cartRepository = cartRepository;
        }
        public override async Task<MISAServiceResult> UpdateServiceAsync( string dataJson, IFormFile? imageFile, Guid id)
        {
            var user = JsonConvert.DeserializeObject<Users>(dataJson);

            //Lấy thông tin người dùng trong Db
            var userDb = await _userRepository.GetByIdAsync(id);

            user.Password = userDb.Password;

            //Nếu thay đổi mật khẩu thì kiểm tra xem mật khẩu hiện tại có khớp với mật khẩu trong db hay không
            if (!string.IsNullOrEmpty(user.CurrentPassword))
            {
                var currentPasswordMD5 = CalculateMD5Hash(user.CurrentPassword);
                //không khớp ném ra lỗi
                if (currentPasswordMD5 != userDb.Password)
                {
                    throw new MISAValidateException("Mật khẩu hiện tại không chính xác.");
                }
                //hợp lệ thì gán lại giá trị password
                else
                {
                    user.Password = CalculateMD5Hash(user.NewPassword);
                }
            }

            //Cập nhật thông tin
            var res = await _userRepository.UpdateAsync(user,id);
            return new MISAServiceResult
            {
                Success = true,
                Data = res,
                StatusCode = System.Net.HttpStatusCode.OK,
                Errors = null
            };
        }
        private string CalculateMD5Hash(string input)
        {
            // Kiểm tra xem input có phải là null không
            //if (input == null)
            //{
            //    return input;
            //}
            // Tạo một đối tượng MD5
            using (MD5 md5 = MD5.Create())
            {
                // Chuyển đổi input thành một mảng byte và tính toán giá trị băm
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Chuyển đổi mảng byte thành một chuỗi hex
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
        public async Task<MISAServiceResult> RegisterServiceAsync(Users user)
        {
            user.Role = "User";
            var newUserId = Guid.NewGuid();
            user.UsersId = newUserId;
            await ValidateBeforeInsert(user);
            var res = await _userRepository.InsertAsync(user);
            if (res > 0)
            {
                var cart = new Carts();
                cart.UsersId = user.UsersId;
                await _cartRepository.InsertAsync(cart);
                //Theem cart lam sau
            }
            return new MISAServiceResult
            {
                Success = true,
                Data = res,
                StatusCode = System.Net.HttpStatusCode.Created,
                Errors = null
            };
        }
        public override async Task ValidateBeforeInsert(Users user)
        {
            if (await _userRepository.CheckUserIsExist(user.Email))
            {
                throw new MISAValidateException("Email này đã tồn tại trong hệ thống");
            }
        }

    }
}
