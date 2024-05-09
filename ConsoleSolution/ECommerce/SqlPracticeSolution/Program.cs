using Transflower.Repositories.Interfaces;
using Transflower.Repositories;
using Transflower.Services.Interfaces;
using Transflower.Services;
using Transflower.Controllers;
using Transflower.Entities;

IHRRepository repo = new HRRepository();
IHRService service=new HRService(repo);

// Get All Employees
HRController controller=new HRController(service);
List<Employee> allemployees=await controller.GetAllEmployees();

foreach (Employee employee in allemployees){
       Console.WriteLine(employee);
}


// Get Particular Employee
// Employee emp=await controller.GetEmployee(2);
// Console.WriteLine(emp);


// Insert new employee
// Employee theEmp=new Employee();
// theEmp.FirstName="ajinkya";
// theEmp.LastName="tambade";
// theEmp.Contact="9023819492";
// theEmp.Email="ajinkya112@gmail.com";
// bool status=await controller.Insert(theEmp);
// Console.WriteLine(status);


// Update existing employee
// Employee theEmployee=await controller.GetEmployee(1);
// emp.FirstName="priya";
// emp.LastName="barve";
// emp.Contact="8708238654";
// emp.Email="barvepriya123@gmail.com";
// bool status=await controller.Update(1,theEmployee);
// Console.WriteLine(status);


// Delete Employee
// bool status= await controller.Delete(4);
// Console.WriteLine(status);


