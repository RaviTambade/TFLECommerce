﻿
using Transflower.ECommerce.HR.Entities;
using Transflower.ECommerce.HR.Controllers;
using Transflower.ECommerce.HR.Services;
using Transflower.ECommerce.HR.Repositories;
using Transflower.ECommerce.HR.Repositories.Interfaces;
using Transflower.ECommerce.HR.Services.Interfaces;

IMembershipRepository repo=new MembershipRepository();
IMembershipService service=new MembershipService(repo);
MembershipController controller=new MembershipController(service);
List<Member> allMembers=await controller.GetAll();

foreach (Member member in allMembers){
       Console.WriteLine(member);
}