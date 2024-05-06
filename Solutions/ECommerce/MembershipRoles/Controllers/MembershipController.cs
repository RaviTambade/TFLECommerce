using Transflower.ECommerce.HR.Entities;
using Transflower.ECommerce.HR.Services.Interfaces;

namespace Transflower.ECommerce.HR.Controllers;
public class MembershipController{

    private IMembershipService _membershipService;

    public MembershipController(IMembershipService membershipService)
    {
        _membershipService = membershipService;
    }

    public async Task<List<Member>> GetAll(){
        return  await  _membershipService.GetAll();
    }

    public async Task<Member> GetDetails(int id){
        
        return await _membershipService.GetDetails(id);
    }
}