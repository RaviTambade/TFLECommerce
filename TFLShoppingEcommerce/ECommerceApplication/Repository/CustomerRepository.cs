using System.Data;
using ECommerceApplication.Models;
using ECommerceApplication.Repository.Interfaces;
using ECommerceApplication.Utils;
using MySql.Data.MySqlClient;

namespace ECommerceApplication.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        public bool addCustomer(Customer customer)
        {
            bool status = false;
            IDbConnection conn = DatabaseConnection.getConnection();
            IDbCommand cmd = new MySqlCommand();
            try
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.CommandText = "INSERT INTO Users(id,username,password,email,address) VALUES (@id, @name,@password,@email,@address)";
                cmd.Parameters.Add(new MySqlParameter("@id", customer.CustomerId));
                cmd.Parameters.Add(new MySqlParameter("@name", customer.UserName));
                cmd.Parameters.Add(new MySqlParameter("@password", customer.Password));
                cmd.Parameters.Add(new MySqlParameter("@email", customer.Email));
                cmd.Parameters.Add(new MySqlParameter("@address", customer.Address));
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

        public bool deleteCustomer(int id)
        {
            throw new NotImplementedException();
        }

        public List<Customer> getAllCustomers()
        {
            List<Customer> customers=new List<Customer>();
            IDbConnection conn = DatabaseConnection.getConnection();
            IDbCommand cmd = new MySqlCommand();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM Users";
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Customer customer = new Customer();
                    customer.CustomerId = int.Parse(reader["id"].ToString());
                    customer.UserName = reader["username"].ToString();
                    customer.Password = reader["password"].ToString();
                    customer.Email = reader["email"].ToString();
                    customer.Address = reader["Address"].ToString();
                    customers.Add(customer);
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
            return customers;
        }

        public Customer getCustomerByEmail(string email)
        {
            Customer customer=new Customer();
            IDbConnection conn = DatabaseConnection.getConnection();
            IDbCommand cmd = new MySqlCommand();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "select * from Users where Email=@email";
                cmd.Parameters.Add(new MySqlParameter("@email", email));
                IDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                   
                    customer.CustomerId = int.Parse(reader["id"].ToString());
                    customer.UserName = reader["username"].ToString();
                    customer.Password = reader["password"].ToString();
                    customer.Email = reader["email"].ToString();
                    customer.Address = reader["address"].ToString();
                   
                    reader.Close();
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
            return customer;
        }

        public Customer getCustomerById(int id)
        {

            Customer customer = new Customer();
            IDbConnection conn = DatabaseConnection.getConnection();
            IDbCommand cmd = new MySqlCommand();
            IDataReader reader;
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "select * from Users where id=@id";
                cmd.Parameters.Add(new MySqlParameter("@id", id));
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    customer.CustomerId = int.Parse(reader["id"].ToString());
                    customer.UserName = reader["username"].ToString();
                    customer.Password = reader["Password"].ToString();
                    customer.Email = reader["Email"].ToString();
                    customer.Address = reader["address"].ToString();
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
                return customer;
            }

        public Customer getCustomerByName(string uname)
        {
            Customer customer = new Customer();
            IDbConnection conn = DatabaseConnection.getConnection();
            IDbCommand cmd = new MySqlCommand();
            IDataReader reader;
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "select * from Users where username=@name";
                cmd.Parameters.Add(new MySqlParameter("@name", uname));
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    customer.CustomerId = int.Parse(reader["id"].ToString());
                    customer.UserName = reader["username"].ToString();
                    customer.Password = reader["Password"].ToString();
                    customer.Email = reader["Email"].ToString();
                    customer.Address = reader["Address"].ToString();
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
            return customer;
        }

        public bool updateCustomer(Customer customer)
        {
            bool status = false;
            IDbConnection conn = DatabaseConnection.getConnection();
            IDbCommand cmd = new MySqlCommand();
            try
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "update Users set username=@name, password=@password, Email=@email, Address=@address where id=@id";
                cmd.Parameters.Add(new MySqlParameter("@id", customer.CustomerId));
                cmd.Parameters.Add(new MySqlParameter("@name", customer.UserName));
                cmd.Parameters.Add(new MySqlParameter("@password", customer.Password));
                cmd.Parameters.Add(new MySqlParameter("@email", customer.Email));
                cmd.Parameters.Add(new MySqlParameter("@address", customer.Address));
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
