using LMIS.Api.Core.DTOs.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.Services
{
    public interface IMemberService
    {
        Task<MemberDTO> CreateMemberAsync(CreateMemberDto createMemberDto, string userIdClaim);
        Task UpdateMemberAsync(CreateMemberDto createMemberDto, int memberId);
        Task DeleteMemberAsync(int memberId);
        MemberDTO GetMemberByIdAsync(int memberId);
        IEnumerable<MemberDTO> GetAllMembers();
           

    }
}
