using LMIS.Api.Core.Repository.IRepository;
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
        public string Condition { get; set; }
        public bool IsAvailable { get; set; }
        public string Location { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedDate { get; set; }
        public ICollection<CheckoutTransaction> CheckoutTransactions { get; set; }
    }
}
