using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.DTOs
{
    public class UserRoleDTO
    {
        public int userRoleId { get; set; }
        public int userId { get; set; }
        public int roleId { get; set; }
    }
}
