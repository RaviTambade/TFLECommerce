namespace ECommerceApplication.Models;

public class Subcategory
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int? ProductId { get; set; }
    public string? ProductName { get; set; }
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public int ProductsCount { get; set; }
}

