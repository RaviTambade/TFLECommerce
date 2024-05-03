// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


OrdersController
       CancelOrder
       PlaceOrder
       GetAllOrders
       GetOrderDetails

Entities
      Order
             Id
             OrderDate
             Customer
             Status
             TotalAmount


Services
IOrderService
OrderService
        CancelOrder
       PlaceOrder
       GetAllOrders
       GetOrderDetails

Repositories
IOrderRepo
OrderServiceRepo
       CancelOrder
       PlaceOrder
       GetAllOrders
       GetOrderDetails
   