using AutoMapper;
using LMIS.Api.Core.DTOs;
using LMIS.Api.Core.DTOs.Genre;
using LMIS.Api.Core.DTOs.Role;
using LMIS.Api.Core.Model;
using LMIS.Api.Core.Repository.IRepository;
using LMIS.Api.Services.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Services.Services
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IBookService _bookService;

        public RoleService(IUnitOfWork unitOfWork, IMapper Mapper, IEmailService emailService, IBookService bookService)
        {
            _mapper = Mapper;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _bookService = bookService;
        }

        public async Task<BaseResponse<RoleDTO>> CreateRole(RoleDTO role, string userIdClaim)
        {
            try
            {
                if (role == null || string.IsNullOrEmpty(userIdClaim))
                    return new()
                    {
                        IsError = true,
                        Message = "Role not found"
                    };

                var userEmail = userIdClaim;
                var user = await _unitOfWork.User.GetFirstOrDefaultAsync(u => u.Email == userEmail);
                if (user == null)
                    return new()
                    {
                        IsError = true,
                        Message = "User not found"
                    };

                var roleExist = await _unitOfWork.Role.ExistsAsync(r => r.RoleName == role.RoleName);
                if (roleExist)
                {
                    //check if it is deleted
                    var getRole = await _unitOfWork.Role.GetFirstOrDefaultAsync(R => R.RoleName == role.RoleName);
                    if (getRole.IsDeleted == true)
                        return new()
                        {
                            IsError = true,
                            Message = "role does not exist"
                        };

                    else
                    {
                        var newRole = new Role
                        {
                            RoleName = role.RoleName,
                            IsDeleted = false,
                        };
                        // check if the role exist
                        await _unitOfWork.Role.CreateAsync(newRole);
                        _unitOfWork.Save();

                        var newRoleDto = new RoleDTO
                        {
                            RoleName = newRole.RoleName,
                        };

                        return new()
                        {
                            IsError = false,
                            Result = newRoleDto
                        };
                    }
                }
                else if (!roleExist)
                {
                    var newRole = new Role
                    {
                        RoleName = role.RoleName,
                        IsDeleted = false,
                    };
                    // check if the role exist
                    await _unitOfWork.Role.CreateAsync(newRole);
                    _unitOfWork.Save();

                    var newRoleDto = new RoleDTO
                    {
                        RoleName = newRole.RoleName,
                    };

                    return new()
                    {
                        IsError = false,
                        Result = newRoleDto
                    };
                }
                return new()
                {
                    IsError = true,
                    Message = "Failed to create role name"
                };
            }
            catch (Exception)
            {
                return new()
                {
                    IsError = true,
                    Message = "Failed to create role name"
                };
            }
        }

        public BaseResponse<IEnumerable<RoleDTO>> GetAllRoles()
        {
            try
            {
                var allRoles = _unitOfWork.Role.GetAllRoles();
                if (allRoles != null)
                {
                    var allRoleDTO = _mapper.Map<IEnumerable<RoleDTO>>(allRoles);
                    return new()
                    {
                        IsError = false,
                        Result = allRoleDTO
                    };
                }
                return new()
                {
                    IsError = true,
                    Message = "No roles was found"
                };
            }
            catch (Exception)
            {
                return new()
                {
                    IsError = true,
                    Message = "No roles was found"
                };
            }
        }

        public async Task<BaseResponse<RoleDTO>> UpdateRoleAsync(RoleDTO role, int Id)
        {
            try
            {

                var selectedRole = await _unitOfWork.Role.GetByIdAsync(Id);
                selectedRole.RoleName = role.RoleName;

                _unitOfWork.Role.Update(selectedRole);
                _unitOfWork.Save();


                var getRoleDTO = _mapper.Map<RoleDTO>(selectedRole);
                return new()
                {
                    IsError = false,
                    Message = "Role updated successfully"

                };
            }
            catch (Exception)
            {
                return new()
                {
                    IsError = true,
                    Message = "An error hass occured, failed to update role"

                };
            }
        }

        public async Task<BaseResponse<bool>> DeleteRoleAsync(int roleId)
        {
            try
            {
                await _unitOfWork.Role.SoftDeleteAsync(roleId);
                return new()
                {
                    IsError = false,
                    Message = "The role has been deleted successfully"

                };
            }
            catch (Exception)
            {
                return new()
                {
                    IsError = true,
                    Message = "An error hass occured, failed to update role"
                };
            }
        }

        public async Task<BaseResponse<RoleDTO>> GetRoleByIdAsync(int roleId)
        {
            try
            {
                var role = await _unitOfWork.Role.GetByIdAsync(roleId);

                if (role != null)
                {
                    var getRoleDTO = _mapper.Map<RoleDTO>(role);
                    return new()
                    {
                        IsError = false,
                        Result = getRoleDTO
                    };
                }
                return new()
                {
                    IsError = true,
                    Message = "Failed to get the select role"

                };
            }
            catch (Exception)
            {
                return new()
                {
                    IsError = true,
                    Message = "Failed to get the select role"

                };
            }
        }
    }
}
