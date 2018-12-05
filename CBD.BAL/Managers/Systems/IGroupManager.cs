using CBD.Model;
using CBD.Model.Common;
using CBD.Model.Group;

namespace CBD.BAL.Managers
{
    public interface IGroupManager
    {
        PagingResult Search(GROUPParams model);
        Result Add(SYS_GROUPS model);
        Result Update(SYS_GROUPS model);
        Result Delete(SYS_GROUPS model);
    }
}
