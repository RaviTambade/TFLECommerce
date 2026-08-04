namespace ECommerceApplication.ViewModels;

public class AdminUserListItem
{
    public int CustomerId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int OrdersCount { get; set; }
    public decimal TotalSpent { get; set; }
}

