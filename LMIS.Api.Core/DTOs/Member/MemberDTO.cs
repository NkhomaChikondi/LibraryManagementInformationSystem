using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Api.Core.DTOs.User;

namespace LMIS.Api.Core.DTOs.Member
{
    public class MemberDTO
    {
        public string MemberCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public int UserId { get; set; }

        public int MemberTypeId { get; set; }
        public ApplicationUserDTO User { get; set; }
        public MemberTypeDTO MemberType { get; set; }
    }
}
