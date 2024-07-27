using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using TestDapperApp.Entities;

using Mysqlx.Crud;

using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Threading.Tasks;

using System.Data.Common;

namespace TestDapperApp

{

    public class StudentRepository

    {

        public static bool Insert(Student student)

        {

            bool status = false;

            string connectionString = "server=localhost;port=3306;user=root;password=password;database=ecommerce";

            string query = "Insert into students (Name,AssignedOn) Values (@name,@assignedon)"; //Paramerized query

            using (IDbConnection con = new MySqlConnection(connectionString))

            {

                if (con.Execute(query, new { name = student.Name, assignedon = student.AssignedOn }) > 0) //Property intialization

                {

                    status = true;

                }

            }

            return status;

        }

        public static bool Delete(int studentId)

        {

            bool status = false;

            string connectionString = "server=localhost;port=3306;user=root;password=password;database=ecommerce";

            string query = "Delete from students where id = " + studentId + " ";

            using (MySqlConnection con = new MySqlConnection(connectionString))

            {
                if (con.Execute(query) > 0)

                    status = true;
            }

            return status;
        }

        public static bool Update(Student student)
        {

            bool status = false;

            string connectionString = "server=localhost;port=3306;user=root;password=password;database=ecommerce";

            string query = "UPDATE students SET name = @Name ,assignedOn=@AssignedOn WHERE id = @Id";

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                
                if (con.Execute(query, new{Name = student.Name, AssignedOn = student.AssignedOn, Id = student.Id}) > 0)

                    status = true;

            }

            return status;

        }

        public static bool UpdateById(String StudentName, int StudentId)
        {

            bool status = false;

            string connectionString = "server=localhost;port=3306;user=root;password=password;database=ecommerce";

            string query = "UPDATE students SET name = @Name WHERE id = @Id";

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {

                if (con.Execute(query, new { Name = StudentName, Id = StudentId }) > 0)

                    status = true;

            }

            return status;

        }
        public static Student GetById(int id)

        {

            Student student = null;

            string connectionString = "server = localhost; port=3306; user= root; password = password; database=ecommerce";

            using (var connection = new MySqlConnection(connectionString))

            {

                //Set up DynamicParameters object to pass parameters  

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("studentId", id);

                //Execute stored procedure and map the returned result to a Customer object  

                student = connection.QuerySingleOrDefault<Student>("GetStudentById", commandType: CommandType.StoredProcedure);


                /* For SQL Server

                var sql = "EXEC GetStudentById @studentId";

                var values = new { studentId = 1};

                var results = connection.Query(sql, values).ToList();

                results.ForEach(s => Console.WriteLine($"{s.Id} {s.Name }"));
 
                */

            }

            return student;

        }


        public static List<Student> GetAll()

        {

            List<Student> students = new List<Student>();

            string connectionString = "server=localhost;port=3306;user=root;password=password;database=ecommerce";

            string query = "select * from  students";

            using (IDbConnection con = new MySqlConnection(connectionString))

            {

                var studentslist = con.Query<Student>(query);

                students = studentslist as List<Student>;


            }

            return students;


        }

        public static IEnumerable<Student> GetStudents()
        {
            IEnumerable<Student> students = new List<Student>();

            string connectionString = "server = localhost; port=3306; user= root; password = password; database=ecommerce";

            using (var connection = new MySqlConnection(connectionString))

            {

                //Set up DynamicParameters object to pass parameters  

                // DynamicParameters parameters = new DynamicParameters();

                //parameters.Add("studentId", id);

                //Execute stored procedure and map the returned result to a Customer object  

                students = connection.Query<Student> ("GetStudents", commandType: CommandType.StoredProcedure);


            }

            return students;

        }

    }

    }