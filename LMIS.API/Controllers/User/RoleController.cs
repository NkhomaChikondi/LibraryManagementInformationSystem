using AutoMapper;
using LMIS.Api.Core.DTOs;
using LMIS.Api.Core.Model;
using LMIS.Api.Core.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LMIS.API.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RoleController(IUnitOfWork unitOfWork, IMapper Mapper)
        {
            _mapper = Mapper;
            _unitOfWork = unitOfWork;
        }
        // GET: api/<UserController>
        [HttpGet("GetAllAsync")]
        public IActionResult Get()
        {
            var allRoles = _unitOfWork.Role.GetAllAsync();
            return Ok(allRoles);
        }

        [HttpGet("GetRoleById{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var role = await _unitOfWork.Role.GetByIdAsync(id);
            return Ok(role);
        }


        // POST api/<RoleController>
        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole([FromBody] RoleDTO createRoleDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var role = _mapper.Map<Role>(createRoleDTO);
            try
            {
                // Check if the role exists
                Expression<Func<Role, bool>> roleExistsExpression = role => role.RoleName == createRoleDTO.RoleName;
                if (await _unitOfWork.Role.ExistsAsync(roleExistsExpression))
                {
                    ModelState.AddModelError(nameof(createRoleDTO.RoleName), $"The role {createRoleDTO.RoleName} already exist in the system");
                    return BadRequest(ModelState);
                }
                // create a new role
                await _unitOfWork.Role.CreateAsync(role);
                _unitOfWork.Save();
                var dtoRole = _mapper.Map<RoleDTO>(role);
                return Ok(dtoRole);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"An {ex.Message} occurred while creating the user.");
            }
        }

        // PUT api/<RoleController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] string value)
        {
            // get role by id
            var role = await _unitOfWork.Role.GetByIdAsync(id);
            // check if role is null
            if (role != null)
            {
                role.RoleName = value.Trim();

                //update the role record
                _unitOfWork.Role.Update(role);
                // save in the database
                _unitOfWork.Save();

                // return mapped result
                return Ok(_mapper.Map<RoleDTO>(role));
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        // DELETE api/<RoleController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            //find a role given the id
            await _unitOfWork.Role.DeleteAsync(id);
            // save changes
            _unitOfWork.Save();
            return Ok();
        }
    }
}
