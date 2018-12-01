using CBD.Model;
using CBD.Model.Common;
using CBD.Model.Sys_User;

namespace CBD.BAL.Managers
{
    public interface IUserManager
    {
        PagingResult Search(USERParams model);
        Result Add(SYS_USERS model);
        Result Update(SYS_USERS model);
        Result Delete(SYS_USERS model);
    }
}
