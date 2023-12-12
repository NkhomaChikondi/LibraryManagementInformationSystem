using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.Repository.IRepository
{
    
    public interface IUnitOfWork
    {
        IUserRepository User { get; }
        IRoleRepository Role { get; }
        IUserRoleRepository UserRole { get; }
        IMemberType MemberType { get; }
        IMember Member { get; }
        IBookInventoryRepository BookInventory { get; }  

        ICheckoutTransactionRepository Checkout { get; } 
        IGenreRepository Genre { get; }
        
        INotificationRepository  Notification { get; }
        void Save();

    }
    
}
