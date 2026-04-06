using ECommerceApplication.Repository.Interfaces;

namespace ECommerceApplication.Services.Interfaces
{
    public interface IOrderProcessingService : IOrderProcessingRepository
    {
        double GetOrderTotal(int customerId);
    }
}