using BookLibraryClassLibrary.DTO;
using BookLibraryClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookLibraryClassLibrary.Data
{
    public interface IBookData
    {
        Task DeleteBook(Guid id);
        Task<BookModel> GetBook(Guid id);
        Task<IEnumerable<BookModel>> GetBooks();
        Task<IEnumerable<BookWithPublisherAndAuthorsModel>> GetBooksWithPublisherAndAuthors();
        Task InsertBook(InsertBookDto book);

        Task InsertManyTestBooks();
        Task<IEnumerable<BookModel>> SearchBooks(string title, DateTime? addedBefore);
        Task InsertBookWithDp(InsertBookDto book);
    }
}