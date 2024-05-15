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
StudentRepository.Insert(student1);
