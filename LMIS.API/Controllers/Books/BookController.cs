using LMIS.Api.Core.Model;
using LMIS.Api.Services.Services;
using LMIS.Api.Services.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LMIS.API.Controllers.Books
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _booksService;

        public BooksController(IBookService booksService) =>
            _booksService = booksService;

        [HttpGet("GetAllBooks")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var response = await _booksService.GetAllAsync();
                if (response == null)
                {
                    return BadRequest("Failed to get all book");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"An {ex.Message} occurred while getting books.");
            }
           
        }
          

        [HttpGet("GetBook{id:length(24)}")]
        public async Task<ActionResult<Book>> Get(string id)
        {

            var book = await _booksService.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }
            return book;
        }

        [HttpPost("CreateBook")]
        public async Task<IActionResult> CreateBook(Book newBook)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (userIdClaim == null)
                {
                    return Unauthorized("You are not authorized to create member");
                }

                await _booksService.CreateAsync(newBook, userIdClaim);

                return CreatedAtAction(nameof(Get), new { id = newBook.ISBN }, newBook);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"An {ex.Message} occurred while creating the book.");
            }            
        }

        [HttpPut("Update{id:length(24)}")]
        public async Task<IActionResult> UpdateBook(string id, Book updatedBook)
        {
            try
            {
                var book = await _booksService.GetAsync(id);

                if (book is null)
                {
                    return NotFound();
                }

                updatedBook.ISBN = book.ISBN;

                await _booksService.UpdateAsync(id, updatedBook);
                return Ok();
            }
            catch (Exception)
            {

                return NoContent();
            }
          
        }

        [HttpDelete("Delete{id:length(24)}")]
        public async Task<IActionResult> DeleteBook(string id)
        {
            var book = await _booksService.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }

            await _booksService.RemoveAsync(id);

            return NoContent();
        }
    }
}
