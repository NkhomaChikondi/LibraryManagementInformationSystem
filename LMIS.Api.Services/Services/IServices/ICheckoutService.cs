using LMIS.Api.Core.DTOs.Book;
using LMIS.Api.Core.Model;

namespace LMIS.Api.Services.Services.IServices
{
    public interface ICheckoutService
    {
        Task<BookDTO> GetSelectedBooks(SearchBookDTO selectedBook, string memberCode, string userIdClaim);
        Task CheckOutBook(SearchBookDTO book, string memberCode, string userIdClaim);
    }
}