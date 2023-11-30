
using AutoMapper;
using LMIS.Api.Core.DTOs.Member;
using LMIS.Api.Core.DTOs.User;
using LMIS.Api.Core.Model;
using LMIS.Api.Core.Repository.IRepository;
using LMIS.Api.Services.Services.IServices;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Services.Services
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public MemberService(IUnitOfWork unitOfWork, IMapper Mapper, IEmailService emailService)
        {
            _mapper = Mapper;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        public async Task<MemberDTO> CreateMemberAsync(CreateMemberDto createMemberDto, string userIdClaim)
        {
            try
            {
                var member_code = _unitOfWork.member.GenerateMemberCode(createMemberDto.FirstName, createMemberDto.LastName);
                var userEmail = userIdClaim;
                var user = await _unitOfWork.User.GetFirstOrDefaultAsync(user => user.Email == userEmail);
                var Membertype = await _unitOfWork.memberType.GetFirstOrDefaultAsync(name => name.Name == createMemberDto.MemberTypeName);
                var member = new Member()
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

                // resend the email

                string pinBody = "Your account type is  " + createMemberDto.MemberTypeName + "  your member code is  " + member_code + " <br /> The member code will be needed each time you visit the library";
                this._emailService.SendMail(member.Email, "Member Account Details", pinBody);

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
            try
            {
                var allMembers = _unitOfWork.member.GetAllAsync();
                // Map the updated member entity to a DTO
                var allMembersDTO = _mapper.Map<IEnumerable<MemberDTO>>(allMembers);

                return allMembersDTO;
            }
            catch (Exception)
            {
                return null!;
            }           
        }

        public async Task<MemberDTO> GetMemberByIdAsync(int memberId)
        {
            try
            { 
                var member = await _unitOfWork.member.GetByIdAsync(memberId);
                // Map the updated member entity to a DTO
                var getMemberDTO = _mapper.Map<MemberDTO>(member);
                return getMemberDTO;

            }
            catch (Exception)
            {
                return null!;
            }            
        }

        public async Task<MemberDTO> UpdateMemberAsync(CreateMemberDto createMemberDto, int memberId)
        {
            try
            { 
                 var member = await _unitOfWork.member.GetByIdAsync(memberId);

                if (member == null)
                {
                    return null;
                }

                member.Email = createMemberDto.Email;
                member.First_Name = createMemberDto.FirstName;
                member.Last_Name = createMemberDto.LastName;
                member.Phone = createMemberDto.Phone;

                _unitOfWork.member.Update(member);
                _unitOfWork.Save();

                // Map the updated member entity to a DTO
                var getMemberDTO = _mapper.Map<MemberDTO>(member);
                return getMemberDTO;

            }
            catch (Exception)
            {

                return null!;
            }           
        }

        public async Task ResendEmail(string email)
        {
            try
            {
                // check if email is null
                if (email == null)
                {
                    return;
                }
                // get the user having this r
                var member = await _unitOfWork.member.GetFirstOrDefaultAsync(r => r.Email == email);
                if (member == null)
                {
                    return;
                }
                var memberDTO = _mapper.Map<MemberDTO>(member);
                
                var memberCode = _unitOfWork.member.GenerateMemberCode(member.First_Name, member.Last_Name);

               
                _unitOfWork.member.Update(member);
                _unitOfWork.Save();

                // resend the email
                string pinBody = "Your member account has been created in LMIS, your account type is  " + member.memberType + "  your member code is  " + memberCode + " <br /> The member code will be needed each time you visit the library";
                this._emailService.SendMail(member.Email, "Member Account reset Details", pinBody);


                return;

            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
