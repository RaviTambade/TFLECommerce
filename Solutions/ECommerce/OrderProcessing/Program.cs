// See https://aka.ms/new-console-template for more information

using System.Collections.Generic;
using System.Reflection;
using Transflower.ECommerce.OrderProcessing.Entities;
using Transflower.ECommerce.OrderProcessing.Repositories.Interfaces;
using  Transflower.ECommerce.OrderProcessing.Services.Interfaces;
using  Transflower.ECommerce.OrderProcessing.Services;
using Transflower.ECommerce.OrderProcessing.Controllers;
using Transflower.ECommerce.OrderProcessing.Repositories;

Order order = new Order();

Console.WriteLine("Id: "+ order.Id + " OrderDate :" + order.OrderDate + " Customer : " + order.Customer + " Status : " + order.Status + " Total Amount : " + order.TotalAmount);
 

IOrderRepository Repo = new OrderRepository();

IOrderService service = new OrderService(Repo);

 
OrderController controller = new OrderController(service);
List<Order> allOrders = await controller.List();

foreach(Order o in allOrders)
{
    Console.WriteLine(o.Id + " " + o.OrderDate + " " + o.Customer + " " + o.Status + " " + o.TotalAmount);
}

