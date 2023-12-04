using LMIS.Api.Core.DTOs.Book;

namespace LMIS.Api.Services.Services.IServices
{
    public interface ICheckoutService
    {
        Task<BookDTO> GetSelectedBooks(SearchBookDTO selectedBook, string memberCode, string userIdClaim);
    }
}