
using ECommerceApplication.Repository.Interfaces;
using ECommerceApplication.Models;
using ECommerceApplication.Utils;
using System.Data.Common;
using System.Data;
using MySql.Data.MySqlClient;

namespace ECommerceApplication.Repository
{

    public class ProductRepository : IProductRepository
    {
        //
        IDbConnection connection;
        public ProductRepository()
        {
            connection = DatabaseConnection.getConnection();
        }
        //getAll Products Data
        public List<Product> getAllProduct()
        {
            List<Product> products = new List<Product>();
            IDbCommand cmd = new MySqlCommand();
            cmd.CommandText = "select * from products";
            cmd.Connection = connection;
            IDataReader reader = null;
            try
            {
                connection.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = int.Parse(reader["id"].ToString());
                    string title = reader["name"].ToString();
                    string description = reader["Description"].ToString();
                    double unitPrice = double.Parse(reader["Price"].ToString());
                    int quantity = int.Parse(reader["stock"].ToString());
                    
                    string image = reader["Image"].ToString();

                    Product product = new Product
                    {
                        ProductId = id,
                        ProductTitle = title,
                        Description = description,
                        Quantity = quantity,
                        UnitPrice = unitPrice,
                        ProductImage = image
                    };

                    products.Add(product);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return products;
        }
        public Product getProductById(int pid)
        {
            Product product = null;
            IDbConnection conn = DatabaseConnection.getConnection();
            IDbCommand cmd = new MySqlCommand();
            string query = "select * from CategoryProduct where id=@id";
            cmd.Parameters.Add(new MySqlParameter("@id", pid));
            cmd.CommandText = query;
            cmd.Connection = conn;
            IDataReader reader = null;


            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                reader.Read();
                int id = int.Parse(reader["id"].ToString());
                string title = reader["name"].ToString();
                string description = reader["Description"].ToString();
                int unitPrice = int.Parse(reader["price"].ToString());
                int quntity = int.Parse(reader["stock"].ToString());
                string image = reader["Image"].ToString();


                product = new Product
                {
                    ProductId = id,
                    ProductTitle = title,
                    Description = description,
                    UnitPrice = unitPrice,
                    Quantity = quntity,
                    ProductImage = image
                };
                conn.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return product;
        }

        public List<Product> getProductByCategoryId(int categoryid)
        {
            List<Product> products = new List<Product>();
            IDbConnection conn = DatabaseConnection.getConnection();
            IDbCommand cmd = new MySqlCommand();
            string query = "select * from products where category_id=@id";
            cmd.Parameters.Add(new MySqlParameter("@id", categoryid));
            cmd.CommandText = query;
            cmd.Connection = conn;
            IDataReader reader = null;
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = int.Parse(reader["id"].ToString());
                    string title = reader["name"].ToString();
                    string description = reader["Description"].ToString();
                    int quantity = int.Parse(reader["stock"].ToString());
                    double unitPrice = double.Parse(reader["price"].ToString());
                    string image = reader["Image"].ToString();

                    Product product = new Product
                    {
                        ProductId = id,
                        ProductTitle = title,
                        Description = description,
                        Quantity = quantity,
                        UnitPrice = unitPrice,
                        ProductImage = image
                    };

                    products.Add(product);

                }
                conn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return products;

        }
        public Product getProductByTitle(string ptitle)
        {
            Product product = null;
            IDbConnection conn = DatabaseConnection.getConnection();
            IDbCommand cmd = new MySqlCommand();
            string query = "select * from products where name=@title";
            cmd.Parameters.Add(new MySqlParameter("@title", ptitle));
            cmd.CommandText = query;
            cmd.Connection = conn;
            IDataReader reader = null;
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                reader.Read();
                int id = int.Parse(reader["id"].ToString());
                string title = reader["name"].ToString();
                string description = reader["Description"].ToString();
                int unitPrice = int.Parse(reader["price"].ToString());
                int quntity = int.Parse(reader["stock"].ToString());
                string image = reader["Image"].ToString();


                product = new Product
                {
                    ProductId = id,
                    ProductTitle = title,
                    Description = description,
                    UnitPrice = unitPrice,
                    Quantity = quntity,
                    ProductImage = image
                };
                conn.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return product;
        }

