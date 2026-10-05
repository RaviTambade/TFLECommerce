using System.Data;
using ECommerceApplication.Models;
using ECommerceApplication.Repository.Interfaces;
using ECommerceApplication.Utils;
using MySql.Data.MySqlClient;

namespace ECommerceApplication.Repository;

public class AdminSubcategoryRepository : IAdminSubcategoryRepository
{
    public List<Subcategory> GetAll()
    {
        List<Subcategory> items = new();
        using IDbConnection conn = DatabaseConnection.getConnection();
        using IDbCommand cmd = conn.CreateCommand();

        cmd.CommandText = @"
SELECT
    s.id,
    s.product_id,
    s.categoryName,
    s.category_id,
    c.name AS category_name,
    p.name AS product_name,
    COALESCE(COUNT(p.id), 0) AS products_count
FROM subcategories s
JOIN categories c ON c.id = s.category_id
LEFT JOIN products p ON p.id = s.product_id
GROUP BY s.id, s.product_id, s.categoryName, s.category_id, c.name, p.name
ORDER BY c.name, s.categoryName;";

        try
        {
            conn.Open();
            using IDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                items.Add(new Subcategory
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Name = reader["categoryName"]?.ToString() ?? string.Empty,
                    ProductId = reader["product_id"] == DBNull.Value ? null : Convert.ToInt32(reader["product_id"]),
                    ProductName = reader["product_name"] == DBNull.Value ? null : reader["product_name"]?.ToString(),
                    CategoryId = Convert.ToInt32(reader["category_id"]),
                    CategoryName = reader["category_name"]?.ToString(),
                    ProductsCount = Convert.ToInt32(reader["products_count"])
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

        return items;
    }

    public List<Subcategory> GetByCategoryId(int categoryId)
    {
        List<Subcategory> items = new();
        using IDbConnection conn = DatabaseConnection.getConnection();
        using IDbCommand cmd = conn.CreateCommand();

        cmd.CommandText = @"
SELECT s.id, s.product_id, s.categoryName, s.category_id
FROM subcategories s
WHERE s.category_id = @categoryId
ORDER BY s.categoryName;";
        cmd.Parameters.Add(new MySqlParameter("@categoryId", categoryId));

        try
        {
            conn.Open();
            using IDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                items.Add(new Subcategory
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Name = reader["categoryName"]?.ToString() ?? string.Empty,
                    ProductId = reader["product_id"] == DBNull.Value ? null : Convert.ToInt32(reader["product_id"]),
                    CategoryId = Convert.ToInt32(reader["category_id"])
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

        return items;
    }

    public Subcategory? GetById(int id)
    {
        using IDbConnection conn = DatabaseConnection.getConnection();
        using IDbCommand cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT id, product_id, categoryName, category_id FROM subcategories WHERE id = @id LIMIT 1;";
        cmd.Parameters.Add(new MySqlParameter("@id", id));

        try
        {
            conn.Open();
            using IDataReader reader = cmd.ExecuteReader();
            if (!reader.Read()) return null;
            return new Subcategory
            {
                Id = Convert.ToInt32(reader["id"]),
                Name = reader["categoryName"]?.ToString() ?? string.Empty,
                ProductId = reader["product_id"] == DBNull.Value ? null : Convert.ToInt32(reader["product_id"]),
                CategoryId = Convert.ToInt32(reader["category_id"])
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

    public int Create(string name, int categoryId, int? productId)
    {
        using var conn = (MySqlConnection)DatabaseConnection.getConnection();

        try
        {
            conn.Open();
            using var tx = conn.BeginTransaction();

            int subcategoryId;
            using (var insertCmd = new MySqlCommand(@"
INSERT INTO subcategories(product_id, categoryName, category_id)
VALUES(NULL, @name, @categoryId);
SELECT LAST_INSERT_ID();", conn, tx))
            {
                insertCmd.Parameters.Add(new MySqlParameter("@name", name.Trim()));
                insertCmd.Parameters.Add(new MySqlParameter("@categoryId", categoryId));
                object? inserted = insertCmd.ExecuteScalar();
                if (inserted == null)
                {
                    tx.Rollback();
                    return 0;
                }
                subcategoryId = Convert.ToInt32(inserted);
            }

            if (productId.HasValue)
            {
                using var clearOtherSubForProductCmd = new MySqlCommand("UPDATE subcategories SET product_id = NULL WHERE product_id = @productId;", conn, tx);
                clearOtherSubForProductCmd.Parameters.Add(new MySqlParameter("@productId", productId.Value));
                clearOtherSubForProductCmd.ExecuteNonQuery();

                using var assignProductCmd = new MySqlCommand("UPDATE subcategories SET product_id = @productId WHERE id = @subcategoryId;", conn, tx);
                assignProductCmd.Parameters.Add(new MySqlParameter("@productId", productId.Value));
                assignProductCmd.Parameters.Add(new MySqlParameter("@subcategoryId", subcategoryId));
                assignProductCmd.ExecuteNonQuery();

                using var updateProductCategoryCmd = new MySqlCommand("UPDATE products SET category_id = @categoryId WHERE id = @productId;", conn, tx);
                updateProductCategoryCmd.Parameters.Add(new MySqlParameter("@categoryId", categoryId));
                updateProductCategoryCmd.Parameters.Add(new MySqlParameter("@productId", productId.Value));
                updateProductCategoryCmd.ExecuteNonQuery();

                using var syncCategoryProductCmd = new MySqlCommand(@"
UPDATE categoryproduct
SET Category_id = @categoryId, SubCategory_id = @subcategoryId
WHERE ProductId = @productId;", conn, tx);
                syncCategoryProductCmd.Parameters.Add(new MySqlParameter("@categoryId", categoryId));
                syncCategoryProductCmd.Parameters.Add(new MySqlParameter("@subcategoryId", subcategoryId));
                syncCategoryProductCmd.Parameters.Add(new MySqlParameter("@productId", productId.Value));
                syncCategoryProductCmd.ExecuteNonQuery();
            }

            tx.Commit();
            return subcategoryId;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return 0;
        }
    }

    public bool Update(int id, string name, int categoryId, int? productId)
    {
        using var conn = (MySqlConnection)DatabaseConnection.getConnection();

        try
        {
            conn.Open();
            using var tx = conn.BeginTransaction();

            int? existingProductId = null;
            using (var getExistingCmd = new MySqlCommand("SELECT product_id FROM subcategories WHERE id=@id LIMIT 1;", conn, tx))
            {
                getExistingCmd.Parameters.Add(new MySqlParameter("@id", id));
                object? existing = getExistingCmd.ExecuteScalar();
                if (existing != null && existing != DBNull.Value)
                {
                    existingProductId = Convert.ToInt32(existing);
                }
            }

            using (var updateSubCmd = new MySqlCommand(@"
UPDATE subcategories
SET categoryName=@name, category_id=@categoryId, product_id=@productId
WHERE id=@id;", conn, tx))
            {
                updateSubCmd.Parameters.Add(new MySqlParameter("@id", id));
                updateSubCmd.Parameters.Add(new MySqlParameter("@name", name.Trim()));
                updateSubCmd.Parameters.Add(new MySqlParameter("@categoryId", categoryId));
                updateSubCmd.Parameters.Add(new MySqlParameter("@productId", (object?)productId ?? DBNull.Value));
                if (updateSubCmd.ExecuteNonQuery() <= 0)
                {
                    tx.Rollback();
                    return false;
                }
            }

            if (productId.HasValue)
            {
                using var clearOtherSubForProductCmd = new MySqlCommand("UPDATE subcategories SET product_id = NULL WHERE product_id = @productId AND id <> @id;", conn, tx);
                clearOtherSubForProductCmd.Parameters.Add(new MySqlParameter("@productId", productId.Value));
                clearOtherSubForProductCmd.Parameters.Add(new MySqlParameter("@id", id));
                clearOtherSubForProductCmd.ExecuteNonQuery();

                using var updateProductCategoryCmd = new MySqlCommand("UPDATE products SET category_id = @categoryId WHERE id = @productId;", conn, tx);
                updateProductCategoryCmd.Parameters.Add(new MySqlParameter("@categoryId", categoryId));
                updateProductCategoryCmd.Parameters.Add(new MySqlParameter("@productId", productId.Value));
                updateProductCategoryCmd.ExecuteNonQuery();

                using var syncCategoryProductCmd = new MySqlCommand(@"
UPDATE categoryproduct
SET Category_id = @categoryId, SubCategory_id = @subcategoryId
WHERE ProductId = @productId;", conn, tx);
                syncCategoryProductCmd.Parameters.Add(new MySqlParameter("@categoryId", categoryId));
                syncCategoryProductCmd.Parameters.Add(new MySqlParameter("@subcategoryId", id));
                syncCategoryProductCmd.Parameters.Add(new MySqlParameter("@productId", productId.Value));
                syncCategoryProductCmd.ExecuteNonQuery();
            }
            else
            {
                using var clearCategoryProductSubCmd = new MySqlCommand("UPDATE categoryproduct SET SubCategory_id = NULL WHERE SubCategory_id = @subcategoryId;", conn, tx);
                clearCategoryProductSubCmd.Parameters.Add(new MySqlParameter("@subcategoryId", id));
                clearCategoryProductSubCmd.ExecuteNonQuery();
            }

            if (existingProductId.HasValue && (!productId.HasValue || existingProductId.Value != productId.Value))
            {
                using var clearOldProductCpCmd = new MySqlCommand("UPDATE categoryproduct SET SubCategory_id = NULL WHERE ProductId = @productId;", conn, tx);
                clearOldProductCpCmd.Parameters.Add(new MySqlParameter("@productId", existingProductId.Value));
                clearOldProductCpCmd.ExecuteNonQuery();
            }

            tx.Commit();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public bool Delete(int id)
    {
        using var conn = (MySqlConnection)DatabaseConnection.getConnection();

        try
        {
            conn.Open();
            using var tx = conn.BeginTransaction();

            using (var checkCmd = new MySqlCommand("SELECT product_id FROM subcategories WHERE id=@id LIMIT 1;", conn, tx))
            {
                checkCmd.Parameters.Add(new MySqlParameter("@id", id));
                object? productId = checkCmd.ExecuteScalar();
                if (productId != null && productId != DBNull.Value)
                {
                    tx.Rollback();
                    return false;
                }
            }

            using (var clearCpCmd = new MySqlCommand("UPDATE categoryproduct SET SubCategory_id = NULL WHERE SubCategory_id = @id;", conn, tx))
            {
                clearCpCmd.Parameters.Add(new MySqlParameter("@id", id));
                clearCpCmd.ExecuteNonQuery();
            }

            using (var deleteCmd = new MySqlCommand("DELETE FROM subcategories WHERE id=@id;", conn, tx))
            {
                deleteCmd.Parameters.Add(new MySqlParameter("@id", id));
                bool ok = deleteCmd.ExecuteNonQuery() > 0;
                if (!ok)
                {
                    tx.Rollback();
                    return false;
                }
            }

            tx.Commit();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }
}

