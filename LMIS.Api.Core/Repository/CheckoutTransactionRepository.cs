using LMIS.Api.Core.DataAccess;
using LMIS.Api.Core.Model;
using LMIS.Api.Core.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.Repository
{
    public class CheckoutTransactionRepository : Repository<CheckoutTransaction>, ICheckoutTransactionRepository
    {

        private ApplicationDbContext _db;
        public CheckoutTransactionRepository(ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }
        public void Update(CheckoutTransaction checkoutTransaction)
        {
            _db.checkoutTransactions.Update(checkoutTransaction);
        }

        public async Task<int> GetOverDueTransaction(Member member)
        {
            // get the member having this member 
            var allMemberTransactions = _db.checkoutTransactions.Where(c => c.member == member).ToList();
            if (allMemberTransactions.Count > 0)
            {
                // get all transactions that are overdue
                var overdueTransactions = allMemberTransactions.Where(o => o.isReturned == false).ToList();

                if(overdueTransactions.Count > 0)
                    return overdueTransactions.Count;

                else return 0;
            }
            return 0;
        }
    }
}
