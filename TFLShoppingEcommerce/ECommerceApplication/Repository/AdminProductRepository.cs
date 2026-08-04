using System.Data;
using ECommerceApplication.Repository.Interfaces;
using ECommerceApplication.Utils;
using ECommerceApplication.ViewModels;
using MySql.Data.MySqlClient;

namespace ECommerceApplication.Repository;

public class AdminProductRepository : IAdminProductRepository
{
    public List<AdminProductListItem> GetAll()
    {
        return GetFiltered(search: null, categoryId: null, subcategoryId: null, minPrice: null, maxPrice: null, inStockOnly: false);
    }

    public List<AdminProductListItem> GetFiltered(string? search, int? categoryId, int? subcategoryId, decimal? minPrice, decimal? maxPrice, bool inStockOnly)
    {
        List<AdminProductListItem> items = new();
        using IDbConnection conn = DatabaseConnection.getConnection();
        using IDbCommand cmd = conn.CreateCommand();

        string query = @"
SELECT
    p.id,
    p.name,
    p.price,
    p.stock,
    p.image,
    p.category_id,
    p.SubCategory_id,
    c.name AS category_name,
    sc.categoryName AS subcategory_name
FROM categoryproduct p
LEFT JOIN categories c ON c.id = p.category_id
LEFT JOIN subcategories sc ON sc.id = p.SubCategory_id
WHERE 1 = 1";

        if (!string.IsNullOrWhiteSpace(search))
        {
            query += " AND p.name LIKE @search";
            cmd.Parameters.Add(new MySqlParameter("@search", $"%{search.Trim()}%"));
        }

        if (categoryId.HasValue)
        {
            query += " AND p.category_id = @categoryId";
            cmd.Parameters.Add(new MySqlParameter("@categoryId", categoryId.Value));
        }

        if (subcategoryId.HasValue)
        {
            query += " AND p.SubCategory_id = @subcategoryId";
            cmd.Parameters.Add(new MySqlParameter("@subcategoryId", subcategoryId.Value));
        }

        if (minPrice.HasValue)
        {
            query += " AND p.price >= @minPrice";
            cmd.Parameters.Add(new MySqlParameter("@minPrice", minPrice.Value));
        }

        if (maxPrice.HasValue)
        {
            query += " AND p.price <= @maxPrice";
            cmd.Parameters.Add(new MySqlParameter("@maxPrice", maxPrice.Value));
        }

        if (inStockOnly)
        {
            query += " AND p.stock > 0";
        }

        query += " ORDER BY p.id DESC;";
        cmd.CommandText = query;

        try
        {
            conn.Open();
            using IDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                items.Add(new AdminProductListItem
                {
                    ProductId = Convert.ToInt32(reader["id"]),
                    ProductTitle = reader["name"]?.ToString() ?? string.Empty,
                    UnitPrice = Convert.ToDecimal(reader["price"]),
                    Quantity = Convert.ToInt32(reader["stock"]),
                    Image = reader["image"] == DBNull.Value ? null : reader["image"]?.ToString(),
                    CategoryId = reader["category_id"] == DBNull.Value ? null : Convert.ToInt32(reader["category_id"]),
                    SubcategoryId = reader["SubCategory_id"] == DBNull.Value ? null : Convert.ToInt32(reader["SubCategory_id"]),
                    CategoryName = reader["category_name"] == DBNull.Value ? null : reader["category_name"]?.ToString(),
                    SubCategoryName = reader["subcategory_name"] == DBNull.Value ? null : reader["subcategory_name"]?.ToString()
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

    public AdminProductEditViewModel? GetById(int id)
    {
        using IDbConnection conn = DatabaseConnection.getConnection();
        using IDbCommand cmd = conn.CreateCommand();
        cmd.CommandText = @"
SELECT id, name, description, price, stock, image, category_id, SubCategory_id
FROM categoryproduct
WHERE id = @id
LIMIT 1;";
        cmd.Parameters.Add(new MySqlParameter("@id", id));

        try
        {
            conn.Open();
            using IDataReader reader = cmd.ExecuteReader();
            if (!reader.Read()) return null;

            return new AdminProductEditViewModel
            {
                ProductId = Convert.ToInt32(reader["id"]),
                ProductTitle = reader["name"]?.ToString() ?? string.Empty,
                Description = reader["description"] == DBNull.Value ? null : reader["description"]?.ToString(),
                UnitPrice = Convert.ToDecimal(reader["price"]),
                Quantity = Convert.ToInt32(reader["stock"]),
                CategoryId = reader["category_id"] == DBNull.Value ? null : Convert.ToInt32(reader["category_id"]),
                SubcategoryId = reader["SubCategory_id"] == DBNull.Value ? null : Convert.ToInt32(reader["SubCategory_id"]),
                ExistingImagePath = reader["image"] == DBNull.Value ? null : reader["image"]?.ToString()
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

    public int Create(AdminProductEditViewModel model, string? imagePath)
    {
        // Your CategoryProduct table uses `Id INT PRIMARY KEY` (not AUTO_INCREMENT),
        // so we must provide Id explicitly.
        using var conn = (MySqlConnection)DatabaseConnection.getConnection();

        try
        {
            conn.Open();
            using var tx = conn.BeginTransaction();

            int nextId;
            using (var getIdCmd = new MySqlCommand("SELECT COALESCE(MAX(id), 0) + 1 FROM categoryproduct FOR UPDATE;", conn, tx))
            {
                nextId = Convert.ToInt32(getIdCmd.ExecuteScalar());
            }

            using (var insertCmd = new MySqlCommand(@"
INSERT INTO categoryproduct (id, name, description, price, stock, image, category_id, SubCategory_id)
VALUES (@id, @name, @description, @price, @stock, @image, @categoryId, @subcategoryId);", conn, tx))
            {
                insertCmd.Parameters.Add(new MySqlParameter("@id", nextId));
                insertCmd.Parameters.Add(new MySqlParameter("@name", model.ProductTitle));
                insertCmd.Parameters.Add(new MySqlParameter("@description", (object?)model.Description ?? DBNull.Value));
                insertCmd.Parameters.Add(new MySqlParameter("@price", model.UnitPrice));
                insertCmd.Parameters.Add(new MySqlParameter("@stock", model.Quantity));
                insertCmd.Parameters.Add(new MySqlParameter("@image", (object?)imagePath ?? DBNull.Value));
                insertCmd.Parameters.Add(new MySqlParameter("@categoryId", (object?)model.CategoryId ?? DBNull.Value));
                insertCmd.Parameters.Add(new MySqlParameter("@subcategoryId", (object?)model.SubcategoryId ?? DBNull.Value));

                int rows = insertCmd.ExecuteNonQuery();
                if (rows <= 0)
                {
                    tx.Rollback();
                    return 0;
                }
            }

            tx.Commit();
            return nextId;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return 0;
        }
    }

    public bool Update(AdminProductEditViewModel model, string? imagePath)
    {
        using IDbConnection conn = DatabaseConnection.getConnection();
        using IDbCommand cmd = conn.CreateCommand();

        cmd.CommandText = @"
UPDATE categoryproduct
SET
    name = @name,
    description = @description,
    price = @price,
    stock = @stock,
    image = @image,
    category_id = @categoryId,
    SubCategory_id = @subcategoryId
WHERE id = @id;";

        cmd.Parameters.Add(new MySqlParameter("@id", model.ProductId));
        cmd.Parameters.Add(new MySqlParameter("@name", model.ProductTitle));
        cmd.Parameters.Add(new MySqlParameter("@description", (object?)model.Description ?? DBNull.Value));
        cmd.Parameters.Add(new MySqlParameter("@price", model.UnitPrice));
        cmd.Parameters.Add(new MySqlParameter("@stock", model.Quantity));
        cmd.Parameters.Add(new MySqlParameter("@image", (object?)imagePath ?? (object?)model.ExistingImagePath ?? DBNull.Value));
        cmd.Parameters.Add(new MySqlParameter("@categoryId", (object?)model.CategoryId ?? DBNull.Value));
        cmd.Parameters.Add(new MySqlParameter("@subcategoryId", (object?)model.SubcategoryId ?? DBNull.Value));

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
        cmd.CommandText = "DELETE FROM categoryproduct WHERE id = @id";
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

    public List<IdNameOption> GetCategories()
    {
        List<IdNameOption> items = new();
        using IDbConnection conn = DatabaseConnection.getConnection();
        using IDbCommand cmd = conn.CreateCommand();
        cmd.CommandText = "SELECT id, name FROM categories ORDER BY name;";

        try
        {
            conn.Open();
            using IDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                items.Add(new IdNameOption
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Name = reader["name"]?.ToString() ?? string.Empty
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

    public List<IdNameOption> GetSubcategoriesByCategory(int categoryId)
    {
        List<IdNameOption> items = new();
        using IDbConnection conn = DatabaseConnection.getConnection();
        using IDbCommand cmd = conn.CreateCommand();
        cmd.CommandText = @"SELECT id, categoryName FROM subcategories WHERE category_id = @categoryId ORDER BY categoryName;";
        cmd.Parameters.Add(new MySqlParameter("@categoryId", categoryId));

        try
        {
            conn.Open();
            using IDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                items.Add(new IdNameOption
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Name = reader["categoryName"]?.ToString() ?? string.Empty
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
}

