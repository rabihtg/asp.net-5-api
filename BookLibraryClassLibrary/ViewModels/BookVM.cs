using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibraryClassLibrary.ViewModels
{
    public class BookVM
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public Guid? PublisherId { get; set; }
    }
    public class InsertBookVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid? PublisherId { get; set; }
        public List<Guid> AuthorIds { get; set; } = new();
    }
    public class UpdateBookVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid? PublisherId { get; set; }
        public List<Guid> AuthorIds { get; set; } = new();
    }

}
