using ECommerceApplication.Models;

namespace ECommerceApplication.Repository.Interfaces
{
    public interface IOrderStatusBackgroundRepository
    {
         void UpdatePendingOrders();
    }
}