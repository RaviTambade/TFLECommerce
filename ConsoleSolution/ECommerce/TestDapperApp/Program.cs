
using Org.BouncyCastle.Asn1.Misc;
using TestDapperApp.Entities;
using TestDapperApp.Repositories.Dapper;
using TestDapperApp.Repositories.Interfaces;


Student s = new Student { Id = 56, Name = "sdfsdfs", AssignedOn = new DateTime(2024, 5, 15) };


//LINQ

//Annonymous Types

/*Student student = new Student { Name = "Sagar", AssignedOn = new DateTime(2024, 3, 1) };
bool status = StudentRepository.InsertParamExecute(student); 
//bool status=StudentRepository.Insert(student);
if (status)
{
    Console.WriteLine("inserted successfully");
}

*/

// Student theStudent=StudentRepository.GetById(1);
// Console.WriteLine(theStudent.Name + theStudent.AssignedOn);

// Student student = new Student { Id=1 };
// // bool status = StudentRepository.Delete(student); 
// bool status=StudentRepository.Delete(student);
// if (status)
// {
//     Console.WriteLine("Delete successfully");
// }

// for update
/*Student std = new Student{Id=2,
                               Name="Samadhan",
                               AssignedOn=new DateTime(2024,4,4)
              };

bool status = StudentRepository.Update(std);
if (status)
{
    Console.WriteLine("succefully Update");
}



var ss = new  { Id = 56, Name = "sdfsdfs", AssignedOn = new DateTime(2024, 5, 15) };

Console.WriteLine("Id= " + ss.Id + " " + "Name=" + ss.Name);

*/

IStudentRepository repo = new StudentRepository();
Student theStudent = repo.GetStudentById(2);
Console.WriteLine(theStudent.Name +  "   " + theStudent.Id);
Console.ReadLine();


