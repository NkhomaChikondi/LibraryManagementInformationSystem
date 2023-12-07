using LMIS.Web.DTOs.Member;
using LMIS.Web.Models;

namespace LMIS.Web.Services.Interfaces
{
    public interface IMemberService
    {
        Task<List<Member>> GetAllMembers(string token);

        Task<bool> CreateMember(MemberDTO memberDTO, string token);

        Task<bool> DeleteMember(int id, string token);

        Task<Member> GetMember(int id, string token);

        Task<bool> UpdateMember(MemberDTO memberDTO, string token);
    }
}
