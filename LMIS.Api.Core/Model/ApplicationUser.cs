using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LMIS.Api.Core.Model
{
    public class ApplicationUser
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
        public string Password { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int Pin { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsConfirmed { get; set; }

        public ICollection<UserRole> userRoles { get; set; }
        [JsonIgnore]
        public ICollection<Member> members { get; set; }
        public ICollection<CheckoutTransaction> checkoutTransactions { get; set; }


        public ApplicationUser()
        {
            CreatedOn = DateTime.Now;
            IsConfirmed = false;
        }
    }
}
