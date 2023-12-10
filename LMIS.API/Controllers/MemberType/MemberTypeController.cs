using LMIS.Api.Core.DTOs.Genre;
using LMIS.Api.Core.DTOs.Member;
using LMIS.Api.Core.Repository.IRepository;
using LMIS.Api.Services.Services;
using LMIS.Api.Services.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using System.Security.Claims;

namespace LMIS.API.Controllers.MemberType
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberTypeController : ControllerBase
    {
        private readonly IMemberTypeService _memberType;

        public MemberTypeController(IConfiguration configuration, IMemberTypeService memberType)
        {
            _memberType = memberType;
        }
        // GET: api/<GenreController>
        [HttpGet("GetAllAsync")]
        public IActionResult Get()
        {
            try
            {
                var response = _memberType.GetAllMembersTypes();
                if (response == null)
                {
                    return BadRequest("Failed to get all member type");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An {ex.Message} occurred while getting member type");
            }
        }

        // GET api/<GenreController>/5
        [HttpGet("GetMemberTypeById{id}")]
        public async Task<IActionResult> GetMemberById(int id)
        {
            try
            {
                var response = await _memberType.GetMemberTypeByIdAsync(id);
                if (response == null)
                {
                    return BadRequest("Failed to get member t");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        // POST api/<GenreController>
        [HttpPost("CreateMemberType")]
        public async Task<IActionResult> CreateMemberType([FromBody] MemberTypeDTO memberTypeDTO)
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
                    return Unauthorized("You are not authorized to create member type");
                }

                var response = await _memberType.CreateMemberTypeAsync(memberTypeDTO, userIdClaim);

                if (response == null)
                {
                    return BadRequest("Failed to create member type");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An {ex.Message} occurred while creating a member type");
            }
        }

        // PUT api/<GenreController>/5
        [HttpPut("Update{memberTypeId}")]
        public async Task<IActionResult> UpdateMemberType(int memberTypeId, [FromBody] MemberTypeDTO memberDTO)
        {
            try
            {
                var response = await _memberType.UpdateMemberAsync(memberDTO, memberTypeId);
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

        // DELETE api/<GenreController>/5
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var response = await _memberType.DeleteMemberTypeAsync(id);
                if (response.IsError)
                {
                    return BadRequest(response.Message);
                }
                return Ok(response.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An {ex.Message} occurred while updating the member type.");
            }
        }
    }
}
