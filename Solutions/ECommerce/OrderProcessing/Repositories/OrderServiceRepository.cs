using Transflower.ECommerce.OrderProcessing.Entities;
using Transflower.ECommerce.OrderProcessing.Repositories.Interfaces;
namespace Transflower.ECommerce.OrderProcessing.Repositories;

public class OrderServiceRepository : IOrderRepository
{
    private List<Order> _orders;
    private int Id;

    public OrderServiceRepository(){
        List<Order> orders = new List<Order>();
        orders.Add(new Order { Id = 1, Customer = "Ashish", OrderDate = DateTime.Now, Status = "Processing", TotalAmount = 500});
        orders.Add(new Order { Id = 2, Customer = "Khush", OrderDate = DateTime.Now, Status = "Completed", TotalAmount = 1500});
        orders.Add(new Order { Id = 3, Customer = "Harsh", OrderDate = DateTime.Now, Status = "Cancle Order", TotalAmount = 1000});
        orders.Add(new Order { Id = 4, Customer = "DP", OrderDate = DateTime.Now, Status = "Intransit", TotalAmount = 400});
        orders.Add(new Order { Id = 5, Customer = "Adi", OrderDate = DateTime.Now, Status = "Shipped", TotalAmount = 600});
}

    public async Task<List<Order>> GetAllOrders()
    {
        await Task.Delay(1000);
        return _orders;
    }

    public async Task<List<Order>> GetOrderDetails()
    {
        await Task.Delay(1000);
        Order theOrder = _orders.Find(order => order.Id == Id);
        return _orders;
    }

      public async Task<bool> PlaceOrder(Order order)
    {
        await Task.Delay(5000);
        _orders.Add(order);
       return true;
    }

     public async Task<bool> CancelOrder(int Id)
    {
        await Task.Delay(5000);
        Order theOrder=_orders.Find(order => order.Id == Id);
        if (theOrder != null){
            _orders.Remove(theOrder);
        }
        return true;
    }

}
