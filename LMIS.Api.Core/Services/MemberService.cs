using AutoMapper;
using AutoMapper.Execution;
using LMIS.Api.Core.DTOs.Member;
using LMIS.Api.Core.Model;
using LMIS.Api.Core.Repository.IRepository;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.Services
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MemberService(IUnitOfWork unitOfWork, IMapper Mapper)
        {
            _mapper = Mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<MemberDTO> CreateMemberAsync(CreateMemberDto createMemberDto, string userIdClaim)
        {
            try
            {
                var member_code = _unitOfWork.member.GenerateMemberCode(createMemberDto.FirstName, createMemberDto.LastName);
                var userEmail = userIdClaim;
                var user = await _unitOfWork.User.GetFirstOrDefaultAsync(user => user.Email == userEmail);
                var Membertype = await _unitOfWork.memberType.GetFirstOrDefaultAsync(name => name.Name == createMemberDto.MemberTypeName);
                var member = new Model.Member()
                {
                    First_Name = createMemberDto.FirstName,
                    CreatedOn = DateTime.UtcNow,
                    Email = createMemberDto.Email,
                    Last_Name = createMemberDto.LastName,
                    Member_Code = member_code,
                    Phone = createMemberDto.Phone,
                    Status = "Booked",
                    user = user,
                    memberType = Membertype,
                    MemberTypeId = Membertype.Id,
                    userId = user.UserId                    
                };

                
                await _unitOfWork.member.CreateAsync(member);
                _unitOfWork.Save();

                
                var createdMemberDTO = _mapper.Map<MemberDTO>(member);

                return createdMemberDTO;
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public Task DeleteMemberAsync(int memberId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable< MemberDTO> GetAllMembers()
        {
            var allMembers = _unitOfWork.member.GetAllAsync();
            // Map the updated member entity to a DTO
            var updatedMemberDTO = _mapper.Map<IEnumerable<MemberDTO>>(allMembers);

            return updatedMemberDTO;
        }

        public MemberDTO GetMemberByIdAsync(int memberId)
        {
            var member = _unitOfWork.member.GetByIdAsync(memberId);
            // Map the updated member entity to a DTO
            var getMemberDTO = _mapper.Map<MemberDTO>(member);
            return getMemberDTO;
        }

        public async Task UpdateMemberAsync(CreateMemberDto createMemberDto, int memberId)
        {
            var member = await _unitOfWork.member.GetByIdAsync(memberId);

            if(member == null)
            {
                return;
            }

            member.Email = createMemberDto.Email;
            member.Status = createMemberDto.FirstName;
            member.Last_Name = createMemberDto.LastName;
            member.Phone = createMemberDto.Phone;
           
            _unitOfWork.member.Update(member);
            _unitOfWork.Save();  
        }


    }
}
