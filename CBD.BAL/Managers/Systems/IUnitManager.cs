using CBD.Model;
using CBD.Model.Common;
using CBD.Model.Sys_Unit;

namespace CBD.BAL.Managers
{
    public interface IUnitManager
    {
        PagingResult Search(UNITParams model);
        Result Add(SYS_UNITS model);
        Result Update(SYS_UNITS model);
        Result Delete(SYS_UNITS model);
        Result GetAllUnits(int? UnitId);
    }
}
