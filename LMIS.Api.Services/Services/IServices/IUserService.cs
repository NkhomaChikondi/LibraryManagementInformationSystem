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
        Task<BaseResponse<bool>> DeleteUserAsync(int memberId);
        BaseResponse<IEnumerable<ApplicationUserDTO>> GetAllUsers();
        Task<BaseResponse<ApplicationUserDTO>> GetUserByIdAsync(int userId);
        Task<BaseResponse<bool>> UpdateUserAsync(ApplicationUserDTO createUserDTO, int userId);
        Task<BaseResponse<bool>> ConfirmAccount(string email, int pin);
        Task<BaseResponse<bool>> ResendEmail(string email);
    }
}
