using CBD.DAL.Entities;
using CBD.DAL.Repositories;
using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;

namespace CBD.DAL.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        #region "Private Member(s)"

        private bool _disposed = false;
        private readonly CBDDbContext _context;

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public UnitOfWork()
        {
            _context = new CBDDbContext();
        }

        #region "Public Member(s)"

        private CBDDbContext _dbContext;
        public CBDDbContext DbContext
        {
            get
            {
                if (_dbContext == null)
                    this._dbContext = _context;
                return _dbContext;
            }
        }

        private IRepository<TBL_SYS_DIMDATE, int> _dimDateRepository;
        public IRepository<TBL_SYS_DIMDATE, int> DimDateRepository
        {
            get
            {
                if (this._dimDateRepository == null)
                    this._dimDateRepository = new Repository<TBL_SYS_DIMDATE, int>(_context);
                return _dimDateRepository;
            }
        }

        private IPageRepository _pageRepository;
        public IPageRepository PageRepository
        {
            get
            {
                if (this._pageRepository == null)
                    this._pageRepository = new PageRepository(_context);
                return _pageRepository;
            }
        }

        private IUnitRepository _unitRepository;
        public IUnitRepository UnitRepository
        {
            get
            {
                if (this._unitRepository == null)
                    this._unitRepository = new UnitRepository(_context);
                return _unitRepository;
            }
        }

        private IUserRepository _userRepository;
        public IUserRepository UserRepository
        {
            get
            {
                if (this._userRepository == null)
                    this._userRepository = new UserRepository(_context);
                return _userRepository;
            }
        }

        private IGroupRepository _groupRepository;
        public IGroupRepository GroupRepository
        {
            get
            {
                if (this._groupRepository == null)
                    this._groupRepository = new GroupRepository(_context);
                return _groupRepository;
            }
        }

        private IUnitUserRepository _unitUserRepository;
        public IUnitUserRepository UnitUserRepository
        {
            get
            {
                if (this._unitUserRepository == null)
                    this._unitUserRepository = new UnitUserRepository(_context);
                return _unitUserRepository;
            }
        }

        private IUnitGroupPageRepository _unitGroupPageRepository;
        public IUnitGroupPageRepository UnitGroupPageRepository
        {
            get
            {
                if (this._unitGroupPageRepository == null)
                    this._unitGroupPageRepository = new UnitGroupPageRepository(_context);
                return _unitGroupPageRepository;
            }
        }

        private IRepository<TBL_SYS_GROUP_USERS, int> _groupUserRepository;
        public IRepository<TBL_SYS_GROUP_USERS, int> GroupUserRepository
        {
            get
            {
                if (this._groupUserRepository == null)
                    this._groupUserRepository = new Repository<TBL_SYS_GROUP_USERS, int>(_context);
                return _groupUserRepository;
            }
        }


        /// <summary>
        /// Save all the entity changed in _context
        /// </summary>
        public void SaveChanges()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    var rs = string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    rs = rs + eve.ValidationErrors.Aggregate(rs, (current, ve) => current + ("<br />" + string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage)));
                    //rs.LogMessage(this);
                }
                //e.LogError(this);
                throw e;
            }
            catch (DbUpdateException ex)
            {
                //ex.LogError(this);
                throw ex;
            }
            catch (Exception ex)
            {
                //ex.LogError(this);
                throw ex;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
