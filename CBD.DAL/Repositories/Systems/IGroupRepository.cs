using CBD.DAL.Common;
using CBD.DAL.Entities;
using CBD.Model;
using CBD.Model.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBD.DAL.Repositories
{
    public interface IGroupRepository : IRepository<TBL_SYS_GROUPS, int>
    {
        List<SYS_GROUPS> Search(GROUPParams model, out int totalRecords);
    }
}
