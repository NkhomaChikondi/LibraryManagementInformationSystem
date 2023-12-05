using LMIS.Api.Core.DTOs;
using LMIS.Api.Core.DTOs.User;
using LMIS.Api.Core.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.Repository.IRepository
{

    public interface IUserRepository : IRepository<ApplicationUser>
    {
         void Update(ApplicationUser user);
         bool IsValidEmail(string email);
         string HashPassword(string password);            
         string GeneratePassword(ApplicationUserDTO applicationUser);
         int GeneratePin();
         Task<LoginTokenDTO> GenerateToken(ApplicationUser applicationUser,IConfiguration configuration,IUnitOfWork unitOfWork);
        Task<IEnumerable<ApplicationUser>> GetAllUsers();
        Task<bool> SoftDeleteAsync(int id);
        bool VerifyPassword(string hashedPasswordFromDatabase, string incomingPlainPassword);
        IEnumerable<ApplicationUser> usersWithRole();    
    }
    
}
