using LMIS.Web.Models;
using LMIS.Web.ViewModels;

namespace LMIS.Web.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthResult> Login(string email, string password);
    }
}
