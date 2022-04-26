using BookLibraryClassLibrary.DataAccess;
using BookLibraryClassLibrary.DTO;
using BookLibraryClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibraryClassLibrary.Data
{
    public class BookData : IBookData
    {

        private readonly ISqlDataAccess _db;

        public BookData(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<IEnumerable<BookModel>> GetBooks()
        {
            return _db.LoadData<BookModel, dynamic>("dbo.spBook_GetAll", new { });
        }

        public Task<IEnumerable<BookModel>> SearchBooks(string title, DateTime? addedBefore)
        {
            return _db.LoadData<BookModel, dynamic>("dbo.spBook_Search", new { Title = title, DateAdded = addedBefore });
        }

        public async Task<IEnumerable<BookWithPublisherAndAuthorsModel>> GetBooksWithPublisherAndAuthors()
        {
            var result = await _db.LoadWithTwoRelations<BookWithPublisherAndAuthorsModel, PublisherModel, AuthorModel, dynamic>
                ("dbo.spBookWithPublisherAndAuthors_GetAll",
                        MapBookPublisherAuthors, new { }, splitCol: "FullName, FullName");

            return result.GroupBy(e => e.Id).Select(g =>
            {
                var gB = g.First();
                gB.AuthorNames = g.Select(b => b.AuthorNames.FirstOrDefault()).ToList();
                return gB;
            });
        }

        public async Task<BookModel> GetBook(Guid id)
        {
            return (await _db.LoadData<BookModel, dynamic>("dbo.spBook_Get", new { Id = id })).FirstOrDefault();
        }

        public Task InsertBook(InsertBookDto book)
        {
            return _db.InsertBookTrans("dbo.spBook_Insert", "dbo.spBook_Author_Insert", book);
        }

        public Task InsertBookWithDp(InsertBookDto book)
        {
            return _db.InsertBookTransDynamicParams(book);
        }

        /*public Task UdpateBook(BookWithPublisherAndAuthorsModel book)
        {
            throw new NotImplementedException();
        }*/

        public Task DeleteBook(Guid id)
        {
            return _db.SaveData("dbo.spBook_Delete", new { Id = id });
        }

        private static BookWithPublisherAndAuthorsModel MapBookPublisherAuthors(BookWithPublisherAndAuthorsModel book, PublisherModel publisher, AuthorModel author)
        {
            book.PublisherName = publisher?.FullName;
            book.AuthorNames.Add(author?.FullName);
            return book;
        }
        public async Task InsertManyTestBooks()
        {
            var authorAvailableIds = new Guid[]
            {
                Guid.Parse("C35A8621-0DCD-406F-812F-409A67702DBD"),
                Guid.Parse("ED1DAC1D-B96D-49F9-A0D9-971A09BA8A80"),
                Guid.Parse("5B2AFB5E-1C27-41D6-B049-D753CD495964"),
                Guid.Parse("4E77FDAD-8BAC-4758-8F33-E626156AC085"),
                Guid.Parse("5CFEF7DF-D1F3-47C2-9713-FB424152E2DA"),
                Guid.Parse("D6F71200-C549-42F1-A135-FBE963CC9861")
            };

            var publisherAvailableIds = new Guid[]
            {
                Guid.Parse("0038CB44-3C8B-42CF-AD6D-25D75F4F67CD"),
                Guid.Parse("DF43F7A0-6FBD-431A-8CCD-3E96A66412A4"),
                Guid.Parse("12F019BC-E42A-4685-8B3D-93AFBA272E40"),
                Guid.Parse("8CD2FFF7-2D6F-4568-B849-97DE5FF4A573"),
                Guid.Parse("AF3CDCCF-FC86-4020-B09D-B9E5E39523BC"),
                Guid.Parse("954770CF-89B4-4635-9225-F83D2A7BF986")
            };


            var randomKeywords = new string[] { "Good Practices", "Design Patterns", "Programming 101", "Intro To", "Advanced", "Notes For Professionals", "Happy Life", "Life Hacks" };

            for (int i = 0; i < 10000; i++)
            {
                Random randGen = new();

                int publisherIndex = randGen.Next(0, 6);
                int randAmountVal = randGen.Next(0, 4);

                var authorIds = new Guid[randAmountVal];
                var x = 0;

                while (x < randAmountVal)
                {
                    var authorIndex = randGen.Next(0, 6);
                    var val = authorAvailableIds[authorIndex];
                    if (!authorIds.Contains(val))
                    {
                        authorIds[x] = authorAvailableIds[authorIndex];
                        x++;
                    }
                }
                //for (int j = 0; j < randAmountVal; j++)
                //{

                //}

                var bookDto = new InsertBookDto
                {
                    Id = Guid.NewGuid(),
                    Title = $"{randomKeywords[randGen.Next(0, 8)]} Book Title",
                    Description = $"{randomKeywords[randGen.Next(0, 8)]} Book Description",
                    PublisherId = publisherAvailableIds[publisherIndex],
                    AuthorIds = authorIds.ToList(),
                    DateAdded = DateTime.Now.AddDays(-1 * randGen.Next(90, 200))
                };
                await _db.InsertBookTrans("dbo.spBook_Insert", "dbo.spBook_Author_Insert", bookDto);
            }
        }

    }
}
