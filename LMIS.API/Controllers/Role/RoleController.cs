using LMIS.Api.Core.DTOs.Role;
using LMIS.Api.Services.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LMIS.API.Controllers.Role
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IConfiguration configuration, IRoleService roleService)
        {
            _roleService = roleService;
        }
        // GET: api/<GenreController>
        [HttpGet("GetAllAsync")]
        public IActionResult Get()
        {
            try
            {
                var response = _roleService.GetAllRoles();
                if (response == null)
                {
                    return BadRequest("Failed to get all roles");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An {ex.Message} occurred while getting roles");
            }
        }

        // GET api/<GenreController>/5
        [HttpGet("GetRoleById/{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            try
            {
                var response = await _roleService.GetRoleByIdAsync(id);
                if (response == null)
                {
                    return BadRequest("Failed to get role");
                }
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        // POST api/<GenreController>
        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole([FromBody] RoleDTO role)
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

                var response = await _roleService.CreateRole(role, userIdClaim);

                if (response == null)
                {
                    return BadRequest("Failed to create role");
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An {ex.Message} occurred while creating a role");
            }
        }

        // PUT api/<GenreController>/5
        [HttpPut("UpdateRole/{roleId}")]
        public async Task<IActionResult> UpdateGenre(int roleId, [FromBody] RoleDTO role)
        {
            try
            {
                var response = await _roleService.UpdateRoleAsync(role, roleId);
                if (response == null)
                    return BadRequest("failed to update role");
                return Ok(response);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"An {ex.Message} occurred while updating the role.");
            }

        }

        // DELETE api/<GenreController>/5
        [HttpDelete("DeleteRole/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _roleService.DeleteRoleAsync(id);
                return Ok("Deleted Successfully");
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"An {ex.Message} occurred while updating the genre.");
            }

        }
    }

}

