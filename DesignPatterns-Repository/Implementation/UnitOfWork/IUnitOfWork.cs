using System;
using DesignPatterns_Repository.Repository.CustomerRepository;

namespace DesignPatterns_Repository.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository CustomerRepository { get; }

        void Commit();
    }
}
