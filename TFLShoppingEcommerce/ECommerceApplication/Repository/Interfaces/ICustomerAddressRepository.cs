using ECommerceApplication.Models;

namespace ECommerceApplication.Repository.Interfaces
{
    public interface ICustomerAddressRepository
    {
        List<ShippingAddress> getAllCustomerAddresses(int userid);
        ShippingAddress getCustomerAddressById(int id);
        bool addCustomerAddress(ShippingAddress customerAddress, int userid);
        bool updateCustomerAddress(ShippingAddress customerAddress);
        bool deleteCustomerAddress(int id);
    }
}