using CBD.Model;
using CBD.Model.Common;
using CBD.Model.Page;
using System.Collections.Generic;

namespace CBD.BAL.Managers
{
    public interface IPageManager
    {
        PagingResult Search(PAGEParams model);
        Result Add(SYS_PAGES model);
        Result Update(SYS_PAGES model);
        Result Delete(SYS_PAGES model);
        Result GetAllPages(int? PageId);
        Result GetNodes();
        //List<Node> GetNodes();
    }
}
