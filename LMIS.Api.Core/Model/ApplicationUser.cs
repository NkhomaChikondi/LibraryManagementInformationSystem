using LMIS.Api.Core.Repository.IRepository;
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
        public string FirstName { get; set; } = string.Empty;   
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Location { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string Gender { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public int Pin { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedDate { get; set; }

        public ICollection<UserRole>? UserRoles { get; set; }
        [JsonIgnore]
        public ICollection<Member>? Members { get; set; }
        public ICollection<Genre>? Genres { get; set; }
        public ICollection<CheckoutTransaction>? CheckoutTransactions { get; set; }
        public ICollection<Notification>?  Notifications { get; set; }


        public ApplicationUser()
        {
            CreatedOn = DateTime.Now;
            IsConfirmed = false;
        }
    }
}
