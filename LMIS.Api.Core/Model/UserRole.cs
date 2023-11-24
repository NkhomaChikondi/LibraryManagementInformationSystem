using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.Model
{
    public class UserRole
    {
        [Key]
        public int userRoleId { get; set; }
        public int userId { get; set; }
        public int roleId { get; set; }

        // Navigation properties
        public ApplicationUser User { get; set; }
        public Role Role { get; set; }
    }
}
