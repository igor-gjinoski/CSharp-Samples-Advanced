using System;
using DesignPatterns_Repository.UnitType;

namespace DesignPatterns_Repository.UnitOfWork
{
    public interface IUnitOfWork<TContext> : IDisposable
    {
        Unit Complete();
    }
}
