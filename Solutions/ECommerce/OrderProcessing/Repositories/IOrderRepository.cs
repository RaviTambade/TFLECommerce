
using Transflower.ECommerce.OrderProcessing.Entities;
namespace Transflower.ECommerce.OrderProcessing.Repositories.Interfaces;
public interface IOrderRepository
{
    public   Task<List<Order>> GetAllOrders();
    public  Task<Order> GetOrderDetails(int id);
    public Task<bool> PlaceOrder(Order order);

    public Task<bool> CancelOrder(int id);

}