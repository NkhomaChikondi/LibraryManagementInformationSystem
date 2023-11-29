using LMIS.Api.Core.Model;

namespace LMIS.Api.Core.DTOs.Member
{
    public class CreateMemberDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string MemberTypeName { get; set; }
    }
}
