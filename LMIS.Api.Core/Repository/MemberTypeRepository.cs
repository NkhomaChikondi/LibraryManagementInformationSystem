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
    public class MemberTypeRepository : Repository<MemberType>, IMemberType
    {
        private ApplicationDbContext _db;
        public MemberTypeRepository(ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }
        public void Update(MemberType memberType)
        {
            _db.memberTypes.Update(memberType);
        }
    }
}
