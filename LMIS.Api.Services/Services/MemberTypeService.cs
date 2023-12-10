using AutoMapper;
using LMIS.Api.Core.DTOs.Member;
using LMIS.Api.Core.DTOs;
using LMIS.Api.Core.Repository.IRepository;
using LMIS.Api.Services.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Api.Core.Model;
using System.Linq.Expressions;

namespace LMIS.Api.Services.Services
{
    public class MemberTypeService : IMemberTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
       

        public MemberTypeService(IUnitOfWork unitOfWork, IMapper Mapper, IEmailService emailService)
        {
            _mapper = Mapper;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        public async Task<BaseResponse<MemberTypeDTO>> CreateMemberTypeAsync(MemberTypeDTO createMemberTypeDTO, string userIdClaim)
        {
            try
            {
                if (createMemberTypeDTO == null || string.IsNullOrEmpty(userIdClaim))
                {
                    return new()
                    {
                        IsError = true,
                        Message = "Make sure all member types details are entered"
                    };
                }

                var userEmail = userIdClaim;
                var user = await _unitOfWork.User.GetFirstOrDefaultAsync(u => u.Email == userEmail);

                if (user == null)
                {
                    return new()
                    {
                        IsError = true,
                        Message = "Member type doesnt exist"
                    };
                }

                // create a new memberType
                var newMemberType = new MemberType
                {
                    Name = createMemberTypeDTO.TypeName,
                    CreatedOn = DateTime.UtcNow,
                };
                await _unitOfWork.memberType.CreateAsync(newMemberType);
                _unitOfWork.Save();

                var createdMemberTypeDTO = new MemberTypeDTO
                {
                    TypeName = createMemberTypeDTO.TypeName,
                };
                    
                    
                return new()
                {
                    IsError = false,
                    Result = createdMemberTypeDTO,
                };
            }
            catch (Exception)
            {
                return new()
                {
                    IsError = true,
                    Message = "Failed to create a new member type"
                };
            }
        }
        public async Task<BaseResponse<bool>> DeleteMemberTypeAsync(int MemberTypeId)
        {
            try
            {
                await _unitOfWork.memberType.SoftDeleteAsync(MemberTypeId);
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
                    Message = $"Failed to delete a memberType an {ex.Message} error occured"
                };
            }

        }

        public async Task<BaseResponse<IEnumerable<MemberTypeDTO>>> GetAllMembersTypes()
        {
            try
            {
                var allMemberTypes = await _unitOfWork.memberType.GetAllMemberType();
                if (allMemberTypes != null)
                {
                    var allMembersDTO = new List<MemberTypeDTO>();
                    foreach (var item in allMemberTypes)
                    {

                        var newMemberType = new MemberTypeDTO()
                        {
                            TypeName = item.Name,                          

                        };
                        allMembersDTO.Add(newMemberType);
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
                    Message = $"No member types found",
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
        public async Task<BaseResponse<MemberTypeDTO>> GetMemberTypeByIdAsync(int memberTypeId)
        {
            try
            {
                var memberType = await _unitOfWork.memberType.GetByIdAsync(memberTypeId);
                if (memberType != null)
                {
                    if (memberType.IsDeleted)
                    {
                        return new()
                        {
                            IsError = true,
                            Message = "Member not found",
                        };
                    }
                    // return the member type
                    var returnedMemberType = new MemberTypeDTO
                    {
                        TypeName = memberType.Name
                    };

                    return new()
                    {
                        IsError = false,
                        Result = returnedMemberType
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
                    Message = $"Failed to get a member type. an {ex.Message} error occured ",
                };
            }
        }

        public async Task<BaseResponse<bool>> UpdateMemberAsync(MemberTypeDTO memberTypeDTO, int memberTypeId)
        {
            try
            {
                var memberType = await _unitOfWork.memberType.GetByIdAsync(memberTypeId);

                if (memberType != null)
                {
                    memberType.Name = memberTypeDTO.TypeName;

                    _unitOfWork.memberType.Update(memberType);
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
                    Message = $"an {ex.Message} occured. Failed to update memeber Type"
                };
            }
        }

    }
}
