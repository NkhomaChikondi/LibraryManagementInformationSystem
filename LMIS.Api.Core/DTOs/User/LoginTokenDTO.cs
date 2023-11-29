using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.DTOs.User
{
    public class LoginTokenDTO
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }

        public string TokenType { get; set; }
        public DateTime TokenExpiryMinutes { get; set; }
    }
}
