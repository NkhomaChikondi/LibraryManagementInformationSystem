using LMIS.Api.Core.Model;

namespace LMIS.Api.Services.Services.IServices
{
    public interface IBookService
    {
        Task CreateAsync(Book newBook, string email);
        Task<List<Book>> GetAsync();
        Task<Book?> GetAsync(string id);
        Task RemoveAsync(string id);
        Task UpdateAsync(string id, Book updatedBook);
    }
}