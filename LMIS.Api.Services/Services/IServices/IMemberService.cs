using LMIS.Api.Core.DTOs;
using LMIS.Api.Core.DTOs.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Services.Services.IServices
{
    public interface IMemberService
    {
        Task<BaseResponse<CreateMemberDto>> CreateMemberAsync(CreateMemberDto createMemberDto, string userIdClaim);
        Task<BaseResponse<bool>> UpdateMemberAsync(CreateMemberDto createMemberDto, int memberId);
        Task<BaseResponse<bool>> DeleteMemberAsync(int memberId);
        Task<BaseResponse<CreateMemberDto>> GetMemberByIdAsync(int memberId);
        Task<BaseResponse<IEnumerable<CreateMemberDto>>> GetAllMembers();
        Task<BaseResponse<bool>> ResendEmail(string email);

    }
}
