using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.Model
{
    public class Temp_Data
    {
        public int Id { get; set; }
        public string BookId { get; set; }
        public string Member_Code { get; set; }
        public bool IsDeleted { get; set; } = false;

    }
}
