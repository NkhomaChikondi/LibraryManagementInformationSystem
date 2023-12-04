using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.Model
{
    public class Notification
    {
        public int NotificationId { get; set; }
        public string Messsage { get; set; }

        public int checkoutTransactionId { get; set; }
        public CheckoutTransaction checkoutTransaction { get; set; }
        public int memberId { get; set; }
        public Member member { get; set; }

        public int userId { get; set; }
        public ApplicationUser user { get; set; }
    }
}
