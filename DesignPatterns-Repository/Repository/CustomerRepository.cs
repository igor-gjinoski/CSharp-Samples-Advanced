using DesignPatterns_Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesignPatterns_Repository.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _applicationContext;

        public CustomerRepository(ApplicationDbContext applicationContext)
            => _applicationContext = applicationContext;


        public IEnumerable<Customer> GetCustomers() 
            => _applicationContext.Customers.ToList();

        public Customer GetCustomerByID(int customerId) 
            => _applicationContext.Customers.Find(customerId);


        public void InsertCustomer(Customer customer)
        {
            _applicationContext.Customers.Add(customer);
        }


        public void DeleteCustomer(int customerId)
        {
            Customer customer = _applicationContext.Customers.Find(customerId);

            _applicationContext.Customers.Remove(customer);
        }


        public void UpdateCustomer(Customer customer)
        {
            _applicationContext.Entry(customer).State = EntityState.Modified;
        }


        public void Save()

        {
            _applicationContext.SaveChanges();
        }
    }
}
