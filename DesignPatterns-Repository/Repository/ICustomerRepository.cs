using DesignPatterns_Repository.Data;
using System.Collections.Generic;

namespace DesignPatterns_Repository.Repository
{
    public interface ICustomerRepository

    {

        IEnumerable<Customer> GetCustomers();

        Customer GetCustomerByID(int customerId);

        void InsertCustomer(Customer customer);

        void DeleteCustomer(int customerId);

        void UpdateCustomer(Customer customer);

        void Save();

    }
}
