using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CBD.DAL.Common
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        private bool _isDisposed;
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;
        public DbSet<TEntity> DbSet
        {
            get { return _dbSet; }
        }

        public Repository(DbContext dbContext)
        {
            this._dbContext = dbContext;
            this._dbSet = _dbContext.Set<TEntity>();
            this._isDisposed = false;
        }

        public TEntity GetById(TKey id)
        {
            return _dbSet.Find(id);
        }

        public ICollection<TEntity> GetAll()
        {
            return _dbSet.ToList<TEntity>();
        }
        public IQueryable<TEntity> QueryAll()
        {
            return _dbSet.AsQueryable<TEntity>();
        }
        public IQueryable<TEntity> Where(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public TEntity Single(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Single(predicate);
        }

        public TEntity SingleOrDefault(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.SingleOrDefault(predicate);
        }

        public TEntity FirstOrDefault()
        {
            return _dbSet.FirstOrDefault();
        }

        public TEntity FirstOrDefault(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        public TEntity LastOrDefault()
        {
            return _dbSet.LastOrDefault();
        }

        public TEntity LastOrDefault(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.LastOrDefault(predicate);
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }
        public void AddRange(IEnumerable<TEntity> entity)
        {
            _dbSet.AddRange(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entity)
        {
            _dbSet.RemoveRange(entity);
        }

        public void DeleteById(TKey id)
        {
            TEntity entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public virtual void Dispose(bool isManuallyDisposing)
        {
            if (!_isDisposed)
            {
                if (isManuallyDisposing)
                    _dbContext.Dispose();
            }

            _isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Repository()
        {
            Dispose(false);
        }
    }
}
