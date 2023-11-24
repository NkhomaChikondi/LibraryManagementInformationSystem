using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.Repository.IRepository
{
    public interface IUnitOfWork
    {
        public interface IUnitOfWork
        {
            IUserRepository User { get; }
            IRoleRepository Role { get; }
            IUserRoleRepository UserRole { get; }
            void Save();

        }
    }
}
