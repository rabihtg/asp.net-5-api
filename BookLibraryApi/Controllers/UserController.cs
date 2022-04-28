using BookLibraryClassLibrary.Authentication;
using BookLibraryClassLibrary.CustomExceptions;
using BookLibraryClassLibrary.Data;
using BookLibraryClassLibrary.JWT;
using BookLibraryClassLibrary.Models;
using BookLibraryClassLibrary.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace BookLibraryApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserData _data;
        private readonly IJwtManager _jwtManager;

        public UserController(IUserData data, IJwtManager jwtManager)
        {
            _data = data;
            _jwtManager = jwtManager;
        }

        [AllowAnonymous]
        [HttpPost("Singup")]
        public async Task<IActionResult> InsertUser(InsertUserVM userVM)
        {
            await _data.InsertUser(userVM);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Authenticate(InsertUserVM userVM)
        {
            var token = await _jwtManager.Authenticate(userVM);

            if (token is null)
            {
                throw new BadRequestExc("Incorrect Validation Info", (int)HttpStatusCode.Unauthorized);
            }

            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost("change-password")]
        public async Task<IActionResult> UpdatePassword(UpdateUserPassVM userVM)
        {
            var user = new InsertUserVM
            {
                UserName = userVM.UserName,
                Password = userVM.OldPassword
            };

            if ((await _data.IsValidUser(user)) == false)
            {
                return BadRequest("Incorrect Credintials");
            }

            await _data.UpdateUserPassword(userVM);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("change-username")]
        public async Task<IActionResult> UpdateUserName(UpdateUserUserNameVM userVM)
        {
            var user = new InsertUserVM
            {
                UserName = userVM.OldUserName,
                Password = userVM.Password
            };

            if ((await _data.IsValidUser(user)) == false)
            {
                return BadRequest("Incorrect Credintials");
            }

            await _data.UpdateUserName(userVM);
            return Ok();
        }

        [HttpPost("change-password-with-token")]
        public async Task<IActionResult> UpdatePassword(string newPassword)
        {
            var userVM = new UpdateUserPassVM
            {
                UserName = User.Identity.Name,
                NewPassword = newPassword
            };
            await _data.UpdateUserPassword(userVM);
            return Ok();
        }

        [HttpPost("change-username-with-token")]
        public async Task<IActionResult> UpdateUserName([FromBody] string newUserName)
        {
            var userVM = new UpdateUserUserNameVM
            {
                OldUserName = User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier).Value.ToString(),
                NewUserName = newUserName
            };

            await _data.UpdateUserName(userVM);

            return Ok(new
            {
                Message = $"oldName: {User.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier).Value}, newName: {newUserName}",
                StatusCode = 200
            });
        }

    }
}
