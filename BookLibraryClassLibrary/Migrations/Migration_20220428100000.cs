using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibraryClassLibrary.Migrations
{
    [Migration(20220428100000, "SeedDataBase")]
    public class Migration_20220428100000 : Migration
    {
        public override void Up()
        {
            var pId1 = Guid.NewGuid();
            var pId2 = Guid.NewGuid();
            var pId3 = Guid.NewGuid();
            var pId4 = Guid.NewGuid();
            var pId5 = Guid.NewGuid();

            Insert.IntoTable(TableNames.PublisherTable)
                .Row(new { Id = pId1, FullName = "publisher.Rabih" })
                .Row(new { Id = pId2, FullName = "publisher.Hasan" })
                .Row(new { Id = pId3, FullName = "publisher.Jamal" })
                .Row(new { Id = pId4, FullName = "publisher.Zeina" })
                .Row(new { Id = pId5, FullName = "publisher.Lina" });

            var aId1 = Guid.NewGuid();
            var aId2 = Guid.NewGuid();
            var aId3 = Guid.NewGuid();
            var aId4 = Guid.NewGuid();
            var aId5 = Guid.NewGuid();

            Insert.IntoTable(TableNames.AuthorTable)
                .Row(new { Id = aId1, FullName = "author.Rabih" })
                .Row(new { Id = aId2, FullName = "author.Jamal" })
                .Row(new { Id = aId3, FullName = "author.Zeina" })
                .Row(new { Id = aId4, FullName = "author.Hasan" })
                .Row(new { Id = aId5, FullName = "author.Lina" });

            var bId1 = Guid.NewGuid();
            var bId2 = Guid.NewGuid();
            var bId3 = Guid.NewGuid();
            var bId4 = Guid.NewGuid();
            var bId5 = Guid.NewGuid();
            var bId6 = Guid.NewGuid();
            var bId7 = Guid.NewGuid();
            var bId8 = Guid.NewGuid();
            var bId9 = Guid.NewGuid();
            var bId10 = Guid.NewGuid();

            Insert.IntoTable(TableNames.BookTable)
                .Row(new { Id = bId1, Title = "First Book Title", Description = "First Book Description", DateAdded = DateTime.Now.AddDays(-20), PublisherId = pId1 })
                .Row(new { Id = bId2, Title = "Second Book Title", Description = "Second Book Description", DateAdded = DateTime.Now.AddDays(-35), PublisherId = pId1 })
                .Row(new { Id = bId3, Title = "Third Book Title", Description = "Third Book Description", DateAdded = DateTime.Now.AddDays(-100), PublisherId = pId1 })
                .Row(new { Id = bId4, Title = "Fourth Book Title", Description = "Fourth Book Description", DateAdded = DateTime.Now.AddDays(-78), PublisherId = pId2 })
                .Row(new { Id = bId5, Title = "Fifth Book Title", Description = "Fifth Book Description", DateAdded = DateTime.Now.AddDays(-90), PublisherId = pId2 })
                .Row(new { Id = bId6, Title = "Sixth Book Title", Description = "Sixth Book Description", DateAdded = DateTime.Now.AddDays(-10), PublisherId = pId3 })
                .Row(new { Id = bId7, Title = "Seventh Book Title", Description = "Seventh Book Description", DateAdded = DateTime.Now.AddDays(-5), PublisherId = pId4 })
                .Row(new { Id = bId8, Title = "Eigth Book Title", Description = "Eigth Book Description", DateAdded = DateTime.Now.AddDays(-120), PublisherId = pId5 })
                .Row(new { Id = bId9, Title = "Ninth Book Title", Description = "Ninth Book Description", DateAdded = DateTime.Now.AddDays(-50), PublisherId = pId2 })
                .Row(new { Id = bId10, Title = "Tenth Book Title", Description = "Tenth Book Description", DateAdded = DateTime.Now.AddDays(-300), PublisherId = pId1 });

            Insert.IntoTable(TableNames.Book_AuthorTable)
                .Row(new { BookId = bId1, AuthorId = aId1 })
                .Row(new { BookId = bId1, AuthorId = aId2 })
                .Row(new { BookId = bId1, AuthorId = aId3 })
                .Row(new { BookId = bId2, AuthorId = aId2 })
                .Row(new { BookId = bId2, AuthorId = aId5 })
                .Row(new { BookId = bId3, AuthorId = aId3 })
                .Row(new { BookId = bId3, AuthorId = aId4 })
                .Row(new { BookId = bId4, AuthorId = aId5 })
                .Row(new { BookId = bId5, AuthorId = aId2 })
                .Row(new { BookId = bId6, AuthorId = aId1 })
                .Row(new { BookId = bId7, AuthorId = aId3 })
                .Row(new { BookId = bId8, AuthorId = aId3 })
                .Row(new { BookId = bId9, AuthorId = aId4 })
                .Row(new { BookId = bId9, AuthorId = aId5 })
                .Row(new { BookId = bId9, AuthorId = aId2 });
        }

        public override void Down()
        {
            Delete.FromTable(TableNames.PublisherTable)
                .AllRows();

            Delete.FromTable(TableNames.AuthorTable)
                .AllRows();
        }
    }
}
