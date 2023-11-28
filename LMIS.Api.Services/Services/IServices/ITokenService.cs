using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Services.Services.IServices
{
    public interface ITokenService
    {
        public string GenerateToken(string userId);
    }
}