        public bool addProduct(Product product)
        {
            bool status = false;
            IDbConnection conn = DatabaseConnection.getConnection();
            IDbCommand cmd = new MySqlCommand();
            try
            {
                conn.Open();
                string query = "insert into products values(@productid,@title,@description,@unitprice,@quantity,@image)";
                cmd.Parameters.Add(new MySqlParameter("@productid", product.ProductId));

                cmd.Parameters.Add(new MySqlParameter("@title", product.ProductTitle));
                cmd.Parameters.Add(new MySqlParameter("@description", product.Description));
                cmd.Parameters.Add(new MySqlParameter("@unitprice", product.UnitPrice));
                cmd.Parameters.Add(new MySqlParameter("@quantity", product.Quantity));
                cmd.Parameters.Add(new MySqlParameter("@image", product.ProductImage));
                cmd.CommandText = query;
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                status = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return status;
        }

        public bool deleteProduct(int id)
        {
            bool status = false;
            IDbConnection conn = DatabaseConnection.getConnection();
            IDbCommand cmd = new MySqlCommand();
            string query = "delete from products where id=@id";
            cmd.Parameters.Add(new MySqlParameter("@id", id));
            cmd.CommandText = query;
            cmd.Connection = conn;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                status = true;
            }
            catch (MySqlException exp)
            {
                string msg = exp.Message;
                Console.WriteLine(msg);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return status;
        }



        public bool updateProduct(Product product)
        {
            bool status = false;
            IDbConnection conn = DatabaseConnection.getConnection();
            IDbCommand cmd = new MySqlCommand();
            try
            {
                conn.Open();
                string query = "update product set name=@title, Description=@description, price=@unitprice, stock=@quantity, Image=@image where id=@id";
                cmd.Parameters.Add(new MySqlParameter("@productid", product.ProductId));
                cmd.Parameters.Add(new MySqlParameter("@title", product.ProductTitle));
                cmd.Parameters.Add(new MySqlParameter("@descriptpion", product.Description));
                cmd.Parameters.Add(new MySqlParameter("@unitprice", product.UnitPrice));
                cmd.Parameters.Add(new MySqlParameter("@quantity", product.Quantity));
                cmd.Parameters.Add(new MySqlParameter("@image", product.ProductImage));
                cmd.CommandText = query;
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                status = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return status;
        }

        public List<Product> getCategoriesProduct(int pid)
        {
            List<Product> products = new List<Product>();
            IDbCommand cmd = new MySqlCommand();
            cmd.CommandText = " SELECT cp.*FROM CategoryProduct cp JOIN subcategories sc ON cp.SubCategory_id = sc.id WHERE sc.Product_Id = @productid";
            cmd.Parameters.Add(new MySqlParameter("@productid", pid));
            cmd.Connection = connection;
            IDataReader reader = null;
            try
            {
                connection.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = int.Parse(reader["id"].ToString());
                    string title = reader["name"].ToString();
                    string description = reader["Description"].ToString();
                    int quantity = int.Parse(reader["stock"].ToString());
                    double unitPrice = double.Parse(reader["price"].ToString());
                    string image = reader["Image"].ToString();

                    Product product = new Product
                    {
                        ProductId = id,
                        ProductTitle = title,
                        Description = description,
                        Quantity = quantity,
                        UnitPrice = unitPrice,
                        ProductImage = image
                    };

                    products.Add(product);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return products;
        }

        public List<Product> getRecommendedProducts(int productId, int count)
        {
            List<Product> products = new List<Product>();
            IDbConnection conn = DatabaseConnection.getConnection();
            IDbCommand cmd = new MySqlCommand();
            IDataReader reader = null;

            string query = @"
SELECT p2.id, p2.name, p2.description, p2.stock, p2.price, p2.image
FROM categoryproduct p1
JOIN categoryproduct p2 ON p1.SubCategory_id = p2.SubCategory_id
WHERE p1.id = @productId
  AND p2.id <> @productId
ORDER BY ABS(p2.price - p1.price), p2.id DESC
LIMIT @count;";

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = query;
                cmd.Parameters.Add(new MySqlParameter("@productId", productId));
                cmd.Parameters.Add(new MySqlParameter("@count", count));
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    products.Add(new Product
                    {
                        ProductId = int.Parse(reader["id"].ToString()),
                        ProductTitle = reader["name"].ToString(),
                        Description = reader["description"].ToString(),
                        Quantity = int.Parse(reader["stock"].ToString()),
                        UnitPrice = double.Parse(reader["price"].ToString()),
                        ProductImage = reader["image"].ToString()
                    });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return products;
        }

        public List<ProductSearchResult> GetFilteredProducts(string? search, int? categoryId, int? subcategoryId, decimal? minPrice, decimal? maxPrice, int? rating)
        {
            List<ProductSearchResult> products = new List<ProductSearchResult>();
            IDbConnection conn = DatabaseConnection.getConnection();
            IDbCommand cmd = new MySqlCommand();

            string query = @"
SELECT p.id, p.name, p.description, p.price, p.image, p.category_id, p.SubCategory_id,
       COALESCE(AVG(r.rating), 0) AS avg_rating
FROM categoryproduct p
LEFT JOIN reviews r ON r.product_id = p.id
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

            query += " GROUP BY p.id, p.name, p.description, p.price, p.image, p.category_id, p.SubCategory_id";

            if (rating.HasValue)
            {
                query += " HAVING COALESCE(AVG(r.rating), 0) >= @rating";
                cmd.Parameters.Add(new MySqlParameter("@rating", rating.Value));
            }

            query += " ORDER BY p.id DESC";

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = query;

                using IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    products.Add(new ProductSearchResult
                    {
                        ProductId = Convert.ToInt32(reader["id"]),
                        ProductTitle = reader["name"]?.ToString() ?? string.Empty,
                        Description = reader["description"]?.ToString() ?? string.Empty,
                        UnitPrice = Convert.ToDecimal(reader["price"]),
                        ProductImage = reader["image"]?.ToString() ?? string.Empty,
                        CategoryId = Convert.ToInt32(reader["category_id"]),
                        SubcategoryId = reader["SubCategory_id"] == DBNull.Value ? null : Convert.ToInt32(reader["SubCategory_id"]),
                        AvgRating = Math.Round(Convert.ToDecimal(reader["avg_rating"]), 1)
                    });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return products;
        }

        public List<string> GetSuggestions(string term, int take = 5)
        {
            List<string> suggestions = new List<string>();
            IDbConnection conn = DatabaseConnection.getConnection();
            IDbCommand cmd = new MySqlCommand();

            cmd.CommandText = @"SELECT name FROM categoryproduct
                                WHERE name LIKE @term
                                ORDER BY name
                                LIMIT @take";
            cmd.Parameters.Add(new MySqlParameter("@term", $"%{term}%"));
            cmd.Parameters.Add(new MySqlParameter("@take", take));
            cmd.Connection = conn;

            try
            {
                conn.Open();
                using IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    suggestions.Add(reader["name"]?.ToString() ?? string.Empty);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

            return suggestions;
        }
    }
}