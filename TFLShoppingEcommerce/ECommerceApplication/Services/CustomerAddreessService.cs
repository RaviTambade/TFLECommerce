using ECommerceApplication.Models;
using ECommerceApplication.Repository.Interfaces;
using ECommerceApplication.Services.Interfaces;

namespace ECommerceApplication.Services
{
    public class CustomerAddressService : ICustomerAddressService
    {
        ICustomerAddressRepository _repo;
        public CustomerAddressService(ICustomerAddressRepository repo)
        {
            _repo = repo;
        }

        public bool addCustomerAddress(ShippingAddress customerAddress, int userid)
        {
            return _repo.addCustomerAddress(customerAddress, userid);
        }

        public bool deleteCustomerAddress(int id)
        {
            return _repo.deleteCustomerAddress(id);
        }

        public List<ShippingAddress> getAllCustomerAddresses(int userid)
        {
            return _repo.getAllCustomerAddresses(userid);
        }

        public ShippingAddress getCustomerAddressById(int id)
        {
            return getCustomerAddressById(id);
        }

        public bool updateCustomerAddress(ShippingAddress customerAddress)
        {
            return updateCustomerAddress(customerAddress);
        }
    }
}