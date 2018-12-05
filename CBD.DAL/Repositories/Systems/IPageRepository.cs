using CBD.DAL.Common;
using CBD.DAL.Entities;
using CBD.Model;
using CBD.Model.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBD.DAL.Repositories
{
    public interface IPageRepository : IRepository<TBL_SYS_PAGES, int>
    {
        IQueryable<SYS_PAGES> Search(PAGEParams model);
        IQueryable<SYS_PAGES> GetAllPages();
    }
}
