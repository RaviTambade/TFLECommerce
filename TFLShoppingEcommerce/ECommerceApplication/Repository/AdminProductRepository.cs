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
    cp.SubCategory_id,
    c.name AS category_name,
    sc.categoryName AS subcategory_name
FROM products p
LEFT JOIN categoryproduct cp ON cp.ProductId = p.id
LEFT JOIN categories c ON c.id = p.category_id
LEFT JOIN subcategories sc ON sc.id = cp.SubCategory_id
WHERE 1 = 1";

        if (!string.IsNullOrWhiteSpace(search))
        {
            query += " AND (p.name LIKE @search OR p.description LIKE @search)";
            cmd.Parameters.Add(new MySqlParameter("@search", $"%{search.Trim()}%"));
        }

        if (categoryId.HasValue)
        {
            query += " AND p.category_id = @categoryId";
            cmd.Parameters.Add(new MySqlParameter("@categoryId", categoryId.Value));
        }

        if (subcategoryId.HasValue)
        {
            query += " AND cp.SubCategory_id = @subcategoryId";
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
SELECT
    p.id,
    p.name,
    p.description,
    p.price,
    p.stock,
    p.image,
    p.category_id,
    cp.SubCategory_id
FROM products p
LEFT JOIN categoryproduct cp ON cp.ProductId = p.id
WHERE p.id = @id
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
        using var conn = (MySqlConnection)DatabaseConnection.getConnection();

        try
        {
            conn.Open();
            using var tx = conn.BeginTransaction();

            int productId;
            int? effectiveSubcategoryId = model.SubcategoryId;
            int? effectiveCategoryId = model.CategoryId;

            using (var insertProductCmd = new MySqlCommand(@"
INSERT INTO products(name, description, price, stock, image, category_id)
VALUES(@name, @description, @price, @stock, @image, @categoryId);
SELECT LAST_INSERT_ID();", conn, tx))
            {
                insertProductCmd.Parameters.Add(new MySqlParameter("@name", model.ProductTitle.Trim()));
                insertProductCmd.Parameters.Add(new MySqlParameter("@description", (object?)model.Description ?? DBNull.Value));
                insertProductCmd.Parameters.Add(new MySqlParameter("@price", model.UnitPrice));
                insertProductCmd.Parameters.Add(new MySqlParameter("@stock", model.Quantity));
                insertProductCmd.Parameters.Add(new MySqlParameter("@image", (object?)imagePath ?? DBNull.Value));
                insertProductCmd.Parameters.Add(new MySqlParameter("@categoryId", (object?)effectiveCategoryId ?? DBNull.Value));
                object? insertedId = insertProductCmd.ExecuteScalar();
                if (insertedId == null)
                {
                    tx.Rollback();
                    return 0;
                }
                productId = Convert.ToInt32(insertedId);
            }

            if (effectiveSubcategoryId.HasValue)
            {
                using var getSubCatCategoryCmd = new MySqlCommand("SELECT category_id FROM subcategories WHERE id=@subCategoryId LIMIT 1;", conn, tx);
                getSubCatCategoryCmd.Parameters.Add(new MySqlParameter("@subCategoryId", effectiveSubcategoryId.Value));
                object? categoryFromSub = getSubCatCategoryCmd.ExecuteScalar();
                if (categoryFromSub == null || categoryFromSub == DBNull.Value)
                {
                    tx.Rollback();
                    return 0;
                }
                effectiveCategoryId = Convert.ToInt32(categoryFromSub);

                using var clearSubcategoryOwnerCmd = new MySqlCommand("UPDATE subcategories SET product_id = NULL WHERE id=@subCategoryId AND product_id <> @productId;", conn, tx);
                clearSubcategoryOwnerCmd.Parameters.Add(new MySqlParameter("@subCategoryId", effectiveSubcategoryId.Value));
                clearSubcategoryOwnerCmd.Parameters.Add(new MySqlParameter("@productId", productId));
                clearSubcategoryOwnerCmd.ExecuteNonQuery();

                using var clearOtherCpMappingCmd = new MySqlCommand("UPDATE categoryproduct SET SubCategory_id = NULL WHERE SubCategory_id=@subCategoryId AND ProductId <> @productId;", conn, tx);
                clearOtherCpMappingCmd.Parameters.Add(new MySqlParameter("@subCategoryId", effectiveSubcategoryId.Value));
                clearOtherCpMappingCmd.Parameters.Add(new MySqlParameter("@productId", productId));
                clearOtherCpMappingCmd.ExecuteNonQuery();

                using var assignSubcategoryCmd = new MySqlCommand("UPDATE subcategories SET product_id=@productId WHERE id=@subCategoryId;", conn, tx);
                assignSubcategoryCmd.Parameters.Add(new MySqlParameter("@productId", productId));
                assignSubcategoryCmd.Parameters.Add(new MySqlParameter("@subCategoryId", effectiveSubcategoryId.Value));
                assignSubcategoryCmd.ExecuteNonQuery();
            }

            using (var enforceCategoryCmd = new MySqlCommand("UPDATE products SET category_id=@categoryId WHERE id=@productId;", conn, tx))
            {
                enforceCategoryCmd.Parameters.Add(new MySqlParameter("@categoryId", (object?)effectiveCategoryId ?? DBNull.Value));
                enforceCategoryCmd.Parameters.Add(new MySqlParameter("@productId", productId));
                enforceCategoryCmd.ExecuteNonQuery();
            }

            using (var insertCpCmd = new MySqlCommand(@"
INSERT INTO categoryproduct (ProductId, Title, Description, UnitPrice, Quantity, image, Category_id, SubCategory_id)
VALUES (@productId, @title, @description, @unitPrice, @quantity, @image, @categoryId, @subCategoryId);", conn, tx))
            {
                insertCpCmd.Parameters.Add(new MySqlParameter("@productId", productId));
                insertCpCmd.Parameters.Add(new MySqlParameter("@title", model.ProductTitle.Trim()));
                insertCpCmd.Parameters.Add(new MySqlParameter("@description", (object?)model.Description ?? DBNull.Value));
                insertCpCmd.Parameters.Add(new MySqlParameter("@unitPrice", model.UnitPrice));
                insertCpCmd.Parameters.Add(new MySqlParameter("@quantity", model.Quantity));
                insertCpCmd.Parameters.Add(new MySqlParameter("@image", (object?)imagePath ?? DBNull.Value));
                insertCpCmd.Parameters.Add(new MySqlParameter("@categoryId", (object?)effectiveCategoryId ?? DBNull.Value));
                insertCpCmd.Parameters.Add(new MySqlParameter("@subCategoryId", (object?)effectiveSubcategoryId ?? DBNull.Value));
                int cpRows = insertCpCmd.ExecuteNonQuery();
                if (cpRows <= 0)
                {
                    tx.Rollback();
                    return 0;
                }
            }

            tx.Commit();
            return productId;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return 0;
        }
    }

    public bool Update(AdminProductEditViewModel model, string? imagePath)
    {
        using var conn = (MySqlConnection)DatabaseConnection.getConnection();

        try
        {
            conn.Open();
            using var tx = conn.BeginTransaction();

            int? effectiveCategoryId = model.CategoryId;
            int? newSubcategoryId = model.SubcategoryId;
            string? finalImagePath = imagePath ?? model.ExistingImagePath;

            using (var clearOldMappingsCmd = new MySqlCommand("UPDATE subcategories SET product_id = NULL WHERE product_id = @productId;", conn, tx))
            {
                clearOldMappingsCmd.Parameters.Add(new MySqlParameter("@productId", model.ProductId));
                clearOldMappingsCmd.ExecuteNonQuery();
            }

            if (newSubcategoryId.HasValue)
            {
                using var getSubCatCategoryCmd = new MySqlCommand("SELECT category_id FROM subcategories WHERE id=@subCategoryId LIMIT 1;", conn, tx);
                getSubCatCategoryCmd.Parameters.Add(new MySqlParameter("@subCategoryId", newSubcategoryId.Value));
                object? categoryFromSub = getSubCatCategoryCmd.ExecuteScalar();
                if (categoryFromSub == null || categoryFromSub == DBNull.Value)
                {
                    tx.Rollback();
                    return false;
                }
                effectiveCategoryId = Convert.ToInt32(categoryFromSub);

                using var clearExistingSubOwnerCmd = new MySqlCommand("UPDATE subcategories SET product_id=NULL WHERE id=@subCategoryId AND product_id <> @productId;", conn, tx);
                clearExistingSubOwnerCmd.Parameters.Add(new MySqlParameter("@subCategoryId", newSubcategoryId.Value));
                clearExistingSubOwnerCmd.Parameters.Add(new MySqlParameter("@productId", model.ProductId));
                clearExistingSubOwnerCmd.ExecuteNonQuery();

                using var clearCpOnSubCmd = new MySqlCommand("UPDATE categoryproduct SET SubCategory_id=NULL WHERE SubCategory_id=@subCategoryId AND ProductId <> @productId;", conn, tx);
                clearCpOnSubCmd.Parameters.Add(new MySqlParameter("@subCategoryId", newSubcategoryId.Value));
                clearCpOnSubCmd.Parameters.Add(new MySqlParameter("@productId", model.ProductId));
                clearCpOnSubCmd.ExecuteNonQuery();

                using var assignSubCmd = new MySqlCommand("UPDATE subcategories SET product_id=@productId WHERE id=@subCategoryId;", conn, tx);
                assignSubCmd.Parameters.Add(new MySqlParameter("@productId", model.ProductId));
                assignSubCmd.Parameters.Add(new MySqlParameter("@subCategoryId", newSubcategoryId.Value));
                assignSubCmd.ExecuteNonQuery();
            }

            using (var updateProductsCmd = new MySqlCommand(@"
UPDATE products
SET
    name=@name,
    description=@description,
    price=@price,
    stock=@stock,
    image=@image,
    category_id=@categoryId
WHERE id=@id;", conn, tx))
            {
                updateProductsCmd.Parameters.Add(new MySqlParameter("@id", model.ProductId));
                updateProductsCmd.Parameters.Add(new MySqlParameter("@name", model.ProductTitle.Trim()));
                updateProductsCmd.Parameters.Add(new MySqlParameter("@description", (object?)model.Description ?? DBNull.Value));
                updateProductsCmd.Parameters.Add(new MySqlParameter("@price", model.UnitPrice));
                updateProductsCmd.Parameters.Add(new MySqlParameter("@stock", model.Quantity));
                updateProductsCmd.Parameters.Add(new MySqlParameter("@image", (object?)finalImagePath ?? DBNull.Value));
                updateProductsCmd.Parameters.Add(new MySqlParameter("@categoryId", (object?)effectiveCategoryId ?? DBNull.Value));

                if (updateProductsCmd.ExecuteNonQuery() <= 0)
                {
                    tx.Rollback();
                    return false;
                }
            }

            using (var updateCpCmd = new MySqlCommand(@"
UPDATE categoryproduct
SET
    Title=@title,
    Description=@description,
    UnitPrice=@unitPrice,
    Quantity=@quantity,
    image=@image,
    Category_id=@categoryId,
    SubCategory_id=@subCategoryId
WHERE ProductId=@productId;", conn, tx))
            {
                updateCpCmd.Parameters.Add(new MySqlParameter("@productId", model.ProductId));
                updateCpCmd.Parameters.Add(new MySqlParameter("@title", model.ProductTitle.Trim()));
                updateCpCmd.Parameters.Add(new MySqlParameter("@description", (object?)model.Description ?? DBNull.Value));
                updateCpCmd.Parameters.Add(new MySqlParameter("@unitPrice", model.UnitPrice));
                updateCpCmd.Parameters.Add(new MySqlParameter("@quantity", model.Quantity));
                updateCpCmd.Parameters.Add(new MySqlParameter("@image", (object?)finalImagePath ?? DBNull.Value));
                updateCpCmd.Parameters.Add(new MySqlParameter("@categoryId", (object?)effectiveCategoryId ?? DBNull.Value));
                updateCpCmd.Parameters.Add(new MySqlParameter("@subCategoryId", (object?)newSubcategoryId ?? DBNull.Value));
                updateCpCmd.ExecuteNonQuery();
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

            using (var clearSubCmd = new MySqlCommand("UPDATE subcategories SET product_id = NULL WHERE product_id = @productId;", conn, tx))
            {
                clearSubCmd.Parameters.Add(new MySqlParameter("@productId", id));
                clearSubCmd.ExecuteNonQuery();
            }

            using (var deleteCpCmd = new MySqlCommand("DELETE FROM categoryproduct WHERE ProductId = @productId;", conn, tx))
            {
                deleteCpCmd.Parameters.Add(new MySqlParameter("@productId", id));
                deleteCpCmd.ExecuteNonQuery();
            }

            using (var deleteProductCmd = new MySqlCommand("DELETE FROM products WHERE id = @id;", conn, tx))
            {
                deleteProductCmd.Parameters.Add(new MySqlParameter("@id", id));
                bool ok = deleteProductCmd.ExecuteNonQuery() > 0;
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

