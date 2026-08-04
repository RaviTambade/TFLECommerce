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
    s.categoryName,
    s.category_id,
    c.name AS category_name,
    COALESCE(COUNT(p.id), 0) AS products_count
FROM subcategories s
JOIN categories c ON c.id = s.category_id
LEFT JOIN categoryproduct p ON p.SubCategory_id = s.id
GROUP BY s.id, s.categoryName, s.category_id, c.name
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
SELECT s.id, s.categoryName, s.category_id
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
        cmd.CommandText = "SELECT id, categoryName, category_id FROM subcategories WHERE id = @id LIMIT 1;";
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

    public int Create(string name, int categoryId)
    {
        using IDbConnection conn = DatabaseConnection.getConnection();
        using IDbCommand cmd = conn.CreateCommand();
        cmd.CommandText = @"
INSERT INTO subcategories(categoryName, category_id)
VALUES(@name, @categoryId);
SELECT LAST_INSERT_ID();";
        cmd.Parameters.Add(new MySqlParameter("@name", name.Trim()));
        cmd.Parameters.Add(new MySqlParameter("@categoryId", categoryId));

        try
        {
            conn.Open();
            object? id = cmd.ExecuteScalar();
            return id == null ? 0 : Convert.ToInt32(id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return 0;
        }
        finally
        {
            if (conn.State == ConnectionState.Open) conn.Close();
        }
    }

    public bool Update(int id, string name, int categoryId)
    {
        using IDbConnection conn = DatabaseConnection.getConnection();
        using IDbCommand cmd = conn.CreateCommand();
        cmd.CommandText = @"
UPDATE subcategories
SET categoryName=@name, category_id=@categoryId
WHERE id=@id;";
        cmd.Parameters.Add(new MySqlParameter("@id", id));
        cmd.Parameters.Add(new MySqlParameter("@name", name.Trim()));
        cmd.Parameters.Add(new MySqlParameter("@categoryId", categoryId));

        try
        {
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
        finally
        {
            if (conn.State == ConnectionState.Open) conn.Close();
        }
    }

    public bool Delete(int id)
    {
        using IDbConnection conn = DatabaseConnection.getConnection();
        using IDbCommand cmd = conn.CreateCommand();
        cmd.CommandText = "DELETE FROM subcategories WHERE id=@id";
        cmd.Parameters.Add(new MySqlParameter("@id", id));

        try
        {
            conn.Open();
            return cmd.ExecuteNonQuery() > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
        finally
        {
            if (conn.State == ConnectionState.Open) conn.Close();
        }
    }
}

