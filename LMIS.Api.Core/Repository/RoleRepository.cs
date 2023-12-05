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
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        private ApplicationDbContext _db;
        public RoleRepository(ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }
        public void Update(Role role)
        {
            _db.roles.Update(role);
        }
        public async Task<bool> SoftDeleteAsync(int id)
        {
            var entity = await _db.roles.FindAsync(id);

            if (entity == null || entity.IsDeleted)
            {
                return false;
            }

            entity.IsDeleted = true;
            entity.DeletedDate = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            var allEntities = _db.roles.Where(U => U.IsDeleted == false).ToList();
            return allEntities;
        }
    }
}
