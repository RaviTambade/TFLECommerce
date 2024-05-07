

using MySql.Data.MySqlClient;

using Transflower.ECommerce.Entities;
using Transflower.ECommerce.Repositories.Interfaces;
namespace Transflower.ECommerce.Repositories;
public class RolesRepository : IRolesRepository
{

    public RolesRepository(){

    }
    public async Task<List<Roles>> GetAllRoles()
    {
        await Task.Delay(100);
        List<Roles> roles = new List<Roles>();  

        string connectionString = "server=localhost;port=3306;user=root;password=password;database=assessmentdb";
        MySqlConnection connection = new MySqlConnection(connectionString);
        MySqlCommand command = new MySqlCommand("SELECT * FROM Roles",connection);
        
        try{
            connection.Open();
            MySqlDataReader reader=command.ExecuteReader();
            while(reader.Read()){

                int id=reader.GetInt32("id");
                string title=reader["title"].ToString();
                Roles theRole=new Roles();
                theRole.Id=id;
                theRole.Title=title;
                roles.Add(theRole);
            }
            reader.Close();
        }
        catch(Exception ex){
                    Console.WriteLine(ex.Message);
        }

        finally{
                connection.Close();
        }

        return roles;      
    }
    public async Task<Roles> GetRoleDetails(int roleId)
        {
            await Task.Delay(100);
            Roles  roles = null;
            string connectionString = "server=localhost;port=3306;user=root;password=password;database=assessmentdb";
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query ="SELECT * FROM Roles WHERE id="+ roleId;

            MySqlCommand command = new MySqlCommand(query,connection);
            
            try{
                connection.Open();
                MySqlDataReader reader=command.ExecuteReader();
                if(reader.Read()){

                    int id=reader.GetInt32("id");
                    string title=reader["title"].ToString();

                    roles =new Roles();
                    roles.Id = id;
                    roles.Title=title;    
                
                }
                reader.Close();
            }
            catch(Exception ex){
                        Console.WriteLine(ex.Message);
            }

            finally{
                    connection.Close();
            }

            return roles;   

        }

    public async Task<bool> InsertDetails(Roles roles)
    {
        await Task.Delay(100);
        string connectionString = "server=localhost;port=3306;user=root;password=password;database=assessmentdb";
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query ="INSERT INTO Roles(title) VALUES("+ roles.Title +");";

            MySqlCommand command = new MySqlCommand(query,connection);
            
            try{
                connection.Open();
                MySqlDataReader reader=command.ExecuteReader();
                if(reader.Read()){

                    int id=reader.GetInt32("id");
                    string title=reader["title"].ToString();

                       
                
                }
                reader.Close();               
            }
            catch(Exception ex){
                        Console.WriteLine(ex.Message);
                        return false;
            }

            finally{
                    connection.Close();
            }

       return true;
    }

    public async Task<bool> UpdateDetails(Roles roles)
    {
        await Task.Delay(100);
        string connectionString = "server=localhost;port=3306;user=root;password=password;database=assessmentdb";
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query ="UPDATE ROLES SET VALUES="+roles.Title+" where id =" + roles.Id;

            MySqlCommand command = new MySqlCommand(query,connection);
            
            try{
                connection.Open();
                MySqlDataReader reader=command.ExecuteReader();
                if(reader.Read()){

                    int id=reader.GetInt32("id");
                    string title=reader["title"].ToString();

                }
                reader.Close();               
            }
            catch(Exception ex){
                        Console.WriteLine(ex.Message);
                        return false;
            }

            finally{
                    connection.Close();
            }
        return true;
    }

      public async Task<Roles> Delete(int roleId)
    {
            await Task.Delay(100);
            Roles  roles = null;
            string connectionString = "server=localhost;port=3306;user=root;password=password;database=assessmentdb";
            MySqlConnection connection = new MySqlConnection(connectionString);
            string query ="DELETE FROM ROLES WHERE ID ="+ roleId;

            MySqlCommand command = new MySqlCommand(query,connection);
            
            try{
                connection.Open();
                MySqlDataReader reader=command.ExecuteReader();
                reader.Close();
            }
            catch(Exception ex){
                        Console.WriteLine(ex.Message);
            }

            finally{
                    connection.Close();
            }

        return roles;
    }

}