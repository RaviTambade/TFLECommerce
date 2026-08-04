using System.Data;
using ECommerceApplication.Models;
using ECommerceApplication.Repository.Interfaces;
using ECommerceApplication.Utils;
using MySql.Data.MySqlClient;

namespace ECommerceApplication.Repository
{
    public class WishlistRepository : IWishlistRepository
    {
        public bool AddToWishlist(int userId, int productId)
        {
            using IDbConnection conn = DatabaseConnection.getConnection();
            using IDbCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"INSERT INTO wishlist(user_id, product_id)
                                SELECT @uid, @pid
                                WHERE NOT EXISTS (
                                    SELECT 1 FROM wishlist WHERE user_id=@uid AND product_id=@pid
                                )";
            cmd.Parameters.Add(new MySqlParameter("@uid", userId));
            cmd.Parameters.Add(new MySqlParameter("@pid", productId));

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool RemoveFromWishlist(int userId, int productId)
        {
            using IDbConnection conn = DatabaseConnection.getConnection();
            using IDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM wishlist WHERE user_id=@uid AND product_id=@pid";
            cmd.Parameters.Add(new MySqlParameter("@uid", userId));
            cmd.Parameters.Add(new MySqlParameter("@pid", productId));

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public List<WishlistItem> GetWishlist(int userId)
        {
            List<WishlistItem> items = new List<WishlistItem>();
            using IDbConnection conn = DatabaseConnection.getConnection();
            using IDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT w.id, w.user_id, w.product_id, w.created_at,
                                       p.name, p.description, p.price, p.stock, p.image
                                FROM wishlist w
                                JOIN categoryproduct p ON p.id = w.product_id
                                WHERE w.user_id = @uid
                                ORDER BY w.created_at DESC";
            cmd.Parameters.Add(new MySqlParameter("@uid", userId));

            try
            {
                conn.Open();
                using IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    WishlistItem item = new WishlistItem
                    {
                        WishlistId = Convert.ToInt32(reader["id"]),
                        UserId = Convert.ToInt32(reader["user_id"]),
                        ProductId = Convert.ToInt32(reader["product_id"]),
                        CreatedAt = Convert.ToDateTime(reader["created_at"]),
                        Product = new Product
                        {
                            ProductId = Convert.ToInt32(reader["product_id"]),
                            ProductTitle = reader["name"]?.ToString() ?? string.Empty,
                            Description = reader["description"]?.ToString() ?? string.Empty,
                            UnitPrice = Convert.ToDouble(reader["price"]),
                            Quantity = Convert.ToInt32(reader["stock"]),
                            ProductImage = reader["image"]?.ToString() ?? string.Empty
                        }
                    };

                    items.Add(item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return items;
        }
    }
}
