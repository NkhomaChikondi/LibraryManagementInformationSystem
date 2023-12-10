using LMIS.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.Repository.IRepository
{
    public interface IMemberType : IRepository<MemberType>
    {
        void Update(MemberType memberType);
        Task<bool> SoftDeleteAsync(int id);
        Task<IEnumerable<MemberType>> GetAllMemberType();

    }
}
