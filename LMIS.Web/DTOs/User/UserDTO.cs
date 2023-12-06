using System.ComponentModel.DataAnnotations;

namespace LMIS.Web.DTOs.User
{
    public class UserDTO
    {
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

        public string RoleName { get; set; }
    }
}
