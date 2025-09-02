using System.Data;
using ECommerceApplication.Models;
using ECommerceApplication.Repository.Interfaces;
using ECommerceApplication.Utils;
using MySql.Data.MySqlClient;

namespace ECommerceApplication.Repository
{
    public class CustomerAddressRepository : ICustomerAddressRepository
    {
        public bool addCustomerAddress(ShippingAddress customerAddress, int userId)
        {
            bool status = false;
            IDbConnection connection = DatabaseConnection.getConnection();
            IDbCommand cmd = new MySqlCommand();
            string query = "insert into shipping_addresses (address,city,state,zip_zode,country,userid) values(@address,@city,@state,@zip,@country,@userid)";
            try
            {
                connection.Open();
                cmd.Connection = connection;
                cmd.CommandText = query;
                cmd.Parameters.Add(new MySqlParameter("@address", customerAddress.Address));
                cmd.Parameters.Add(new MySqlParameter("@city", customerAddress.City));
                cmd.Parameters.Add(new MySqlParameter("@state", customerAddress.State));
                cmd.Parameters.Add(new MySqlParameter("@zip", customerAddress.ZipCode));
                cmd.Parameters.Add(new MySqlParameter("@userid", userId));
                cmd.ExecuteNonQuery();
                status = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return status;
        }

        public bool deleteCustomerAddress(int addressid)
        {
            bool status = false;
            IDbConnection connection = DatabaseConnection.getConnection();
            IDbCommand cmd = new MySqlCommand();
            string query = "delete from shipping_addresses where shipping_address_id=@addressid ";
            try
            {
                connection.Open();
                cmd.Connection = connection;
                cmd.CommandText = query;
                cmd.Parameters.Add(new MySqlParameter("@addressid", addressid));
                cmd.ExecuteNonQuery();
                status = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return status;
        }

        public List<ShippingAddress> getAllCustomerAddresses(int userid)
        {
            List<ShippingAddress> customerAddress = new List<ShippingAddress>();
            IDbConnection connection = DatabaseConnection.getConnection();
            IDbCommand cmd = new MySqlCommand();
            string query = "select * from shipping_addresses where userid=@userid";
            try
            {
                connection.Open();
                cmd.Connection = connection;
                cmd.CommandText = query;
                cmd.Parameters.Add(new MySqlParameter("@userid", userid));

                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ShippingAddress add = new ShippingAddress();
                    add.Address = reader["address"].ToString();
                    add.City = reader["city"].ToString();
                    add.State = reader["state"].ToString();
                    add.ZipCode = int.Parse(reader["zip_code"].ToString());
                    add.Country = reader["country"].ToString();
                    customerAddress.Add(add);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return customerAddress;
        }

        public ShippingAddress getCustomerAddressById(int addressId)
        {
            ShippingAddress customerAddress = new ShippingAddress();
            IDbConnection connection = DatabaseConnection.getConnection();
            IDbCommand cmd = new MySqlCommand();
            string query = "select * from shipping_addresses where shipping_address_id=@addressId";
            try
            {
                connection.Open();
                cmd.Connection = connection;
                cmd.CommandText = query;
                cmd.Parameters.Add(new MySqlParameter("@shipping_address_id", addressId));

                IDataReader reader = cmd.ExecuteReader();
                reader.Read();

                ShippingAddress add = new ShippingAddress();
                add.Address = reader["address"].ToString();
                add.City = reader["city"].ToString();
                add.State = reader["state"].ToString();
                add.ZipCode = int.Parse(reader["zip_code"].ToString());
                add.Country = reader["country"].ToString();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return customerAddress;
        }

        public bool updateCustomerAddress(ShippingAddress customerAddress)
        {
            bool status = false;
            IDbConnection connection = DatabaseConnection.getConnection();
            IDbCommand cmd = new MySqlCommand();
            string query = "update shipping_address set address=@address city=@city, state=@state,zipcode=@zip country=@country where shipping_address_id=@addressid ";
            try
            {
                connection.Open();
                cmd.Connection = connection;
                cmd.CommandText = query;
                cmd.Parameters.Add(new MySqlParameter("@address", customerAddress.Address));
                cmd.Parameters.Add(new MySqlParameter("@city", customerAddress.City));
                cmd.Parameters.Add(new MySqlParameter("@state", customerAddress.State));
                cmd.Parameters.Add(new MySqlParameter("@zip", customerAddress.ZipCode));
                cmd.Parameters.Add(new MySqlParameter("@addressid", customerAddress.AddressId));

                cmd.ExecuteNonQuery();
                status = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return status;
        }
    }
}