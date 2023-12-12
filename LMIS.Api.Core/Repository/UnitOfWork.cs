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
        public IMemberType MemberType { get; set; }
        public IGenreRepository Genre { get; private set; }
        public IMember Member {  get; set; }
        public IBookInventoryRepository BookInventory { get; set; }      
        public ICheckoutTransactionRepository Checkout { get; set; }       
        public INotificationRepository Notification { get; set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            User = new UserRepository(_db);
            Role = new RoleRepository(_db);
            UserRole = new UserRoleRepository(_db);
            Member = new MemberRepository(_db);
            MemberType = new MemberTypeRepository(_db);
            BookInventory = new BookInventoryRepository(_db);
            Genre = new GenreRepository(_db);            
            Checkout = new CheckoutTransactionRepository(_db);
            Notification = new NotificationRepository(_db);
           
        }

        public void Save()
        {
            try
            {
                _db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception($"Failed to save, an {ex.Message} occured");
              
            }
           
        }


    }
}
