using AutoMapper;
using LMIS.Api.Core.DTOs;
using LMIS.Api.Core.DTOs.Book;
using LMIS.Api.Core.DTOs.Member;
using LMIS.Api.Core.Model;
using LMIS.Api.Core.Repository.IRepository;
using LMIS.Api.Services.Services.IServices;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Services.Services
{
    public class BookService : IBookService
    {
        private readonly IMongoCollection<Book> _booksCollection;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BookService(IOptions<BookDatabaseSettings> bookDatabaseSettings, IUnitOfWork unitOfWork, IMapper mapper)
        {
            var mongoClient = new MongoClient(bookDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(bookDatabaseSettings.Value.DatabaseName);

            _booksCollection = mongoDatabase.GetCollection<Book>(bookDatabaseSettings.Value.BooksCollectionName);
            _unitOfWork = unitOfWork;
        }
        public async Task<List<Book?>> GetAllAsync() =>      
           await _booksCollection.Find(book => !book.IsDeleted).ToListAsync();
      
    


        public async Task<Book?> GetAsync(string id) =>
     await _booksCollection.Find(book => book.Id == id && !book.IsDeleted).FirstOrDefaultAsync();

        public async Task<BaseResponse<CreateBookDTO>> CreateAsync(CreateBookDTO newBook, string userEmail)
        {
            try
            {
                if (newBook == null || string.IsNullOrEmpty(userEmail))
                {
                    return new()
                    {
                        IsError = true,
                        Message = "Make sure all book details are entered"
                    };
                }

                var user = await _unitOfWork.User.GetFirstOrDefaultAsync(u => u.Email == userEmail);
                if (user == null)
                {
                    return new()
                    {
                        IsError = true,
                        Message = "User doesnt exist"
                    };
                }

                var books = await GetAllAsync();
                if (books == null || books.Any(book => book.ISBN == newBook.ISBN))
                {
                    return new()
                    {
                        IsError = true,
                        Message = "No books in the system"
                    };
                }

                var CopyNumber = 1;

                // Convert book title to uppercase
                newBook.Title = newBook.Title.ToUpper();

                var similarBooks = books.Where(b => b.Title == newBook.Title && b.Author == newBook.Author).ToList();
                if (similarBooks != null)
                {
                    CopyNumber = similarBooks.Count;
                }

                var bookitem = new Book
                {
                    Title = newBook.Title,
                    Author = newBook.Author,
                    Publisher = newBook.Publisher,
                    CopyNumber = CopyNumber,
                    ObtainedThrough = newBook.ObtainedThrough,
                    Genre = newBook.Genre,
                    ISBN = newBook.ISBN,
                    CreatedOn = DateTime.UtcNow,
                    userId = user.UserId
                };

                bookitem.Id = "";

                await _booksCollection.InsertOneAsync(bookitem);

                try
                {
                    var bookInventory = new BookInventory
                    {
                        Condition = newBook.Condition,
                        isAvailable = newBook.isAvailable,
                        Location = newBook.Location,
                        BookId = bookitem.Id,
                        checkoutTransactions = null,
                    };

                    await _unitOfWork.BookInventory.CreateAsync(bookInventory);
                    _unitOfWork.Save();

                    var createdBookDTO = new CreateBookDTO
                    {
                        Title = bookitem.Title,
                        Author = bookitem.Author,
                        Publisher = bookitem.Publisher,
                        Genre = bookitem.Genre,
                        ISBN = bookitem.ISBN,
                        ObtainedThrough = bookitem.ObtainedThrough
                    };
                    return new()
                    {
                        IsError = false,
                        Result = createdBookDTO,
                    };
                }
                catch (Exception)
                {
                    return new()
                    {
                        IsError = true,
                        Message = "Failed to create a new book"
                    };
                }
            }
            catch (Exception ex)
            {
                return new()
                {
                    IsError = true,
                    Message = $"{ex.Message} error occured. Failed to create a new member"
                };
            }
        }

        public async Task UpdateAsync(string id, Book updatedBook) =>
            await _booksCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

        public async Task RemoveAsync(string id)
        {
            var book = await GetAsync(id);
            if (book != null)
            {
                book.IsDeleted = true;
                book.DeletedDate = DateTime.UtcNow;
                await UpdateAsync(id, book);
            }
        }
    }
}
