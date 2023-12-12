
using AutoMapper;
using LMIS.Api.Core.DTOs;
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

        public async Task<BaseResponse<CreateMemberDto>> CreateMemberAsync(CreateMemberDto createMemberDto, string userIdClaim)
        {
            try
            {
                if (createMemberDto == null || string.IsNullOrEmpty(userIdClaim))
                {
                    return new()
                    {
                        IsError = true,
                        Message = "Make sure all member details are entered"
                    };
                }

                var member_code = _unitOfWork.Member.GenerateMemberCode(createMemberDto.FirstName, createMemberDto.LastName);

                var userEmail = userIdClaim;
                var user = await _unitOfWork.User.GetFirstOrDefaultAsync(u => u.Email == userEmail);

                if (user == null)
                {
                    return new()
                    {
                        IsError = true,
                        Message = "User doesnt exist"
                    };
                }

                // verify if the email is valid
                if (!_unitOfWork.Member.IsValidEmail(createMemberDto.Email))
                {
                    return new()
                    {
                        IsError = true,
                        Message = "Invalid email"
                    };
                }
                if (await _unitOfWork.Member.ExistsAsync(m => m.Email == createMemberDto.Email))
                {
                    return new()
                    {
                        IsError = true,
                        Message = "This email is already used, use another one"
                    };
                }
                // Verify if the phone number is valid
                if (!_unitOfWork.Member.IsPhoneNumberValid(createMemberDto.Phone))
                {
                    return new()
                    {
                        IsError = true,
                        Message = "Invalid Phone Number"
                    };
                }


                var memberTypeName = createMemberDto.MemberTypeName;
                var Membertype = await _unitOfWork.MemberType.GetFirstOrDefaultAsync(name => name.Name == memberTypeName);

                if (Membertype == null)
                {
                    return new()
                    {
                        IsError = true,
                        Message = "Member type doesnt exist"
                    };
                }

                var member = new Member()
                {
                    FirstName = createMemberDto.FirstName,
                    CreatedOn = DateTime.UtcNow,
                    Email = createMemberDto.Email,
                    LastName = createMemberDto.LastName,
                    MemberCode = member_code,
                    Phone = createMemberDto.Phone,
                    Status = "None",
                    User = user,
                    MemberType = Membertype,
                    MemberTypeId = Membertype.Id,
                    UserId = user.UserId
                };

                await _unitOfWork.Member.CreateAsync(member);
                _unitOfWork.Save();

                // Resend the email
                string pinBody = $"Your account type is {memberTypeName}. Your member code is {member_code}.<br />The member code will be needed each time you visit the library.";
                _emailService.SendMail(member.Email, "Member Account Details", pinBody);

                var createdMemberDTO = new CreateMemberDto
                {
                    FirstName = createMemberDto.FirstName,
                    Email = createMemberDto.Email,  
                    LastName = createMemberDto.LastName,
                    MemberTypeName = memberTypeName,
                    Phone = createMemberDto.Phone
                };
                
               
                return new()
                {
                    IsError = false,
                    Result = createdMemberDTO,
                };
            }
            catch (Exception)
            {
                return new()
                {
                    IsError = true,
                    Message = "Failed to create a new member"
                };
            }
        }
        public async Task<BaseResponse<bool>> DeleteMemberAsync(int memberId)
        {
            try
            {
                await _unitOfWork.Member.SoftDeleteAsync(memberId);
                return new()
                {
                    IsError = false,
                    Message = "Deleted Successfully"
                };
            }
            catch (Exception ex)
            {
                return new()
                {
                    IsError = true,
                    Message = $"Failed to delete a member an {ex.Message} error occured"
                };
            }

        }

        public async Task<BaseResponse<IEnumerable<CreateMemberDto>>> GetAllMembers()
        {
            try
            {
                var allMembers =  _unitOfWork.Member.GetAllRoles();
                if (allMembers != null)
                {
                    var allMembersDTO = new List<CreateMemberDto>();
                    foreach (var item in allMembers)
                    {
                        // get member type from the member id
                        var memberType = await _unitOfWork.MemberType.GetFirstOrDefaultAsync(m => m.Id == item.MemberId);
                        var newMember = new CreateMemberDto()
                        {
                            FirstName = item.FirstName,
                            LastName = item.LastName,
                            Email = item.Email,
                            Phone = item.Phone,
                            MemberTypeName = memberType.Name,
                        };
                        allMembersDTO.Add(newMember);
                    }
                    return new()
                    {
                        IsError = false,
                        Result = allMembersDTO,
                    };
                }
                return new()
                {
                    IsError = true,
                    Message = $"No users found",
                };
            }
            catch (Exception ex)
            {
                return new()
                {
                    IsError = true,
                    Message = $"Failed to get members. an {ex.Message} error occured ",
                };
            }
        }
        public async Task<BaseResponse<CreateMemberDto>> GetMemberByIdAsync(int memberId)
        {
            try
            {
                var member = await _unitOfWork.Member.GetByIdAsync(memberId);
                if (member != null)
                {
                    if (member.IsDeleted)
                    {
                        return new()
                        {
                            IsError = true,
                            Message = "Member not found",
                        };
                    }
                    // get member type from the member id
                    var memberType = await _unitOfWork.MemberType.GetFirstOrDefaultAsync(m => m.Id == member.MemberId);
                    var getMemberDTO = new CreateMemberDto
                    {
                        FirstName = member.FirstName,
                        LastName = member.LastName,
                        Email = member.Email,
                        Phone = member.Phone,
                        MemberTypeName = memberType.Name,
                    };
                    return new()
                    {
                        IsError = false,
                        Result = getMemberDTO
                    };
                }
                return new()
                {
                    IsError = true,
                    Message = $"Member not found",
                };
            }
            catch (Exception ex)
            {
                return new()
                {
                    IsError = true,
                    Message = $"Failed to get member. an {ex.Message} error occured ",
                };
            }
        }

        public async Task<BaseResponse<bool>> UpdateMemberAsync(CreateMemberDto createMemberDto, int memberId)
        {
            try
            {
                var member = await _unitOfWork.Member.GetByIdAsync(memberId);

                if (member != null)
                {
                    member.Email = createMemberDto.Email;
                    member.FirstName = createMemberDto.FirstName;
                    member.LastName = createMemberDto.LastName;
                    member.Phone = createMemberDto.Phone;

                    _unitOfWork.Member.Update(member);
                    _unitOfWork.Save();

                    return new()
                    {
                        IsError = false,
                        Message = "Updated Successfully"
                    };

                }
                return new()
                {
                    IsError = true,
                    Message = "failed to update member"
                };
            }
            catch (Exception ex)
            {
                return new()
                {
                    IsError = true,
                    Message = $"an {ex.Message} occured. Failed to update member"
                };
            }
        }
        public async Task<BaseResponse<bool>> ResendEmail(string email)
        {
            try
            {
                if (email == null)
                {
                    return new()
                    {
                        IsError = true,
                        Message = " Please enter your email"
                    };
                }

                var member = await _unitOfWork.Member.GetFirstOrDefaultAsync(r => r.Email == email);
                if (member != null)
                {

                    var memberDTO = _mapper.Map<MemberDTO>(member);
                    var memberCode = _unitOfWork.Member.GenerateMemberCode(member.FirstName, member.LastName);

                    _unitOfWork.Member.Update(member);
                    _unitOfWork.Save();

                    string pinBody = $"Your member account has been created in LMIS, your account type is {member.MemberType}, your member code is {memberCode}<br /> The member code will be needed each time you visit the library";
                    this._emailService.SendMail(member.Email, "Member Account reset Details", pinBody);

                    return new()
                    {
                        IsError = false,
                        Message = " Email is resent successfully"
                    };
                }
                else
                {
                    return new()
                    {
                        IsError = true,
                        Message = " Failed to find member with the provided email"
                    };
                }

            }
            catch (Exception ex)
            {
                return new()
                {
                    IsError = true,
                    Message = $"an {ex.Message} occured, failed to resend the email"
                };
            }
        }

    }
}
