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
                cmd.CommandText = "select o.id, o.order_date, o.total_amount,o.shipping_date,o.status, s.address as address from orders o join shipping_addresses s on s.shipping_address_id = o.shipping_address_id where o.customer_id = @userid";
                cmd.Connection = conn;
                cmd.Parameters.Add(new MySqlParameter("@userid", userId));
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Order order = new Order();

                    order.OrderId = reader.GetInt32(reader.GetOrdinal("id"));
                    order.orderDate = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("order_date")));
                    order.TotalAmount = reader.GetDouble(reader.GetOrdinal("total_amount"));
                    order.ShippingDate = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("shipping_date")));
                    order.status = reader.GetString(reader.GetOrdinal("status"));
                    order.Address = reader.GetString(reader.GetOrdinal("address"));
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

                cmd.CommandText = @"SELECT oi.id, cp.name, cp.image, cp.price, oi.quantity 
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
    
    
    }
}
