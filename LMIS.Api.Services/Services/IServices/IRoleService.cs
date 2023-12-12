using LMIS.Api.Core.DTOs;
using LMIS.Api.Core.DTOs.Role;

namespace LMIS.Api.Services.Services.IServices
{
    public interface IRoleService
    {
        Task<BaseResponse<RoleDTO>> CreateRole(RoleDTO role, string userIdClaim);
        Task<BaseResponse<bool>> DeleteRoleAsync(int roleId);
        BaseResponse<IEnumerable<RoleDTO>> GetAllRoles();
        Task<BaseResponse<RoleDTO>> GetRoleByIdAsync(int roleId);
        Task<BaseResponse<RoleDTO>> UpdateRoleAsync(RoleDTO role, int Id);
    }
}