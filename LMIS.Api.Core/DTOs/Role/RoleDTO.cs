using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.DTOs.Role
{
    public class RoleDTO
    {
        
        [Required]
        public string RoleName { get; set; }
    }
}
