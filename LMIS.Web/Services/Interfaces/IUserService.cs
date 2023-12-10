using LMIS.Web.DTOs.User;
using LMIS.Web.Models;

namespace LMIS.Web.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers(string token);

        Task<List<Role>> GetAllRoles(string token);

        Task<bool> CreateUser(UserDTO userDTO, string token);

        Task<bool> DeleteUser(int id, string token);

        Task<User> GetUser(int id, string token);

        Task<bool> UpdateUser(UserDTO userDTO, string token);
    }
}
