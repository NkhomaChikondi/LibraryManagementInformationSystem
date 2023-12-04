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
    public class MemberGenreRepository : Repository<MemberGenre>, IMemberGenreRepository
    {
        private ApplicationDbContext _db;
        public MemberGenreRepository(ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }
        public void Update(MemberGenre memberGenre)
        {
            _db.membersGenres.Update(memberGenre);
        }
    }
}
