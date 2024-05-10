using Dapper;
using System.Data.SqlClient;
using DapperApp.Entities;
using System.Data;

string connectionString = @"Data Source=DESKTOP-H1K53PL\SQLEXPRESS;Initial Catalog=AssessmentDB;Integrated Security=True;Connect Timeout=30;Encrypt=False"; ;
using (IDbConnection con = new SqlConnection(connectionString))
{

   /* var employees = con.Query<Employee>("SELECT * FROM employees");

    foreach (Employee employee in employees)
    {
        Console.WriteLine("Id: " + employee.Id + " , FirstName: " + employee.FirstName +
                           " , LastName: " + employee.LastName);
    }


    var empNew = new Employee
    {
        Id = 179,
        FirstName = "Rutuja",
        LastName = "Tambade",
        Contact = "9884578569",
        Email = "rutuja.tambade@gmail.com"
    };
    con.Execute("INSERT INTO employees (Id, firstname, lastname, email, contact) VALUES (@Id, @FirstName, @LastName, @Email, @Contact)", empNew);
    foreach (Employee employee in employees)
    {
        Console.WriteLine("Id: " + employee.Id + " , FirstName: " + employee.FirstName +
                           " , LastName: " + employee.LastName);
    }

    */

    var empToUpdate = new Employee
    {
        Id = 179,
        FirstName = "Sagar",
        LastName = "Patil",
        Contact = "9884578569",
        Email = "sagar.patil@gmail.com"
    };

    con.Execute("UPDATE  employees SET firstname= @FirstName, lastname=@LastName WHERE Id=@Id", empToUpdate);
    // con.Execute("DELETE  FROM employees WHERE Id=@Id", new { Id = 91 });

    Console.WriteLine("After Updating Employee details......");
    var employees = con.Query<Employee>("SELECT * FROM employees");
    foreach (Employee employee in employees)
    {
        Console.WriteLine("Id: " + employee.Id + " , FirstName: " + employee.FirstName +
                           " , LastName: " + employee.LastName);
    }



    //Query

    /*var emp = new Employee { Id = 91, FirstName = "Chaitali", LastName = "Handore", 
                                    Contact = "9884578569", Email = "chaitali.handore@gmail.com" };
    string insertQuery = "INSERT INTO employees (Id, firstname, lastname, email, contact) VALUES (@Id, @FirstName, @LastName, @Email, @Contact)";
    con.Execute(insertQuery,  emp);
    */
       
}
Console.ReadLine();
