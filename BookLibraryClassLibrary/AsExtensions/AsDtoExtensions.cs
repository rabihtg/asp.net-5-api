using BookLibraryClassLibrary.DTO;
using BookLibraryClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibraryClassLibrary.AsExtensions
{
    public static class AsDtoExtensions
    {
        public static InsertBookDto AsInsertDto(this BookModel book, List<Guid> authorIds)
        {
            return new InsertBookDto
            {
                Id = book.Id,
                DateAdded = book.DateAdded,
                Title = book.Title,
                Description = book.Description,
                PublisherId = book.PublisherId,
                AuthorIds = authorIds

            };
        }
    }
}
