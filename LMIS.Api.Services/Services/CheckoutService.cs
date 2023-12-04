using AutoMapper;
using LMIS.Api.Core.DTOs.Book;
using LMIS.Api.Core.DTOs.Member;
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
        private  string memberCode;
        private ApplicationUser user;
        private Member getMember;
     
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
                if (lastTransaction == null) 
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

                        var memberGenres = await _unitOfWork.memberGenre.GetFirstOrDefaultAsync(m => m.memberCode == memberCode);
                        if (memberGenres == null)
                        {
                            // create a new memberGenre
                            var newMemberGenre = new MemberGenre
                            {
                                GenreName = getGenre?.Name,
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
                            if (memberGenres.Counter <= getGenre.MaximumBooksAllowed)
                            {
                                var getBookDTO = _mapper.Map<BookDTO>(getBook);

                                memberGenres.Counter++;
                                _unitOfWork.memberGenre.Update(memberGenres);
                                _unitOfWork.Save();
                                availableBook = getBookDTO;
                                return getBookDTO;
                            }
                            else if (memberGenres.Counter > getGenre.MaximumBooksAllowed)
                            {
                                return null;
                            }

                        }                        
                    }
                }
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

                            var memberGenres = await _unitOfWork.memberGenre.GetFirstOrDefaultAsync( m => m.memberCode == memberCode);
                            if (memberGenres == null )
                            {
                                // create a new memberGenre
                                var newMemberGenre = new MemberGenre
                                {
                                    GenreName = getGenre?.Name,
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
                                if (memberGenres.Counter <= getGenre.MaximumBooksAllowed)
                                {
                                    var getBookDTO = _mapper.Map<BookDTO>(getBook);

                                    memberGenres.Counter++;
                                    _unitOfWork.memberGenre.Update(memberGenres);
                                    _unitOfWork.Save();
                                    availableBook = getBookDTO;
                                    return getBookDTO;
                                }
                                else if (memberGenres.Counter > getGenre.MaximumBooksAllowed)
                                {
                                    return null;
                                }

                            }
                            
                        }
                    }
                  else
                  {
                        // get all transactions that are over due
                    var overdueTransactions=    await _unitOfWork.Checkout.GetOverDueTransaction(member);

                        var OverDuefine = 1000 * overdueTransactions;

                        // send the notification to the member via email

                        string pinBody = "You have " + overdueTransactions + "that are over due " + " your over due fine is  MWK" + OverDuefine + " be informed that you will not be able to borrow any book  until these books are returned and the fine is setlled ";
                        _emailService.SendMail(member.Email, "OverDue Fine", pinBody);

                        try
                        {
                            // create a new instance of notification
                            var newNotification = new Notification
                            {
                                Messsage = pinBody,
                                member = member,
                                user = user,
                                memberId = member.MemberId,
                                userId = user.UserId
                            };
                            await _unitOfWork.notification.CreateAsync(newNotification);
                            _unitOfWork.Save();
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                       
                    }
                }
              
                return null;   
            }          
            catch (Exception)
            {
                      
                return null;
            }
        }

        public async Task CheckOutBook(SearchBookDTO book, string memberCode, string userIdClaim)
        {
            // get book 
            var getAllBooks = await _bookService.GetAllAsync();
            if (getAllBooks != null) { return; }

            var getBook = getAllBooks.Where(b => b.Title == book.Title).FirstOrDefault();
            
            if (getBook == null)
                return;
            if (memberCode == null)
                return;
            if (string.IsNullOrEmpty(userIdClaim))
                return;            
            // get user
            var user = await _unitOfWork.User.GetFirstOrDefaultAsync(b => b.Email == userIdClaim);
            if (user == null)
                return;
            

            // get the book inventory that is having this book
            var bookInventory = await _unitOfWork.BookInventory.GetFirstOrDefaultAsync(b => b.BookId == getBook.Id);

            if (bookInventory == null)
                return;

            // get member 
            var member = await _unitOfWork.member.GetFirstOrDefaultAsync(b => b.Member_Code == memberCode);
            if (member == null)
                return;

            // create a new checkout transaction
            var newCheckoutTransaction = new CheckoutTransaction
            {
                book = getBook,
                BookId = getBook.Id,
                CheckOutDate = DateTime.Today,
                DueDate = DateTime.Today.AddDays(7),
                bookInventory = bookInventory,  
                bookInventoryId = bookInventory.Id,
                isReturned = false,
                MemberId = member.MemberId,
                member = member,
                user = user,
            };

            // save in the database
            await _unitOfWork.Checkout.CreateAsync(newCheckoutTransaction);
            _unitOfWork.Save();
        }
       
    }
}
