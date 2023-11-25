using AutoMapper;
using LMIS.Api.Core.DTOs;
using LMIS.Api.Core.Model;
using LMIS.Api.Core.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LMIS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserController(IUnitOfWork unitOfWork, IMapper Mapper) 
        {
           _mapper = Mapper;
            _unitOfWork = unitOfWork;
        }
        // GET: api/<UserController>
        [HttpGet("GetAllAsync")]
        public IActionResult Get()
        {            
            var allUsers = _unitOfWork.User.GetAllAsync();
            return Ok(allUsers);
        }

        // GET api/<UserController>/5
        [HttpGet("GetUserById{id}")]
        public IActionResult GetUserById(int id)
        {
           var User = _unitOfWork.User.GetByIdAsync(id);
            return Ok(User);
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] ApplicationUserDTO createUserDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = _mapper.Map<ApplicationUser>(createUserDTO);
            // Map the DTO to your entity model or use the provided method in the repository to create a new user                    
            try
            {
                // Create an expression that checks if a role with a specific name exists
                Expression<Func<Role, bool>> roleExistsExpression = role => role.RoleName == "Administrator";
                if(await _unitOfWork.Role.ExistsAsync(roleExistsExpression) == false)
                {
                    ModelState.AddModelError(nameof(createUserDTO.RoleName), $"This Role {createUserDTO.RoleName} doees not exists in the system");

                    return BadRequest(ModelState);
                }
                else 
                {           
                     await _unitOfWork.User.CreateAsync(user);
                    _unitOfWork.Save();

                    var createdUserDTO = _mapper.Map<ApplicationUserDTO>(user); // Mapping ApplicationUser to UserDTO
                    return Ok(createdUserDTO);
                }                             
            }
            catch (Exception ex)
            {
                // Handle exceptions or return an appropriate error response
                return StatusCode(500, "An error occurred while creating the user.");
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
