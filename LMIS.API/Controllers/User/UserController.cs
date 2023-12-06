using AutoMapper;
using LMIS.Api.Core.DTOs;
using LMIS.Api.Core.DTOs.Book;
using LMIS.Api.Core.DTOs.User;
using LMIS.Api.Core.Model;
using LMIS.Api.Core.Repository.IRepository;
using LMIS.Api.Services.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq.Expressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LMIS.API.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
       
        public UserController(IUserService userService)
        {
           _userService = userService;
        }
        // GET: api/<UserController>
        [HttpGet("GetAllAsync")]
        public async Task< IActionResult> Get()
        {
            try
            {
                var response = await _userService.GetAllUsers();
                if (response.IsError)
                {
                    return BadRequest(response.Message);
                }
                return Ok(response.Result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An {ex.Message} occurred while getting all users.");
            }
          
        }
        
        // GET api/<UserController>/5
        [HttpGet("GetUserById{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var response = await _userService.GetUserByIdAsync(id);
                if (response == null)
                    return BadRequest("Failed to get user");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An {ex.Message} occurred while getting a user.");
            }
        
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] ApplicationUserDTO createUserDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }          

            try
            {
               var response = await _userService.CreateUserAsync(createUserDTO);

                if (response.IsError)
                {
                    return BadRequest(response.Message);
                }
                    
                return Ok(response.Result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An {ex.Message} occurred while creating the user.");
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("Update{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] ApplicationUserDTO updateUserDto)
        {
            try
            {
                await _userService.UpdateUserAsync(updateUserDto, userId);
                return Ok("Updated successfully");
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"An {ex.Message} occurred while updating the user.");
            }
        }

        // DELETE api/<UserController>/5       
        [HttpDelete("Delete{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
                return Ok("Deleted Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An {ex.Message} occurred while updating the user.");
            }        
        }

        [HttpGet]
        [Route("ConfirmAccount/{email}/{pin}")]
        public async Task<IActionResult> ConfirmAccount(string email, int pin)
        {
            try
            {
                await _userService.ConfirmAccount(email, pin);
                return Ok(new { response = "Account confirmed" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An {ex.Message} occurred while confirming the user.");
            }
           
        }

        [HttpGet]
        [Route("ResendPin/{email}")]
        // Resending account activation pin
        public async Task<IActionResult> ResendPin(string email)
        {
            try
            {
                await _userService.ResendEmail(email);
                return Ok("Check your email for the pin");
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"An {ex.Message} occurred while resending the email.");
            }          

        }

        //// GET: api/<UserController>
        //[HttpGet("GetAlUsersWithRolesAsync")]
        //public Task<ActionResult<List<BookDTO>>> GetAlUsersWithRolesAsync()
        //{
        //    var allUsers = _userService.
        //    // map to dto
        //    List<UserWithRoleDTO> usersWithRolesDTO = _mapper.Map<IEnumerable<ApplicationUser>, List<UserWithRoleDTO>>(allUsers);
        //    return Ok(usersWithRolesDTO);
        //}




    }
}
