using LMIS.Api.Core.DTOs.Checkout;
using LMIS.Api.Core.DTOs;
using LMIS.Api.Core.Model;

namespace LMIS.Api.Core.Repository.IRepository
{
    public interface ITemp_DataRepository: IRepository<Temp_Data>
    {
        void Update(Temp_Data temp_Data);
        Task<IEnumerable<Temp_Data>> GetAllCheckoutTransactions();
    }
}