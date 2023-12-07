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

        public MemberController(IConfiguration configuration, IMemberService memberService)
        {

            _memberService = memberService;
        }
        // GET: api/<UserController>
        [HttpGet("GetAllAsync")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var response = await _memberService.GetAllMembers();
                if (response.IsError)
                {
                    return BadRequest(response.Message);
                }
                return Ok(response.Result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An {ex.Message} occurred while getting members");
            }
        }

        // GET api/<MemberController>/5
        [HttpGet("GetMemberById{id}")]
        public async Task<IActionResult> GetMemberById(int id)
        {
            try
            {
                var response = await _memberService.GetMemberByIdAsync(id);
                if (response.IsError)
                {
                    return BadRequest(response.Message);
                }
                return Ok(response.Result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An {ex.Message} occurred while getting the member.");
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

                if (response.IsError)
                {
                    return BadRequest(response.Message);
                }

                return Ok(response.Result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An {ex.Message} occurred while creating a member");
            }
        }

        // PUT api/<MemberController>/5

        [HttpPut("Update{memberId}")]
        public async Task<IActionResult> UpdateMember(int memberId, [FromBody] CreateMemberDto updateMemberDto)
        {
            try
            {
                var response = await _memberService.UpdateMemberAsync(updateMemberDto, memberId);
                if (response.IsError)
                {
                    return BadRequest(response.Message);
                }
                return Ok(response.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An {ex.Message} occurred while updating the member.");
            }
        }
        [HttpDelete("Delete{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var response = await _memberService.DeleteMemberAsync(id);
                if (response.IsError)
                {
                    return BadRequest(response.Message);
                }
                return Ok(response.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An {ex.Message} occurred while updating the member.");
            }
        }

        [HttpGet]
        [Route("ResendEmail/{email}")]
        // Resending account activation pin
        public async Task<IActionResult> ResendPin(string email)
        {
            try
            {
                var response = await _memberService.ResendEmail(email);
                if (response.IsError)
                {
                    return BadRequest(response.Message);
                }
                return Ok(response.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An {ex.Message} occurred while resending the email.");
            }
        }
    }
}
