using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.DTOs.User
{
    public class ApplicationUserDTO
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string FrstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Location { get; set; } = string.Empty;
        [Required]
        public string Gender { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;

        public string RoleName { get; set; } = string.Empty;
    }
}
