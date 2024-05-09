using Transflower.Entities;
using System.Data.SqlClient;
using System.Data;
using Transflower.Repositories.Interfaces;
namespace Transflower.Repositories;

public class HRRepository:IHRRepository{
private string connectionString = @"Data Source=DESKTOP-RQSMQ1M\SQLEXPRESS;Initial Catalog=assessmentdb;Integrated Security=True;Connect Timeout=30;Encrypt=False";

public async Task<List<Employee>> GetAll()
    {
        List<Employee> employees = new List<Employee>();
        string query = "select * from employees";
        IDbConnection connection = new SqlConnection(connectionString);
        IDbCommand command = new SqlCommand(query, connection as SqlConnection);
        try
        {
           connection.Open();
            IDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int id = int.Parse(reader["id"].ToString());
                string firstName = reader["firstname"].ToString();
                string lastName = reader["lastname"].ToString();
                string contact = reader["contact"].ToString();
                string email = reader["email"].ToString();
                Employee employee = new Employee();
                employee.Id = id;
                employee.FirstName = firstName;
                employee.LastName = lastName;
                employee.Contact = contact;
                employee.Email = email;
                employees.Add(employee);
            }
            reader.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            connection.Close();
        }
        return employees;
    }

    public async Task<Employee> GetEmployee(int id)
    {
        Employee employee = null;
        string query = @"select * from employees where id="+id;
        IDbConnection connection = new SqlConnection(connectionString);
        IDbCommand command = new SqlCommand(query, connection as SqlConnection);
        try
        {
            connection.Open();
            IDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                string firstName = reader["firstname"].ToString();
                string lastName = reader["lastname"].ToString();
                string contact = reader["contact"].ToString();
                string email = reader["email"].ToString();
                employee = new Employee();
                employee.Id = id;
                employee.FirstName = firstName;
                employee.LastName = lastName;
                employee.Contact = contact;
                employee.Email = email;
                
            }
             reader.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        finally
        {
            connection.Close();
        }
        return employee;
    }

    public async Task<bool> Insert(Employee employee)
    {
        bool status = false;
        IDbConnection connection = new SqlConnection(connectionString);
        string query = @"INSERT INTO employees(firstname,lastname,contact,email) VALUES (@firstname,@lastname,@contact,@email)";
        SqlCommand command = new SqlCommand(query, connection as SqlConnection);
        command.Parameters.AddWithValue("@firstname", employee.FirstName);
        command.Parameters.AddWithValue("@lastname", employee.LastName);
        command.Parameters.AddWithValue("@contact", employee.Contact);
        command.Parameters.AddWithValue("@email", employee.Email);
        try
        {
            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                status = true;
            }

        }
        catch (Exception ee)
        {
            Console.WriteLine(ee.Message);
        }
        finally
        {
           connection.Close();
        }
        return status;
    }      

    public async Task<bool> Update(int id,Employee employee)
    {
        bool status = false;
        IDbConnection connection = new SqlConnection(connectionString);
        string query = @"UPDATE employees set firstname=@firstname,lastname=@lastname,contact=@contact,email=@email where id=@id ";
        SqlCommand command = new SqlCommand(query, connection as SqlConnection);
        command.Parameters.AddWithValue("@id", id);
        command.Parameters.AddWithValue("@firstname", employee.FirstName);
        command.Parameters.AddWithValue("@lastname", employee.LastName);
        command.Parameters.AddWithValue("@contact", employee.Contact);
        command.Parameters.AddWithValue("@email", employee.Email);
        try
        {
            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                status = true;
            }

        }
        catch (Exception ee)
        {
            Console.WriteLine(ee.Message);
        }
        finally
        {
           connection.Close();
        }
        return status;
    } 

    public async Task<bool> Delete(int id)
    {
        bool status = false;
        IDbConnection connection = new SqlConnection(connectionString);
        string query = @"Delete from employees where id=@id ";
        SqlCommand command = new SqlCommand(query, connection as SqlConnection);
        command.Parameters.AddWithValue("@id", id);
        try
        {
            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                status = true;
            }

        }
        catch (Exception ee)
        {
            Console.WriteLine(ee.Message);
        }
        finally
        {
           connection.Close();
        }
        return status;
    }    
}