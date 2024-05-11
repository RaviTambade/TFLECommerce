using MySql.Data.MySqlClient;
using Transflower.ECommerce.HR.Entities;
using Transflower.ECommerce.HR.Repositories.Interfaces;
namespace MembershipRoles.Repositories.MySql;
public class MembershipMySqlRepository : IMembershipRepository
{

    public MembershipMySqlRepository()
    {

    }
    public async Task<List<Member>> GetAll()
    {
        await Task.Delay(100);
        List<Member> members = new List<Member>();

        string connectionString = "server=localhost;port=3306;user=root;password=password;database=assessmentdb";
        MySqlConnection connection = new MySqlConnection(connectionString);
        MySqlCommand command = new MySqlCommand("SELECT * FROM Employees", connection);

        try
        {
            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {

                int id = reader.GetInt32("id");
                string firstName = reader["firstname"].ToString();
                string lastName = reader["lastname"].ToString();
                string email = reader["email"].ToString();
                string contact = reader["contact"].ToString();
                Member theMember = new Member();
                theMember.Id = id;
                theMember.FirstName = firstName;
                theMember.LastName = lastName;
                theMember.Email = email;
                theMember.Contact = contact;
                members.Add(theMember);
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

        return members;
    }
    public async Task<Member> GetDetails(int membershipId)
    {
        await Task.Delay(100);
        Member member = null;
        string connectionString = "server=localhost;port=3306;user=root;password=password;database=assessmentdb";
        MySqlConnection connection = new MySqlConnection(connectionString);
        string query = "SELECT * FROM Employees WHERE id=" + membershipId;

        MySqlCommand command = new MySqlCommand(query, connection);

        try
        {
            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {

                int id = reader.GetInt32("id");
                string firstName = reader["firstname"].ToString();
                string lastName = reader["lastname"].ToString();
                string email = reader["email"].ToString();
                string contact = reader["contact"].ToString();

                member = new Member();
                member.Id = id;
                member.FirstName = firstName;
                member.LastName = lastName;
                member.Email = email;
                member.Contact = contact;

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

        return member;

    }

    public async Task<bool> Insert(Member member)
    {
        await Task.Delay(100);
        string connectionString = "server=localhost;port=3306;user=root;password=password;database=assessmentdb";
        MySqlConnection connection = new MySqlConnection(connectionString);
        string query = "insert into employees(firstname, lastname, email, contact)values(" + member.FirstName + "," + member.LastName + "," + member.Email + "," + member.Contact + ");";

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
            return false;
        }

        finally
        {
            connection.Close();
        }

        return true;
    }

    public async Task<bool> Update(Member member)
    {
        await Task.Delay(100);
        string connectionString = "server=localhost;port=3306;user=root;password=password;database=assessmentdb";
        MySqlConnection connection = new MySqlConnection(connectionString);
        string query = "UPDATE ROLES SET VALUES=(" + member.FirstName + "," + member.LastName + "," + member.Email + "," + member.Contact + ") where id =" + member.Id;

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
            return false;
        }

        finally
        {
            connection.Close();
        }
        return true;
    }

    public async Task<bool> Delete(int membershipId)
    {
        await Task.Delay(100);
        Member member = null;
        string connectionString = "server=localhost;port=3306;user=root;password=password;database=assessmentdb";
        MySqlConnection connection = new MySqlConnection(connectionString);
        string query = "DELETE FROM Employees WHERE ID =" + membershipId;

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