using LMIS.Api.Core.Model;
using LMIS.Api.Services.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Services.Services
{
    public class BorrowedBookService : IBorrowedBookService
    {

        private readonly Dictionary<string, BorroedBooksByMember> _dataStore;

        public BorrowedBookService()
        {
            _dataStore = new Dictionary<string, BorroedBooksByMember>();
        }

        public Task<BorroedBooksByMember> GetBorrowedBooksByMember(string memberCode)
        {
            if (_dataStore.ContainsKey(memberCode))
            {
                return Task.FromResult(_dataStore[memberCode]);
            }
            return Task.FromResult<BorroedBooksByMember>(null);
        }

        public Task UpdateBorrowedBooks(BorroedBooksByMember borrowedBooks)
        {
            _dataStore[borrowedBooks.MemberCode] = borrowedBooks;
            return Task.CompletedTask;
        }

    }
}
