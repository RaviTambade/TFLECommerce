
using Transflower.ECommerce.HR.Entities;
using Transflower.ECommerce.HR.Controllers;
using Transflower.ECommerce.HR.Services;
using Transflower.ECommerce.HR.Repositories;
using Transflower.ECommerce.HR.Repositories.Interfaces;
using Transflower.ECommerce.HR.Services.Interfaces;

using Transflower.ECommerce.Entities;
using Transflower.ECommerce.Controllers;
using Transflower.ECommerce.Services;
using Transflower.ECommerce.Repositories;
using Transflower.ECommerce.Repositories.Interfaces;
using Transflower.ECommerce.Services.Interfaces;
using MembershipRoles.Repositories.MySql;
using MembershipRoles.Repositories.MsSql.Dapper;

IMembershipRepository repo = new MembershipMSSQLDapperRepository();
// IMembershipRepository repo=new MembershipMySqlRepository();
IMembershipService service=new MembershipService(repo);
MembershipController controller=new MembershipController(service);
List<Member> allMembers=await controller.GetAll();

foreach (Member member in allMembers){
       Console.WriteLine(member);
}

// Console.WriteLine(" ");

// IRolesRepository repo1=new RolesRepository();
// IRolesService service1=new RolesService(repo1);
// RolesController controllers=new RolesController(service1);
// List<Role> allRoles=await controllers.GetAll();

// foreach (Role role in allRoles){
//        Console.WriteLine(role);
// }

// Console.WriteLine("get data");
// Member emp=await controller.GetDetails(2);
// Console.WriteLine(emp.Id+" "+emp.FirstName);

// Insert new employee
// Member theEmp=new Member();
// theEmp.FirstName="ajinkya";
// theEmp.LastName="tambade";
// theEmp.Contact="9023819492";
// theEmp.Email="ajinkya112@gmail.com";
// bool status=await controller.Insert(theEmp);
// Console.WriteLine(status);

// Update existing employee
// Member emp=await controller.GetDetails(2);

// bool status=await controller.Update(emp);
// Console.WriteLine(status);



// Delete Employee
// bool status= await controller.Delete(5);
// Console.WriteLine(status);
