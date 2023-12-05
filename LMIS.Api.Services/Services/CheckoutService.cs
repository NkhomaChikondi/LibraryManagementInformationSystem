﻿using AutoMapper;
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

        public async Task<BookDTO> GetSelectedBooks(SearchBookDTO selectedBook, string memberCode, string userIdClaim)
        {
            try
            {
                var userEmail = userIdClaim;
                var user = await _unitOfWork.User.GetFirstOrDefaultAsync(u => u.Email == userEmail);
                var member = await _unitOfWork.member.GetFirstOrDefaultAsync(m => m.Member_Code == memberCode);

                if (user == null || member == null)
                    return null;


                // check if they dont have any outstanding books not returned
                // get checkout transaction
                var lastTransaction = await _unitOfWork.Checkout.GetLastOrDefault(member.MemberId);
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

                        // get all transactions that happened today and by this member
                        var getAllTransactions = _unitOfWork.Checkout.GetAllAsync();

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
                                    return null;

                                var getBookDTO = _mapper.Map<BookDTO>(getBook);
                                availableBook = getBookDTO;
                                return getBookDTO;
                            }
                        }
                        else if (getAllTransactions == null && getAllTransactions.Count() == 0)
                        {
                            var getBookDTO = _mapper.Map<BookDTO>(getBook);
                            availableBook = getBookDTO;
                            return getBookDTO;

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
                                    return null;
                                }
                            }

                        }

                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }

        public async Task CheckOutBook(SearchBookDTO book, string memberCode, string userIdClaim)
        {
            if (string.IsNullOrEmpty(memberCode) || string.IsNullOrEmpty(userIdClaim))
                return;

            var getBook = (await _bookService.GetAllAsync())
                            ?.FirstOrDefault(b => b.Title == book.Title);

            if (getBook == null)
                return;

            var user = await _unitOfWork.User.GetFirstOrDefaultAsync(u => u.Email == userIdClaim);
            var bookInventory = await _unitOfWork.BookInventory.GetFirstOrDefaultAsync(b => b.BookId == getBook.Id);
            var member = await _unitOfWork.member.GetFirstOrDefaultAsync(m => m.Member_Code == memberCode);

            if (user == null || bookInventory == null || member == null)
                return;

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

            await _unitOfWork.Checkout.CreateAsync(newCheckoutTransaction);
            _unitOfWork.Save();
        }

        public async Task ReturnBook(string memberId, string Booktitle)
        {
            // check if member id is not null
            if(memberId == null) return;
            var member = await _unitOfWork.member.GetFirstOrDefaultAsync(m => m.Member_Code == memberCode);
            if (member == null) return;

            // get all checkoutTransaction
            var  allTransaction = _unitOfWork.Checkout.GetAllAsync();

            if (allTransaction == null && allTransaction.Any()) return;

            // get the book
            var books =await _bookService.GetAllAsync();
            if (books == null) return;

            var getBook = books.Where(b => b.Title == Booktitle).FirstOrDefault();
            if (getBook == null) return;

            // check the last id 
            var getTransaction = 

        }
    }
}
