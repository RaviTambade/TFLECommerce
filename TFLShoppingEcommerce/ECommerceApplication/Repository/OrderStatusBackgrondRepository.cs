using Microsoft.Extensions.Hosting;
using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using ECommerceApplication.Utils;
using ECommerceApplication.Repository.Interfaces;

public class OrderStatusBackgroundRepository :IOrderStatusBackgroundRepository
  {

    public void UpdatePendingOrders()
    {
        using IDbConnection conn = DatabaseConnection.getConnection();
        using IDbCommand cmd = new MySqlCommand();

        try
        {
            conn.Open();
            cmd.Connection = conn;

            cmd.CommandText = @"
                UPDATE orders
                SET status = 'Processing'
                WHERE status = 'Pending'
                AND order_date <= DATE_SUB(NOW(), INTERVAL 24 HOUR);
            ";

            cmd.ExecuteNonQuery(); 
        } 
        catch (Exception ex)
        {
            Console.WriteLine("Background Service Error: " + ex.Message);
        }
    }
}    