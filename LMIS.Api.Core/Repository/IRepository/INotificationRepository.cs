﻿using LMIS.Api.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMIS.Api.Core.Repository.IRepository
{
    public interface INotificationRepository : IRepository<Notification>
    {
        void Update(Notification notification);
    }
}
