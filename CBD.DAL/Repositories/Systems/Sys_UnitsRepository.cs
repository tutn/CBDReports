using CBD.DAL.Common;
using CBD.DAL.Entities;
using CBD.Model;
using CBD.Model.Sys_Unit;
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
    public class Sys_UnitsRepository : Repository<TBL_SYS_UNITS, int>, ISys_UnitsRepository
    {
        private Repository<TBL_SYS_UNITS, int> _hrRepository;
        private DbSet<TBL_SYS_UNITS> _dbSet;
        private readonly CBDDbContext _dbContext;

        public Sys_UnitsRepository(CBDDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
            _hrRepository = new Repository<TBL_SYS_UNITS, int>(_dbContext);
            this._dbSet = _dbContext.Set<TBL_SYS_UNITS>();
        }

        public IQueryable<SYS_UNITS> Search(UNITParams model)
        {
            var query = (from u in _dbContext.TBL_SYS_UNITS
                         from up in _dbContext.TBL_SYS_UNITS.Where(x => x.ID == u.PARENT_ID).DefaultIfEmpty()
                         where (model.CODE == null || model.CODE == "" || u.CODE.Contains(model.CODE))
                         && (model.NAME == null || model.NAME == "" || u.NAME.Contains(model.NAME))
                         && (model.PARENT_ID == null || model.PARENT_ID == 0 || u.PARENT_ID == model.PARENT_ID)
                         && (model.USED_STATE == null || model.USED_STATE == 0 || u.USED_STATE == model.USED_STATE)
                         select new SYS_UNITS
                         {
                             ID = u.ID,
                             CODE = u.CODE,
                             NAME = u.NAME,
                             PARENT_ID = u.PARENT_ID,
                             PARENT_NAME = up.NAME,
                             USED_STATE = u.USED_STATE,
                             CREATED_DATE = u.CREATED_DATE,
                             CREATED_BY = u.CREATED_BY,
                             MODIFIED_DATE = u.MODIFIED_DATE,
                             MODIFIED_BY = u.MODIFIED_BY,
                         });
            return query;
        }

        public IQueryable<SYS_UNITS> GetAllUnits()
        {
            var query = (from p in _dbContext.TBL_SYS_UNITS
                         select new SYS_UNITS
                         {
                             ID = p.ID,
                             CODE = p.CODE,
                             NAME = p.NAME,
                             PARENT_ID = p.PARENT_ID,
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
