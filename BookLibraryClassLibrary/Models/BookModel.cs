using BookLibraryClassLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibraryClassLibrary.Models
{
    public class BookModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public Guid? PublisherId { get; set; }
    }
    public class BookWithPublisherAndAuthorsModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> AuthorNames { get; set; } = new();
        public string PublisherName { get; set; }
    }
}
