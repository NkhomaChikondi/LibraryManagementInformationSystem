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

            try
            {
                if (!_unitOfWork.User.IsValidEmail(createUserDTO.Email))
                {
                    ModelState.AddModelError(nameof(createUserDTO.Email), $"Invalid email format");
                    return BadRequest(ModelState);
                }

                // Check if the role exists
                Expression<Func<Role, bool>> roleExistsExpression = role => role.RoleName == createUserDTO.RoleName;
                if (!await _unitOfWork.Role.ExistsAsync(roleExistsExpression))
                {
                    ModelState.AddModelError(nameof(createUserDTO.RoleName), $"The role {createUserDTO.RoleName} does not exist in the system");
                    return BadRequest(ModelState);
                }

                // Check if the email already exists
                Expression<Func<ApplicationUser, bool>> userEmailExistsExpression = userEmail => userEmail.Email == createUserDTO.Email;
                if (await _unitOfWork.User.ExistsAsync(userEmailExistsExpression))
                {
                    ModelState.AddModelError(nameof(createUserDTO.Email), $"This email already exists in the system");
                    return BadRequest(ModelState);
                }
                //generate password
                var password = _unitOfWork.User.GeneratePassword(user);
                // Hash the password
                user.Password = _unitOfWork.User.HashPassword(password);

                // Generate and assign the PIN
                user.Pin = _unitOfWork.User.GeneratePin();

                await _unitOfWork.User.CreateAsync(user);
                _unitOfWork.Save();

                //  get all roles
                var roles =  _unitOfWork.Role.GetAllAsync();
                // get a speficic rolle
                var role = await _unitOfWork.Role.GetFirstOrDefaultAsync(role => role.RoleName == createUserDTO.RoleName);

                // create a new userRole
                var userRole = new UserRole
                {
                    User = user,
                    Role = role
                };
                await _unitOfWork.UserRole.CreateAsync(userRole);
                _unitOfWork.Save();

                var createdUserDTO = _mapper.Map<ApplicationUserDTO>(user);
                return Ok(createdUserDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An {ex.Message} occurred while creating the user.");
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
