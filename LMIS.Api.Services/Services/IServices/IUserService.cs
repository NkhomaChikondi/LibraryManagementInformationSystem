using LMIS.Api.Core.DTOs;
using LMIS.Api.Core.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Services.Services.IServices
{
    public interface IUserService
    {
        public Task<BaseResponse<ApplicationUserDTO>> CreateUserAsync(ApplicationUserDTO createUserDTO);
        Task DeleteUserAsync(int memberId);
        IEnumerable<ApplicationUserDTO> GetAllUsers();
        Task<ApplicationUserDTO> GetUserByIdAsync(int userId);
        Task UpdateUserAsync(ApplicationUserDTO createUserDTO, int userId);
        Task ConfirmAccount(string email, int pin);
        Task ResendEmail(string email);
    }
}
