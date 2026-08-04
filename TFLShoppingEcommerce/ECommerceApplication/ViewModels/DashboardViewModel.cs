namespace ECommerceApplication.ViewModels;

public class DashboardViewModel
{
    public decimal TotalSales { get; set; }
    public int TotalOrders { get; set; }
    public int TotalUsers { get; set; }
    public decimal RevenueToday { get; set; }

    public List<TopProductRow> TopProducts { get; set; } = new();
    public List<RevenuePerDayRow> RevenueChartData { get; set; } = new();

    public List<PaymentStatusRow> PaymentStatus { get; set; } = new();
    public List<MonthlyRevenueRow> MonthlyRevenue { get; set; } = new();
    public List<UserGrowthRow> UserGrowth { get; set; } = new();

    public List<LowStockProductRow> LowStockProducts { get; set; } = new();

    public List<CouponUsageRow> CouponUsage { get; set; } = new();
}

public class TopProductRow
{
    public string ProductName { get; set; } = string.Empty;
    public int TotalSold { get; set; }
}

public class RevenuePerDayRow
{
    public DateTime Date { get; set; }
    public decimal Revenue { get; set; }
}

public class PaymentStatusRow
{
    public string Status { get; set; } = string.Empty;
    public int Count { get; set; }
}

public class MonthlyRevenueRow
{
    public int Month { get; set; }
    public decimal Revenue { get; set; }
}

public class UserGrowthRow
{
    public DateTime Date { get; set; }
    public int Users { get; set; }
}

public class LowStockProductRow
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Stock { get; set; }
}

public class CouponUsageRow
{
    public string CouponCode { get; set; } = string.Empty;
    public int UsageCount { get; set; }
}

