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

        public BookService(IOptions<BookDatabaseSettings> bookDatabaseSettings, IUnitOfWork unitOfWork)
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

        public async Task CreateAsync(Book newBook,string userEmail)
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

            newBook.userId = user.UserId;
            newBook.CreatedOn = DateTime.UtcNow;
            await _booksCollection.InsertOneAsync(newBook);

            try
            {
                var bookInventory = new BookInventory
                {
                    Book = newBook,
                };
                await _unitOfWork.BookInventory.CreateAsync(bookInventory);
                _unitOfWork.Save();

            }
            catch (Exception)
            {

                throw;
            }
            // a new inventory item should be created
          
        }     
         
        public async Task UpdateAsync(string id, Book updatedBook) =>
            await _booksCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

        public async Task RemoveAsync(string id) =>
            await _booksCollection.DeleteOneAsync(x => x.Id == id);
    }
}
