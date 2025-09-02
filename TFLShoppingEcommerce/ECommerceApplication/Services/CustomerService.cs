using ECommerceApplication.Models;
using ECommerceApplication.Repository.Interfaces;
using ECommerceApplication.Services.Interfaces;

namespace ECommerceApplication.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _AuthRepo;

        public CustomerService(ICustomerRepository repo)
        {
            _AuthRepo = repo;
        }
        public bool addCustomer(Customer customer)
        {
            bool status = _AuthRepo.addCustomer(customer);
            return status;
        }

        public bool deleteCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public List<Customer> getAllCustomers()
        {
            List<Customer> customers = _AuthRepo.getAllCustomers();
            return customers;
        }

        public Customer getCustomerByEmail(string email)
        {
            Customer customer = _AuthRepo.getCustomerByEmail(email);
            return customer;
        }

        public Customer getCustomerById(int id)
        {
            return _AuthRepo.getCustomerById(id);
        }

        public Customer getCustomerByName(string title)
        {
            throw new NotImplementedException();
        }

        public bool updateCustomer(Customer customer)
        {
            bool status = _AuthRepo.updateCustomer(customer);
            return status;
        }
    }
}