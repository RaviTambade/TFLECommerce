
using TestDapperApp;
using TestDapperApp.Entities;

Student student = new Student { Name = "Sagar", AssignedOn = new DateTime(2024, 3, 1) };
bool status = StudentRepository.InsertParamExecute(student); 
//bool status=StudentRepository.Insert(student);
if (status)
{
    Console.WriteLine("inserted successfully");
}
