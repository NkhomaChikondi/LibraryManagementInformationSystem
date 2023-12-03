using LMIS.Api.Core.DTOs.Book;

namespace LMIS.Api.Services.Services.IServices
{
    public interface ICheckoutService
    {
        Task<List<BookDTO>> GetSelectedBooks(SearchBookListDTO selectedBooks, string memberCode);
    }
}