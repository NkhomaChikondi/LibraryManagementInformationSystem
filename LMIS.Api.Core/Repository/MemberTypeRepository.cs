
using LMIS.Api.Core.DataAccess;
using LMIS.Api.Core.Repository.IRepository;
using LMIS.Api.Core.Repository;
using LMIS.Api.Core.Model;

public class MemberTypeRepository : Repository<MemberType>, IMemberType
{
    private ApplicationDbContext _db;
    public MemberTypeRepository(ApplicationDbContext db) : base(db)
    {
        this._db = db;
    }
    public void Update(MemberType memberType)
    {
        _db.MemberTypes.Update(memberType);
    }

    public async Task<bool> SoftDeleteAsync(int id)
    {
        var entity = await _db.ApplicationUsers.FindAsync(id);

        if (entity == null || entity.IsDeleted)
        {
            return false;
        }

        entity.IsDeleted = true;
        entity.DeletedDate = DateTime.UtcNow;

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<MemberType>> GetAllMemberType()
    {
        var allMemberType = _db.MemberTypes.Where(U => U.IsDeleted == false).ToList();
        return allMemberType;
    }
}
