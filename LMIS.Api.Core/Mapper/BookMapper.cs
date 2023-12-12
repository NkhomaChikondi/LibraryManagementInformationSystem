using AutoMapper;
using LMIS.Api.Core.DTOs.Book;
using LMIS.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.Mapper
{
    public class BookMapper : Profile
    {
        public BookMapper()
        {
            CreateMap<BookDTO,Book>().ReverseMap();
            CreateMap<CreateBookDTO,Book>().ReverseMap();
        }

    }
}
