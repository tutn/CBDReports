using CBD.Model;
using CBD.Model.Common;
using CBD.Model.UnitUser;
using System.Collections.Generic;

namespace CBD.BAL.Managers
{
    public interface IUnitUserManager
    {
        PagingResult Search(UNITUSERTParams model);
        Result Add(List<SYS_UNIT_USERS> model);
        //Result Update(SYS_UNITS model);
        //Result Delete(SYS_UNITS model);
        //Result GetAllUnits(int? UnitId);
    }
}
