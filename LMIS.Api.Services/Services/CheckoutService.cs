using AutoMapper;
using LMIS.Api.Core.DTOs;
using LMIS.Api.Core.DTOs.Book;
using LMIS.Api.Core.DTOs.Checkout;
using LMIS.Api.Core.DTOs.Member;
using LMIS.Api.Core.DTOs.User;
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

        public async Task<BaseResponse<BookDTO>> GetSelectedBooks(SearchBookDTO selectedBook, string memberCode, string userIdClaim)
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


                // check if they dont have any outstanding books not returned
                // get checkout transaction
                var lastTransaction = await _unitOfWork.Checkout.GetLastOrDefault(member.MemberId);
                if (lastTransaction == null)
                {
                    // Check if there are selected books
                    if (selectedBook == null)
                    {
                        return new ()
                        {
                            IsError = true,
                            Message = "The book is not available"
                        };
                    }

                    // Check if the book is available
                    var allBooks = await _bookService.GetAllAsync();
                    var getBook = allBooks.FirstOrDefault(b =>
                        b.Title == selectedBook.Title.ToUpper());

                    if (getBook != null)
                    {
                        var getGenre = await _unitOfWork.Genre.GetFirstOrDefaultAsync(genre => genre.Name == getBook.Genre);

                        if (getGenre == null)
                        {
                            return new ()
                            {
                                IsError = true,
                                Message = "failed to get the book "
                            };
                        }

                        // get all transactions that happened today and by this member
                        var getAllTransactions = await _unitOfWork.Checkout.GetAllTransactions();

                        if (getAllTransactions != null && getAllTransactions.Any())
                        {
                            var memberTransactions = getAllTransactions
                             .Where(t => t.member.Equals(member) &&
                                         t.CheckOutDate.Date == DateTime.Today &&
                                         t.book != null &&
                                         t.book.Genre == getGenre.Name)
                             .ToList();

                            if (memberTransactions.Count() > 0 && memberTransactions != null)
                            {
                                // check if count is more than the maximum limit
                                if (memberTransactions.Count() >= getGenre.MaximumBooksAllowed)
                                {
                                    return new ()
                                    {
                                        IsError = true,
                                        Message = "The member has reached his or her maximum borrowing limit"
                                    };
                                }

                                var getBookDTO = _mapper.Map<BookDTO>(getBook);
                                availableBook = getBookDTO;
                                // store transaction details in the temp_data table
                                var newTemp_Data = new Temp_Data
                                {
                                    BookId = getBook.Id,
                                    Member_Code = memberCode
                                }; 

                                await _unitOfWork.temp_DataRepository.CreateAsync(newTemp_Data);
                                _unitOfWork.Save();


                                return new()
                                {
                                    IsError = false,
                                    Result = availableBook
                                };
                               
                            }
                        }
                        else if (getAllTransactions == null || getAllTransactions.Count() == 0)
                        {
                            var getBookDTO = _mapper.Map<BookDTO>(getBook);
                            availableBook = getBookDTO;

                            // store transaction details in the temp_data table
                            var newTemp_Data = new Temp_Data
                            {
                                BookId = getBook.Id,
                                Member_Code = memberCode
                            };
                            await _unitOfWork.temp_DataRepository.CreateAsync(newTemp_Data);
                            _unitOfWork.Save();

                            return new()
                            {
                                IsError = false,
                                Result = availableBook
                            };
                            
                        }
                        else
                        {
                            // check there is any book return overdue
                            var overDueBooks = getAllTransactions.Where(T => T.isReturned == false).ToList();
                            if (overDueBooks.Any())
                            {
                                var OverDuefine = 1000 * overDueBooks.Count();

                                // send the notification to the member via email

                                string pinBody = "You have " + overDueBooks.Count() + "that are over due " + " your over due fine is  MWK" + OverDuefine + " be informed that you will not be able to borrow any book  until these books are returned and the fine is setlled ";
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
                                catch (Exception ex)
                                {
                                    return new ()
                                    {
                                        IsError = true,
                                        Message = $"Failed create a new notification "
                                    };
                                }
                            }

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                return new()
                {
                    IsError = true,
                    Message = $"an {ex.Message} error occured. failed to get a book "
                };
            }
            return new()
            {
                IsError = true,
                Message = $"failed to get a book "
            };
        }
        public async Task<BaseResponse<bool>> CheckOutBook(string userIdClaim)
        {
            try
            {
                if (string.IsNullOrEmpty(userIdClaim))
                {
                    return new()
                    {
                        IsError = true,
                        Message = "Failed to identify the member "
                    };
                }

                // get  the item in temp_data
                var tempDatas = await _unitOfWork.temp_DataRepository.GetAllCheckoutTransactions();
                if (tempDatas == null)
                {
                    return new()
                    {
                        IsError = true,
                        Message = "failed process the transaction"
                    };
                }

                // get the last entry
                var lastEntry = tempDatas.LastOrDefault();

                // get bookId

                var getBook = (await _bookService.GetAllAsync())
                                ?.FirstOrDefault(b => b.Id == lastEntry?.BookId);

                if (getBook == null)
                {
                    return new()
                    {
                        IsError = true,
                        Message = "Failed to identify the book selected "
                    };
                }

                var user = await _unitOfWork.User.GetFirstOrDefaultAsync(u => u.Email == userIdClaim);
                var bookInventory = await _unitOfWork.BookInventory.GetFirstOrDefaultAsync(b => b.BookId == getBook.Id);
                var member = await _unitOfWork.member.GetFirstOrDefaultAsync(m => m.Member_Code == lastEntry.Member_Code);

                if (user == null || bookInventory == null || member == null)
                {

                    return new()
                    {
                        IsError = true,
                        Message = "Failed to process the transaction "
                    };
                }

                var newCheckoutTransaction = new CheckoutTransaction
                {

                    BookId = getBook.Id,
                    CheckOutDate = DateTime.Today,
                    DueDate = DateTime.Today.AddDays(7),

                    bookInventoryId = bookInventory.Id,
                    isReturned = false,
                    MemberId = member.MemberId,

                    user = user,
                };

                await _unitOfWork.Checkout.CreateAsync(newCheckoutTransaction);
                _unitOfWork.Save();

                return new()
                {
                    IsError = false,
                    Message = "The checkout Transaction was successful"
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

                var getTransaction = allTransactions.FirstOrDefault(T => T.member == member && T.book == getBook && T.isReturned == false);
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
