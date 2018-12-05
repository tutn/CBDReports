using CBD.DAL.Common;
using CBD.DAL.Entities;
using CBD.Model;
using CBD.Model.Page;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CBD.Model.Enums;
using CBD.Model.Configuration;

namespace CBD.DAL.Repositories
{
    public class PageRepository : Repository<TBL_SYS_PAGES, int>, IPageRepository
    {
        private Repository<TBL_SYS_PAGES, int> _hrRepository;
        private DbSet<TBL_SYS_PAGES> _dbSet;
        private readonly CBDDbContext _dbContext;

        public PageRepository(CBDDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
            _hrRepository = new Repository<TBL_SYS_PAGES, int>(_dbContext);
            this._dbSet = _dbContext.Set<TBL_SYS_PAGES>();
        }

        public IQueryable<SYS_PAGES> Search(PAGEParams model)
        {
            var query = (from p in _dbContext.TBL_SYS_PAGES
                         from pp in _dbContext.TBL_SYS_PAGES.Where(x => x.ID == p.PARENT_ID).DefaultIfEmpty()
                         where (model.CODE == null || model.CODE == "" || p.CODE.Contains(model.CODE))
                         && (model.NAME == null || model.NAME == "" || p.NAME.Contains(model.NAME))
                         && (model.NAME_VI == null || model.NAME_VI == "" || p.NAME_VI.Contains(model.NAME_VI))
                         && (model.NAME_EN == null || model.NAME_EN == "" || p.NAME_EN.Contains(model.NAME_EN))
                         && (model.PARENT_ID == null || model.PARENT_ID == 0 || p.PARENT_ID == model.PARENT_ID)
                         && (model.ORDER == null || model.ORDER == 0 || p.ORDER == model.ORDER)
                         && (model.USED_STATE == null || model.USED_STATE == 0 || p.USED_STATE == model.USED_STATE)
                         select new SYS_PAGES
                         {
                             ID = p.ID,
                             CODE = p.CODE,
                             NAME = p.NAME,
                             NAME_VI = p.NAME_VI,
                             NAME_EN = p.NAME_EN,
                             URL = p.URL,
                             ICON = p.ICON,
                             PARENT_ID = p.PARENT_ID,
                             PARENT_NAME = pp.NAME,
                             ORDER = p.ORDER,
                             USED_STATE = p.USED_STATE,
                             DESCRIPTION = p.DESCRIPTION,
                             CREATED_DATE = p.CREATED_DATE,
                             CREATED_BY = p.CREATED_BY,
                             MODIFIED_DATE = p.MODIFIED_DATE,
                             MODIFIED_BY = p.MODIFIED_BY,
                         });
            return query;
        }

        public IQueryable<SYS_PAGES> GetAllPages()
        {
            var query = (from p in _dbContext.TBL_SYS_PAGES
                         select new SYS_PAGES
                         {
                             ID = p.ID,
                             CODE = p.CODE,
                             NAME = p.NAME,
                             NAME_VI = p.NAME_VI,
                             NAME_EN = p.NAME_EN,
                             URL = p.URL,
                             ICON = p.ICON,
                             PARENT_ID = p.PARENT_ID,
                             ORDER = p.ORDER,
                             USED_STATE = p.USED_STATE,
                             CREATED_DATE = p.CREATED_DATE,
                             CREATED_BY = p.CREATED_BY,
                             MODIFIED_DATE = p.MODIFIED_DATE,
                             MODIFIED_BY = p.MODIFIED_BY,
                         });
            return query;
        }

        #region Private Method
        #endregion
    }
}
