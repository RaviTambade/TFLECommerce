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

        public List<Order> getOrderByUserId(int userId)
        {
            return _orderRepo.getOrderByUserId(userId);
        }

        public int placeOrder(int userid, int shipping_address_id)
        {
            return _orderRepo.placeOrder(userid, shipping_address_id);
        }

        public List<OrderItem> getOrderItem(int orderid)
        {
            return _orderRepo.getOrderItem(orderid);
        }

        public bool cancelOrder(int orderid)
        {
            return _orderRepo.cancelOrder(orderid);
        }

        public double GetOrderTotal(int orderid)
        {
            var items = getOrderItem(orderid);

            double total = 0;

            foreach (var item in items)
            {
                double price = item.product.UnitPrice;
                double amount = price * item.Quantity;
                total = total + amount;
            }

            return total;
        }
    }
}