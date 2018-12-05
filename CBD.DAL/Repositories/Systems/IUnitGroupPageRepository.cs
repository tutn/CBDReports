using CBD.DAL.Common;
using CBD.DAL.Entities;
using CBD.Model;
using CBD.Model.UnitGroupPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBD.DAL.Repositories
{
    public interface IUnitGroupPageRepository : IRepository<TBL_SYS_UNIT_GROUP_PAGES, int>
    {
        List<SYS_UNIT_GROUP_PAGES> Search(UNITGROUPPAGEParams model, out int totalRecords);
    }
}
