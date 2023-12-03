using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.DTOs.Book
{
    public class SearchBookListDTO
    {
        public List<SearchBookDTO> Books { get; set; }

        public SearchBookListDTO()
        {
            Books = new List<SearchBookDTO>();
        }
    }
}
