using BookLibraryClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookLibraryClassLibrary.Data
{
    public interface IPublisherData
    {
        Task DeletePublisher(Guid id);
        Task<PublisherModel> GetPublisher(Guid id);
        Task<IEnumerable<PublisherModel>> GetPublishers();
        Task<IEnumerable<PublisherWithBooksModel>> GetPublishersWithBooks();
        Task<IEnumerable<PublisherWithBooksModel>> GetPublisherWithBooks(int id);
        Task InsertPublisher(PublisherModel publisher);
        Task UpdatePublisher(PublisherModel publisher);
    }
}