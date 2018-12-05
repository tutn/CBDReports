using CBD.DAL.Common;
using CBD.DAL.Entities;
using CBD.Model;
using CBD.Model.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBD.DAL.Repositories
{
    public interface IUnitRepository : IRepository<TBL_SYS_UNITS, int>
    {
        IQueryable<SYS_UNITS> Search(UNITParams model);
        IQueryable<SYS_UNITS> GetAllUnits();
    }
}
