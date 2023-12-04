using LMIS.Api.Core.DTOs.Book;
using LMIS.Api.Services.Services.IServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LMIS.API.Controllers.CheckoutController
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly ICheckoutService _checkoutService;

        public CheckoutController(ICheckoutService checkoutService) =>
            _checkoutService = checkoutService;
        // GET: api/<CheckoutController>
        [HttpPost("GetSerachedBooks")]
        public async Task<ActionResult<List<BookDTO>>> Get(string memberCode, [FromBody] SearchBookDTO selectedBooks)
        {
            try
            {
                var response = await _checkoutService.GetSelectedBooks(selectedBooks, memberCode);
                if (response == null)
                {
                    return BadRequest("Failed to get books");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while getting books: {ex.Message}");
            }
        }

        // GET api/<CheckoutController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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
