using AutoMapper;
using LMIS.Api.Core.DTOs.Genre;
using LMIS.Api.Core.DTOs.Member;
using LMIS.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.Mapper
{
    public class genreMapper : Profile
    {
        public genreMapper()
        {
            CreateMap<GenreDTO, Genre>().ReverseMap();
        }
    }
}
