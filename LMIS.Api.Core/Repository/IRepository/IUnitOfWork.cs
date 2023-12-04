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
        IMemberType memberType { get; }
        IMember member { get; }
        IBookInventoryRepository BookInventory { get; } 
        ICheckoutTransactionRepository Checkout { get; } 
        IGenreRepository Genre { get; }
        IMemberGenreRepository  memberGenre { get; }
        void Save();

    }
    
}
