using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.DTOs.Book
{
    public class BookDTO
    {

        public string? ISBN { get; set; }
        public int userId { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public int CopyNumber { get; set; }
        public string? ObtainedThrough { get; set; }
        public string? Publisher { get; set; }
        public string? Genre { get; set; }
        public string? Condition { get; set; }
        public bool isAvailable { get; set; }
        public string? Location { get; set; }
    }
}
