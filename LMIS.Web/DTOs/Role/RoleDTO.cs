using System.ComponentModel.DataAnnotations;

namespace LMIS.Web.DTOs.Role
{
    public class RoleDTO
    {
        public int RoleId { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}
