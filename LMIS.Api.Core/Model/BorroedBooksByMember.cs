using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.Model
{
    public class BorroedBooksByMember
    {
        public string MemberCode { get; set; }
        public List<BorrowedBookByGenre> BorrowedBooks { get; set; }

        public BorroedBooksByMember()
        {
            BorrowedBooks = new List<BorrowedBookByGenre>();
        }
    }
}
