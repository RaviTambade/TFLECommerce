using System.Collections.Generic;
namespace Transflower.ECommerce.OrderProcessing.Entities;
public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public string Customer { get; set; }
    public string Status { get; set; }
    public float TotalAmount { get; set; }

    public Order(){
        this.Id = 10;
        this.OrderDate = DateTime.Now;
        this.Customer = "Ashish";
        this.Status = "Processing";
        this.TotalAmount = 500;
    }

    public Order(int id, DateTime orderDate, string customer, string status, float totalAmount){
        this.Id = id;
        this.OrderDate = orderDate;
        this.Customer = customer;
        this.Status = status;
        this.TotalAmount = totalAmount;

    }

}            