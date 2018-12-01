using CBD.DAL.Common;
using CBD.DAL.Entities;
using CBD.Model;
using CBD.Model.Sys_Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBD.DAL.Repositories
{
    public interface ISys_UnitsRepository : IRepository<TBL_SYS_UNITS, int>
    {
        IQueryable<SYS_UNITS> Search(UNITParams model);
        IQueryable<SYS_UNITS> GetAllUnits();
    }
}
