using System;
using DesignPatterns_Repository.UnitOfWork;
using DesignPatterns_Repository.UnitType;
using Microsoft.EntityFrameworkCore;

namespace DesignPatterns_Repository.Implementation.UnitOfWork
{
    public abstract class UnitOfWork<TContext> :
        IUnitOfWork<TContext>,
        IDisposable
        where TContext : DbContext
    {
        protected TContext _contextAccessor;

        protected UnitOfWork(TContext context)
        {
            _contextAccessor = context;
        }

        public virtual Unit Complete()
        {
            _contextAccessor.SaveChanges();

            return Unit.Value;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }
    }
}
