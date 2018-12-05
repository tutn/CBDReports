using CBD.DAL.Common;
using CBD.DAL.Entities;
using CBD.Model;
using CBD.Model.UnitUser;
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
    public class UnitUserRepository : Repository<TBL_SYS_UNIT_USERS, int>, IUnitUserRepository
    {
        private Repository<TBL_SYS_UNIT_USERS, int> _repository;
        private DbSet<TBL_SYS_UNIT_USERS> _dbSet;
        private readonly CBDDbContext _dbContext;

        public UnitUserRepository(CBDDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
            _repository = new Repository<TBL_SYS_UNIT_USERS, int>(_dbContext);
            this._dbSet = _dbContext.Set<TBL_SYS_UNIT_USERS>();
        }

        public List<SYS_UNIT_USERS> Search(UNITUSERTParams model, out int totalRecords)
        {
            var skipRecord = model.PageSize * model.PageNumber;
            var query = (from uu in _dbContext.TBL_SYS_UNIT_USERS
                         from un in _dbContext.TBL_SYS_UNITS.Where(x=>x.ID.Equals(uu.UNIT_ID)).DefaultIfEmpty()
                         where (model.USER_NAME == null || model.USER_NAME == "" || uu.USER_NAME.Contains(model.USER_NAME))
                         && (model.UNIT_ID == null || model.UNIT_ID == 0 || un.NAME.Equals(model.UNIT_ID))
                         && (model.USED_STATE == null || model.USED_STATE == 0 || uu.USED_STATE.Equals(model.USED_STATE))
                         select new { uu, un });
            totalRecords = query.Count();
            var data = query.OrderBy(o => o.uu.USER_NAME).Skip(skipRecord).Take(model.PageSize).AsEnumerable();
            var dataList = data != null && totalRecords > 0 ? data.Select(s => new SYS_UNIT_USERS
            {
                ID = s.uu.ID,
                CODE = s.uu.CODE,
                USER_NAME = s.uu.USER_NAME,
                UNIT_ID = s.uu.UNIT_ID,
                UNIT_NAME = s.un.NAME,
                ORDER = s.uu.ORDER,
                USED_STATE = s.uu.USED_STATE,
                USEDSTATE_NAME = s.uu.USED_STATE != null && s.uu.USED_STATE > 0 ? Enums.Description((USED_STATE)s.uu.USED_STATE) : string.Empty,
                CREATED_DATE = s.uu.CREATED_DATE,
                CREATED_BY = s.uu.CREATED_BY,
                MODIFIED_DATE = s.uu.MODIFIED_DATE,
                MODIFIED_BY = s.uu.MODIFIED_BY,
            }).ToList() : null;
            return dataList;
        }

        #region Private Method
        #endregion
    }
}
