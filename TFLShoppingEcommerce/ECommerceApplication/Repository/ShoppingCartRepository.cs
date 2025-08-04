using System.Data;
using System.Data.Common;
using ECommerceApplication.Models;
using ECommerceApplication.Utils;
using MySql.Data.MySqlClient;

namespace ECommerceApplication.Repository.Interfaces
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        public bool addItem(Item item, int userid)
        {
            bool Status=false;

            IDbConnection conn = DatabaseConnection.getConnection();
            IDbCommand cmd = conn.CreateCommand();
       
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "AddToCart"; // Procedure name
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameters in the correct order
                cmd.Parameters.Add(new MySqlParameter("uid", userid));
                cmd.Parameters.Add(new MySqlParameter("pid", item.product.ProductId));
                cmd.Parameters.Add(new MySqlParameter("stock", item.Quantity));
                cmd.Parameters.Add(new MySqlParameter("img", item.product.ProductImage));

                cmd.ExecuteNonQuery();
                Status = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return Status;
        }

        public bool deleteItem(int id)
        {
            bool Status = false;
            IDbConnection conn = DatabaseConnection.getConnection();
            IDbCommand cmd = new MySqlCommand();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "RemoveFromCart"; // Procedure name
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new MySqlParameter("pid", id));
                cmd.ExecuteNonQuery();
               
                Status = true;
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

            return Status;
        }

        public List<Item> getAllItem(int uid)
        {
            List<Item> items = new List<Item>();
            // implement logic to get all items from database
            IDbConnection conn = DatabaseConnection.getConnection();
            IDbCommand cmd = new MySqlCommand();
            IDataReader reader = null;

            string query = @"select ci.cart_item_id as cart_item_id,
                                    p.name as productname, 
                                    ci.itemimage as itemimage,
                                    ci.quantity as stock ,
                                    p.price as price ,
                                    p.id as productid ,
                                    c.customer_id as customerid 
                                    from cart c 
                                    join cart_items ci on c.cart_id = ci.cart_id
                                    join categoryproduct p on p.id = ci.product_id
                                    where c.customer_id = @userid; ";

            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = query;
                cmd.Parameters.Add(new MySqlParameter("@userid", uid));
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int id = int.Parse(reader["cart_item_id"].ToString());
                    string name = reader["productname"].ToString();
                    string image = reader["itemimage"].ToString();
                    int quantity = int.Parse(reader["stock"].ToString());
                    double unitPrice = double.Parse(reader["price"].ToString());
                    int productId = int.Parse(reader["productid"].ToString());
                    int userid = int.Parse(reader["customerid"].ToString());
                    Product product = new Product
                    {
                        ProductId = productId,
                        ProductTitle = name,
                        ProductImage = image,
                        UnitPrice = unitPrice
                    };
                    items.Add(new Item(id, product, quantity));

                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return items;
        }

        public List<Product> getItemByCategoryId(int id)
        {
            throw new NotImplementedException();
        }

        public Item getItemById(int id, int userid)
        {
            Item item = null;
            IDbConnection conn = DatabaseConnection.getConnection();
            IDbCommand cmd = new MySqlCommand();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                //cmd.CommandText = "select * from cart where itemid=@id";
                string query = @"select ci.cart_item_id as cart_item_id,
                                    p.name as productname, 
                                    ci.itemimage as itemimage,
                                    ci.quantity as stock ,
                                    p.price as price ,
                                    p.id as productid ,
                                    c.customer_id as customerid 
                                    from cart c 
                                    join cart_items ci on c.cart_id = ci.cart_id
                                    join categoryproduct p on p.id = ci.product_id
                                    where ci.product_id = @id and c.customer_id=@uid";

                cmd.Parameters.Add(new MySqlParameter("@id", id));
                cmd.Parameters.Add(new MySqlParameter("@uid", userid));
                cmd.CommandText = query;
                IDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int id1 = int.Parse(reader["cart_item_id"].ToString());
                    string title = reader["productname"].ToString();
                    string img = reader["itemimage"].ToString();
                    int quantity = int.Parse(reader["stock"].ToString());
                    double price = double.Parse(reader["price"].ToString());
                    int productId = int.Parse(reader["productid"].ToString());

                    Product product = new Product
                    {
                        ProductId = productId,
                        ProductTitle = title,
                        ProductImage = img,
                        UnitPrice = price
                    };
                    item = new Item(id1, product, quantity);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return item;
        }

        public Product getItemByTitle(string title)
        {
            throw new NotImplementedException();
        }

        public bool updateItem(Item item)
        {
            bool status = false;
            IDbConnection conn = DatabaseConnection.getConnection();
            IDbCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE cart_items SET Quantity=@quantity where product_id=@id";
            cmd.Parameters.Add(new MySqlParameter("@quantity", item.Quantity));
            cmd.Parameters.Add(new MySqlParameter("@id", item.product.ProductId));
            try
            {
                conn.Open();

                cmd.ExecuteNonQuery();
                status = true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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
    }
}