

using System.Data;
using Dapper;
using System.Data.SqlClient;

using Transflower.ECommerce.HR.Entities;
using Transflower.ECommerce.HR.Repositories.Interfaces;
namespace MembershipRoles.Repositories.MsSql.Dapper;
public class MembershipMSSQLDapperRepository : IMembershipRepository
{
    private string connectionString = @"Data Source=DESKTOP-RQSMQ1M\SQLEXPRESS;Initial Catalog=assessmentdb;Integrated Security=True;Connect Timeout=30;Encrypt=False";

    public MembershipMSSQLDapperRepository()
    {

    }
    public async Task<List<Member>> GetAll()
    {
        await Task.Delay(100);
        List<Member> members = new List<Member>();
        using (IDbConnection con = new SqlConnection(connectionString))
        {
            var employees = con.Query<Member>("SELECT * FROM employees");
            members = employees as List<Member>;
        }
        return members;
    }
    public async Task<Member> GetDetails(int membershipId)
    {
        await Task.Delay(100);
        Member member = null;
        using (IDbConnection con = new SqlConnection(connectionString))
        {
            member = con.QueryFirstOrDefault<Member>("SELECT * FROM employees where id=@membershipId",new { membershipId });
        }
        return member;

    }

    public async Task<bool> Insert(Member member)
    {
        await Task.Delay(100);
        bool status=false;
        using (IDbConnection con = new SqlConnection(connectionString))
        {
        //     var empToUpdate = new Member
        // {
        // Id = 179,
        // FirstName = "Sagar",
        // LastName = "Patil",
        // Contact = "9884578569",
        // Email = "sagar.patil@gmail.com"
        // };
        con.Execute("INSERT INTO employees (firstname, lastname, email, contact) VALUES ( @FirstName, @LastName, @Email, @Contact)", member);
        }
        return true;
    }

    public async Task<bool> Delete(int membershipId)
    {
        await Task.Delay(100);
        bool status=false;
        using (IDbConnection con = new SqlConnection(connectionString))
        {
        con.Execute("DELETE  FROM employees WHERE Id=@membershipId", new { membershipId });
        }
        return true;
    }

    public async Task<bool> Update(Member member)
    {
        await Task.Delay(100);
        bool status=false;
        using (IDbConnection con = new SqlConnection(connectionString))
        {
        var empToUpdate = new Member
        {
        Id = 2,
        FirstName = "Sagar",
        LastName = "Patil",
        Contact = "9884578569",
        Email = "sagar.patil@gmail.com"
        };
        con.Execute("UPDATE  employees SET firstname= @FirstName, lastname=@LastName WHERE Id=@Id", empToUpdate);
        }
        return true;
    }
}