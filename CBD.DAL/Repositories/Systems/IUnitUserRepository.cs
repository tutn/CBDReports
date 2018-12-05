using CBD.DAL.Common;
using CBD.DAL.Entities;
using CBD.Model;
using CBD.Model.UnitUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBD.DAL.Repositories
{
    public interface IUnitUserRepository : IRepository<TBL_SYS_UNIT_USERS, int>
    {
        List<SYS_UNIT_USERS> Search(UNITUSERTParams model, out int totalRecords);
    }
}
