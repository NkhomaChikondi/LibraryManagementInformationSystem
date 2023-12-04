using LMIS.Api.Core.DataAccess;
using LMIS.Api.Core.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IUserRepository User { get; private set; }
        public IRoleRepository Role { get; private set; }
        public IUserRoleRepository UserRole { get; private set; }
        public IMemberType memberType { get; set; }
        public IGenreRepository Genre { get; private set; }
        public IMember member {  get; set; }
        public IBookInventoryRepository BookInventory { get; set; }
        public ICheckoutTransactionRepository Checkout { get; set; }
        public IMemberGenreRepository memberGenre { get; set; }
        public INotificationRepository notification { get; set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            User = new UserRepository(_db);
            Role = new RoleRepository(_db);
            UserRole = new UserRoleRepository(_db);
            member = new MemberRepository(_db);
            memberType = new MemberTypeRepository(_db);
            BookInventory = new BookInventoryRepository(_db);
            Genre = new GenreRepository(_db);
            memberGenre = new MemberGenreRepository(_db);
            Checkout = new CheckoutTransactionRepository(_db);
            notification = new NotificationRepository(_db);
        }
        public void Save()
        {
            try
            {
                _db.SaveChanges();
            }
            catch (Exception ex)
            {

                var error = ex.Message;
                var getError = error;
            }
           
        }

    }
}
