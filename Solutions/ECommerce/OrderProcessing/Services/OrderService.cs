using Transflower.ECommerce.OrderProcessing.Entities;
using Transflower.ECommerce.OrderProcessing.Repositories.Interfaces;
using Transflower.ECommerce.OrderProcessing.Services.Interfaces;

public class OrderService : IOrderService
{
    private IOrderRepository _repository;

    public OrderService(IOrderRepository repository)
    {
        _repository = repository;
    }

    public  async Task<bool> CancelOrder(int Id)
    {
       return await _repository.CancelOrder(Id);
    }

    public async Task<List<Order>> GetAllOrders()
    {
       return  await _repository.GetAllOrders();
    }

    public async Task<List<Order>> GetOrderDetails()
    {   
        return await _repository.GetOrderDetails();   
    }

    public async Task<bool> PlaceOrder(Order order)
    {
        return await _repository.PlaceOrder(order);
    }

}