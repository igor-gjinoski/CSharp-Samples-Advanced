using DesignPatterns_Repository.Data;
using DesignPatterns_Repository.Repository;

namespace DesignPatterns_Repository.CustomerRepository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext applicationContext)
            : base(applicationContext)
        {
        }

        public ApplicationDbContext ApplicationDbContext
        {
            get 
            { 
                return _context as ApplicationDbContext; 
            }
        }
    }
}
