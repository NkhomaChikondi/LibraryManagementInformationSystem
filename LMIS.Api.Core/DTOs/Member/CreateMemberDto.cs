using LMIS.Api.Core.Model;

namespace LMIS.Api.Core.DTOs.Member
{
    public class CreateMemberDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string MemberTypeName { get; set; } = string.Empty;
    }
}
