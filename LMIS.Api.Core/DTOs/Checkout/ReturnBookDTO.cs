using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.DTOs.Checkout
{
    public class ReturnBookDTO
    {
        public string ISBN { get; set; } = string.Empty;
        public string MemberCode { get; set; } = string.Empty;
    }
}
