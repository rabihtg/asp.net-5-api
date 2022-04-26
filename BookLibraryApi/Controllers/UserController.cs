using BookLibraryClassLibrary.Authentication;
using BookLibraryClassLibrary.Data;
using BookLibraryClassLibrary.JWT;
using BookLibraryClassLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace BookLibraryApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserData _data;
        private readonly IJwtManager _jwtManager;

        public UserController(IUserData data, IJwtManager jwtManager)
        {
            _data = data;
            _jwtManager = jwtManager;
        }

        [HttpPost("Singup")]
        public async Task<IActionResult> InsertUser(InsertUserVM userVM)
        {
            await _data.InsertUser(userVM);
            return Ok();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Authenticate(InsertUserVM userVM)
        {
            var token = await _jwtManager.Authenticate(userVM);

            if (token is null)
            {
                return Unauthorized("Authorization Failed Due To Bad Request.");
            }
            return Ok(token);
        }

    }
}
