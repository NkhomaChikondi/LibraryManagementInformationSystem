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
    public class MemberRepository : Repository<Member>, IMember
    {
        private ApplicationDbContext _db;
        public MemberRepository(ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }
        public void Update(Member member)
        {
            _db.members.Update(member);
        }
    }
}
