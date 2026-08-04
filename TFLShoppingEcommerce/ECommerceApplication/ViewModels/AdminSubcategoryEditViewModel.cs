using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerceApplication.ViewModels;

public class AdminSubcategoryEditViewModel
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public int? CategoryId { get; set; }

    public List<SelectListItem> Categories { get; set; } = new();
}

