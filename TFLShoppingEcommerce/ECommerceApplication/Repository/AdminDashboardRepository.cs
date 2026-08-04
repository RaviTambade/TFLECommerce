using System.Data;
using ECommerceApplication.Repository.Interfaces;
using ECommerceApplication.Utils;
using ECommerceApplication.ViewModels;
using MySql.Data.MySqlClient;

namespace ECommerceApplication.Repository;

public class AdminDashboardRepository : IAdminDashboardRepository
{
    public DashboardViewModel GetDashboard()
    {
        return new DashboardViewModel
        {
            TotalSales = GetTotalSales(),
            TotalOrders = GetTotalOrders(),
            TopProducts = GetTopProducts(),
            RevenueChartData = GetRevenuePerDay(),
            TotalUsers = GetTotalUsers(),
            RevenueToday = GetRevenueToday(),
            PaymentStatus = GetPaymentStatusBreakdown(),
            MonthlyRevenue = GetMonthlyRevenue(),
            UserGrowth = GetUserGrowth(),
            LowStockProducts = GetLowStockProducts(10),
            CouponUsage = GetCouponUsage()
        };
    }

    private static decimal GetTotalSales()
    {
        using IDbConnection conn = DatabaseConnection.getConnection();
        using IDbCommand cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT COALESCE(SUM(total_amount), 0) FROM orders;";

        try
        {
            conn.Open();
            object? result = cmd.ExecuteScalar();
            return result == null || result == DBNull.Value ? 0 : Convert.ToDecimal(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return 0;
        }
        finally
        {
            if (conn.State == ConnectionState.Open) conn.Close();
        }
    }

    private static int GetTotalOrders()
    {
        using IDbConnection conn = DatabaseConnection.getConnection();
        using IDbCommand cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT COALESCE(COUNT(*), 0) FROM orders;";

        try
        {
            conn.Open();
            object? result = cmd.ExecuteScalar();
            return result == null || result == DBNull.Value ? 0 : Convert.ToInt32(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return 0;
        }
        finally
        {
            if (conn.State == ConnectionState.Open) conn.Close();
        }
    }

    private static List<TopProductRow> GetTopProducts()
    {
        List<TopProductRow> rows = new();
        using IDbConnection conn = DatabaseConnection.getConnection();
        using IDbCommand cmd = conn.CreateCommand();
        cmd.CommandText = @"
SELECT cp.name, SUM(oi.quantity) as total_sold
FROM order_items oi
JOIN categoryproduct cp ON oi.item_id = cp.id
GROUP BY cp.name
ORDER BY total_sold DESC
LIMIT 5;";

        try
        {
            conn.Open();
            using IDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                rows.Add(new TopProductRow
                {
                    ProductName = reader["name"]?.ToString() ?? string.Empty,
                    TotalSold = Convert.ToInt32(reader["total_sold"])
                });
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            if (conn.State == ConnectionState.Open) conn.Close();
        }

        return rows;
    }

    private static List<RevenuePerDayRow> GetRevenuePerDay()
    {
        List<RevenuePerDayRow> rows = new();
        using IDbConnection conn = DatabaseConnection.getConnection();
        using IDbCommand cmd = conn.CreateCommand();
        cmd.CommandText = @"
SELECT DATE(order_date) as date, SUM(total_amount) as revenue
FROM orders
GROUP BY DATE(order_date)
ORDER BY date;";

        try
        {
            conn.Open();
            using IDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                rows.Add(new RevenuePerDayRow
                {
                    Date = Convert.ToDateTime(reader["date"]),
                    Revenue = Convert.ToDecimal(reader["revenue"])
                });
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            if (conn.State == ConnectionState.Open) conn.Close();
        }

        return rows;
    }

    private static int GetTotalUsers()
    {
        using IDbConnection conn = DatabaseConnection.getConnection();
        using IDbCommand cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT COALESCE(COUNT(*), 0) FROM users;";
        try
        {
            conn.Open();
            object? result = cmd.ExecuteScalar();
            return result == null || result == DBNull.Value ? 0 : Convert.ToInt32(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return 0;
        }
        finally
        {
            if (conn.State == ConnectionState.Open) conn.Close();
        }
    }

    private static decimal GetRevenueToday()
    {
        using IDbConnection conn = DatabaseConnection.getConnection();
        using IDbCommand cmd = conn.CreateCommand();
        cmd.CommandText = @"
SELECT COALESCE(SUM(total_amount), 0)
FROM orders
WHERE DATE(order_date) = CURDATE();";
        try
        {
            conn.Open();
            object? result = cmd.ExecuteScalar();
            return result == null || result == DBNull.Value ? 0 : Convert.ToDecimal(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return 0;
        }
        finally
        {
            if (conn.State == ConnectionState.Open) conn.Close();
        }
    }

    private static List<PaymentStatusRow> GetPaymentStatusBreakdown()
    {
        List<PaymentStatusRow> rows = new();
        using IDbConnection conn = DatabaseConnection.getConnection();
        using IDbCommand cmd = conn.CreateCommand();
        cmd.CommandText = @"
SELECT COALESCE(payment_status, 'Pending') AS status, COUNT(*) AS cnt
FROM payments
GROUP BY COALESCE(payment_status, 'Pending')
ORDER BY cnt DESC;";

        try
        {
            conn.Open();
            using IDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                rows.Add(new PaymentStatusRow
                {
                    Status = reader["status"]?.ToString() ?? "Pending",
                    Count = Convert.ToInt32(reader["cnt"])
                });
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            if (conn.State == ConnectionState.Open) conn.Close();
        }
        return rows;
    }

    private static List<MonthlyRevenueRow> GetMonthlyRevenue()
    {
        List<MonthlyRevenueRow> rows = new();
        using IDbConnection conn = DatabaseConnection.getConnection();
        using IDbCommand cmd = conn.CreateCommand();
        cmd.CommandText = @"
SELECT MONTH(order_date) AS month, SUM(total_amount) AS revenue
FROM orders
GROUP BY MONTH(order_date)
ORDER BY month;";
        try
        {
            conn.Open();
            using IDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                rows.Add(new MonthlyRevenueRow
                {
                    Month = Convert.ToInt32(reader["month"]),
                    Revenue = Convert.ToDecimal(reader["revenue"])
                });
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            if (conn.State == ConnectionState.Open) conn.Close();
        }
        return rows;
    }

    private static List<UserGrowthRow> GetUserGrowth()
    {
        List<UserGrowthRow> rows = new();
        using IDbConnection conn = DatabaseConnection.getConnection();
        using IDbCommand cmd = conn.CreateCommand();
        cmd.CommandText = @"
SELECT DATE(created_at) AS date, COUNT(*) AS users
FROM users
GROUP BY DATE(created_at)
ORDER BY date;";
        try
        {
            conn.Open();
            using IDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                rows.Add(new UserGrowthRow
                {
                    Date = Convert.ToDateTime(reader["date"]),
                    Users = Convert.ToInt32(reader["users"])
                });
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            if (conn.State == ConnectionState.Open) conn.Close();
        }
        return rows;
    }

    private static List<LowStockProductRow> GetLowStockProducts(int threshold)
    {
        List<LowStockProductRow> rows = new();
        using IDbConnection conn = DatabaseConnection.getConnection();
        using IDbCommand cmd = conn.CreateCommand();
        cmd.CommandText = @"
SELECT id, name, stock
FROM categoryproduct
WHERE stock < @t
ORDER BY stock ASC, id DESC
LIMIT 20;";
        cmd.Parameters.Add(new MySqlParameter("@t", threshold));

        try
        {
            conn.Open();
            using IDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                rows.Add(new LowStockProductRow
                {
                    ProductId = Convert.ToInt32(reader["id"]),
                    ProductName = reader["name"]?.ToString() ?? string.Empty,
                    Stock = Convert.ToInt32(reader["stock"])
                });
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            if (conn.State == ConnectionState.Open) conn.Close();
        }
        return rows;
    }

    private static List<CouponUsageRow> GetCouponUsage()
    {
        List<CouponUsageRow> rows = new();
        using IDbConnection conn = DatabaseConnection.getConnection();
        using IDbCommand cmd = conn.CreateCommand();
        cmd.CommandText = @"
SELECT discount_code, COUNT(*) as usage_count
FROM order_discounts
GROUP BY discount_code
ORDER BY usage_count DESC
LIMIT 20;";

        try
        {
            conn.Open();
            using IDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                rows.Add(new CouponUsageRow
                {
                    CouponCode = reader["discount_code"]?.ToString() ?? string.Empty,
                    UsageCount = Convert.ToInt32(reader["usage_count"])
                });
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        finally
        {
            if (conn.State == ConnectionState.Open) conn.Close();
        }
        return rows;
    }
}

