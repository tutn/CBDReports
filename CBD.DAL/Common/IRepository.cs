using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CBD.DAL.Common
{
    public interface IRepository<TEntity, in TKey> : IDisposable where TEntity : class
    {
        TEntity GetById(TKey id);
        ICollection<TEntity> GetAll();
        IQueryable<TEntity> QueryAll();
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        TEntity Single(Expression<Func<TEntity, bool>> predicate);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        TEntity FirstOrDefault();
        TEntity FirstOrDefault(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);
        TEntity LastOrDefault();
        TEntity LastOrDefault(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate);
        void Save();
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entity);
        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entity);
        void DeleteById(TKey id);
    }
}
