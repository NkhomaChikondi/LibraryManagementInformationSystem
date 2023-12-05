using AutoMapper.Execution;
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
    public class MemberRepository : Repository<LMIS.Api.Core.Model.Member>, IMember
    {
        private ApplicationDbContext _db;
        public MemberRepository(ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }
        public void Update(LMIS.Api.Core.Model.Member member)
        {
            _db.members.Update(member);
        }
        public string GenerateMemberCode(string firstname, string lastname)
        {
            int id = 0;
            // get all member in the datadase
            var members = _db.members.ToList();

            // get  last member
            var lastMember = members.LastOrDefault();
            if (lastMember != null) 
            {
                // get id of last member
                 id = lastMember.MemberId + 1;            
            }
            else
                id = id + 1;
            // Get the first letters of first name and last name(ensure they are not null or empty)
             char firstLetterOfFirstName = string.IsNullOrEmpty(firstname) ? 'X' : firstname[0];
            char firstLetterOfLastName = string.IsNullOrEmpty(lastname) ? 'X' : lastname[0];

            // Generate the member code by combining the initials and member ID with leading zeros
            string memberCode = $"{firstLetterOfFirstName}{firstLetterOfLastName}{id:D4}";

            return memberCode;
        }
        public async Task<bool> SoftDeleteAsync(int id)
        {
            var entity = await _db.members.FindAsync(id);

            if (entity == null || entity.IsDeleted)
            {
                return false;
            }

            entity.IsDeleted = true;
            entity.DeletedDate = DateTime.UtcNow;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Model.Member>> GetAllRoles()
        {
            var allEntities = _db.members.Where(U => U.IsDeleted == false).ToList();
            return allEntities;
        }
    }
}
