using BookLibraryClassLibrary.Data;
using BookLibraryClassLibrary.Models;
using BookLibraryClassLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibraryApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublsiherController : ControllerBase
    {
        private readonly IPublisherData _data;

        public PublsiherController(IPublisherData data)
        {
            _data = data;
        }

        [HttpPost]
        public async Task<IActionResult> InsertPublisher(InsertPublisherVM publisherVM)
        {
            var publisher = new PublisherModel { Id = Guid.NewGuid(), FullName = publisherVM.FullName };
            await _data.InsertPublisher(publisher);
            return Ok(publisher);
        }

        [HttpGet]
        public async Task<IActionResult> GetPublishers()
        {
            var result = await _data.GetPublishers();
            return Ok(result);
        }

        [HttpGet("wiht-books")]
        public async Task<IActionResult> GetPublishersWithBooks()
        {
            var result = await _data.GetPublishersWithBooks();
            return Ok(result);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetPublisher(Guid id)
        {
            var result = await _data.GetPublisher(id);
            return Ok(result);
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> UpdatePublisher(Guid id, UpdatePublisherVM publisherVM)
        {
            var publisher = new PublisherModel
            {
                Id = id,
                FullName = publisherVM.FullName
            };
            await _data.UpdatePublisher(publisher);
            return Ok();
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeletePublisher(Guid id)
        {
            await _data.DeletePublisher(id);
            return Ok();
        }
    }
}
