

using MySql.Data.MySqlClient;

using Transflower.ECommerce.HR.Entities;
using Transflower.ECommerce.HR.Repositories.Interfaces;
namespace Transflower.ECommerce.HR.Repositories;
public class MembershipRepository : IMembershipRepository
{

    public MembershipRepository(){

    }
    public async Task<List<Member>> GetAll()
    {
        await Task.Delay(100);
        List<Member> members = new List<Member>();  

        string connectionString = "server=localhost;port=3306;user=root;password=password;database=assessmentdb";
        MySqlConnection connection = new MySqlConnection(connectionString);
        MySqlCommand command = new MySqlCommand("SELECT * FROM Employees",connection);
        
        try{
            connection.Open();
            MySqlDataReader reader=command.ExecuteReader();
            while(reader.Read()){

                int id=reader.GetInt32("id");
                string firstName=reader["firstname"].ToString();
                string lastName=reader["lastname"].ToString();
                string email=reader["email"].ToString();
                string contact=reader["contact"].ToString();
                Member theMember=new Member();
                theMember.Id=id;
                theMember.FirstName=firstName;    
                theMember.LastName=lastName;
                theMember.Email=email;
                theMember.Contact=contact;
                members.Add(theMember);
            }
            reader.Close();
        }
        catch(Exception ex){
                    Console.WriteLine(ex.Message);
        }

        finally{
                connection.Close();
        }

        return members;      
    }
    public async Task<Member> GetDetails(int membershipId)
        {
            await Task.Delay(100);
            Member  member = null;
            string connectionString = "server=localhost;port=3306;user=root;password=password;database=assessmentdb";
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query ="SELECT * FROM Employees WHERE id="+ membershipId;

            MySqlCommand command = new MySqlCommand(query,connection);
            
            try{
                connection.Open();
                MySqlDataReader reader=command.ExecuteReader();
                if(reader.Read()){

                    int id=reader.GetInt32("id");
                    string firstName=reader["firstname"].ToString();
                    string lastName=reader["lastname"].ToString();
                    string email=reader["email"].ToString();
                    string contact=reader["contact"].ToString();

                    member =new Member();
                    member.Id = id;
                    member.FirstName=firstName;    
                    member.LastName=lastName;
                    member.Email=email;
                    member.Contact=contact;
                
                }
                reader.Close();
            }
            catch(Exception ex){
                        Console.WriteLine(ex.Message);
            }

            finally{
                    connection.Close();
            }

            return member;   

        }

    public async Task<bool> InsertMemberDetails(Member member)
    {
        await Task.Delay(100);
        return true;
    }

    public async Task<bool> MemberDelete(int membershipId)
    {
        await Task.Delay(100);
        return true;
    }

    public async Task<bool> UpdateMemberDetails(Member member)
    {
        await Task.Delay(100);
        return true;
    }
}