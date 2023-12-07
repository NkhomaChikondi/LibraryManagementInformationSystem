using LMIS.Api.Core.DataAccess;
using LMIS.Api.Core.DTOs;
using LMIS.Api.Core.DTOs.Checkout;
using LMIS.Api.Core.Model;
using LMIS.Api.Core.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.Repository
{
    public class Temp_DataRepository : Repository<Temp_Data>, ITemp_DataRepository
    {
        private ApplicationDbContext _db;
        public Temp_DataRepository(ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }

        public async Task<IEnumerable<Temp_Data>> GetAllCheckoutTransactions()
        {

            var tempDatas =  _db.temp_data.Where(U => U.IsDeleted == false).ToList();
            return tempDatas;
        }

        public void Update(Temp_Data temp_Data)
        {
            _db.temp_data.Update(temp_Data);
        }
    }
}
