using LMIS.Api.Core.Repository.IRepository;
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
        public string Messsage { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public DateTime DeletedDate { get; set; }
        public int checkoutTransactionId { get; set; }
        public CheckoutTransaction? CheckoutTransaction { get; set; }
        public int MemberId { get; set; }
        public Member? Member { get; set; }

        public int UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
