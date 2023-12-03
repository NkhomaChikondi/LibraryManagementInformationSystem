using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.Model
{
    public class BorrowedBookByGenre
    {
        public int GenreId { get; set; }
        public int BorrowedCount { get; set; }
    }
}
