using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.Model
{
    public class BookInventory
    {
        public int Id { get; set; }      
        public string BookLocation { get; set; }       
        public string BookName { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }

        public int CheckoutTransactionId {  get; set; }
        public ICollection<CheckoutTransaction> checkoutTransactions { get; set;}

    }
}
