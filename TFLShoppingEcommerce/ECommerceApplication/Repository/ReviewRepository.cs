using System.Data;
using ECommerceApplication.Models;
using ECommerceApplication.Repository.Interfaces;
using ECommerceApplication.Utils;
using MySql.Data.MySqlClient;

namespace ECommerceApplication.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        public bool AddReview(int productId, int userId, int rating, string reviewText)
        {
            bool status = false;
            using IDbConnection conn = DatabaseConnection.getConnection();
            using IDbCommand cmd = conn.CreateCommand();

            try
            {
                conn.Open();

                // Try with created_at first.
                cmd.CommandText = @"INSERT INTO reviews(product_id, user_id, rating, review_text, created_at)
                                    VALUES(@pid, @uid, @rating, @reviewText, NOW())";
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new MySqlParameter("@pid", productId));
                cmd.Parameters.Add(new MySqlParameter("@uid", userId));
                cmd.Parameters.Add(new MySqlParameter("@rating", rating));
                cmd.Parameters.Add(new MySqlParameter("@reviewText", reviewText));

                try
                {
                    status = cmd.ExecuteNonQuery() > 0;
                }
                catch
                {
                    // Fallback for schemas without created_at column.
                    cmd.CommandText = @"INSERT INTO reviews(product_id, user_id, rating, review_text)
                                        VALUES(@pid, @uid, @rating, @reviewText)";
                    status = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return status;
        }

        public List<Review> GetReviews(int productId)
        {
            List<Review> reviews = new List<Review>();
            using IDbConnection conn = DatabaseConnection.getConnection();
            using IDbCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"SELECT r.id, r.product_id, r.user_id, r.rating, r.review_text, r.created_at, u.username
                                FROM reviews r
                                JOIN users u ON u.id = r.user_id
                                WHERE r.product_id = @pid
                                ORDER BY r.created_at DESC";
            cmd.Parameters.Add(new MySqlParameter("@pid", productId));

            try
            {
                conn.Open();
                using IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    reviews.Add(new Review
                    {
                        ReviewId = Convert.ToInt32(reader["id"]),
                        ProductId = Convert.ToInt32(reader["product_id"]),
                        UserId = Convert.ToInt32(reader["user_id"]),
                        Rating = Convert.ToInt32(reader["rating"]),
                        ReviewText = reader["review_text"]?.ToString() ?? string.Empty,
                        CreatedAt = Convert.ToDateTime(reader["created_at"]),
                        UserName = reader["username"]?.ToString() ?? string.Empty
                    });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return reviews;
        }

        public decimal GetAverageRating(int productId)
        {
            decimal avg = 0;
            using IDbConnection conn = DatabaseConnection.getConnection();
            using IDbCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT COALESCE(AVG(rating), 0) FROM reviews WHERE product_id = @pid";
            cmd.Parameters.Add(new MySqlParameter("@pid", productId));

            try
            {
                conn.Open();
                object? result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    avg = Math.Round(Convert.ToDecimal(result), 1);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return avg;
        }

        public bool HasUserPurchasedProduct(int userId, int productId)
        {
            using IDbConnection conn = DatabaseConnection.getConnection();
            using IDbCommand cmd = conn.CreateCommand();

            try
            {
                conn.Open();
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new MySqlParameter("@uid", userId));
                cmd.Parameters.Add(new MySqlParameter("@pid", productId));

                // Try item_id schema first.
                cmd.CommandText = @"
                    SELECT COUNT(1)
                    FROM orders o
                    JOIN order_items oi ON oi.order_id = o.id
                    WHERE o.customer_id = @uid
                      AND oi.item_id = @pid";
                object? result;
                try
                {
                    result = cmd.ExecuteScalar();
                }
                catch
                {
                    // Fallback for product_id schema.
                    cmd.CommandText = @"
                        SELECT COUNT(1)
                        FROM orders o
                        JOIN order_items oi ON oi.order_id = o.id
                        WHERE o.customer_id = @uid
                          AND oi.product_id = @pid";
                    result = cmd.ExecuteScalar();
                }

                if (result == null || result == DBNull.Value) return false;
                return Convert.ToInt32(result) > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
