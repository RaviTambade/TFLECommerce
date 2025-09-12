using ECommerceApplication.Models;
using ECommerceApplication.Repository.Interfaces;
using ECommerceApplication.Services.Interfaces;

namespace ECommerceApplication.Services
{
    public class OrderProcessingService : IOrderProcessingService
    {
        private readonly IOrderProcessingRepository _orderRepo;

        public OrderProcessingService(IOrderProcessingRepository repo)
        {
            _orderRepo = repo;
        }

        public bool placeOrder(int userid, int shipping_address_id)
        {
            return _orderRepo.placeOrder(userid, shipping_address_id);
        }
    }
}