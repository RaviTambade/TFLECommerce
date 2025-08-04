
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
    }
}