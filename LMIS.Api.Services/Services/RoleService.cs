﻿using AutoMapper;
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

        public async Task<RoleDTO> CreateRole(RoleDTO role, string userIdClaim)
        {
            try
            {
                if (role == null || string.IsNullOrEmpty(userIdClaim))
                    return null;

                var userEmail = userIdClaim;
                var user = await _unitOfWork.User.GetFirstOrDefaultAsync(u => u.Email == userEmail);
                if (user == null)
                    return null;
                var roleExist = await _unitOfWork.Role.ExistsAsync(r => r.RoleName == role.RoleName);
                if (roleExist)
                {
                    //check if it is deleted
                    var getRole = await _unitOfWork.Role.GetFirstOrDefaultAsync(R => R.RoleName == role.RoleName);
                    if(!getRole.IsDeleted)
                    {
                        return null;
                    }
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

                        return newRoleDto;
                    }
                }
                else if(!roleExist) 
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

                    return newRoleDto;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<RoleDTO> GetAllRoles()
        {
            try
            {
                var allRoles = _unitOfWork.Role.GetAllRoles().Result;
                if (allRoles != null)
                {
                    var allRoleDTO = _mapper.Map<IEnumerable<RoleDTO>>(allRoles);
                    return allRoleDTO;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<RoleDTO> UpdateRoleAsync(RoleDTO role, int Id)
        {
            try
            {

                var selectedRole = await _unitOfWork.Role.GetByIdAsync(Id);
                selectedRole.RoleName = role.RoleName;

                _unitOfWork.Role.Update(selectedRole);
                _unitOfWork.Save();


                var getRoleDTO = _mapper.Map<RoleDTO>(selectedRole);
                return getRoleDTO;
            }
            catch (Exception)
            {

                return null!;
            }
        }

        public async Task DeleteRoleAsync(int roleId)
        {
            try
            {
                await _unitOfWork.Role.SoftDeleteAsync(roleId);
                return;
            }
            catch (Exception)
            {
                return;
            }
        }

        public async Task<RoleDTO> GetRoleByIdAsync(int roleId)
        {
            try
            {
                var role = await _unitOfWork.Role.GetByIdAsync(roleId);

                if (role != null)
                {
                    var getRoleDTO = _mapper.Map<RoleDTO>(role);
                    return getRoleDTO;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
