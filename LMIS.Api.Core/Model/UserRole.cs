using LMIS.Api.Core.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.Model
{
    public class UserRole  {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserRoleId { get; set; }      
        
        public int UserId { get; set; }
        
        public ApplicationUser User { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public bool IsDeleted { get; set ; }
        public DateTime? DeletedDate { get; set ; }
    }
}
