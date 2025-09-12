using System.Data;
using ECommerceApplication.Models;
using ECommerceApplication.Repository.Interfaces;
using ECommerceApplication.Utils;
using MySql.Data.MySqlClient;

namespace ECommerceApplication.Repository
{
    public class OrderProcessingRepository : IOrderProcessingRepository
    {
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
