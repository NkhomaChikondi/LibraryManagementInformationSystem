using LMIS.Api.Core.DTOs.Book;
using LMIS.Api.Core.DTOs.Checkout;
using LMIS.Api.Core.DTOs.Member;
using LMIS.Api.Core.Model;
using LMIS.Api.Services.Services;
using LMIS.Api.Services.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LMIS.API.Controllers.CheckoutController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly ICheckoutService _checkoutService;
        private string memberCode;
       
        private SearchBookDTO book;


        public CheckoutController(ICheckoutService checkoutService) =>
            _checkoutService = checkoutService;

        // GET: api/<CheckoutController>
        [HttpPost("GetSerachedBooks")]
        public async Task<ActionResult<List<BookDTO>>> Get(string memberCode, [FromBody] SearchBookDTO selectedBook)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (userIdClaim == null)
                {
                    return Unauthorized("You are not authorized to create member");
                }
                var response = await _checkoutService.GetSelectedBooks(selectedBook, memberCode, userIdClaim);
                if (response == null)
                {
                    return BadRequest("Failed to get books");
                }

                this.memberCode = memberCode;
                
                book = selectedBook;

                return Ok(response);
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while getting books: {ex.Message}");
            }
        }

        // GET api/<CheckoutController>/5
        [HttpPost("CheckoutTransaction{id}")]
        public async Task<IActionResult> CreateCheckoutTransaction([FromBody] CheckoutDTO createCheckoutTransactionDTO)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (userIdClaim == null)
                {
                    return Unauthorized("You are not authorized to create member");
                }

                var response =  _checkoutService.CheckOutBook(book, memberCode, userIdClaim);

                if (response == null)
                {
                    return BadRequest("Failed to create member");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An {ex.Message} occurred while creating a member");
            }
        }

        // POST api/<CheckoutController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CheckoutController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CheckoutController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
