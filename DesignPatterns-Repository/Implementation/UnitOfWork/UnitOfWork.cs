using DesignPatterns_Repository.Data;
using DesignPatterns_Repository.Repository;

namespace DesignPatterns_Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationContext;
        private Repository<Customer> _customers;

        public UnitOfWork(ApplicationDbContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public IRepository<Customer> Repository
        {
            get
            {
                return _customers ??
                    (_customers = new Repository<Customer>(_applicationContext));
            }
        }

        public void Commit()
        {
            _applicationContext.SaveChanges();
        }

        public void Dispose()
        {
            _applicationContext.Dispose();
        }
    }
}
