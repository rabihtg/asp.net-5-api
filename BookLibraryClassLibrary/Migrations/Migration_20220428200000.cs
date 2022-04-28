using BookLibraryClassLibrary.DTO;
using Dapper;
using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibraryClassLibrary.Migrations
{
    [Migration(20220428200000, "InsertManyRandomBooks")]
    public class Migration_20220428200000 : Migration
    {
        public Guid[] AddedBooksIds = new Guid[10_000];
        public override void Up()
        {
            InsertManyTestBooks();
        }

        public override void Down()
        {
            for (int i = 0; i < AddedBooksIds.Length; i++)
            {
                Delete.FromTable(TableNames.BookTable)
                    .Row(new { Id = AddedBooksIds[i] });
            }
        }

        public void InsertManyTestBooks()
        {
            using var conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=NewGuidBookLibraryDB;Integrated Security=True;Connect Timeout=60;");

            Guid[] authorAvailableIds = conn.Query<Guid>("SELECT Id FROM Author").ToArray();
            Guid[] publisherAvailableIds = conn.Query<Guid>("SELECT Id FROM Publisher").ToArray();


            var randomKeywords = new string[] { "Good Practices", "Design Patterns", "Programming 101", "Intro To", "Advanced", "Notes For Professionals", "Happy Life", "Life Hacks", "Intersting", "Something Boring", "Let's Go To The Mall", "Hello World" };

            for (int i = 0; i < 10_000; i++)
            {
                Random randGen = new();
                int publisherIndex = randGen.Next(0, publisherAvailableIds.Length);
                int randAmountVal = randGen.Next(0, 4);

                var authorIds = new Guid[randAmountVal];
                var x = 0;

                while (x < randAmountVal)
                {
                    var authorIndex = randGen.Next(0, authorAvailableIds.Length);
                    var val = authorAvailableIds[authorIndex];
                    if (!authorIds.Contains(val))
                    {
                        authorIds[x] = authorAvailableIds[authorIndex];
                        x++;
                    }
                }

                var randKeyWord = randomKeywords[randGen.Next(0, 8)];
                var bookId = Guid.NewGuid();

                AddedBooksIds[i] = bookId;

                Insert.IntoTable(TableNames.BookTable).
                    Row(new
                    {
                        Id = bookId,
                        Title = $"{randKeyWord} Book Title",
                        Description = $"{randKeyWord} Book Description",
                        PublisherId = publisherAvailableIds[publisherIndex],
                        DateAdded = DateTime.Now.AddDays(-1 * randGen.Next(90, 200))
                    });
                foreach (var id in authorIds)
                {
                    Insert.IntoTable(TableNames.Book_AuthorTable)
                    .Row(new { AuthorId = id, BookId = bookId });
                }

            }
        }
    }
}
