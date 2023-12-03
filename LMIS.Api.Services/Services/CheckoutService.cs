using AutoMapper;
using LMIS.Api.Core.DTOs.Book;
using LMIS.Api.Core.Model;
using LMIS.Api.Core.Repository.IRepository;
using LMIS.Api.Services.Services.IServices;
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
        private readonly List<BookDTO> availableBooks = new List<BookDTO>();
        private readonly ILogger<BookDTO> _logger;

        public CheckoutService(IUnitOfWork unitOfWork, IMapper Mapper, IEmailService emailService, IBookService bookService)
        {
            _mapper = Mapper;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _bookService = bookService;
        }

        public async Task<List<BookDTO>> GetSelectedBooks(SearchBookListDTO selectedBooks, string memberCode)
        {
            try
            {
                // Verify the member through the member code
                var member = await _unitOfWork.member.GetFirstOrDefaultAsync(m => m.Member_Code == memberCode);
                if (member == null)
                {
                    return null;
                }

                if (selectedBooks == null || selectedBooks.Books == null || !selectedBooks.Books.Any())
                {
                    return null;
                }
               
                // Loop through the books and check if each of those is available
                foreach (var book in selectedBooks.Books)
                {
                    try
                    {
                        if (book == null)
                        {
                            return null;
                        }

                        // Check if the book is available
                        var allBooks = await _bookService.GetAllAsync();
                        var getBook = allBooks.FirstOrDefault(b =>
                            b.Title == book.Title || b.Publisher == book.Publisher || b.Author == book.Author);

                        // Get the book by the book
                        if (getBook != null)
                        {
                            var getBookDTO = _mapper.Map<BookDTO>(getBook);
                            availableBooks.Add(getBookDTO);
                        }
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }

                return availableBooks;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
