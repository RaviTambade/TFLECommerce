using System.Data;
using ECommerceApplication.Models;
using ECommerceApplication.Repository.Interfaces;
using ECommerceApplication.Utils;
using ECommerceApplication.ViewModels;
using MySql.Data.MySqlClient;

namespace ECommerceApplication.Repository;

public class AdminUserRepository : IAdminUserRepository
{
    public List<AdminUserListItem> GetAllUsersWithStats()
    {
        List<AdminUserListItem> users = new();

        using IDbConnection conn = DatabaseConnection.getConnection();
        using IDbCommand cmd = conn.CreateCommand();
        cmd.CommandText = @"
SELECT
    u.id,
    u.username,
    u.email,
    u.address,
    COALESCE(COUNT(o.id), 0) AS orders_count,
    COALESCE(SUM(o.total_amount), 0) AS total_spent
FROM users u
LEFT JOIN orders o ON o.customer_id = u.id
GROUP BY u.id, u.username, u.email, u.address
ORDER BY u.id DESC;";

        try
        {
            conn.Open();
            using IDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                users.Add(new AdminUserListItem
                {
                    CustomerId = Convert.ToInt32(reader["id"]),
                    UserName = reader["username"]?.ToString() ?? string.Empty,
                    Email = reader["email"]?.ToString() ?? string.Empty,
                    Address = reader["address"]?.ToString() ?? string.Empty,
                    OrdersCount = Convert.ToInt32(reader["orders_count"]),
                    TotalSpent = Convert.ToDecimal(reader["total_spent"])
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

        return users;
    }

    public AdminUserDetailsViewModel? GetUserDetails(int userId)
    {
        AdminUserDetailsViewModel? vm = null;

        using (IDbConnection conn = DatabaseConnection.getConnection())
        using (IDbCommand cmd = conn.CreateCommand())
        {
            cmd.CommandText = "SELECT id, username, email, address FROM users WHERE id = @id LIMIT 1;";
            cmd.Parameters.Add(new MySqlParameter("@id", userId));

            try
            {
                conn.Open();
                using IDataReader reader = cmd.ExecuteReader();
                if (!reader.Read()) return null;

                vm = new AdminUserDetailsViewModel
                {
                    CustomerId = Convert.ToInt32(reader["id"]),
                    UserName = reader["username"]?.ToString() ?? string.Empty,
                    Email = reader["email"]?.ToString() ?? string.Empty,
                    Address = reader["address"]?.ToString() ?? string.Empty
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
            finally
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
        }

        // Orders for that user (reusing same shape as existing Order model expects)
        vm.Orders = GetOrdersForUser(userId);
        return vm;
    }

    public bool DeleteUser(int userId)
    {
        using IDbConnection conn = DatabaseConnection.getConnection();
        using IDbCommand cmd = conn.CreateCommand();
        cmd.CommandText = "DELETE FROM users WHERE id = @id";
        cmd.Parameters.Add(new MySqlParameter("@id", userId));

        try
        {
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
        finally
        {
            if (conn.State == ConnectionState.Open) conn.Close();
        }
    }

    private static List<Order> GetOrdersForUser(int userId)
    {
        List<Order> orders = new();

        using IDbConnection conn = DatabaseConnection.getConnection();
        using IDbCommand cmd = conn.CreateCommand();
        cmd.CommandText = @"
SELECT
    o.id,
    o.order_date,
    o.total_amount,
    o.shipping_date,
    o.status,
    s.address AS address,
    dc.code AS coupon_code,
    COALESCE(dc.discount_percentage, 0) AS discount_percentage,
    COALESCE((o.total_amount * dc.discount_percentage) / 100, 0) AS discount_amount,
    (o.total_amount - COALESCE((o.total_amount * dc.discount_percentage) / 100, 0)) AS final_amount,
    COALESCE(p.payment_status, 'Pending') AS payment_status
FROM orders o
LEFT JOIN shipping_addresses s ON s.shipping_address_id = o.shipping_address_id
LEFT JOIN order_discounts od ON o.id = od.order_id
LEFT JOIN discount_codes dc ON od.discount_code = dc.code
LEFT JOIN payments p ON o.id = p.order_id
WHERE o.customer_id = @userid
ORDER BY o.order_date DESC;";
        cmd.Parameters.Add(new MySqlParameter("@userid", userId));

        try
        {
            conn.Open();
            using IDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var order = new Order
                {
                    OrderId = Convert.ToInt32(reader["id"]),
                    orderDate = DateOnly.FromDateTime(Convert.ToDateTime(reader["order_date"])),
                    TotalAmount = Convert.ToDouble(reader["total_amount"]),
                    ShippingDate = reader["shipping_date"] == DBNull.Value
                        ? null
                        : DateOnly.FromDateTime(Convert.ToDateTime(reader["shipping_date"])),
                    status = reader["status"]?.ToString() ?? string.Empty,
                    Address = reader["address"]?.ToString() ?? string.Empty,
                    CouponCode = reader["coupon_code"] == DBNull.Value ? null : reader["coupon_code"]?.ToString(),
                    DiscountPercentage = Convert.ToDecimal(reader["discount_percentage"]),
                    DiscountAmount = Convert.ToDecimal(reader["discount_amount"]),
                    FinalAmount = Convert.ToDecimal(reader["final_amount"]),
                    PaymentStatus = reader["payment_status"]?.ToString() ?? "Pending"
                };
                orders.Add(order);
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

        return orders;
    }
}

