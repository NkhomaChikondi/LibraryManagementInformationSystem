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
    public class NotificationRepository : Repository<Notification>, INotificationRepository
    {

        private ApplicationDbContext _db;
        public NotificationRepository(ApplicationDbContext db) : base(db)
        {
            this._db = db;
        }
        public void Update(Notification notification)
        {
            _db.GetNotifications.Update(notification);
        }
    }
}
