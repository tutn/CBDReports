using CBD.Model;
using CBD.Model.Common;
using CBD.Model.UnitGroupPage;

namespace CBD.BAL.Managers
{
    public interface IUnitGroupPageManager
    {
        PagingResult Search(UNITGROUPPAGEParams model);
        //Result Add(SYS_UNITS model);
        //Result Update(SYS_UNITS model);
        //Result Delete(SYS_UNITS model);
    }
}
