using BookLibraryClassLibrary.Authentication;
using BookLibraryClassLibrary.DataAccess;
using BookLibraryClassLibrary.Models;
using BookLibraryClassLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BookLibraryClassLibrary.Data
{
    public class UserData : IUserData
    {
        private readonly ISqlDataAccess _db;

        public UserData(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<IEnumerable<UserModel>> GetUsers()
        {
            return _db.LoadData<UserModel, dynamic>("dbo.spUser_GetAll", new { });
        }

        public async Task<UserModel> GetUser(Guid id)
        {
            return (await _db.LoadData<UserModel, dynamic>("dbo.spUser_Get", new { Id = id })).FirstOrDefault();
        }

        public async Task<bool> IsValidUser(InsertUserVM userVM)
        {
            var checkUser = await GetUser(userVM.UserName);
            if (checkUser is null) return false;

            var salt = checkUser.Password.Split("$")[1].Split(":")[0];
            if (!UserPassHashHandlerTwo.VerifyHash(userVM.Password, salt, checkUser.Password, typeof(SHA256Managed))) return false;

            return true;
        }

        public async Task<UserModel> GetUser(string userName)
        {
            return (await _db.LoadData<UserModel, dynamic>("dbo.spUser_GetByName", new { UserName = userName })).FirstOrDefault();
        }

        public Task InsertUser(InsertUserVM userVM)
        {
            UserModel user = new()
            {
                Id = Guid.NewGuid(),
                UserName = userVM.UserName,
                Password = UserPassHashHandlerTwo.Compute(userVM.Password, typeof(SHA256Managed)).ToString()
            };

            return _db.SaveData("dbo.spUser_Insert", user);
        }

        public Task UpdateUserName(UpdateUserUserNameVM userVM)
        {
            return _db.SaveData("dbo.spUser_UpdateUserName", new { userVM.NewUserName, userVM.OldUserName });
        }

        public Task UpdateUserPassword(UpdateUserPassVM user)
        {
            string newPassHash = UserPasswordHashHandler.CreateHash(user.NewPassword, typeof(SHA256Managed)).ToString();
            return _db.SaveData("dbo.spUser_UpdatePassword", new { NewPassword = newPassHash, user.UserName });
        }

        public Task DeleteUser(Guid id)
        {
            return _db.SaveData("dbo.spUser_Delete", new { Id = id });
        }

    }
}
