using BookLibraryClassLibrary.Authentication;
using BookLibraryClassLibrary.Data;
using BookLibraryClassLibrary.Models;
using BookLibraryClassLibrary.ViewModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;

namespace BookLibraryClassLibrary.JWT
{
    public class JwtManager : IJwtManager
    {
        private readonly IConfiguration _config;
        private readonly IUserData _data;

        public JwtManager(IConfiguration config, IUserData data)
        {
            _config = config;
            _data = data;
        }

        public async Task<TokenModel> Authenticate(InsertUserVM userVM)
        {
            var checkUser = await _data.GetUser(userVM.UserName);
            if (checkUser is null) return null;

            var salt = checkUser.Password.Split("$")[1].Split(":")[0];
            if (!UserPasswordHashHandler.VerifyHash(userVM.Password, salt, checkUser.Password, typeof(SHA256Managed)))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_config["JWT:Key"]);

            var tokenDesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userVM.UserName),
                    new Claim(ClaimTypes.Role, "x-1"),
                    new Claim("region", "MiddleEast")
                }),
                Expires = DateTime.Now.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDesc);

            return new TokenModel { Token = tokenHandler.WriteToken(token) };
        }
    }
}
