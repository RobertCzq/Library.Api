using Library.Api.Infrastructure.Models;
using Library.Api.Models;
using Library.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult> Get([FromServices] IBooksService booksService, int id)
        {
            var book = await booksService.Get(id);
            if (book != null)
            {
                return Ok(book);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> AddBook([FromServices] IBooksService booksService,
            [FromBody] BookInputModel book)
        {
            var added = await booksService.Add(new Book()
            {
                Title = book.Title,
                Author = book.Author,
                PublicationYear = book.PublicationYear,
                IsAvailable = true
            });
            if (added)
            {
                return Created("", book);
            }

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromServices] IBooksService booksService,
            int id,
            [FromBody] BookInputUpdateModel book)
        {
            var updated = await booksService.Update(id, new Book()
            {
                Title = book.Title,
                Author = book.Author,
                PublicationYear = book.PublicationYear,
                IsAvailable = book.IsAvailable
            });

            if (updated)
            {
                return Ok(book);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromServices] IBooksService booksService, int id)
        {
            var deleted = await booksService.Delete(id);

            if (deleted)
            {
                return Ok(id);
            }

            return NoContent();
        }
    }
}
