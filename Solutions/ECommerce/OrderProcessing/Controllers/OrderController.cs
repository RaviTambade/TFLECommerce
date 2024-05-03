using Transflower.ECommerce.OrderProcessing.Entities;
using Transflower.ECommerce.OrderProcessing.Services.Interfaces;

namespace Transflower.ECommerce.OrderProcessing.Controllers;
public class OrderController{

    private IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<List<Order>> List(){
        return  await  _orderService.GetAllOrders();
    }
    public async Task<List<Order>> GetOrderDetails(){
        
        return await _orderService.GetOrderDetails();
    }
    public async Task<bool>  Delete(int id){
        return await _orderService.CancelOrder(id);
    }
}