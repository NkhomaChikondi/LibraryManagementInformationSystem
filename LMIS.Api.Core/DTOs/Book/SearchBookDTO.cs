using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.DTOs.Book
{
    public class SearchBookDTO
    {
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Genre { get; set; }
    }
}
