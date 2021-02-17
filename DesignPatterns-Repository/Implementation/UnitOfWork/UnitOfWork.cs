using DesignPatterns_Repository.Data;
using DesignPatterns_Repository.Repository.CustomerRepository;

namespace DesignPatterns_Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private ICustomerRepository _customers;

        private readonly ApplicationDbContext _applicationContext;

        public UnitOfWork(ApplicationDbContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public ICustomerRepository CustomerRepository
        {
            get
            {
                return _customers ??
                    (_customers = new CustomerRepository(_applicationContext));
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
