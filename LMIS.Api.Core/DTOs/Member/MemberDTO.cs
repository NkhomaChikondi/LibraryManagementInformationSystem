using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMIS.Api.Core.DTOs.User;
using LMIS.Api.Core.Model;

namespace LMIS.Api.Core.DTOs.Member
{
    public class MemberDTO
    {
        public string Member_Code { get; set; } = string.Empty;
        public string First_Name { get; set; } = string.Empty;
        public string Last_Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;  
        public DateTime CreatedOn { get; set; }
        public int userId { get; set; }
        public int MemberTypeId { get; set; }
        public ApplicationUser? User { get; set; }
        public MemberType? MemberType { get; set; }
    }
}
