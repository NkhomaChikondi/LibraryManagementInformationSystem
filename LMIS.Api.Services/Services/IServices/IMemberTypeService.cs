using LMIS.Api.Core.DTOs;
using LMIS.Api.Core.DTOs.Member;

namespace LMIS.Api.Services.Services
{
    public interface IMemberTypeService
    {
        Task<BaseResponse<MemberTypeDTO>> CreateMemberTypeAsync(MemberTypeDTO createMemberTypeDTO, string userIdClaim);
        Task<BaseResponse<bool>> DeleteMemberTypeAsync(int MemberTypeId);
        BaseResponse<IEnumerable<MemberTypeDTO>> GetAllMembersTypes();
        Task<BaseResponse<MemberTypeDTO>> GetMemberTypeByIdAsync(int memberTypeId);
        Task<BaseResponse<bool>> UpdateMemberAsync(MemberTypeDTO memberTypeDTO, int memberTypeId);
    }
}