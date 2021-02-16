using DesignPatterns_Repository.Data;
using DesignPatterns_Repository.Repository;

namespace DesignPatterns_Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<Customer> Repository { get; }

        void Commit();
    }
}
