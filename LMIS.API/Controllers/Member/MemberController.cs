using AutoMapper;
using LMIS.Api.Core.DTOs.Member;
using LMIS.Api.Core.DTOs.User;
using LMIS.Api.Core.Model;
using LMIS.Api.Core.Repository.IRepository;
using LMIS.Api.Services.Services;
using LMIS.Api.Services.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LMIS.API.Controllers.Member
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {        
        private readonly IMemberService _memberService;
       
        public MemberController( IConfiguration configuration, IMemberService memberService)
        {                   
           
            _memberService = memberService;
        }
        // GET: api/<UserController>
        [HttpGet("GetAllAsync")]
        public IActionResult Get()
        {
            try
            {
                var response = _memberService.GetAllMembers();
                if (response == null)
                {
                    return BadRequest("Failed to get all member");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }         
        }

        // GET api/<MemberController>/5
        [HttpGet("GetMemberById{id}")]
        public  async Task<IActionResult> GetMemberById(int id)
        {
            try
            {
                var response = await _memberService.GetMemberByIdAsync(id);
                if (response == null)
                {
                    return BadRequest("Failed to get member");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        // POST api/<MemberController>
        [HttpPost("CreateMember")]        
        public async Task<IActionResult> CreateMember([FromBody] CreateMemberDto createMemberDTO)
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

                var response = await _memberService.CreateMemberAsync(createMemberDTO, userIdClaim);

                if (response == null)
                {
                    return BadRequest("Failed to create member");
                }

                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        // PUT api/<MemberController>/5
      
        [HttpPut("Update{memberId}")]
        public async Task<IActionResult> UpdateMember(int memberId, [FromBody] CreateMemberDto updateMemberDto)
        {
            try
            {
                var response = await _memberService.UpdateMemberAsync(updateMemberDto, memberId);
                if (response == null)
                    return BadRequest("failed to update member");
                return Ok(response);
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
           
        }
        [HttpGet]
        [Route("ResendEmail/{email}")]
        // Resending account activation pin
        public async Task<IActionResult> ResendPin(string email)
        {
            try
            {
                await _memberService.ResendEmail(email);
                return Ok("Check your email for the member code");
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"An {ex.Message} occurred while resending the email.");
            }

        }
        // DELETE api/<MemberController>/5
        [HttpDelete("Delete{id}")]
        public void Delete(int id)
        {
        }
    }
}
