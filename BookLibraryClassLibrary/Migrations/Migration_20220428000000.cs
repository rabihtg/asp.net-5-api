using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibraryClassLibrary.Migrations
{
    [Migration(20220428000000, "InitialCreate")]
    public class Migration_20220428000000 : Migration
    {
        public override void Up()
        {
            Create.Table(TableNames.PublisherTable)
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("FullName").AsString().NotNullable();

            Create.Table(TableNames.AuthorTable)
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("FullName").AsString().NotNullable();

            Create.Table(TableNames.BookTable)
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Title").AsString().NotNullable()
                .WithColumn("Description").AsString().Nullable()
                .WithColumn("DateAdded").AsDateTime().NotNullable()
                .WithColumn("PublisherId").AsGuid().ForeignKey(TableNames.PublisherTable, "Id").OnDeleteOrUpdate(Rule.Cascade).Nullable();

            Create.Table(TableNames.UserTable)
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("UserName").AsString(200).NotNullable()
                .WithColumn("Password").AsString(200).NotNullable();

            Create.Index("IX_Book_Publisher")
                .OnTable(TableNames.BookTable)
                .OnColumn("PublisherId")
                .Ascending()
                .WithOptions().NonClustered();


            Create.Table(TableNames.Book_AuthorTable)
                .WithColumn("Id").AsInt32().NotNullable().Identity()
                .WithColumn("AuthorId").AsGuid().NotNullable().ForeignKey(TableNames.AuthorTable, "Id").OnDeleteOrUpdate(Rule.Cascade)
                .WithColumn("BookId").AsGuid().NotNullable().ForeignKey(TableNames.BookTable, "Id").OnDeleteOrUpdate(Rule.Cascade);

            Create.PrimaryKey().OnTable(TableNames.Book_AuthorTable)
                .Columns(new string[] { "AuthorId", "BookId" });

        }

        public override void Down()
        {
            Delete.Index("IX_Book_Publisher")
                .OnTable(TableNames.BookTable);


            Delete.Table(TableNames.Book_AuthorTable);
            Delete.Table(TableNames.BookTable);
            Delete.Table(TableNames.PublisherTable);
            Delete.Table(TableNames.AuthorTable);
            Delete.Table(TableNames.UserTable);

        }
    }
}
