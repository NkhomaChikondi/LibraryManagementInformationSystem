using AutoMapper;
using LMIS.Api.Core.DTOs;
using LMIS.Api.Core.DTOs.Book;
using LMIS.Api.Core.DTOs.Checkout;
using LMIS.Api.Core.DTOs.Member;
using LMIS.Api.Core.DTOs.User;
using LMIS.Api.Core.Model;
using LMIS.Api.Core.Repository;
using LMIS.Api.Core.Repository.IRepository;
using LMIS.Api.Services.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
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

        private BookDTO availableBook;
        private string memberCode;
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

        public async Task<BaseResponse<CheckoutDTO>> CheckoutBook(SearchBookDTO selectedBook, string memberCode, string userIdClaim)
        {
            try
            {
                var userEmail = userIdClaim;
                var user = await _unitOfWork.User.GetFirstOrDefaultAsync(u => u.Email == userEmail);
                var member = await _unitOfWork.Member.GetFirstOrDefaultAsync(m => m.MemberCode == memberCode);

                if (user == null || member == null)
                {
                    return new ()
                    {
                        IsError = true,
                        Message = "Check if the member code is entered correctly"
                    };
                }

                // get all transactions
                var memberCheckoutTransactions =  _unitOfWork.Checkout.GetAllAsync().Where(m => m.MemberId == member.MemberId).ToList();

                // check if there is any outstanding transaction not finalised
                var outstandingTransaction = memberCheckoutTransactions.Where(t => !t.IsReturned && t .CheckOutDate < DateTime.Today).ToList();
                
                if(outstandingTransaction.Count > 0 )
                {
                    var OverDuefine = 1000 * outstandingTransaction.Count();

                    // send the notification to the member via email

                    string overDueBody = "You have " + outstandingTransaction.Count() + "that are over due " + " your over due fine is  MWK" + OverDuefine + " be informed that you will not be able to borrow any book  until these books are returned and the fine is setlled ";
                    _emailService.SendMail(member.Email, "OverDue Fine", overDueBody);

                    return new()
                    {
                        IsError = true,
                        Message = "You have an outstanding book un returned"
                    };
                   
                }

                var Books = await _bookService.GetAllAsync();

                var getBook = Books.Where(B => B.Title == selectedBook.Title.ToUpper() && B.ISBN == selectedBook.ISBN && !B.IsDeleted).FirstOrDefault();

                var getGenre = await _unitOfWork.Genre.GetFirstOrDefaultAsync(genre => genre.Name == getBook.Genre);
                                

                var memberTransactions = memberCheckoutTransactions
                             .Where(t => t.MemberId == member.MemberId &&
                                         t.CheckOutDate.Date == DateTime.Today.Date).ToList();                

                if (memberTransactions.Count() > 0 )
                {
                    int genreCount = 0;
                    foreach (var memberTransaction in memberTransactions)
                    {
                        // get book having the book id
                        var book = Books.Where(b => b.Id == memberTransaction.BookId).FirstOrDefault();
                        if (book != null)
                        {
                            if (book.Genre == getGenre.Name)
                            {
                                genreCount++;
                            }
                        }
                    }
                    // check if count is more than the maximum limit
                    if (genreCount >= getGenre.MaximumBooksAllowed)
                    {
                        return new()
                        {
                            IsError = true,
                            Message = "The member has reached his or her maximum borrowing limit"
                        };
                    }
                }

                    // Find the book inventory for the requested book
                    var bookInventory = await _unitOfWork.BookInventory.GetFirstOrDefaultAsync(b => b.BookId == getBook.Id && b.IsAvailable && !b.IsDeleted);

                    if (bookInventory == null)
                    {
                        return new()
                        {
                            IsError = true,
                            Message = "The the book is not available"
                        };
                    }

                    // Check if the book is already checked out
                    var isBookAlreadyCheckedOut = await _unitOfWork.BookInventory.GetFirstOrDefaultAsync(b => b.BookId == getBook.Id && b.IsAvailable);

                    if (isBookAlreadyCheckedOut == null )
                    {
                        return new()
                        {
                            IsError = true,
                            Message = "The transaction failed, book is already borrowed"
                        };
                    }

                    // Update book inventory and create checkout transaction
                    var currentDate = DateTime.UtcNow;
                    var dueDate = currentDate.AddDays(7); 

                    var checkoutTransaction = new CheckoutTransaction
                    {
                        CheckOutDate = currentDate,
                        DueDate = dueDate,
                        UserId = user.UserId,
                        BookId = getBook.Id,
                        MemberId = member.MemberId,
                        IsReturned = false,
                        IsDeleted = false,
                        BookInventoryId = bookInventory.Id,
                       
                    };

                     await _unitOfWork.Checkout.CreateAsync(checkoutTransaction);
                    _unitOfWork.Save();

                    // Update book inventory status
                    bookInventory.IsAvailable = false; 
                    bookInventory.CheckoutTransactions.Add(checkoutTransaction); 

                    _unitOfWork.BookInventory.Update(bookInventory);
                    _unitOfWork.Save();

                string pinBody = $"You have borrowed, { getBook.Title} , The book is to be returned on {checkoutTransaction.DueDate}";
                _emailService.SendMail(member.Email, "Borrowed Book", pinBody);

                var checkoutDetails = new CheckoutDTO
                {
                     book = getBook.Title,
                     DueDate = checkoutTransaction.DueDate,
                     CheckOutDate = checkoutTransaction.DueDate,
                };
                return new()
                {
                    IsError = false,
                    Result = checkoutDetails,
                    Message = "transaction is successful"
                };

            }
            catch (Exception ex)
            {
                return new()
                {
                    IsError = true,
                    Message = $"an {ex.Message} error occured. Transaction failed "
                };
            }
           
        }


        public async Task<BaseResponse<bool>> ReturnBook(ReturnBookDTO returnBookDTO, string userIdClaim)
        {
            try
            {
                // Retrieve user and member based on provided identifiers
                var userEmail = userIdClaim;
                string lmisBookId = null;
                var user = await _unitOfWork.User.GetFirstOrDefaultAsync(u => u.Email == userEmail);
                var member = await _unitOfWork.Member.GetFirstOrDefaultAsync(m => m.MemberCode == returnBookDTO.MemberCode);

                if (user == null || member == null)
                {
                    return new ()
                    {
                        IsError = true,
                        Message = "Check if the member code is entered correctly."
                    };
                }
                try
                {
                    // get book using the isbn
                    var books = await _bookService.GetAllAsync();
                    var getBook = books.Where(b => b.ISBN == returnBookDTO.ISBN).FirstOrDefault();
                    lmisBookId = getBook.Id;
                }
                catch (Exception)
                {
                    return new()
                    {
                        IsError = true,
                        Message = "book not found"
                    };
                }
                

                // Find the book inventory for the specified book
                var bookInventory = await _unitOfWork.BookInventory.GetFirstOrDefaultAsync(b => b.BookId == lmisBookId && !b.IsDeleted);

                if (bookInventory == null || bookInventory.IsAvailable)
                {
                    return new ()
                    {
                        IsError = true,
                        Message = "The book is not checked out or does not exist."
                    };
                }

                // Retrieve the checkout transaction related to the book
                var checkoutTransaction = await _unitOfWork.Checkout.GetFirstOrDefaultAsync(t => t.BookId == lmisBookId && !t.IsReturned);

                if (checkoutTransaction == null)
                {
                    return new ()
                    {
                        IsError = true,
                        Message = "No active checkout transaction found for the book."
                    };
                }

                // Update checkout transaction to mark the book as returned
                checkoutTransaction.IsReturned = true;
                _unitOfWork.Checkout.Update(checkoutTransaction);
                _unitOfWork.Save();

                // Update book inventory status to mark the book as available
                bookInventory.IsAvailable = true;
                _unitOfWork.BookInventory.Update(bookInventory);
                _unitOfWork.Save();

                // get the book having the checkout book id
                var book = await  _bookService.GetAsync(lmisBookId);
                // Send return confirmation notification
                var returnConfirmation = new CheckoutDTO
                {
                    book = book.Title,
                    DueDate = checkoutTransaction.DueDate,
                };

                if(DateTime.Today > checkoutTransaction.DueDate)
                {
                    TimeSpan overdueDuration = DateTime.Today - checkoutTransaction.DueDate;
                    int daysOverdue = overdueDuration.Days;

                    var OverDuefine = 1000 * daysOverdue;

                    // send the notification to the member via email

                    string overDueBody = "You have " + daysOverdue + "that are over due " + " your over due fine is  MWK" + OverDuefine + " be informed that you will not be able to borrow any book  until these books are returned and the fine is setlled ";
                    _emailService.SendMail(member.Email, "OverDue Fine", overDueBody);
                }

                // Send return confirmation email to member/user
                string returnBody = $"You have successfully returned the book: {returnConfirmation.book}.";
                _emailService.SendMail(member.Email, "Book Return Confirmation", returnBody);

                return new ()
                {
                    IsError = false,                    
                    Message = "Book return process completed successfully."
                };
            }
            catch (Exception ex)
            {
                return new ()
                {
                    IsError = true,
                    Message = $"An error occurred: {ex.Message}. Book return failed."
                };
            }
        }


        public async Task<BaseResponse<IEnumerable<CheckoutDTO>>> GetAllCheckoutTransactions()
        {
            try
            {
                var allTransactions = await _unitOfWork.Checkout.GetAllTransactions();
               
                var allTransactionDTO = new List<CheckoutDTO>();
                foreach (var item in allTransactions)
                {
                    // get book
                    var books = await _bookService.GetAllAsync();
                    var book = books.Where(b => b.Id == item.BookId).FirstOrDefault();
                   
                    var newTransaction = new CheckoutDTO()
                    {
                        book = book.Title,
                        DueDate = item.DueDate,
                        CheckOutDate = item.CheckOutDate,
                    };
                    allTransactionDTO.Add(newTransaction);
                }
                return new()
                {
                    IsError = false,
                    Result = allTransactionDTO,
                };
                return new()
                {
                    IsError = true,
                    Result = allTransactionDTO
                };
               
            }
            catch (Exception ex)
            {

                return new()
                {
                    IsError = true,
                    Message = $"Failed to get all transactions. an {ex.Message} error occured ",
                };
            }
        }
     
        public async Task<BaseResponse<CheckoutDTO>> GetCheckoutTransactionByIdAsync(int transId)
        {
            try
            {
                var _transaction = await _unitOfWork.Checkout.GetByIdAsync(transId);
                if (_transaction != null)
                {
                    if(_transaction.IsDeleted)
                    {
                        return new()
                        {
                            IsError = true,
                            Message = "No transaction found ",
                        };
                    }
                    var gettransactionDTO = _mapper.Map<CheckoutDTO>(_transaction);
                    return new ()
                    {
                        IsError = false,
                        Result = gettransactionDTO
                    };
                }
                return new ()
                {
                    IsError = true,
                    Message = $"No transaction found",
                };
            }
            catch (Exception ex)
            {
                return new()
                {
                    IsError = true,
                    Message = $"Failed to get transaction. an {ex.Message} error occured ",
                };
            }
        }
        public async Task<BaseResponse<bool>> DeleteTransactionAsync(int TransId)
        {
            try
            {
                await _unitOfWork.Checkout.SoftDeleteAsync(TransId);
                return new()
                {
                    IsError = false,
                    Message = "Deleted Successfully"
                };
            }
            catch (Exception ex)
            {
                return new()
                {
                    IsError = true,
                    Message = $"Failed to delete a user an {ex.Message} error occured"
                };
            }
        }

    }
}
