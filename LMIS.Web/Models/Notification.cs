namespace LMIS.Web.Models
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public string Messsage { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedDate { get; set; }
        public int checkoutTransactionId { get; set; }
        public CheckoutTransaction checkoutTransaction { get; set; }
        public int memberId { get; set; }
        public Member member { get; set; }

        public int userId { get; set; }
        public User user { get; set; }
    }
}
