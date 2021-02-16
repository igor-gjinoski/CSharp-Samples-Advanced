using System.Threading.Tasks;
using System.Collections.Generic;
using DesignPatterns_Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace DesignPatterns_Repository.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _applicationContext;

        public CustomerService(ApplicationDbContext applicationContext)
            => _applicationContext = applicationContext;


        public async Task<IEnumerable<Customer>> GetCustomers() 
            => await _applicationContext.Customers.ToListAsync();


        public async Task<Customer> GetCustomerByID(int customerId) 
            => await _applicationContext.Customers.FindAsync(customerId);


        public async Task InsertCustomer(Customer customer)
        {
            await _applicationContext.Customers.AddAsync(customer);
        }


        public async Task DeleteCustomer(int customerId)
        {
            Customer customer = await _applicationContext.Customers.FindAsync(customerId);

            lock(customer)
            {
                _applicationContext.Customers.Remove(customer);
            }
        }


        public async Task Save()
        {
            await _applicationContext.SaveChangesAsync();
        }
    }
}
