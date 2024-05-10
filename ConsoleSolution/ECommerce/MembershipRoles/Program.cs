
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


<<<<<<< HEAD
IMembershipRepository repo = new MembershipMySqlRepository();
//IMembershipRepository repo=new MembershipMySqlRepository();
=======
//IMembershipRepository repo = new MembershipMSSQLRepository();
IMembershipRepository repo=new MembershipMySqlRepository();
>>>>>>> 7c6694f8847d0cf46af20bed90763e2d6956cb57
IMembershipService service=new MembershipService(repo);
MembershipController controller=new MembershipController(service);
List<Member> allMembers=await controller.GetAll();

foreach (Member member in allMembers){
       Console.WriteLine(member);
}

Console.WriteLine(" ");
<<<<<<< HEAD
/*IRolesRepository repo1=new RolesRepository();
=======

IRolesRepository repo1=new RolesRepository();
>>>>>>> 7c6694f8847d0cf46af20bed90763e2d6956cb57
IRolesService service1=new RolesService(repo1);
RolesController controllers=new RolesController(service1);
List<Role> allRoles=await controllers.GetAll();

foreach (Role role in allRoles){
       Console.WriteLine(role);
}
<<<<<<< HEAD
*/
=======


>>>>>>> 7c6694f8847d0cf46af20bed90763e2d6956cb57
