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

       

        public CheckoutService(IUnitOfWork unitOfWork, IMapper Mapper, IEmailService emailService, IBookService bookService)
        {
            _mapper = Mapper ?? throw new ArgumentNullException(nameof(Mapper));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
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
                    return new BaseResponse<CheckoutDTO>
                    {
                        IsError = true,
                        Message = "Check if the member code is entered correctly"
                    };
                }

                var memberCheckoutTransactions = (_unitOfWork.Checkout.GetAllAsync())
                    .Where(m => m.MemberId == member.MemberId)
                    .ToList();

                var outstandingTransaction = memberCheckoutTransactions
                    .Where(t => !t.IsReturned && t.CheckOutDate < DateTime.Today)
                    .ToList();

                if (outstandingTransaction.Count > 0)
                {
                    var overdueFine = 1000 * outstandingTransaction.Count;

                    string overdueBody = $"You have {outstandingTransaction.Count} books that are overdue. Your overdue fine is MWK {overdueFine}. Please return these books to borrow again.";
                    _emailService.SendMail(member.Email, "Overdue Fine", overdueBody);

                    return new BaseResponse<CheckoutDTO>
                    {
                        IsError = true,
                        Message = "You have an outstanding book unreturned"
                    };
                }

                var Books = await _bookService.GetAllAsync();

                var getBook = Books?.FirstOrDefault(B => B.Title == selectedBook.Title.ToUpper() && B.ISBN == selectedBook.ISBN && !B.IsDeleted);

                if (getBook != null)
                {
                    var getGenre = await _unitOfWork.Genre.GetFirstOrDefaultAsync(genre => genre.Name == getBook.Genre);

                    if (getGenre != null)
                    {
                        var memberTransactions = memberCheckoutTransactions
                            .Where(t => t.MemberId == member.MemberId && t.CheckOutDate.Date == DateTime.Today.Date)
                            .ToList();

                        if (memberTransactions.Any())
                        {
                            int genreCount = memberTransactions
                                .Count(memberTransaction => Books?.FirstOrDefault(b => b.Id == memberTransaction.BookId && b.Genre == getGenre.Name) != null);

                            if (genreCount >= getGenre.MaximumBooksAllowed)
                            {
                                return new BaseResponse<CheckoutDTO>
                                {
                                    IsError = true,
                                    Message = "The member has reached the maximum borrowing limit"
                                };
                            }
                        }

                        var bookInventory = await _unitOfWork.BookInventory.GetFirstOrDefaultAsync(b => b.BookId == getBook.Id && b.IsAvailable && !b.IsDeleted);

                        if (bookInventory == null)
                        {
                            return new BaseResponse<CheckoutDTO>
                            {
                                IsError = true,
                                Message = "The book is not available"
                            };
                        }

                        var isBookAlreadyCheckedOut = await _unitOfWork.BookInventory.GetFirstOrDefaultAsync(b => b.BookId == getBook.Id && b.IsAvailable);

                        if (isBookAlreadyCheckedOut == null)
                        {
                            return new BaseResponse<CheckoutDTO>
                            {
                                IsError = true,
                                Message = "The transaction failed, the book is already borrowed"
                            };
                        }

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

                        bookInventory.IsAvailable = false;
                        bookInventory.CheckoutTransactions.Add(checkoutTransaction);

                        _unitOfWork.BookInventory.Update(bookInventory);
                        _unitOfWork.Save();

                        string pinBody = $"You have borrowed {getBook.Title}. The book is to be returned on {checkoutTransaction.DueDate}";
                        _emailService.SendMail(member.Email, "Borrowed Book", pinBody);

                        var checkoutDetails = new CheckoutDTO
                        {
                            Book = getBook.Title,
                            DueDate = checkoutTransaction.DueDate,
                            CheckOutDate = checkoutTransaction.DueDate,
                        };

                        return new BaseResponse<CheckoutDTO>
                        {
                            IsError = false,
                            Result = checkoutDetails,
                            Message = "Transaction is successful"
                        };
                    }
                }

                return new BaseResponse<CheckoutDTO>
                {
                    IsError = true,
                    Message = "Book details not found or incorrect"
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<CheckoutDTO>
                {
                    IsError = true,
                    Message = $"An error occurred: {ex.Message}. Transaction failed."
                };
            }
        }
        public async Task<BaseResponse<bool>> ReturnBook(ReturnBookDTO returnBookDTO, string userIdClaim)
        {
            try
            {
                var userEmail = userIdClaim;
                string lmisBookId = "";
                var user = await _unitOfWork.User.GetFirstOrDefaultAsync(u => u.Email == userEmail);
                var member = await _unitOfWork.Member.GetFirstOrDefaultAsync(m => m.MemberCode == returnBookDTO.MemberCode);

                if (user == null || member == null)
                {
                    return new BaseResponse<bool>
                    {
                        IsError = true,
                        Message = "Check if the member code is entered correctly."
                    };
                }

                try
                {
                    var books = await _bookService.GetAllAsync();
                    var getBook = books?.FirstOrDefault(b => b.ISBN == returnBookDTO.ISBN);
                    if (getBook == null)
                    {
                        return new BaseResponse<bool>
                        {
                            IsError = true,
                            Message = "Book not found."
                        };
                    }
                    lmisBookId = getBook.Id;
                }
                catch (Exception)
                {
                    return new BaseResponse<bool>
                    {
                        IsError = true,
                        Message = "Error occurred while finding the book."
                    };
                }

                var bookInventory = await _unitOfWork.BookInventory.GetFirstOrDefaultAsync(b => b.BookId == lmisBookId && !b.IsDeleted);

                if (bookInventory == null || bookInventory.IsAvailable)
                {
                    return new BaseResponse<bool>
                    {
                        IsError = true,
                        Message = "The book is not checked out or does not exist."
                    };
                }

                var checkoutTransaction = await _unitOfWork.Checkout.GetFirstOrDefaultAsync(t => t.BookId == lmisBookId && !t.IsReturned);

                if (checkoutTransaction == null)
                {
                    return new BaseResponse<bool>
                    {
                        IsError = true,
                        Message = "No active checkout transaction found for the book."
                    };
                }

                checkoutTransaction.IsReturned = true;
                _unitOfWork.Checkout.Update(checkoutTransaction);
                _unitOfWork.Save();

                bookInventory.IsAvailable = true;
                _unitOfWork.BookInventory.Update(bookInventory);
                _unitOfWork.Save();

                var book = await _bookService.GetAsync(lmisBookId);

                var returnConfirmation = new CheckoutDTO
                {
                    Book = book?.Title ?? "Book Not Found",
                    DueDate = checkoutTransaction.DueDate,
                };

                if (DateTime.Today > checkoutTransaction.DueDate)
                {
                    TimeSpan overdueDuration = DateTime.Today - checkoutTransaction.DueDate;
                    int daysOverdue = overdueDuration.Days;

                    var overdueFine = 1000 * daysOverdue;

                    string overdueBody = $"You have {daysOverdue} days overdue. Your overdue fine is MWK {overdueFine}. You won't be able to borrow until these books are returned and the fine is settled.";
                    _emailService.SendMail(member.Email, "Overdue Fine", overdueBody);
                }

                string returnBody = $"You have successfully returned the book: {returnConfirmation.Book}.";
                _emailService.SendMail(member.Email, "Book Return Confirmation", returnBody);

                return new BaseResponse<bool>
                {
                    IsError = false,
                    Message = "Book return process completed successfully."
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>
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
                var allTransactions = _unitOfWork.Checkout.GetAllTransactions();

                var allTransactionDTO = new List<CheckoutDTO>();
                var books = await _bookService.GetAllAsync();

                foreach (var item in allTransactions)
                {
                    var book = books?.FirstOrDefault(b => b.Id == item.BookId);
                    if (book != null)
                    {
                        var newTransaction = new CheckoutDTO()
                        {
                            Book = book.Title ?? "Book Not Found",
                            DueDate = item.DueDate,
                            CheckOutDate = item.CheckOutDate,
                        };
                        allTransactionDTO.Add(newTransaction);
                    }
                }
                if (allTransactions == null)
                    return new()
                    {
                        IsError = true,
                        Message = "No checkout transaction found"
                    };

                return new BaseResponse<IEnumerable<CheckoutDTO>>
                {
                    IsError = false,
                    Result = allTransactionDTO,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<CheckoutDTO>>
                {
                    IsError = true,
                    Message = $"Failed to get all transactions. An error occurred: {ex.Message}",
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
