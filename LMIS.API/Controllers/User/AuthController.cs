using AutoMapper;
using LMIS.Api.Core.DTOs.User;
using LMIS.Api.Core.Model;
using LMIS.Api.Core.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LMIS.API.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;       
        private readonly IMapper _mapper;

        public IConfiguration _configuration { get; set; }

        public AuthController(IUnitOfWork unitOfWork, IMapper Mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = Mapper;
            _configuration = configuration;             
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDTO loginModel)
        {
            // get user from the database
            var user = await _unitOfWork.User.GetFirstOrDefaultAsync(user => user.Email == loginModel.Email);

            if (user == null || user.IsDeleted)
                return null;

            if (user != null)
            {
                // checking is the account is confirmed already
                if (user.IsConfirmed == false)
                {
                    return BadRequest("Account not confirmed, please check your email for the activation link");
                }
                // hash the password

             //  loginModel.Password = _unitOfWork.User.HashPassword(loginModel.Password);
                
                // check if the password match
                var isMatched = _unitOfWork.User.VerifyPassword(user.Password, loginModel.Password);

                if(isMatched)
                {
                    // generate token
                    var tokenHandler = new JwtSecurityToken();
                    LoginTokenDTO userData = await _unitOfWork.User.GenerateToken(user,_configuration,_unitOfWork);
                    _unitOfWork.Save();
                    return Ok(new { TokenData = userData });
                }
               
                else
                    return BadRequest("Login Credentials do not match");
            }
            return BadRequest("Account not found");
        }


        
    }
}
