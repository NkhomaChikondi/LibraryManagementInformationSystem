using Amazon.Runtime.Internal.Util;
using AutoMapper;
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

        public async Task<ApplicationUserDTO> CreateUserAsync(ApplicationUserDTO createUserDTO)
        {
            try
            {
                if (!_unitOfWork.User.IsValidEmail(createUserDTO.Email))
                    return null;

                var roleName = createUserDTO.RoleName;
                if (!await _unitOfWork.Role.ExistsAsync(role => role.RoleName == roleName))
                {
                    _logger.LogError("The Role entered does not exist");
                    return null;
                }

                if (await _unitOfWork.User.ExistsAsync(user => user.Email == createUserDTO.Email))
                    return null;

                var role = await _unitOfWork.Role.GetFirstOrDefaultAsync(r => r.RoleName == roleName);
                if (role == null)
                    return null;

                var password = _unitOfWork.User.GeneratePassword(createUserDTO);
                var hashedPassword = _unitOfWork.User.HashPassword(password);
                var pin = _unitOfWork.User.GeneratePin();

                var user = new ApplicationUser
                {
                    Email = createUserDTO.Email,
                    Password = hashedPassword,
                    firstName = createUserDTO.firstName,
                    lastName = createUserDTO.lastName,
                    Gender = createUserDTO.Gender,
                    Location = createUserDTO.Location,
                    IsConfirmed = false,
                    Pin = pin,
                    CreatedOn = DateTime.UtcNow
                };

                var userRole = new UserRole
                {
                    User = user,
                    Role = role
                };

                await _unitOfWork.UserRole.CreateAsync(userRole);
                _unitOfWork.Save();

                string passwordBody = $"Your account has been created on LMIS. Your password is: {password}  your OTP is: {user.Pin}<br /> Enter the OTP to activate your account <br /> You can activate your account by clicking here</a>";
                _emailService.SendMail(user.Email, "Login Details", passwordBody);

                var createdUserDTO = _mapper.Map<ApplicationUserDTO>(user);
                return createdUserDTO;
            }
            catch (Exception)
            {                
                return null!;
            }
        }


        public async Task DeleteUserAsync(int userId)
        {
            try
            {
                await _unitOfWork.User.DeleteAsync(userId);
                // save changes
                _unitOfWork.Save();

              
            }
            catch (Exception)
            {


                return ;
            }          
           
        }

        public IEnumerable<ApplicationUserDTO> GetAllUsers()
        {
            try
            {
                var allUsers = _unitOfWork.User.GetAllAsync();
                if (allUsers != null)
                {
                    var allMembersDTO = _mapper.Map<IEnumerable<ApplicationUserDTO>>(allUsers);
                    return allMembersDTO;
                }
                return null;
            }
            catch (Exception)
            {                
                return null;
            }
        }

        public async Task<ApplicationUserDTO> GetUserByIdAsync(int userId)
        {
            try
            {
                var user = await _unitOfWork.User.GetByIdAsync(userId);
                if (user != null)
                {
                    var getUserDTO = _mapper.Map<ApplicationUserDTO>(user);
                    return getUserDTO;
                }
                return null;
            }
            catch (Exception)
            {               
                return null;
            }
        }

        public async Task UpdateUserAsync(ApplicationUserDTO createUserDTO, int userId)
        {
            try
            {
                var user = await _unitOfWork.User.GetByIdAsync(userId);

                if (user != null)
                {
                    // Update user properties based on the received DTO
                    user.firstName = createUserDTO.firstName;
                    user.lastName = createUserDTO.lastName;
                    user.Location = createUserDTO.Location;
                    user.Gender = createUserDTO.Gender;
                    user.Email = createUserDTO.Email;

                    _unitOfWork.User.Update(user);
                    _unitOfWork.Save();
                }
            }
            catch (Exception)
            {               
                return;
            }
        }

        public async Task ConfirmAccount(string email, int pin)
        {
            try
            {
                if (pin < 0 || email == null)
                    return;

                var user = await _unitOfWork.User.GetFirstOrDefaultAsync(u => u.Email == email);
                if (user == null || user.Pin != pin)
                    return;

                user.IsConfirmed = true;
                _unitOfWork.User.Update(user);
                _unitOfWork.Save();
            }
            catch (Exception)
            {               
                return;
            }
        }

        public async Task ResendEmail(string email)
        {
            try
            {
                if (email == null)
                    return;

                var user = await _unitOfWork.User.GetFirstOrDefaultAsync(u => u.Email == email);
                if (user == null)
                    return;

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

                return;
            }
            catch (Exception)
            {               
                return;
            }
        }

        //public async Task
    }
}
