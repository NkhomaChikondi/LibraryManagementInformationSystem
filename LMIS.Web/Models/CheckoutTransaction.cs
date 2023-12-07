namespace LMIS.Web.Models
{
    public class CheckoutTransaction
    {
        public int Id { get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime DueDate { get; set; }
        public int UserId { get; set; }
        public string BookId { get; set; }
        public int MemberId { get; set; }
        public bool isReturned { get; set; } = false;
        public User user { get; set; }
        public Book book { get; set; }
        public Member member { get; set; }

        public int bookInventoryId { get; set; }
        public BookInventory bookInventory { get; set; }
    }
}
