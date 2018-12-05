using CBD.DAL.Common;
using CBD.DAL.Entities;
using CBD.Model;
using CBD.Model.UnitGroupPage;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CBD.Model.Enums;

namespace CBD.DAL.Repositories
{
    public class UnitGroupPageRepository : Repository<TBL_SYS_UNIT_GROUP_PAGES, int>, IUnitGroupPageRepository
    {
        private Repository<TBL_SYS_UNIT_GROUP_PAGES, int> _hrRepository;
        private DbSet<TBL_SYS_UNIT_GROUP_PAGES> _dbSet;
        private readonly CBDDbContext _dbContext;

        public UnitGroupPageRepository(CBDDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
            _hrRepository = new Repository<TBL_SYS_UNIT_GROUP_PAGES, int>(_dbContext);
            this._dbSet = _dbContext.Set<TBL_SYS_UNIT_GROUP_PAGES>();
        }

        public List<SYS_UNIT_GROUP_PAGES> Search(UNITGROUPPAGEParams model, out int totalRecords)
        {
            var skipRecord = model.PageSize * model.PageNumber;
            var query = (from ugp in _dbContext.TBL_SYS_UNIT_GROUP_PAGES
                         from uu in _dbContext.TBL_SYS_UNIT_USERS.Where(x => x.ID.Equals(ugp.UNIT_USER_ID))
                         from gu in _dbContext.TBL_SYS_GROUP_USERS.Where(x => x.ID.Equals(ugp.GROUP_USER_ID))
                         from u in _dbContext.TBL_SYS_UNITS.Where(x => x.ID.Equals(uu.UNIT_ID)).DefaultIfEmpty()
                         from g in _dbContext.TBL_SYS_GROUPS.Where(x => x.ID.Equals(gu.GROUP_ID)).DefaultIfEmpty()
                         from p in _dbContext.TBL_SYS_PAGES.Where(x => x.ID.Equals(ugp.PAGE_ID)).DefaultIfEmpty()
                         where (model.UNIT_ID == null || model.UNIT_ID == 0 || uu.UNIT_ID.Equals(model.UNIT_ID))
                         && (model.GROUP_ID == null || model.GROUP_ID == 0 || gu.GROUP_ID.Equals(model.GROUP_ID))
                         && (model.USER_NAME == null || model.USER_NAME == "" || uu.USER_NAME.Contains(model.USER_NAME))
                         && (model.PAGE_ID == null || model.PAGE_ID == 0 || ugp.PAGE_ID.Equals(model.PAGE_ID))
                         && (model.USED_STATE == null || model.USED_STATE == 0 || g.USED_STATE == model.USED_STATE)
                         select new { ugp, uu, gu, u, g, p });
            totalRecords = query.Count();
            var data = query.OrderBy(o => o.uu.USER_NAME).Skip(skipRecord).Take(model.PageSize).AsEnumerable();
            var dataList = data != null && totalRecords > 0 ? data.Select(s => new SYS_UNIT_GROUP_PAGES
            {
                ID = s.ugp.ID,
                CODE = s.ugp.CODE,
                UNIT_NAME = s.u.NAME,
                GROUP_NAME = s.g.NAME,
                USER_NAME = s.uu.USER_NAME,
                PAGE_NAME = s.p.NAME,
                ORDER = s.ugp.ORDER,
                USED_STATE = s.ugp.USED_STATE,
                USEDSTATE_NAME = s.ugp.USED_STATE != null && s.ugp.USED_STATE > 0 ? Enums.Description((USED_STATE)s.ugp.USED_STATE) : string.Empty,
                CREATED_DATE = s.ugp.CREATED_DATE,
                CREATED_BY = s.ugp.CREATED_BY,
                MODIFIED_DATE = s.ugp.MODIFIED_DATE,
                MODIFIED_BY = s.ugp.MODIFIED_BY,
            }).ToList() : null;
            return dataList;
        }

        #region Private Method
        #endregion
    }
}
