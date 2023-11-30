using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.Model
{
    public class CheckoutTransaction
    {
        public int Id { get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int MemberId { get; set; }
        public int BookInventoryId { get; set; }

        public ApplicationUser user { get; set; }
        public Book book { get; set; }
        public Member member { get; set; }
        public BookInventory Inventory { get; set; }
    }
}
