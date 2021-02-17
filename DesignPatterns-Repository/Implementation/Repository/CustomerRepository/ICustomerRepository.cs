using System.Collections.Generic;
using DesignPatterns_Repository.Data;

namespace DesignPatterns_Repository.Repository.CustomerRepository
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        IEnumerable<string> FindByName(string name);
    }
}
