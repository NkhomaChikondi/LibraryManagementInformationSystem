using LMIS.Api.Core.DataAccess;
using LMIS.Api.Core.Model;
using LMIS.Api.Core.Repository.IRepository;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.Repository
{
    public class UserRepository : Repository<ApplicationUser>,IUserRepository
    {
        private ApplicationDbContext _db;
        public UserRepository(ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }
        public string GeneratePassword(ApplicationUser applicationUser)        
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
            _db.applicationUsers.Update(user);
        }
    }
}
