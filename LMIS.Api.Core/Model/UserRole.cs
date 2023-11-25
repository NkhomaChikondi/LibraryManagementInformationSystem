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
        public Guid userRoleId { get; set; }
        public Guid userId { get; set; }
        public Guid roleId { get; set; }

        // Navigation properties
        public ApplicationUser User { get; set; }
        public Role Role { get; set; }
    }
}
