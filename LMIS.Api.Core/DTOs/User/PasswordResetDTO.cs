using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.DTOs.User
{
    public class PasswordResetDTO
    {
        public string Email { get; set; }

        public int Pin { get; set; }

        public string Password { get; set; }
    }
}
