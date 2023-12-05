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
                {
                    
                    return null;
                }
                // Check if the role exists
                Expression<Func<Role, bool>> roleExistsExpression = role => role.RoleName == createUserDTO.RoleName;
                if (!await _unitOfWork.Role.ExistsAsync(roleExistsExpression))
                {
                    _logger.LogError("The Role entered does not exist");
                    return null;
                }
                // Check if the email already exists
                Expression<Func<ApplicationUser, bool>> userEmailExistsExpression = userEmail => userEmail.Email == createUserDTO.Email;
                if (await _unitOfWork.User.ExistsAsync(userEmailExistsExpression))
                {
                    return null;
                }
                // get role
                var role = await _unitOfWork.Role.GetFirstOrDefaultAsync(r => r.RoleName == createUserDTO.RoleName);
                //generate password
                var password = _unitOfWork.User.GeneratePassword(createUserDTO);
                // Hash the password
                var Password = _unitOfWork.User.HashPassword(password);

                // Generate and assign the PIN
                var Pin = _unitOfWork.User.GeneratePin();
                var user = new ApplicationUser
                { 
                    Email = createUserDTO.Email ,
                    Password = Password,
                    firstName = createUserDTO.firstName,
                    lastName = createUserDTO.lastName ,
                    Gender = createUserDTO.Gender,
                    Location = createUserDTO.Location ,
                    IsConfirmed  =false,
                    Pin = Pin,
                    CreatedOn = DateTime.UtcNow             
                };

                // create a new userRole
                var userRole = new UserRole
                {
                    User = user,
                    Role = role
                };
                await _unitOfWork.UserRole.CreateAsync(userRole);
                _unitOfWork.Save();
                // send an email containing Login details
                string PasswordBody = "Your account has been created on LMIS. Your password is:  " + password + "  your OTP is:  " + user.Pin + "<br /> Enter the OTP to activate your account\" + \" <br /> You can activate your account by clicking here</a>";
                _emailService.SendMail(user.Email, "Login Details", PasswordBody);

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
                if (allUsers == null)
                    return null;
                // Map the updated member entity to a DTO
                var allMembersDTO = _mapper.Map<IEnumerable<ApplicationUserDTO>>(allUsers);

                return allMembersDTO;
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async Task<ApplicationUserDTO>GetUserByIdAsync(int userId)
        {
            try
            {
                var user = await  _unitOfWork.User.GetByIdAsync(userId);
                // Map the updated member entity to a DTO
                var getUserDTO = _mapper.Map<ApplicationUserDTO>(user);
                return getUserDTO;
            }
            catch (Exception)
            {

                return null!;
            }
        }

        public async Task UpdateUserAsync(ApplicationUserDTO createUserDTO, int userId)
        {
            try
            {
                var user = await _unitOfWork.User.GetByIdAsync(userId);

                if (user == null)
                {
                    return;
                }
              
                // Update user properties based on the received DTO
                user.firstName = createUserDTO.firstName;
                user.lastName = createUserDTO.lastName;
                user.Location = createUserDTO.Location;
                user.Gender = createUserDTO.Gender;
                user.Email = createUserDTO.Email;

                _unitOfWork.User.Update(user);
                _unitOfWork.Save();                
               
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
                //check if values are not null
                if (pin < 0 || email == null)
                {                    
                    return ;
                }
                // get user with that email
                var user = await _unitOfWork.User.GetFirstOrDefaultAsync(user => user.Email == email);
                if (user == null)
                {
                    return ;
                }
                // check if the pin submitted is equal to the one in the database
                if (user.Pin != pin)
                {
                    return ;
                }
                // update user
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
                // check if email is null
                if (email == null)
                {
                    return;
                }
                // get the user having this email
                var user = await _unitOfWork.User.GetFirstOrDefaultAsync(user => user.Email == email);
                if (user == null)
                {
                    return;
                }
                var userDTO = _mapper.Map<ApplicationUserDTO>(user);
                var pin = _unitOfWork.User.GeneratePin();
                var password = _unitOfWork.User.GeneratePassword(userDTO);
                var hashPassword = _unitOfWork.User.HashPassword(password);

                // save new password and pin user details
                user.Pin = pin;
                user.Password = hashPassword;
                _unitOfWork.User.Update(user);
                _unitOfWork.Save();

                // resend the email

                string pinBody = "Your OTP for LMIS is " + pin + " Your new password is " + password + " <br /> Enter the OTP, email address and the new password to reset your account";
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
