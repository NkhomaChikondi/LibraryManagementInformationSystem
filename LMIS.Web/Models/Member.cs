using System.Text.Json.Serialization;

namespace LMIS.Web.Models
{
    public class Member
    {
        public int MemberId { get; set; }
        public string Member_Code { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public int userId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedDate { get; set; }
        public int MemberTypeId { get; set; }
        public User user { get; set; }
        public ICollection<CheckoutTransaction> checkoutTransactions { get; set; }
        public ICollection<Notification> notifications { get; set; }
        [JsonIgnore]
        public MemberType memberType { get; set; }
    }
}
