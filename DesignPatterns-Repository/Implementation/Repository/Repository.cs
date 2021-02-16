using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using DesignPatterns_Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace DesignPatterns_Repository.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> 
        where TEntity : class
    {
        internal ApplicationDbContext _applicationContext;
        internal DbSet<TEntity> _dbSet;

        public Repository(ApplicationDbContext applicationContext)
        {
            _applicationContext = applicationContext;
            _dbSet = applicationContext.Set<TEntity>();
        }


        public virtual TEntity GetByID(object id)
            => _dbSet.Find(id);


        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null, 
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            IList<string> includePropertiesList =
                includeProperties
                .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .ToList();

            if (includePropertiesList.NullOrAny())
            {
                foreach (var includeProperty in includePropertiesList)
                    query = query.Include(includeProperty);
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


        public virtual void Delete(TEntity entityToDelete)
        {
            if (_applicationContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }


        public virtual void Delete(object id)
        {
            TEntity entityToDelete = _dbSet.Find(id);

            Delete(entityToDelete);
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
