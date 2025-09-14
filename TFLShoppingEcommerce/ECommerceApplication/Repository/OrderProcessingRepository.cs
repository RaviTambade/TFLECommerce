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

        public bool placeOrder(int userid, int shipping_address_id)
        {
            bool status = false;
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
                cmd.Parameters.Add(new MySqlParameter("odate", DateTime.Now.Date));
                cmd.Parameters.Add(new MySqlParameter("shipdate", DateTime.Now.Date.AddDays(7)));
                cmd.Parameters.Add(new MySqlParameter("shipId", shipping_address_id));

                cmd.ExecuteNonQuery();
                status = true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return status;
        }
    }
}
