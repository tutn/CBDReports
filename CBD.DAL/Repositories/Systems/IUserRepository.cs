﻿using CBD.DAL.Common;
using CBD.DAL.Entities;
using CBD.Model;
using CBD.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBD.DAL.Repositories
{
    public interface IUserRepository : IRepository<TBL_SYS_USERS, int>
    {
        List<SYS_USERS> Search(USERParams model, out int totalRecords);
        SYS_USERS GetByUser(SYS_USERS model);
    }
}
