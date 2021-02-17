using System.Linq;
using System.Collections.Generic;
using DesignPatterns_Repository.Data;

namespace DesignPatterns_Repository.Repository.CustomerRepository
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

        public IEnumerable<string> FindByName(string name)
        {
            return ApplicationDbContext.Customers
                .Where(x => x.FirstName.Contains(name)
                         || x.LastName.Contains(name))
                .Select(x => $"Customer: {x.FirstName} {x.LastName}");
        }
    }
}
