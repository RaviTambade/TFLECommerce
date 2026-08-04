using System.ComponentModel.DataAnnotations;

namespace ECommerceApplication.ViewModels;

public class AdminProductEditViewModel
{
    public int ProductId { get; set; }

    [Required]
    public string ProductTitle { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Range(0, 10000000)]
    public decimal UnitPrice { get; set; }

    [Range(0, 1000000)]
    public int Quantity { get; set; }

    [Required]
    public int? CategoryId { get; set; }

    [Required]
    public int? SubcategoryId { get; set; }

    public string? ExistingImagePath { get; set; }
}

