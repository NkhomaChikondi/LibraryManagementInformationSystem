using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.Repository.IRepository
{
    public interface IDeletableEntity
    {
        bool IsDeleted { get; set; }
        DateTime DeletedDate { get; set; }
    }
}
