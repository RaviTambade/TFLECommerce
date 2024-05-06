using Transflower.HR;
 
using MySql.Data.MySqlClient;

Console.WriteLine("Welcome to Database Programming");
Employee employee= new Employee();
employee.FirstName="Raj";
employee.LastName="Kapoor";
employee.Email="raj.kapoor@gmail.com";
employee.Contact="9881735801";

Console.WriteLine(employee);

//Datbase connectity Program (ADO.NET)

//Create connection 
//Create command
//Execute Command
//iterate resultset retrived after command execution
//ReCreate each Employee object from data receieved from resultSet
//Add inside empty list of Employees
//Use forteach loop to print each Employee object detail from list employees

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
        Console.WriteLine( firstName + "  "+ lastName + "  "+ email + "  "+ contact);
    }
    reader.Close();
}
catch(Exception ex){

            Console.WriteLine(ex.Message);
}

finally{
        connection.Close();
}
