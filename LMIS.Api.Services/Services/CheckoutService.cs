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
                var member = await _unitOfWork.member.GetFirstOrDefaultAsync(m => m.Member_Code == memberCode);

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
                var outstandingTransaction = memberCheckoutTransactions.Where(t => !t.isReturned && t .CheckOutDate < DateTime.Today).ToList();
                
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
                    var bookInventory = await _unitOfWork.BookInventory.GetFirstOrDefaultAsync(b => b.BookId == getBook.Id && b.isAvailable && !b.IsDeleted);

                    if (bookInventory == null)
                    {
                        return new()
                        {
                            IsError = true,
                            Message = "The the book is not available"
                        };
                    }

                    // Check if the book is already checked out
                    var isBookAlreadyCheckedOut = await _unitOfWork.BookInventory.GetFirstOrDefaultAsync(b => b.BookId == getBook.Id && b.isAvailable);

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
                        isReturned = false,
                        IsDeleted = false,
                        bookInventoryId = bookInventory.Id,
                       
                    };

                     await _unitOfWork.Checkout.CreateAsync(checkoutTransaction);
                    _unitOfWork.Save();

                    // Update book inventory status
                    bookInventory.isAvailable = false; 
                    bookInventory.checkoutTransactions.Add(checkoutTransaction); 

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

       
        public async Task<BaseResponse<bool>> ReturnBook(string memberId, string Booktitle)
        {
            try
            {
                if (string.IsNullOrEmpty(memberId) || string.IsNullOrEmpty(Booktitle))
                {
                    return new()
                    {
                        IsError = true,
                        Message = "Check if all details are entered correctly"
                    };
                }

                var member = await _unitOfWork.member.GetFirstOrDefaultAsync(m => m.Member_Code == memberId);
                if (member == null)
                {
                    return new()
                    {
                        IsError = true,
                        Message = "Failed to identify the member "
                    };
                }

                var allTransactions =  _unitOfWork.Checkout.GetAllAsync();
                if (allTransactions == null || !allTransactions.Any())
                {
                    return new()
                    {
                        IsError = true,
                        Message = "Failed to get the transaction "
                    };

                }

                var books = await _bookService.GetAllAsync();
                if (books == null)
                {
                    return new()
                    {
                        IsError = true,
                        Message = "Failed to get the transaction "
                    };
                }                  

                // Convert Booktitle to uppercase
                Booktitle = Booktitle.ToUpper();

                var getBook = books.FirstOrDefault(b => b.Title.ToUpper() == Booktitle);
                if (getBook == null)
                {
                    return new()
                    {
                        IsError = true,
                        Message = "The book cannot be found "
                    };

                }

                var getTransaction = allTransactions.FirstOrDefault(T => T.member == member && T.BookId == getBook.Id && T.isReturned == false);
                if (getTransaction == null)
                { 
                    return new()
                    {
                        IsError = true,
                        Message = "Failed to get the transaction "
                    };
                }

                getTransaction.isReturned = true;

                _unitOfWork.Checkout.Update(getTransaction);
                 _unitOfWork.Save();
                return new()
                {
                    IsError = false,
                    Message = "Transaction updated successfully "
                };
            }
            catch (Exception)
            {
                return new()
                {
                    IsError = true,
                    Message = "An error occured whilst returning a book"
                };
            }
        }

        public async Task<BaseResponse<IEnumerable<CheckoutDTO>>> GetAllCheckoutTransactions()
        {
            try
            {
                var allTransactions = _unitOfWork.Checkout.GetAllAsync();
                // Map the updated member entity to a DTO
                var allTransactionDTO = _mapper.Map<IEnumerable<CheckoutDTO>>(allTransactions);

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
