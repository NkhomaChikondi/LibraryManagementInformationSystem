using LMIS.Api.Core.DataAccess;
using LMIS.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.Repository.IRepository
{
    public interface ICheckoutTransactionRepository : IRepository<CheckoutTransaction>
    {
        void Update(CheckoutTransaction checkoutTransaction);
        Task<int> GetOverDueTransaction(Member member);
        Task<CheckoutTransaction> GetLastOrDefault(int memberId);


    }
}
