using System.Data;
using ECommerceApplication.Models;
using ECommerceApplication.Repository.Interfaces;
using ECommerceApplication.Utils;
using MySql.Data.MySqlClient;

namespace ECommerceApplication.Repository
{
    public class OrderProcessingRepository : IOrderProcessingRepository
    {
        public List<Order> getOrderByUserId(int userId)
        {
            List<Order> orders = new List<Order>();
            IDbConnection conn = DatabaseConnection.getConnection();
            IDbCommand cmd = new MySqlCommand();
            try
            {
                conn.Open();
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
ORDER BY o.order_date DESC";
                cmd.Connection = conn;
                cmd.Parameters.Add(new MySqlParameter("@userid", userId));
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Order order = new Order();

                    order.OrderId = reader.GetInt32(reader.GetOrdinal("id"));
                    order.orderDate = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("order_date")));
                    order.TotalAmount = reader.GetDouble(reader.GetOrdinal("total_amount"));
                    order.ShippingDate = reader["shipping_date"] == DBNull.Value
                        ? null
                        : DateOnly.FromDateTime(Convert.ToDateTime(reader["shipping_date"]));
                    order.status = reader["status"]?.ToString() ?? string.Empty;
                    order.Address = reader["address"]?.ToString() ?? string.Empty;
                    order.CouponCode = reader["coupon_code"] == DBNull.Value ? null : reader["coupon_code"].ToString();
                    order.DiscountPercentage = Convert.ToDecimal(reader["discount_percentage"]);
                    order.DiscountAmount = Convert.ToDecimal(reader["discount_amount"]);
                    order.FinalAmount = Convert.ToDecimal(reader["final_amount"]);
                    order.PaymentStatus = reader["payment_status"]?.ToString() ?? "Pending";
                    orders.Add(order);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return orders;

        }

        public int GetOrderIdFromOrderItem(int orderItemId)
        {
            int orderId = 0;

            IDbConnection conn = DatabaseConnection.getConnection();
            IDbCommand cmd = new MySqlCommand();

            try
            {
                conn.Open();

                cmd.Connection = conn;
                cmd.CommandText = "SELECT order_id FROM order_items WHERE id = @id";
                cmd.Parameters.Add(new MySqlParameter("@id", orderItemId));

                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    orderId = Convert.ToInt32(result);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                conn.Close();
            }

            return orderId;
        }
        public int placeOrder(int userid, int shipping_address_id)
        {
            int orderId = 0;
            IDbConnection conn = DatabaseConnection.getConnection();
            IDbCommand cmd = new MySqlCommand();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "Place_Order";
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameters in the correct order
                cmd.Parameters.Add(new MySqlParameter("userid", userid));
                cmd.Parameters.Add(new MySqlParameter("odate", DateTime.Now));
                cmd.Parameters.Add(new MySqlParameter("shipdate", DateTime.Now.Date.AddDays(7)));
                cmd.Parameters.Add(new MySqlParameter("shipId", shipping_address_id));

                cmd.ExecuteNonQuery();

                cmd.Parameters.Clear();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT LAST_INSERT_ID();";



               int orderitemId = Convert.ToInt32(cmd.ExecuteScalar());  // ✅ GET ORDER ID

              orderId= GetOrderIdFromOrderItem(orderitemId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return orderId;
        }

        public List<OrderItem> getOrderItem(int orderid)
        {
            List<OrderItem> orders = new List<OrderItem>();
            IDbConnection conn = DatabaseConnection.getConnection();
            IDbCommand cmd = new MySqlCommand();

            try
            {
                conn.Open();

                cmd.CommandText = @"SELECT oi.id, oi.item_id, cp.name, cp.image, cp.price, oi.quantity 
                            FROM order_items oi 
                            JOIN categoryproduct cp ON cp.id = oi.item_id 
                            WHERE oi.order_id = @orderid";

                cmd.Connection = conn;
                cmd.Parameters.Add(new MySqlParameter("@orderid", orderid));

                IDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    OrderItem order = new OrderItem();

                    // ✅ IMPORTANT: Initialize product object
                    order.product = new Product();

                    order.OrderItemId = reader.GetInt32(reader.GetOrdinal("id"));
                    int productId = Convert.ToInt32(reader["item_id"]);
                    order.ItemId = productId;
                    order.product.ProductId = productId;
                    order.product.ProductTitle = reader["name"].ToString();
                    order.product.ProductImage = reader["image"].ToString();
                    order.product.UnitPrice = Convert.ToDouble(reader["price"]);
                    order.Quantity = Convert.ToInt32(reader["quantity"]);

                    orders.Add(order);
                }

                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                conn.Close(); // ✅ always close connection
            }

            return orders;
        }

        public bool cancelOrder(int orderid)
        {
            bool status = false;

            IDbConnection conn = DatabaseConnection.getConnection();
            IDbCommand cmd = new MySqlCommand();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText= "cancel_order";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("orderid", orderid));
                cmd.ExecuteNonQuery();
                status = true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return status;
        }

        public bool saveOrderDiscount(int orderId, string discountCode)
        {
            bool status = false;
            IDbConnection conn = DatabaseConnection.getConnection();
            IDbCommand cmd = new MySqlCommand();

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = @"INSERT INTO order_discounts(order_id, discount_code)
                                    VALUES(@orderId, @discountCode)";
                cmd.Parameters.Add(new MySqlParameter("@orderId", orderId));
                cmd.Parameters.Add(new MySqlParameter("@discountCode", discountCode));
                cmd.ExecuteNonQuery();
                status = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return status;
        }

        public decimal getOrderDiscountPercentage(int orderId)
        {
            decimal percentage = 0;
            IDbConnection conn = DatabaseConnection.getConnection();
            IDbCommand cmd = new MySqlCommand();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = @"SELECT COALESCE(dc.discount_percentage, 0)
                                    FROM order_discounts od
                                    JOIN discount_codes dc ON dc.code = od.discount_code
                                    WHERE od.order_id = @orderId
                                    LIMIT 1";
                cmd.Parameters.Add(new MySqlParameter("@orderId", orderId));
                object? result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    percentage = Convert.ToDecimal(result);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return percentage;
        }

    }
}
