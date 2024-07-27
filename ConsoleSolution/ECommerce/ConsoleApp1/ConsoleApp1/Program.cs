using TestDapperApp;
using TestDapperApp.Entities;

IEnumerable<Student> students = StudentRepository.GetStudents();
 
foreach (Student student in students)
{
    Console.WriteLine(student.Name + " " + student.AssignedOn);

}
Student student1 = new Student();
student1.Name = "Nayan";
student1.AssignedOn = new DateTime(2024, 5, 14);
/*StudentRepository.Insert(student1);
bool status = StudentRepository.Delete(4);
Console.WriteLine(status);
if (status == true)
   Console.WriteLine("Record Deleted Successfully"); */
/*Student student2 = new Student();
student2.Name = "Ajay";
student2.Id = 1;
student2.AssignedOn = new DateTime(2024, 5, 16);
bool status1 = StudentRepository.Update(student2);
Console.WriteLine(status1); */
bool status3 = StudentRepository.UpdateById("Akshay", 2); //It is a function call. Function call never contains data type. In function calling we pass the values only.
Console.WriteLine(status3);