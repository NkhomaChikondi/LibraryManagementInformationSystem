using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.DTOs
{
    public class UserWithRoleDTO
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Email { get; set; }

        public List<string> RoleName { get; set; }
    }
}

