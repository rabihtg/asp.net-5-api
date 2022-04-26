using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibraryClassLibrary.Models
{
    public class PublisherModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }

    }
    public class PublisherWithBooksModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public List<string> BookTitles { get; set; } = new();
    }
}
