
using Org.BouncyCastle.Asn1.Misc;
using TestDapperApp;
using TestDapperApp.Entities;

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
Student student1 = new Student{Id=2};
bool status = StudentRepository.Update(student1);
if (status)
{
    Console.WriteLine("succefully Update");
    
}













