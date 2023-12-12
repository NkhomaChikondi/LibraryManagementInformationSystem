using LMIS.Api.Core.DataAccess;
using LMIS.Api.Core.DTOs;
using LMIS.Api.Core.DTOs.User;
using LMIS.Api.Core.Model;
using LMIS.Api.Core.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.Repository
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        private ApplicationDbContext _db;
       
       
        public UserRepository(ApplicationDbContext db) : base(db)
        {
            this._db = db;           
        }


        public string GeneratePassword(ApplicationUserDTO applicationUser)        
        {
            // Get the first letters of the first and last names
            char firstLetterFirstName = char.ToLower(applicationUser.firstName[0]);
            char firstLetterLastName = char.ToLower(applicationUser.lastName[0]);

            // Generate random letters, numbers, and special characters
            Random random = new Random();
            const string alphanumericChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            const string numericChars = "0123456789";
            const string specialChars = "!@#$%^&*()_+-=[]{};:,.<>?";

            // Generate random characters
            string randomChars = new string(Enumerable.Repeat(alphanumericChars, 2)
                                        .Select(s => s[random.Next(s.Length)])
                                        .ToArray());

            // Pick a random digit
            char randomDigit = numericChars[random.Next(numericChars.Length)];

            // Pick a random uppercase letter
            char randomUppercase = char.ToUpper(alphanumericChars[random.Next(alphanumericChars.Length)]);

            // Pick a random special character
            char randomSpecialChar = specialChars[random.Next(specialChars.Length)];

            // Combine elements to create the password
            string generatedPassword = $"{firstLetterFirstName}{firstLetterLastName}{randomChars}{randomDigit}{randomUppercase}{randomSpecialChar}";

            return generatedPassword;
        }        
        public int GeneratePin()
        {
            Random rand = new Random();

            // Generate a random 5-digit PIN
            int pin = rand.Next(10000, 100000);

            return pin; 
        }

        public async Task<LoginTokenDTO> GenerateToken(ApplicationUser applicationUser,IConfiguration _configuration,IUnitOfWork _unitOfWork)
        {
            //if successful generate the token based on details given. Valid for one day
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("TokenString:TokenKey"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                            new Claim(ClaimTypes.NameIdentifier, applicationUser.Email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_configuration.GetValue<double>("TokenString:expiryMinutes")),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // getting user role
            var role = await _unitOfWork.UserRole.GetFirstOrDefaultAsync(u => u.UserId == applicationUser.UserId);
            var roleData = await _unitOfWork.Role.GetFirstOrDefaultAsync(u => u.RoleId == role.RoleId);


            // login DTO
            var userData = new LoginTokenDTO()
            {
                Token = tokenString,
                UserId = applicationUser.UserId.ToString(),
                FirstName = applicationUser.FirstName,
                LastName = applicationUser.LastName,
                Role = roleData.RoleName,
                Email = applicationUser.Email,
                TokenType = "bearer",
                TokenExpiryMinutes = (DateTime)tokenDescriptor.Expires,

            };
            return userData;
        }

        public string HashPassword(string password)
        {
            // Hash the password before storing it
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            return hashedPassword;
        }
        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public void Update(ApplicationUser user)
        {
            _db.ApplicationUsers.Update(user);
        }

       
        public bool VerifyPassword(string hashedPasswordFromDatabase, string incomingPlainPassword)
        {
            return BCrypt.Net.BCrypt.Verify(incomingPlainPassword, hashedPasswordFromDatabase);
        }

        public async Task<bool> SoftDeleteAsync(int id)
        {
            var entity = await _db.ApplicationUsers.FindAsync(id);

            if (entity == null || entity.IsDeleted)
            {
                return false; 
            }

            entity.IsDeleted = true;
            entity.DeletedDate = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return true; 
        }

        public IEnumerable <ApplicationUser> GetAllUsers()
        {
            var allUsers =  _db.ApplicationUsers.Where(U => U.IsDeleted == false).ToList();
            return allUsers;
        }
       
    }
}
