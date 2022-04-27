using BookLibraryClassLibrary.Data;
using BookLibraryClassLibrary.DTO;
using BookLibraryClassLibrary.Models;
using BookLibraryClassLibrary.Paging;
using BookLibraryClassLibrary.ViewModels;
using BookLibraryClassLibrary.ModelResponseWrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookData _data;
        private readonly ILogger<BookController> _logger;

        public BookController(IBookData data, ILogger<BookController> logger)
        {
            _data = data;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> InsertBook(InsertBookVM bookVM)
        {
            var bookDto = new InsertBookDto
            {
                Id = Guid.NewGuid(),
                Title = bookVM.Title,
                Description = bookVM.Description,
                PublisherId = bookVM.PublisherId,
                AuthorIds = bookVM.AuthorIds,
                DateAdded = DateTime.Now
            };

            await _data.InsertBook(bookDto);
            return Ok();
        }

        [HttpPost("with-dp")]
        public async Task<IActionResult> InsertBookWithDp(InsertBookVM bookVM)
        {
            var bookDto = new InsertBookDto
            {
                Id = Guid.NewGuid(),
                Title = bookVM.Title,
                Description = bookVM.Description,
                PublisherId = bookVM.PublisherId,
                AuthorIds = bookVM.AuthorIds,
                DateAdded = DateTime.Now
            };

            await _data.InsertBookWithDp(bookDto);
            return Ok();
        }

        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<IActionResult> SearchBooks(string title, DateTime? addedBefore, int? pageIndex)
        {

            var result = await _data.SearchBooks(title, addedBefore);
            var bookPage = PaginatedList<BookModel>.Create(result.AsQueryable(), pageIndex, 100);
            return Ok(new ExtraInfoWrapper<BookModel>(bookPage));

        }

        [HttpGet]
        public async Task<IActionResult> GetBooks(int? pageIndex)
        {
            var result = await _data.GetBooks();
            PaginatedList<BookModel> booksPage = PaginatedList<BookModel>.Create(result.AsQueryable(), pageIndex);

            _logger.LogInformation("From book controller Request Handle Succesfuly");

            return Ok(new ExtraInfoWrapper<BookModel>(booksPage));
        }

        [HttpGet("with-authors-and-publisher")]
        public async Task<IActionResult> GetBooksWithAuthorsAndPublisher(int? pageIndex)
        {
            var result = await _data.GetBooksWithPublisherAndAuthors();

            PaginatedList<BookWithPublisherAndAuthorsModel> booksPage = PaginatedList<BookWithPublisherAndAuthorsModel>.Create(result.AsQueryable(), pageIndex);

            return Ok(booksPage);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetBook(Guid id)
        {
            var result = await _data.GetBook(id);
            return Ok(result);
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateBook(Guid id, UpdateBookVM bookVM)
        {
            await _data.GetBooks();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            await _data.DeleteBook(id);
            return Ok();
        }

    }
}
