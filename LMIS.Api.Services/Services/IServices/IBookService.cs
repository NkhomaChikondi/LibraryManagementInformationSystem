using LMIS.Api.Core.DTOs;
using LMIS.Api.Core.DTOs.Book;
using LMIS.Api.Core.Model;

namespace LMIS.Api.Services.Services.IServices
{
    public interface IBookService
    {
        Task<BaseResponse<CreateBookDTO>> CreateAsync(CreateBookDTO newBook, string email);
        Task<List<Book?>> GetAllAsync();
        Task<Book?> GetAsync(string id);
        Task RemoveAsync(string id);
        Task UpdateAsync(string id, Book updatedBook);
    }
}