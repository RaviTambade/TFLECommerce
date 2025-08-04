
using ECommerceApplication.Repository.Interfaces;
using ECommerceApplication.Models;
using ECommerceApplication.Utils;
using System.Data.Common;
using System.Data;
using MySql.Data.MySqlClient;

namespace ECommerceApplication.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        // Initialize products list
        public List<Categories> categories = new List<Categories>();
        //create connection 
        IDbConnection connection;
        public CategoryRepository()
        {
            // create database connection;
            connection = DatabaseConnection.getConnection();
        }
        //getAll Products Data
        public List<Categories> getAllCategories()
        {
            IDbCommand cmd = new MySqlCommand();
            cmd.CommandText = "select * from categories";
            cmd.Connection = connection;
            IDataReader reader = null;
            try
            {
                connection.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = int.Parse(reader["id"].ToString());
                    string name = reader["name"].ToString();
                    Categories category = new Categories
                    {
                        CategoryId = id,
                        CategoryName = name
                    };

                    categories.Add(category);

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
            return categories;
        }
        public Categories getCategoryById(int categoriesid)
        {
            Categories category = null;
            IDbConnection conn = DatabaseConnection.getConnection();
            IDbCommand cmd = new MySqlCommand();
            string query = "select * from categories where id=@id";
            cmd.Parameters.Add(new MySqlParameter("@id", categoriesid));
            cmd.CommandText = query;
            cmd.Connection = conn;
            IDataReader reader = null;
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                reader.Read();
                int id = int.Parse(reader["id"].ToString());
                string name = reader["name"].ToString();
                category = new Categories
                {
                    CategoryId = id,
                    CategoryName = name
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

            return category;
        }
        public Categories getCategoryByName(string categoryname)
        {
            Categories category = null;
            IDbConnection conn = DatabaseConnection.getConnection();
            IDbCommand cmd = new MySqlCommand();
            string query = "select * from categories where name=@title";
            cmd.Parameters.Add(new MySqlParameter("@title", categoryname));
            cmd.CommandText = query;
            cmd.Connection = conn;
            IDataReader reader = null;
            try
            {
                conn.Open();
                reader = cmd.ExecuteReader();
                reader.Read();
                int id = int.Parse(reader["id"].ToString());
                string name = reader["name"].ToString();
                category = new Categories
                {
                    CategoryId = id,
                    CategoryName = name
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
            return category;
        }

        public bool addNewCategory(Categories categories)
        {
            bool status = false;
            IDbConnection conn = DatabaseConnection.getConnection();
            IDbCommand cmd = new MySqlCommand();
            try
            {
                conn.Open();
                string query = "insert into categories values(@id,@name)";
                cmd.Parameters.Add(new MySqlParameter("@id", categories.CategoryId));
                cmd.Parameters.Add(new MySqlParameter("@name", categories.CategoryName));

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

        public bool deleteCategory(int id)
        {
            bool status = false;
            IDbConnection conn = DatabaseConnection.getConnection();
            IDbCommand cmd = new MySqlCommand();
            string query = "delete from categories where id=@id";
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

        public bool updateCategory(Categories categories)
        {
            bool status = false;
            IDbConnection conn = DatabaseConnection.getConnection();
            IDbCommand cmd = new MySqlCommand();
            try
            {
                conn.Open();
                string query = "update categories set name=@name where id=@id";
                cmd.Parameters.Add(new MySqlParameter("@id", categories.CategoryId));
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

       

    }
}