using AutoMapper;
using LMIS.Api.Core.DTOs.Member;
using LMIS.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.Mapper
{
    public class MemberMapper : Profile
    {
        public MemberMapper()
        {
            CreateMap<MemberDTO, Member>().ReverseMap();
        }
    }
}
