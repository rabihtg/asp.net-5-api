using BookLibraryClassLibrary.Data;
using BookLibraryClassLibrary.Models;
using BookLibraryClassLibrary.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibraryApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize("AtLeast18")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorData _data;

        public AuthorController(IAuthorData data)
        {
            _data = data;
        }

        [HttpPost]
        public async Task<IActionResult> InsertAuthor(InsertAuthorVM authorVM)
        {

            var author = new AuthorModel
            {
                Id = Guid.NewGuid(),
                FullName = authorVM.FullName
            };
            await _data.InsertAuthor(author);
            return Ok(author);

        }

        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {

            var result = await _data.GetAuthors();
            return Ok(result);

        }

        [HttpGet("with-books")]
        public async Task<IActionResult> GetAuthorsWithBooks()
        {
            var result = await _data.GetAuthorWithBooks();
            return Ok(result);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetAuthor(Guid id)
        {
            var result = await _data.GetAuthor(id);
            return Ok(result);
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdateAuthor(Guid id, UpdateAuthorVM authorVM)
        {

            var author = new AuthorModel
            {
                Id = id,
                FullName = authorVM.FullName
            };
            await _data.UpdateAuthor(author);
            return Ok();

        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteAuthor(Guid id)
        {
            await _data.DeleteAuthor(id);
            return Ok();
        }
    }
}
