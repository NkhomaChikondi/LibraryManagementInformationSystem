using LMIS.Api.Core.DataAccess;
using LMIS.Api.Core.Model;
using LMIS.Api.Core.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
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
            _db.CheckoutTransactions.Update(checkoutTransaction);
        }

        public int GetOverDueTransaction(Member member)
        {
            // get the member having this member 
            var allMemberTransactions = _db.CheckoutTransactions.Where(c => c.Member == member).ToList();
            if (allMemberTransactions.Count > 0)
            {
                // get all transactions that are overdue
                var overdueTransactions = allMemberTransactions.Where(o => o.IsReturned == false).ToList();

                if (overdueTransactions.Count > 0)
                    return overdueTransactions.Count;

                else return 0;
            }
            return 0;
        }


        public async Task<bool> SoftDeleteAsync(int id)
        {
            var entity = await _db.CheckoutTransactions.FindAsync(id);

            if (entity == null || entity.IsDeleted)
            {
                return false;
            }

            entity.IsDeleted = true;
            entity.DeletedDate = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return true;
        }

        public IEnumerable<CheckoutTransaction> GetAllTransactions()
        {
            var alltransactions = _db.CheckoutTransactions.Where(U => U.IsDeleted == false).ToList();
            return alltransactions;
        }

       
    }
}
