using AutoMapper;
using LMIS.Api.Core.DTOs.User;
using LMIS.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.Mapper
{
    public class LoginMapper : Profile
    {
        public LoginMapper()
        {
          CreateMap<ApplicationUser,LoginDTO>().ReverseMap();
        }
    }
}
