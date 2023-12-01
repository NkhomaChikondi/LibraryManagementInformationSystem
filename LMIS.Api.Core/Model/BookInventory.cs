using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.Model
{
    public class BookInventory
    {
        [Key]
        public int Id { get; set; }      
       
        public string BookId { get; set; }
        public Book Book { get; set; }

        public int? ChckOutTransId { get; set; }
        public CheckoutTransaction checkoutTransaction { get; set; }
 
       

    }
}
