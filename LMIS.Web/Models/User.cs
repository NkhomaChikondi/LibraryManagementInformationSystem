namespace LMIS.Web.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string Location { get; set; }
        public string Password { get; set; }

        public string Gender { get; set; }

        public string Email { get; set; }
        public ICollection<Genre> genres { get; set; }
        public ICollection<CheckoutTransaction> checkoutTransactions { get; set; }
    }
}
