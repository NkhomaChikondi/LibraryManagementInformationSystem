using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.DTOs.Book
{
    public class BookDTO
    {

        public string ISBN { get; set; } = string.Empty;
        public int userId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int CopyNumber { get; set; }
        public string ObtainedThrough { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Condition { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
        public string Location { get; set; } = string.Empty;
    }
}
