using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibraryClassLibrary.Migrations
{
    public struct TableNames
    {
        public static string BookTable { get; } = "Book";

        public static string AuthorTable { get; } = "Author";

        public static string PublisherTable { get; } = "Publisher";

        public static string Book_AuthorTable { get; } = "Book_Author";

        public static string UserTable { get; } = "User";

    }
}
