using ECommerceApplication.Models;

namespace ECommerceApplication.Repository.Interfaces
{
    public interface IOrderProcessingRepository
    {
        bool placeOrder(int userid, int shipping_address_i);
        List<Order> getOrderByUserId(int userId);
        List<OrderItem> getOrderItem(int orderid);
    }
}