using BookLibraryClassLibrary.DataAccess;
using BookLibraryClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibraryClassLibrary.Data
{
    public class PublisherData : IPublisherData
    {
        private readonly ISqlDataAccess _db;

        public PublisherData(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<IEnumerable<PublisherModel>> GetPublishers()
        {
            return _db.LoadData<PublisherModel, dynamic>("dbo.spPublisher_GetAll", new { });
        }

        public async Task<IEnumerable<PublisherWithBooksModel>> GetPublishersWithBooks()
        {

            var result = await _db.LoadWithRelation<PublisherWithBooksModel, BookModel, dynamic>("dbo.spPublisherWithBooks_GetAll", MapPublisherBook, new { });
            return result.GroupBy(e => e.Id).Select(g =>
            {
                var gP = g.First();
                gP.BookTitles = g.Select(g => g.BookTitles.FirstOrDefault()).ToList();
                return gP;
            });

        }

        public async Task<PublisherModel> GetPublisher(Guid id)
        {
            return (await _db.LoadData<PublisherModel, dynamic>("dbo.spPublisher_Get", new { Id = id })).FirstOrDefault();
        }

        public async Task<IEnumerable<PublisherWithBooksModel>> GetPublisherWithBooks(int id)
        {
            var result = await _db.LoadWithRelation<PublisherWithBooksModel, BookModel, dynamic>("dbo.spPublisherWithBooks_Get", MapPublisherBook, new { Id = id });
            return result.GroupBy(e => e.Id).Select(g =>
            {
                var gP = g.First();
                gP.BookTitles = g.Select(g => g.BookTitles.FirstOrDefault()).ToList();
                return gP;
            });
        }

        public Task InsertPublisher(PublisherModel publisher)
        {
            return _db.SaveData("dbo.spPublisher_Insert", publisher);
        }

        public Task UpdatePublisher(PublisherModel publisher)
        {
            return _db.SaveData("dbo.spPublisher_Update", publisher);
        }

        public Task DeletePublisher(Guid id)
        {
            return _db.SaveData("dbo.spPublisher_Delete", new { Id = id });
        }

        private static PublisherWithBooksModel MapPublisherBook(PublisherWithBooksModel publisher, BookModel book)
        {

            publisher.BookTitles.Add(book?.Title);
            return publisher;

        }
    }
}
