using ECommerceApplication.Models;

namespace ECommerceApplication.Repository.Interfaces
{
    public interface ICustomerRepository
    {
        List<Customer> getAllCustomers();
        Customer getCustomerById(int id);
        Customer getCustomerByEmail(string email);
        Customer getCustomerByName(string title);
        bool addCustomer(Customer customer);
        bool updateCustomer(Customer customer);
        bool deleteCustomer(int id);
    }
}