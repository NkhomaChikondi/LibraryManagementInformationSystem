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
        Task<MemberDTO> CreateMemberAsync(CreateMemberDto createMemberDto, string userIdClaim);
        Task<MemberDTO> UpdateMemberAsync(CreateMemberDto createMemberDto, int memberId);
        Task DeleteMemberAsync(int memberId);
        Task<MemberDTO> GetMemberByIdAsync(int memberId);
        IEnumerable<MemberDTO> GetAllMembers();
        Task ResendEmail(string email);

    }
}
