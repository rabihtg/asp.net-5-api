using BookLibraryClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookLibraryClassLibrary.Data
{
    public interface IAuthorData
    {
        Task DeleteAuthor(Guid id);
        Task<AuthorModel> GetAuthor(Guid id);
        Task<IEnumerable<AuthorModel>> GetAuthors();
        Task<IEnumerable<AuthorWithBooksModel>> GetAuthorWithBooks();
        Task InsertAuthor(AuthorModel author);
        Task UpdateAuthor(AuthorModel author);
    }
}