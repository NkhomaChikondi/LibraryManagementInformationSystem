using LMIS.Api.Core.DTOs.User;
using Microsoft.Extensions.Configuration;

namespace LMIS.Api.Services.Services.IServices
{
    public interface IAuthService
    {
        IConfiguration _configuration { get; set; }

        Task Login(LoginDTO loginDTO);
        LoginTokenDTO ReturnTokenDetail(LoginTokenDTO loginDTO);
    }
}