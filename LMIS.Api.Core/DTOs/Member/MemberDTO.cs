﻿using System;
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
        public string Member_Code { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public int userId { get; set; }
        public int MemberTypeId { get; set; }
        public ApplicationUser user { get; set; }
        public MemberType memberType { get; set; }
    }
}
