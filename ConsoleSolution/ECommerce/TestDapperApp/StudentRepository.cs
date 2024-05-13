
using TestDapperApp.Entities;
using MySql.Data.MySqlClient;
using Dapper;
using System.Data;

namespace TestDapperApp
{
    public  class StudentRepository
    {


        //Using Stored Procedure
        public static Student GetById(int id)
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
        public static bool InsertParamExecute(Student student)
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


        public  static bool Insert(Student student)
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
    }
}
