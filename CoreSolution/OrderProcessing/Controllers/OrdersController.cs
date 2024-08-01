using Transflower.ECommerce.OrderProcessing.Entities;
using Transflower.ECommerce.OrderProcessing.Services.Interfaces;

namespace Transflower.ECommerce.OrderProcessing.Controllers;
public class OrdersController{

    private IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<List<Order>> List(){
        return  await  _orderService.GetAllOrders();
    }
    public async Task<Order> GetOrderDetails(int id){
        
        return await _orderService.GetOrderDetails(id);
    }
    public async Task<bool>  Delete(int id){
        return await _orderService.CancelOrder(id);
    }
}