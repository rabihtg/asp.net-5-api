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

        public UserData(ISqlDataAccess _db)
        {
            this._db = _db;
        }

        public Task<IEnumerable<UserModel>> GetUsers()
        {
            return _db.LoadData<UserModel, dynamic>("dbo.spUser_GetAll", new { });
        }

        public async Task<UserModel> GetUser(Guid id)
        {
            return (await _db.LoadData<UserModel, dynamic>("dbo.spUser_Get", new { Id = id })).FirstOrDefault();
        }

        public async Task<UserModel> GetUser(string userName)
        {
            return (await _db.LoadData<UserModel, dynamic>("dbo.spUser_GetByName", new { UserName = userName })).FirstOrDefault();
        }

        public Task InsertUser(InsertUserVM userVM)
        {
            UserPasswordHashHandler hasher = new(userVM.Password);
            hasher.ToString();
            UserModel user = new()
            {
                Id = Guid.NewGuid(),
                UserName = userVM.UserName,
                Password = hasher.CreateHash(typeof(SHA256Managed))
            };

            return _db.SaveData("dbo.spUser_Insert", user);
        }

        public Task UpdateUserName(string userName)
        {
            return _db.SaveData("dbo.spUser_UpdateUserName", new { UserName = userName });
        }
        public Task UpdateUserPassword(string password)
        {
            return _db.SaveData("dbo.spUser_UpdatePassword", new { Password = password });
        }
        public Task DeleteUser(Guid id)
        {
            return _db.SaveData("dbo.spUser_Delete", new { Id = id });
        }

    }
}
