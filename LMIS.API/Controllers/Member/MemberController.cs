using AutoMapper;
using LMIS.Api.Core.DTOs;
using LMIS.Api.Core.Repository.IRepository;
using LMIS.Api.Services.Services.IServices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LMIS.API.Controllers.Member
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
       
        public IConfiguration _configuration { get; }
        public MemberController(IUnitOfWork unitOfWork, IMapper Mapper, IConfiguration configuration)
        {
            _mapper = Mapper;
            _unitOfWork = unitOfWork;            
            _configuration = configuration;
        }
        // GET: api/<UserController>
        [HttpGet("GetAllAsync")]
        public IActionResult Get()
        {
            var allMembers = _unitOfWork.member.GetAllAsync();
            return Ok(allMembers);
        }

        // GET api/<MemberController>/5
        [HttpGet("GetMemberById{id}")]
        public IActionResult GetMemberById(int id)
        {
            var member = _unitOfWork.member.GetByIdAsync(id);
            return Ok(member);
        }

        // POST api/<MemberController>
        [HttpPost("CreateMember")]
        public async Task<IActionResult> CreateMember([FromBody] MemberDTO createMemberDTO)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            // map the dto to member
            var member = _mapper.Map<MemberDTO>(createMemberDTO);
            try
            {


            }
            catch (Exception
            {

                throw;
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
