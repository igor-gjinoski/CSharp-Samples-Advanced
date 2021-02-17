using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace DesignPatterns_Repository.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> 
        where TEntity : class
    {
        internal DbContext _context;
        internal DbSet<TEntity> _dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }


        public virtual TEntity GetByID(object id)
            => _dbSet.Find(id);


        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }


        public virtual IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters)
        {
            return _dbSet.FromSqlRaw(query, parameters).ToList();
        }


        public virtual void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }


        public virtual void Delete(object id)
        {
            TEntity entityToDelete = _dbSet.Find(id);

            Delete(entityToDelete);
        }


        public virtual void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }
    }

    public static class Extensions
    {
        public static bool NullOrAny<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
                return false;

            if (source is ICollection<TSource> collectionoft)
            {
                return collectionoft.Count != 0;
            }

            using (IEnumerator<TSource> e = source.GetEnumerator())
            {
                return e.MoveNext();
            }
        }
    }
}
