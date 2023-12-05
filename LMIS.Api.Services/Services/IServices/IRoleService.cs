using LMIS.Api.Core.DTOs.Role;

namespace LMIS.Api.Services.Services.IServices
{
    public interface IRoleService
    {
        Task<RoleDTO> CreateRole(RoleDTO role, string userIdClaim);
        Task DeleteRoleAsync(int roleId);
        IEnumerable<RoleDTO> GetAllRoles();
        Task<RoleDTO> GetRoleByIdAsync(int roleId);
        Task<RoleDTO> UpdateRoleAsync(RoleDTO role, int Id);
    }
}