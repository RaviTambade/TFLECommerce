using System.ComponentModel;
namespace Transflower.ECommerce.ProductCatalog.Entities;

public class Product
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public float UnitPrice { get; set; }
    public int StockAvailable { get; set; }

    public Product(){
        this.Id = 987;
        this.Title = "Motog";
        this.Description = "Vehicle";
        this.Category = "Two Wheelers";
        this.UnitPrice =50000;
        this.StockAvailable =25;
    }

    public Product(int id, string title, string description, string category, float unitPrice, int stockAvailable){
        this.Id = id;
        this.Title = title;
        this.Description = description;
        this.Category = category;
        this.UnitPrice = unitPrice;
        this.StockAvailable = stockAvailable;
    }
    

}
