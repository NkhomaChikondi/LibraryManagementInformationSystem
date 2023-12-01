using AutoMapper;
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
        public async Task<List<Book>> GetAllAsync() =>
        await _booksCollection.Find(_ => true).ToListAsync();

        public async Task<Book?> GetAsync(string id) =>
            await _booksCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(BookDTO newBook,string userEmail)
        {
            // get the user using the user email
            var user = await _unitOfWork.User.GetFirstOrDefaultAsync( u => u.Email == userEmail);
            if (user == null)
            {
                return;
            }
            // check through the books and see if there if any book having the same isbn
            var books = await GetAllAsync();

            // get the book that has the same isbn as the incoming one
            if (books.Any(book => book.ISBN == newBook.ISBN))
                return;
            newBook.CopyNumber = 1;
            try
            {
                // check if they are books having the same properties as the the incoming one
                var similarBooks = books.Where(b => b.Title == newBook.Title && b.Author == newBook.Author).ToList();
                if(similarBooks is not null)
                {
                   newBook.CopyNumber = similarBooks.Count; 
                };
            }
            catch (Exception)
            {

                throw;
            }
           
            // create a new book object
            var bookitem = new Book
            {
                
               Title = newBook.Title,
               Author = newBook.Author,
               Publisher = newBook.Publisher,
               CopyNumber = newBook.CopyNumber,
               ObtainedThrough = newBook.ObtainedThrough,    
               Genre = newBook.Genre,
               ISBN = newBook.ISBN,
               CreatedOn = DateTime.UtcNow,
               userId = newBook.userId
            };
            bookitem.Id = "";

            await _booksCollection.InsertOneAsync(bookitem);

            try
            {
                // create a new book inventory
                var bookInventory = new BookInventory
                {
                    Book = bookitem,
                    Condition = newBook.Condition,
                    isAvailable = newBook.isAvailable,
                    Location = newBook.Location,
                    BookId = bookitem.Id,
                    checkoutTransactions = null,
                };

                await _unitOfWork.BookInventory.CreateAsync(bookInventory);
                _unitOfWork.Save();
            }
            catch (Exception)
            {

                throw;
            }            
        }     
         
        public async Task UpdateAsync(string id, Book updatedBook) =>
            await _booksCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

        public async Task RemoveAsync(string id) =>
            await _booksCollection.DeleteOneAsync(x => x.Id == id);
    }
}
