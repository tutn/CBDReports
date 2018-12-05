using CBD.Model;
using CBD.Model.Common;
using CBD.Model.User;

namespace CBD.BAL.Managers
{
    public interface IGroupUserManager
    {
        PagingResult Search(USERParams model);
        //Result Add(SYS_GROUPS model);
        //Result Update(SYS_GROUPS model);
        //Result Delete(SYS_GROUPS model);
    }
}
