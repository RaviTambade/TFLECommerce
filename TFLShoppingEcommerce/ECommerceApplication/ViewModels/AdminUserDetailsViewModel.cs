using ECommerceApplication.Models;

namespace ECommerceApplication.ViewModels;

public class AdminUserDetailsViewModel
{
    public int CustomerId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

    public List<Order> Orders { get; set; } = new();
}

