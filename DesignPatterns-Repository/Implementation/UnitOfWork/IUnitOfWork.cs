using System;
using DesignPatterns_Repository.Repository.CustomerRepository;

namespace DesignPatterns_Repository.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository Repository { get; }

        void Commit();
    }
}
