

using System.Data;
using System.Data.SqlClient;
using Transflower.ECommerce.HR.Entities;
using Transflower.ECommerce.HR.Repositories.Interfaces;
namespace MembershipRoles.Repositories.MsSql.ADO.Connected;
public class MembershipMSSQLRepository : IMembershipRepository
{
    private string connectionString = @"Data Source=DESKTOP-H1K53PL\SQLEXPRESS;Initial Catalog=AssessmentDB;Integrated Security=True;Connect Timeout=30;Encrypt=False";

    public MembershipMSSQLRepository()
    {

    }
    public async Task<List<Member>> GetAll()
    {
        await Task.Delay(100);
        List<Member> members = new List<Member>();

        IDbConnection connection = new SqlConnection(connectionString);
        IDbCommand command = new SqlCommand("SELECT * FROM employees", connection as SqlConnection);

        try
        {
            connection.Open();

            IDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {

                int id = int.Parse(reader["id"].ToString());
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
        IDbConnection connection = new SqlConnection(connectionString);
        string query = "SELECT * FROM Employees WHERE id=" + membershipId;

        IDbCommand command = new SqlCommand(query, connection as SqlConnection);

        try
        {
            connection.Open();
            IDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {

                int id = int.Parse(reader["id"].ToString());
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
        return true;
    }

    public async Task<bool> Delete(int membershipId)
    {
        await Task.Delay(100);
        return true;
    }

    public async Task<bool> Update(Member member)
    {
        await Task.Delay(100);
        return true;
    }


}