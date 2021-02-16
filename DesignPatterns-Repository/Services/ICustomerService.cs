using DesignPatterns_Repository.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesignPatterns_Repository.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetCustomers();

        Task<Customer> GetCustomerByID(int customerId);

        Task InsertCustomer(Customer customer);
        
        Task DeleteCustomer(int customerId);
        
        Task Save();
    }
}
