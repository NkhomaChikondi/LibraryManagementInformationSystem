using LMIS.Api.Core.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.Model
{
    public class CheckoutTransaction 
    {
        [Key]
        public int Id { get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime DueDate { get; set; }      
        public int UserId { get; set; }
        public string BookId { get; set; } = string.Empty;  
        public int MemberId { get; set; }      
        public bool IsReturned {  get; set; } = false;
        public ApplicationUser? User { get; set; }       
        public Member? Member { get; set; }
        public int BookInventoryId { get; set; }
        public BookInventory? BookInventory { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedDate { get; set; }

    }
}
