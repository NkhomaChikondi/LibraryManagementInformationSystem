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

        [HttpGet("GetAllTransactions")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var response = await _checkoutService.GetAllCheckoutTransactions();
                if (response.IsError)
                {
                    return BadRequest(response.Message);
                }
                return Ok(response.Result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An {ex.Message} occurred while getting all transactions.");
            }

        }

        [HttpGet("GetUserById{id}")]
        public async Task<IActionResult> GetTransactionById(int id)
        {
            try
            {
                var response = await _checkoutService.GetCheckoutTransactionByIdAsync(id);
                if(response.IsError)
                    return BadRequest(response.Message);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An {ex.Message} occurred while getting a transaction.");
            }

        }
        [HttpDelete("Delete{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _checkoutService.DeleteTransactionAsync(id);
                return Ok("Deleted Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An {ex.Message} occurred while updating the user.");
            }
        }

        // GET: api/<CheckoutController>
        [HttpPost("CheckoutBook")]
        public async Task<IActionResult> Get(string memberCode, [FromBody] SearchBookDTO selectedBook)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (userIdClaim == null)
                {
                    return Unauthorized("You are not authorized to create member");
                }
                var response = await _checkoutService.CheckoutBook(selectedBook, memberCode, userIdClaim);
                if (response.IsError)
                {
                    return BadRequest(response.Message);
                }

                this.memberCode = memberCode;
                
                book = selectedBook;

                return Ok(response.Result);
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while getting books: {ex.Message}");
            }
        }

        [HttpPost("returnBook")]
        public async Task<IActionResult> ReturnBook([FromBody] ReturnBookDTO selectedBook)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (userIdClaim == null)
                {
                    return Unauthorized("You are not authorized to create member");
                }

                var response = await _checkoutService.ReturnBook(selectedBook, userIdClaim);
                if (response.IsError)
                {
                    return BadRequest(response.Message);
                }
                
                return Ok(response.Result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while getting books: {ex.Message}");
            }
        }        

    }
}
