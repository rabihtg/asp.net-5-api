using BookLibraryClassLibrary.Models;
using BookLibraryClassLibrary.ViewModels;
using System.Threading.Tasks;

namespace BookLibraryClassLibrary.JWT
{
    public interface IJwtManager
    {
        Task<TokenModel> Authenticate(InsertUserVM userVM);
    }
}