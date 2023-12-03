using LMIS.Api.Core.Model;

namespace LMIS.Api.Services.Services.IServices
{
    public interface IBorrowedBookService
    {
        Task<BorroedBooksByMember> GetBorrowedBooksByMember(string memberCode);
        Task UpdateBorrowedBooks(BorroedBooksByMember borrowedBooks);
    }
}