using Amazon.Runtime.Internal.Util;
using AutoMapper;
using LMIS.Api.Core.DTOs;
using LMIS.Api.Core.DTOs.Member;
using LMIS.Api.Core.DTOs.User;
using LMIS.Api.Core.Model;
using LMIS.Api.Core.Repository.IRepository;
using LMIS.Api.Services.Services.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<UserService> _logger;
        public IConfiguration _configuration { get; }

        public UserService(IUnitOfWork unitOfWork, IMapper Mapper, IConfiguration configuration, IEmailService emailService,ILogger<UserService> logger)
        {
            _mapper = Mapper;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<BaseResponse<ApplicationUserDTO>> CreateUserAsync(ApplicationUserDTO createUserDTO)
        {//ApplicationUserDTO
            try
            {
                if (!_unitOfWork.User.IsValidEmail(createUserDTO.Email))
                    return new BaseResponse<ApplicationUserDTO>() 
                    {
                        IsError = true,
                        Message = "invalid email"
                    };

                var roleName = createUserDTO.RoleName;
                if (!await _unitOfWork.Role.ExistsAsync(role => role.RoleName == roleName))
                {
                    return new BaseResponse<ApplicationUserDTO>()
                    {
                        IsError = true,
                        Message = "Role doesnt exist"
                    };
                }

                if (await _unitOfWork.User.ExistsAsync(user => user.Email == createUserDTO.Email))
                {
                    return new BaseResponse<ApplicationUserDTO>()
                    {
                        IsError = true,
                        Message = "The email is used already, use another one"
                    };
                }
                 
                var role = await _unitOfWork.Role.GetFirstOrDefaultAsync(r => r.RoleName == roleName);
                if (role == null)
                {
                    return new BaseResponse<ApplicationUserDTO>()
                    {
                        IsError = true,
                        Message = "Role doesnt exist"
                    };
                }
                   

                var password = _unitOfWork.User.GeneratePassword(createUserDTO);
                var hashedPassword = _unitOfWork.User.HashPassword(password);
                var pin = _unitOfWork.User.GeneratePin();

                var user = new ApplicationUser
                {
                    Email = createUserDTO.Email,
                    Password = hashedPassword,
                    FirstName = createUserDTO.firstName,
                    LastName = createUserDTO.lastName,
                    Gender = createUserDTO.Gender,
                    Location = createUserDTO.Location,
                    IsConfirmed = false,
                    IsDeleted = false,
                    Pin = pin,
                    CreatedOn = DateTime.UtcNow,                   
                };
                 await _unitOfWork.User.CreateAsync(user);
                _unitOfWork.Save();

                var userRole = new UserRole
                {
                    User = user,
                    Role = role
                };

                await _unitOfWork.UserRole.CreateAsync(userRole);
                _unitOfWork.Save();

                string passwordBody = $"Your account has been created on LMIS. Your password is: {password}  your OTP is: {user.Pin}<br /> Enter the OTP to activate your account <br /> You can activate your account by clicking here</a>";
                _emailService.SendMail(user.Email, "Login Details", passwordBody);


                var createdUserDTO = new ApplicationUserDTO
                {
                    Email = user.Email,
                    firstName = user.FirstName,
                    Gender= user.Gender,
                    lastName = user.LastName,
                    Location = user.Location,
                    RoleName = roleName,
                    
                };                                      
                  
                return new ()
                {
                    IsError = false,
                    Result = createdUserDTO,
                };
            }
            catch (Exception)
            {
                return new ()
                {
                    IsError = true,
                    Message = "Failed to create a new user "
                }; 
            }
        }

        public async Task<BaseResponse<bool>> DeleteUserAsync(int userId)
        {
            try
            {
              await _unitOfWork.User.SoftDeleteAsync(userId);
                return new ()
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
                    Message = $"Failed to delete a user an {ex.Message} error occured"
                };
            }          
           
        }

        public async Task<BaseResponse<IEnumerable<ApplicationUserDTO>>> GetAllUsers()
        {
            try
            {
                var allUsers = await _unitOfWork.User.GetAllUsers();
                if (allUsers != null)
                {                   
                    var allMembersDTO = _mapper.Map<IEnumerable<ApplicationUserDTO>>(allUsers);
                    return new ()
                    {
                        IsError = false,
                        Result = allMembersDTO,
                    };
                }
                return new ()
                {
                    IsError = true,
                    Message = $"No users found",
                };
            }
            catch (Exception ex)
            {

                return new ()
                {
                    IsError = true,
                    Message = $"Failed to get users. an {ex.Message} error occured ",
                };
            }
        }

        public async Task<BaseResponse<ApplicationUserDTO>> GetUserByIdAsync(int userId)
        {
            try
            {
                var user = await _unitOfWork.User.GetByIdAsync(userId);
                if (user != null)
                {
                    // check if the user is deleted
                    if (user.IsDeleted)
                    {
                        return new BaseResponse<ApplicationUserDTO>()
                        {
                            IsError = true,
                            Message = "No user found ",
                        };
                    }
                        
                    var getUserDTO = _mapper.Map<ApplicationUserDTO>(user);
                    return new BaseResponse<ApplicationUserDTO>()
                    {
                        IsError = false,
                       Result = getUserDTO
                    };                   
                }
                return new BaseResponse<ApplicationUserDTO>()
                {
                    IsError = true,
                    Message = $"No user found",
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ApplicationUserDTO>()
                {
                    IsError = true,
                    Message = $"Failed to get users. an {ex.Message} error occured ",
                };
            }
        }

        public async Task<BaseResponse<bool>> UpdateUserAsync(ApplicationUserDTO createUserDTO, int userId)
        {
            try
            {
                var user = await _unitOfWork.User.GetByIdAsync(userId);

                if (user != null)
                {                    
                    // Update user properties based on the received DTO
                    user.FirstName = createUserDTO.firstName;
                    user.LastName = createUserDTO.lastName;
                    user.Location = createUserDTO.Location;
                    user.Gender = createUserDTO.Gender;
                    user.Email = createUserDTO.Email;

                    _unitOfWork.User.Update(user);
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
                    Message = "failed to update user"
                };
            }
            catch (Exception ex)
            {
                return new()
                {
                    IsError = true,
                    Message = $"an {ex.Message} occured, failed to update user"
                };
            }
        }

        public async Task<BaseResponse<bool>> ConfirmAccount(string email, int pin)
        {
            try
            {
                if (pin < 0 || email == null)
                {
                    return new()
                    {
                        IsError = true,
                        Message = $"Check if your email or pin are correct"
                    };
                }
                   

                var user = await _unitOfWork.User.GetFirstOrDefaultAsync(u => u.Email == email);
                if (user == null || user.Pin != pin)
                {
                    return new()
                    {
                        IsError = false,
                        Message = "Your account is confirmed"
                    };
                }
                   

                user.IsConfirmed = true;
                _unitOfWork.User.Update(user);
                _unitOfWork.Save();
                return new()
                {
                    IsError = false,
                    Message = "Your account is confirmed"
                };
            }
            catch (Exception ex)
            {
                return new()
                {
                    IsError = true,
                    Message = $"an {ex.Message} occured, failed to confirm your account"
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

                var user = await _unitOfWork.User.GetFirstOrDefaultAsync(u => u.Email == email);
                if (user == null)
                {
                    return new()
                    {
                        IsError = true,
                        Message = " Failed to find user with the provided email"
                    };
                }
                   

                var userDTO = _mapper.Map<ApplicationUserDTO>(user);
                var pin = _unitOfWork.User.GeneratePin();
                var password = _unitOfWork.User.GeneratePassword(userDTO);
                var hashPassword = _unitOfWork.User.HashPassword(password);

                user.Pin = pin;
                user.Password = hashPassword;
                _unitOfWork.User.Update(user);
                _unitOfWork.Save();

                string pinBody = $"Your OTP for LMIS is {pin}. Your new password is {password}<br /> Enter the OTP, email address, and the new password to reset your account";
                this._emailService.SendMail(user.Email, "Account Reset Details", pinBody);

                return new()
                {
                    IsError = false,
                    Message = " Email is resent successfully"
                };
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

        public async Task<bool> IsRoleDeleted(string roleName)
        {
            var existingRole = await _unitOfWork.Role.GetFirstOrDefaultAsync(r => r.RoleName == roleName);
            return existingRole?.IsDeleted ?? false;
        }

        //public async Task
    }
}
