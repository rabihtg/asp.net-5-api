using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibraryClassLibrary.DTO
{
    public record BookDto
    {

    }
    public record InsertBookDto
    {
        public Guid Id { get; set; }
        public Guid? PublisherId { get; set; }

        [Required()]
        [MinLength(3)]
        [MaxLength(50)]
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DateAdded { get; set; }

        public List<Guid> AuthorIds { get; set; } = new();
    }
    public record UpdateBookDto
    {
        public Guid? PublisherId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Guid> AuthorIds { get; set; } = new();

    }
}
