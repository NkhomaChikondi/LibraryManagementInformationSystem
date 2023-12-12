using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.DTOs.Checkout
{
    public class CheckoutDTO
    {
        public DateTime CheckOutDate { get; set; }
        public DateTime DueDate { get; set; }        
        public string Book { get; set; } = string.Empty;


       
    }
}
