using AutoMapper;
using LMIS.Api.Core.DTOs.Member;
using LMIS.Api.Core.DTOs.User;
using LMIS.Api.Core.Model;
using LMIS.Api.Core.Repository.IRepository;
using LMIS.Api.Core.Services;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMemberService _memberService;
       
        public IConfiguration _configuration { get; }
        public MemberController(IUnitOfWork unitOfWork, IMapper Mapper, IConfiguration configuration, IMemberService memberService)
        {
            _mapper = Mapper;
            _unitOfWork = unitOfWork;            
            _configuration = configuration;
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
        public IActionResult GetMemberById(int id)
        {
            try
            {
                var response = _memberService.GetMemberByIdAsync(id);
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
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MemberController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
