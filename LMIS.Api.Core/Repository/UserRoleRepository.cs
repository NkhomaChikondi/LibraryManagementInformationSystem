using LMIS.Api.Core.DataAccess;
using LMIS.Api.Core.Model;
using LMIS.Api.Core.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.Repository
{
    public class UserRoleRepository: Repository<UserRole>,IUserRoleRepository
    {
        private ApplicationDbContext _db;
        public UserRoleRepository(ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }
        public void Update(UserRole userRole)
        {
            _db.userRoles.Update(userRole);
        }

        public void Delete(UserRole userRole)
        {
        
        }
    }
}
