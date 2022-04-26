using BookLibraryClassLibrary.Models;
using BookLibraryClassLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookLibraryClassLibrary.Data
{
    public interface IUserData
    {
        Task DeleteUser(Guid id);
        Task<UserModel> GetUser(Guid id);
        Task<UserModel> GetUser(string userName);
        Task<bool> IsValidUser(InsertUserVM userVM);
        Task<IEnumerable<UserModel>> GetUsers();
        Task InsertUser(InsertUserVM userVM);
        Task UpdateUserName(UpdateUserUserNameVM userVM);
        Task UpdateUserPassword(UpdateUserPassVM user);
    }
}