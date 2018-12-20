using CBD.DAL.Common;
using CBD.DAL.Entities;
using CBD.Model;
using CBD.Model.User;
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
    public class UserRepository : Repository<TBL_SYS_USERS, int>, IUserRepository
    {
        private Repository<TBL_SYS_USERS, int> _hrRepository;
        private DbSet<TBL_SYS_USERS> _dbSet;
        private readonly CBDDbContext _dbContext;

        public UserRepository(CBDDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
            _hrRepository = new Repository<TBL_SYS_USERS, int>(_dbContext);
            this._dbSet = _dbContext.Set<TBL_SYS_USERS>();
        }

        public List<SYS_USERS> Search(USERParams model, out int totalRecords)
        {
            var skipRecord = model.PageSize * model.PageNumber;
            var query = (from u in _dbContext.TBL_SYS_USERS
                         where (model.USER_NAME == null || model.USER_NAME == "" || u.USER_NAME.Contains(model.USER_NAME))
                         && (model.FULL_NAME == null || model.FULL_NAME == "" || u.FULL_NAME.Contains(model.FULL_NAME))
                         && (model.EMAIL == null || model.EMAIL == "" || u.EMAIL.Contains(model.EMAIL))
                         && (model.USED_STATE == null || model.USED_STATE == 0 || u.USED_STATE == model.USED_STATE)
                         select u);
            totalRecords = query.Count();
            var data = query.OrderBy(o => o.USER_NAME).Skip(skipRecord).Take(model.PageSize).AsEnumerable();
            var dataList = data != null && totalRecords > 0 ? data.Select(s => new SYS_USERS
            {
                ID = s.ID,
                USER_NAME = s.USER_NAME,
                PASSWORD = s.PASSWORD,
                FULL_NAME = s.FULL_NAME,
                EMAIL = s.EMAIL,
                AVATAR = s.AVATAR,
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

        public SYS_USERS GetByUser(SYS_USERS model)
        {
            var query = (from u in _dbContext.TBL_SYS_USERS
                         from uu in _dbContext.TBL_SYS_UNIT_USERS.Where(x => x.USER_NAME.Equals(u.USER_NAME)).DefaultIfEmpty()
                         from un in _dbContext.TBL_SYS_UNITS.Where(x => x.ID.Equals(uu.UNIT_ID)).DefaultIfEmpty()
                         from gu in _dbContext.TBL_SYS_GROUP_USERS.Where(x => x.USER_NAME.Equals(u.USER_NAME)).DefaultIfEmpty()
                         from g in _dbContext.TBL_SYS_GROUPS.Where(x => x.ID.Equals(gu.GROUP_ID)).DefaultIfEmpty()
                         where (u.USER_NAME.Equals(model.USER_NAME) && u.PASSWORD.Equals(model.PASSWORD))
                         select new { u, uu, un, gu, g });
            var user = query != null && query.Count() > 0 ? query.AsEnumerable().Select(s => new SYS_USERS
            {
                ID = s.u.ID,
                USER_NAME = s.u.USER_NAME,
                PASSWORD = s.u.PASSWORD,
                FULL_NAME = s.u.FULL_NAME,
                EMAIL = s.u.EMAIL,
                AVATAR = s.u.AVATAR,
                USED_STATE = s.u.USED_STATE,
                USEDSTATE_NAME = s.u.USED_STATE != null && s.u.USED_STATE > 0 ? Enums.Description((USED_STATE)s.u.USED_STATE) : string.Empty,
                DESCRIPTION = s.u.DESCRIPTION,
                CREATED_DATE = s.u.CREATED_DATE,
                CREATED_BY = s.u.CREATED_BY,
                MODIFIED_DATE = s.u.MODIFIED_DATE,
                MODIFIED_BY = s.u.MODIFIED_BY,
                UNIT_NAME = s.un.NAME,
                GROUP_NAME = s.g.NAME,
            }).FirstOrDefault():null;
            return user;
        }

        #region Private Method
        #endregion
    }
}
