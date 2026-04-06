
using ECommerceApplication.Repository.Interfaces;
using ECommerceApplication.Utils;
using System.Data;
using MySql.Data.MySqlClient;

namespace ECommerceApplication.Repository
{

    public class PaymentProcessingRepository : IPaymentProcessingRepository
    {
        public bool SavePayment(int orderId, DateTime paymentDate, int paymentAmount, string paymentMethod, string paymentStatus)
        {
            string query = @"INSERT INTO payments
                        (order_id, payment_date, payment_amount, payment_method, payment_status)
                        VALUES
                        (@OrderId, @PaymentDate, @PaymentAmount, @PaymentMethod, @PaymentStatus)";

            using IDbConnection connection = DatabaseConnection.getConnection();
            using IDbCommand cmd = new MySqlCommand(query, (MySqlConnection)connection);

            cmd.Parameters.Add(new MySqlParameter("@OrderId", orderId));
            cmd.Parameters.Add(new MySqlParameter("@PaymentDate", paymentDate));
            cmd.Parameters.Add(new MySqlParameter("@PaymentAmount", paymentAmount));
            cmd.Parameters.Add(new MySqlParameter("@PaymentMethod", paymentMethod));
            cmd.Parameters.Add(new MySqlParameter("@PaymentStatus", paymentStatus));

            connection.Open();
            return cmd.ExecuteNonQuery() > 0;
        }

    }
}