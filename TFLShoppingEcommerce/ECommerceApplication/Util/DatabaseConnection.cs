using System.Data;
using MySql.Data.MySqlClient;

namespace ECommerceApplication.Utils
{
    public class DatabaseConnection
    {
        public static IDbConnection getConnection()
        {
            IDbConnection conn = new MySqlConnection();
            conn.ConnectionString = @"server=localhost;port=3306;user=root;password=password;database=TFLShoppingEcommerce";
            return conn;
        }
    }
}