using CBD.DAL.Common;
using CBD.DAL.Entities;
using CBD.Model;
using CBD.Model.Group;
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
    public class GroupRepository : Repository<TBL_SYS_GROUPS, int>, IGroupRepository
    {
        private Repository<TBL_SYS_GROUPS, int> _hrRepository;
        private DbSet<TBL_SYS_GROUPS> _dbSet;
        private readonly CBDDbContext _dbContext;

        public GroupRepository(CBDDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
            _hrRepository = new Repository<TBL_SYS_GROUPS, int>(_dbContext);
            this._dbSet = _dbContext.Set<TBL_SYS_GROUPS>();
        }

        public List<SYS_GROUPS> Search(GROUPParams model, out int totalRecords)
        {
            var skipRecord = model.PageSize * model.PageNumber;
            var query = (from g in _dbContext.TBL_SYS_GROUPS
                         where (model.CODE == null || model.CODE == "" || g.CODE.Contains(model.CODE))
                         && (model.NAME == null || model.NAME == "" || g.NAME.Contains(model.NAME))
                         && (model.ORDER == null || model.ORDER == 0 || g.ORDER == model.ORDER)
                         && (model.USED_STATE == null || model.USED_STATE == 0 || g.USED_STATE == model.USED_STATE)
                         select g);
            totalRecords = query.Count();
            var data = query.OrderBy(o => o.NAME).Skip(skipRecord).Take(model.PageSize).AsEnumerable();
            var dataList = data != null && totalRecords > 0 ? data.Select(s => new SYS_GROUPS
            {
                ID = s.ID,
                CODE = s.CODE,
                NAME = s.NAME,
                ORDER = s.ORDER,
                USED_STATE = s.USED_STATE,
                USEDSTATE_NAME = s.USED_STATE != null && s.USED_STATE > 0 ? Enums.Description((USED_STATE)s.USED_STATE) : string.Empty,
                DESCRIPTION = s.DESCRIPTION,
                CREATED_DATE = s.CREATED_DATE,
                CREATED_BY = s.CREATED_BY,
                MODIFIED_DATE = s.MODIFIED_DATE,
                MODIFIED_BY = s.MODIFIED_BY,
            }).ToList() : null;
            return dataList;
        }

        #region Private Method
        #endregion
    }
}
