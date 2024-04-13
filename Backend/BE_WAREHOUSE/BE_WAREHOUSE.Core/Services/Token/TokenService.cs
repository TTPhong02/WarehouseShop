using AutoMapper;
using BE_WAREHOUSE.Core.DTOs;
using BE_WAREHOUSE.Core.Entities;
using BE_WAREHOUSE.Core.Interfaces.Token;
using BE_WAREHOUSE.Core.Interfaces.User;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MISA.AMISDemo.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BE_WAREHOUSE.Core.Services.Token
{
    public class TokenService : ITokenService
    {
        ITokenRepository _tokenRepository;
        IUsersRepository _usersRepository;
        IConfiguration _configuration;
        IMapper _mapper;

        public TokenService(ITokenRepository tokenRepository, IUsersRepository usersRepository, IConfiguration configuration, IMapper mapper)
        {
            _tokenRepository = tokenRepository;
            _usersRepository = usersRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        public JwtSecurityToken GenerateToken(List<Claim> claims)
        {
            var key = _configuration["Jwt:Key"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);


            var expires = DateTime.Now.AddMinutes(tokenValidityInMinutes); // Sử dụng UtcNow để tránh vấn đề múi giờ
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: credentials);

            return token;
        }

        public async Task<TokenModel> LoginTakeToken(UserLogin userLogin)
        {
            var user = await _usersRepository.FindUserByEmailAndPassword(userLogin);
            if(user != null) { 
                var authClaims = new List<Claim>();
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Email);
                    new Claim(ClaimTypes.Name, user.Fullname);
                    new Claim(ClaimTypes.Email, user.Email);
                    new Claim(ClaimTypes.Role, user.Role);
                    //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
                }
                var token = GenerateToken(authClaims);
                var refreshToken = GenerateRefreshToken();
                _ = int.TryParse(_configuration["Jwt:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);
                
                var userMapper = _mapper.Map<Users>(user);
                await _usersRepository.UpdateToken(userMapper, userMapper.UsersId);
                return new TokenModel
                {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                    RefreshToken = user.RefreshToken,
                    Users = user
                };
            }
            else
            {
                return new TokenModel
                {
                    AccessToken = null,
                    RefreshToken = null
                };
            }
        }

        public async Task LogoutAsync(string email)
        {
            var user = await _usersRepository.FindByEmailAsync(email);

            if (user == null)
            {
                throw new MISAValidateException("Thông tin tài khoản không chính xác.");
            }
            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;

            await _usersRepository.UpdateAsync(user,user.UsersId);
        }

        public async Task<TokenModel> RefreshToken(TokenModel tokenModel)
        {
            string? accessToken = tokenModel.AccessToken;
            var principal = GetPrincipalFromExpiredToken(accessToken);

            var user = await _usersRepository.GetUserByToken(tokenModel.RefreshToken.ToString());

            var newAccessToken = GenerateToken(principal.Claims.ToList());
            var newRefreshToken = GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;
            await _usersRepository.UpdateAsync(user, user.UsersId);

            return new TokenModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken
            };
        }

        public async Task<int> UpdateAccount(Users users)
        {
            var res = await _tokenRepository.UpdateUser(users);
            return res;
        }
        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Sai token");

            return principal;

        }
    }
}
