

using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using Transflower.ECommerce.HR.Entities;
using Transflower.ECommerce.HR.Repositories.Interfaces;
namespace MembershipRoles.Repositories.MySql;
public class MembershipMySQLDapperRepository : IMembershipRepository
{
    private string connectionString = @"server=localhost;port=3306;user=root;password=password;database=assessmentdb";

    public MembershipMySQLDapperRepository()
    {

    }
    public async Task<List<Member>> GetAll()
    {
        await Task.Delay(100);
        List<Member> members = new List<Member>();
        using (IDbConnection con = new MySqlConnection(connectionString))
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