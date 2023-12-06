namespace LMIS.Web.Models
{
    public class BookInventory
    {
        public int Id { get; set; }
        public string BookId { get; set; }
        public string Condition { get; set; }
        public bool isAvailable { get; set; }
        public string Location { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedDate { get; set; }
        public ICollection<CheckoutTransaction> checkoutTransactions { get; set; }
    }
}
