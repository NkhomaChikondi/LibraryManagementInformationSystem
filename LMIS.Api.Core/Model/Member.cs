using LMIS.Api.Core.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LMIS.Api.Core.Model
{
    public class Member 
    {
        [Key]
        public int MemberId { get; set; }
        
        public string MemberCode { get; set; } = string.Empty;
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Phone { get; set; } = string.Empty;
        [Required]
        public string Status { get; set; } = string.Empty;
        [Required]
        public DateTime CreatedOn { get; set; }
       
        public int UserId { get;set; }
        
        public bool IsDeleted { get; set; }
       
        public DateTime? DeletedDate { get; set; }
        public int MemberTypeId { get; set; }
        public ApplicationUser? User { get; set; }
        public ICollection<CheckoutTransaction> ? CheckoutTransactions { get; set; }
        public ICollection<Notification>? Notifications { get; set; }
        [JsonIgnore]
        public MemberType? MemberType { get; set; }
    }
}
