using BE_WAREHOUSE.API.Controllers.Base;
using BE_WAREHOUSE.Core.DTOs;
using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.Base;
using BE_WAREHOUSE.Core.Interfaces.Token;
using BE_WAREHOUSE.Core.Interfaces.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BE_WAREHOUSE.API.Controllers.User
{
    [ApiController]
    public class UsersController : BaseController<Users>
    {
        IUsersService _usersService;
        ITokenService _tokenService;
        public UsersController(IBaseRepository<Users> baseRepository, IBaseService<Users> baseService, IUsersService usersService, ITokenService tokenService) : base(baseRepository, baseService)
        {
            _usersService = usersService;
            _tokenService = tokenService;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            var res = await _tokenService.LoginTakeToken(userLogin);
            return StatusCode(201,res);
        }
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(TokenModel refreshToken)
        {
            var res = await _tokenService.RefreshToken(refreshToken);
            return StatusCode(201, res);
        }
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUserAsync(Users user)
        {
            var res =await _usersService.RegisterServiceAsync(user);
            return StatusCode(201, res);
        }

        [HttpPost("ChangeProfile")]
        public async Task<IActionResult> ChangeProfile(String dataJson,IFormFile imageFile,Guid id)
        {
            var res = await _usersService.UpdateServiceAsync(dataJson, imageFile, id);
            return Ok(res);
        }
    }
}
