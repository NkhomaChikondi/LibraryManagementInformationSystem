using LMIS.Web.DTOs.Role;
using LMIS.Web.DTOs.User;
using LMIS.Web.Models;

namespace LMIS.Web.Services.Interfaces
{
    public interface IroleService
    {
        Task<List<Role>> GetAllRoles(string token);

        Task<bool> CreateRole(RoleDTO userDTO, string token);

        Task<bool> DeleteRole(int id, string token);

        Task<Role> GetRole(int id, string token);

        Task<bool> UpdateRole(RoleDTO userDTO, string token);
    }
}
