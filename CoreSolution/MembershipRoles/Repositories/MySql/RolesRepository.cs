using MySql.Data.MySqlClient;
using Transflower.ECommerce.Entities;
using Transflower.ECommerce.Repositories.Interfaces;
namespace MembershipRoles.Repositories.MySql;
public class RolesRepository : IRolesRepository
{

    public RolesRepository()
    {

    }
    public async Task<List<Role>> GetAll()
    {
        await Task.Delay(100);
        List<Role> roles = new List<Role>();

        string connectionString = "server=localhost;port=3306;user=root;password=password;database=assessmentdb";
        MySqlConnection connection = new MySqlConnection(connectionString);
        MySqlCommand command = new MySqlCommand("SELECT * FROM Roles", connection);

        try
        {
            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {

                int id = reader.GetInt32("id");
                string title = reader["title"].ToString();
                Role theRole = new Role();
                theRole.Id = id;
                theRole.Title = title;
                roles.Add(theRole);
            }
            reader.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        finally
        {
            connection.Close();
        }
        return roles;
    }
    public async Task<Role> GetRole(int roleId)
    {
        await Task.Delay(100);
        Role role = null;
        string connectionString = "server=localhost;port=3306;user=root;password=password;database=assessmentdb";
        MySqlConnection connection = new MySqlConnection(connectionString);
        string query = "SELECT * FROM Roles WHERE id=" + roleId;
        MySqlCommand command = new MySqlCommand(query, connection);

        try
        {
            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {

                int id = reader.GetInt32("id");
                string title = reader["title"].ToString();

                role = new Role();
                role.Id = id;
                role.Title = title;

            }
            reader.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            connection.Close();
        }
        return role;
    }

    public async Task<bool> Insert(Role role)
    {
        await Task.Delay(100);
        string connectionString = "server=localhost;port=3306;user=root;password=password;database=assessmentdb";
        MySqlConnection connection = new MySqlConnection(connectionString);
        string query = "INSERT INTO Roles(title) VALUES(" + role.Title + ");";

        MySqlCommand command = new MySqlCommand(query, connection);

        try
        {
            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {

                int id = reader.GetInt32("id");
                string title = reader["title"].ToString();
            }
            reader.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }

        finally
        {
            connection.Close();
        }

        return true;
    }

    public async Task<bool> Update(Role role)
    {
        await Task.Delay(100);
        string connectionString = "server=localhost;port=3306;user=root;password=password;database=assessmentdb";
        MySqlConnection connection = new MySqlConnection(connectionString);
        string query = "UPDATE ROLES SET VALUES=" + role.Title + " where id =" + role.Id;

        MySqlCommand command = new MySqlCommand(query, connection);

        try
        {
            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {

                int id = reader.GetInt32("id");
                string title = reader["title"].ToString();

            }
            reader.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }

        finally
        {
            connection.Close();
        }
        return true;
    }

    public async Task<bool> Delete(int roleId)
    {
        await Task.Delay(100);
        Role role = null;
        string connectionString = "server=localhost;port=3306;user=root;password=password;database=assessmentdb";
        MySqlConnection connection = new MySqlConnection(connectionString);
        string query = "DELETE FROM ROLES WHERE ID =" + roleId;

        MySqlCommand command = new MySqlCommand(query, connection);

        try
        {
            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            reader.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        finally
        {
            connection.Close();
        }

        return true;
    }

}