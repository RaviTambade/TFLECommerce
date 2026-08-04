using Microsoft.AspNetCore.Mvc.Rendering;
using ECommerceApplication.Repository.Interfaces;

namespace ECommerceApplication.ViewModels;

public class AdminProductIndexViewModel
{
    // Filters
    public string? Search { get; set; }
    public int? CategoryId { get; set; }
    public int? SubcategoryId { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public bool InStockOnly { get; set; }

    // Dropdown data
    public List<SelectListItem> Categories { get; set; } = new();
    public List<SelectListItem> Subcategories { get; set; } = new();

    // Results
    public List<AdminProductListItem> Products { get; set; } = new();

    // UX
    public int LowStockThreshold { get; set; } = 10;
}

