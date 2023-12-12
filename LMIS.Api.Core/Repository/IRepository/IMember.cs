
using LMIS.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.Repository.IRepository
{
    public interface IMember : IRepository<Member>
    {
        void Update(Member member);
        public string GenerateMemberCode(string firstname, string lastname);
        bool IsPhoneNumberValid(string phoneNumber);
        public bool IsValidEmail(string email);
        Task<bool> SoftDeleteAsync(int id);
        IEnumerable<Model.Member> GetAllRoles();
    }
}
