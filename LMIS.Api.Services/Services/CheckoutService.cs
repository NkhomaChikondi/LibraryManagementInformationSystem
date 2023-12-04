using AutoMapper;
using LMIS.Api.Core.DTOs.Book;
using LMIS.Api.Core.Model;
using LMIS.Api.Core.Repository.IRepository;
using LMIS.Api.Services.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Services.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IBookService _bookService;

        private  BookDTO availableBook;
        private readonly ILogger<BookDTO> _logger;

        public CheckoutService(IUnitOfWork unitOfWork, IMapper Mapper, IEmailService emailService, IBookService bookService)
        {
            _mapper = Mapper;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _bookService = bookService;
           
        }

        public async Task<BookDTO> GetSelectedBooks(SearchBookDTO selectedBook, string memberCode, string userIdClaim)
        {
            try
            {
                var userEmail = userIdClaim;
                var user = await _unitOfWork.User.GetFirstOrDefaultAsync(user => user.Email == userEmail);
                if (user == null)
                {
                    return null;
                }
                // Verify the member through the member code
                var member = await _unitOfWork.member.GetFirstOrDefaultAsync(m => m.Member_Code == memberCode);
                if (member == null)
                {
                    return null;
                }
                // check if they dont have any outstanding books not returned
                // get checkout transaction
                var lastTransaction = await _unitOfWork.Checkout.GetLastOrDefaultAsync( c => c.MemberId == member.MemberId );
                if (lastTransaction != null)
                {
                  // check if the last book was returned
                  if(lastTransaction.isReturned)
                  {
                        // Check if there are selected books
                        if (selectedBook == null)
                        {
                            return null;
                        }

                        // Check if the book is available
                        var allBooks = await _bookService.GetAllAsync();
                        var getBook = allBooks.FirstOrDefault(b =>
                            b.Title == selectedBook.Title);

                        if (getBook != null)
                        {
                            var getGenre = await _unitOfWork.Genre.GetFirstOrDefaultAsync(genre => genre.Name == getBook.Genre);

                            if (getGenre == null)
                            {
                                return null;
                            }

                            var memberGenres = _unitOfWork.memberGenre.GetAllAsync();
                            if (memberGenres == null)
                            {
                                // create a new memberGenre
                                var newMemberGenre = new MemberGenre
                                {
                                    GenreName = getGenre.Name,
                                    memberCode = memberCode,
                                    Counter = 1
                                };

                                await _unitOfWork.memberGenre.CreateAsync(newMemberGenre);
                                _unitOfWork.Save();

                                var getBookDTO = _mapper.Map<BookDTO>(getBook);
                                availableBook = getBookDTO;
                                return getBookDTO;

                            }
                            else if (memberGenres != null)
                            {
                                var getMemberGenreRecord = memberGenres.FirstOrDefault(m =>
                                m.memberCode == memberCode && m.GenreName == getGenre.Name);
                                if (getMemberGenreRecord == null)
                                {
                                    // create a new memberGenre
                                    var newMemberGenre = new MemberGenre
                                    {
                                        GenreName = getGenre.Name,
                                        memberCode = memberCode,
                                        Counter = 1
                                    };

                                    await _unitOfWork.memberGenre.CreateAsync(newMemberGenre);
                                    _unitOfWork.Save();

                                    var getBookDTO = _mapper.Map<BookDTO>(getBook);
                                    availableBook = getBookDTO;
                                    return getBookDTO;
                                }
                                else if (memberGenres != null)
                                {
                                    if (getMemberGenreRecord.Counter <= getGenre.MaximumBooksAllowed)
                                    {
                                        var getBookDTO = _mapper.Map<BookDTO>(getBook);

                                        getMemberGenreRecord.Counter++;
                                        _unitOfWork.memberGenre.Update(getMemberGenreRecord);
                                        _unitOfWork.Save();
                                        availableBook = getBookDTO;
                                        return getBookDTO;
                                    }
                                    else if (getMemberGenreRecord.Counter > getGenre.MaximumBooksAllowed)
                                    {
                                        return null;
                                    }

                                }
                            }
                        }
                    }
                  else
                  {

                  }
                }
              
                return null;   
            }          
            catch (Exception)
            {
                      
                return null;
            }
        }
       
             
            



    }
}
