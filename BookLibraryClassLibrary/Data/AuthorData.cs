using BookLibraryClassLibrary.DataAccess;
using BookLibraryClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibraryClassLibrary.Data
{
    public class AuthorData : IAuthorData
    {
        private readonly ISqlDataAccess _db;

        public AuthorData(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<IEnumerable<AuthorModel>> GetAuthors()
        {
            return _db.LoadData<AuthorModel, dynamic>("dbo.spAuthor_GetAll", new { });
        }

        public async Task<IEnumerable<AuthorWithBooksModel>> GetAuthorWithBooks()
        {
            var result = await _db.LoadWithRelation<AuthorWithBooksModel, BookModel, dynamic>("dbo.spAuthorWithBooks_GetAll", MapAuthorBook, new { });
            return result.GroupBy(e => e.Id).Select(g =>
            {
                var gA = g.First();
                gA.BookTitles = g.Select(g => g.BookTitles.FirstOrDefault()).ToList();
                return gA;
            });
        }

        public async Task<AuthorModel> GetAuthor(Guid id)
        {
            return (await _db.LoadData<AuthorModel, dynamic>("dbo.spAuthor_Get", new { Id = id })).FirstOrDefault();
        }

        public Task InsertAuthor(AuthorModel author)
        {
            return _db.SaveData("dbo.spAuthor_Insert", author);
        }

        public Task UpdateAuthor(AuthorModel author)
        {
            return _db.SaveData("dbo.spAuthor_Update", author);
        }

        public Task DeleteAuthor(Guid id)
        {
            return _db.SaveData("dbo.spAuthor_Delete", new { Id = id });
        }

        private static AuthorWithBooksModel MapAuthorBook(AuthorWithBooksModel author, BookModel book)
        {
            author.BookTitles.Add(book?.Title);
            return author;
        }
    }
}
