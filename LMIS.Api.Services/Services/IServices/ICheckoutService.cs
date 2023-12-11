using LMIS.Api.Core.DTOs;
using LMIS.Api.Core.DTOs.Book;
using LMIS.Api.Core.DTOs.Checkout;
using LMIS.Api.Core.Model;

namespace LMIS.Api.Services.Services.IServices
{
    public interface ICheckoutService
    {


        Task<BaseResponse<CheckoutDTO>> CheckoutBook(SearchBookDTO selectedBook, string memberCode, string userIdClaim);
       
        Task<BaseResponse<bool>> ReturnBook(string memberId, string Booktitle);
        Task<BaseResponse<IEnumerable<CheckoutDTO>>> GetAllCheckoutTransactions();
        Task<BaseResponse<CheckoutDTO>> GetCheckoutTransactionByIdAsync(int transId);
        Task<BaseResponse<bool>> DeleteTransactionAsync(int TransId);

    }
}