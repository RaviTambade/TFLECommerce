
using TestDapperApp.Entities;
using MySql.Data.MySqlClient;
using Dapper;
using System.Data;
using TestDapperApp.Repositories.Interfaces;

namespace TestDapperApp.Repositories.ADONET.Disconnected
{
    public  class StudentRepository:IStudentRepository
    {
        //Using Stored Procedure
        public  Student GetById(int id)
        {
            var student=new Student();

            string connectionString = "server=localhost;port=3306;user=root;password=password;database=ecommerce";
            using (var connection = new MySqlConnection(connectionString))
            {
                //Set up DynamicParameters object to pass parameters  
                 DynamicParameters parameters = new DynamicParameters();
                 parameters.Add("studentId", 1);

                 //Execute stored procedure and map the returned result to a Customer object  
                 student = connection.QuerySingleOrDefault<Student>("GetStudentById",
                                                                     parameters, 
                                                                     commandType: CommandType.StoredProcedure);

                 

                /* For SQL Server
                  
                var sql = "EXEC GetStudentById @studentId";
                var values = new { studentId = 1};
                var results = connection.Query(sql, values).ToList();
                results.ForEach(s => Console.WriteLine($"{s.Id} {s.Name }"));

                */
            }
            return student;
        }
        public  bool InsertParamExecute(Student student)
        {
            //DML Operations
            bool status = false;
            string connectionString = "server=localhost;port=3306;user=root;password=password;database=ecommerce";
            string studentName = student.Name;
            string query = "insert into students (name, assignedon)values(@name,@assignedDate)";
            using (IDbConnection con = new MySqlConnection(connectionString))
            {
                if (con.Execute(query, new { name = student.Name, assignedDate = student.AssignedOn }) > 0)
                    status = true;
            }
            return status;
        }
        public  bool Insert(Student student)
        {
            //Basic Code

            bool status = false;
            string connectionString = "server=localhost;port=3306;user=root;password=password;database=ecommerce";
            int month = student.AssignedOn.Month;
            int year = student.AssignedOn.Year;
            int day = student.AssignedOn.Day;
            string assignedDate = year + "-" + month + "-" + day;
            string studentName=student.Name;    

            string query = "insert into students ( name, assignedon)values('"+ studentName + "','"+assignedDate +"')";
            using (IDbConnection con = new MySqlConnection(connectionString))
            {
                if (con.Execute(query) > 0)
                    status = true;
            }
            return status;
        }
        public  bool Delete(Student student)
    {
            bool status =false; 
             string connectionString = "server=localhost;port=3306;user=root;password=password;database=ecommerce"; 
            int studentId=student.Id;
            string query = "Delete from students  where id="+studentId+"";
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                if(con.Execute(query)> 0)
                status =true;
            } 
        return status;
    }
        public  bool Update(Student student)
        {
            //Basic Code

            bool status = false;
            string connectionString = "server=localhost;port=3306;user=root;password=password;database=ecommerce";
            string studentName=student.Name; 
            int studentId=student.Id;   
            string query = "UPDATE students SET name = @Name ,assignedOn=@AssignedOn WHERE id = @Id";
           // string query = "UPDATE students SET name = 'simran' , assignedOn='2025/5/15'  WHERE id = 2";
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                //What is Annonymous object
                object[] parameters = { 
                                        new { 
                                            Name = student.Name,
                                            AssignedOn = student.AssignedOn,
                                            Id = student.Id 
                                        } 
                };
                if (con.Execute(query,parameters)> 0)
                status =true;
            } 
        return status;
        
    }
        public   Student GetStudentById(int id)
        {
            Student std = new Student(); ;
            string connectionString = "server=localhost;port=3306;user=root;password=password;database=ecommerce";
            string query = "select name, id from students WHERE id = @Id";
            // string query = "UPDATE students SET name = 'simran' , assignedOn='2025/5/15'  WHERE id = 2";
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                var result = con.QueryFirstOrDefault<Student>(query, new { Id = id });
                std=result as Student;
                Console.WriteLine(std.Id + " "+ std.Name);
            }
            return std;
        }
    }
}
