using System.Security.Policy;
using Transflower.ECommerce.HR.Entities;
using Transflower.ECommerce.HR.Repositories.Interfaces;
using Transflower.ECommerce.HR.Services.Interfaces;
namespace Transflower.ECommerce.HR.Services;



public class MembershipService : IMembershipService
{
    private readonly IMembershipRepository _membershipRepo;

    public MembershipService(IMembershipRepository repository){
        _membershipRepo = repository;
    }
    public async Task<List<Member>> GetAll()
    {
       return  await _membershipRepo.GetAll();
      
    }

    public async Task<Member> GetDetails(int membershipId)
    {
          return  await _membershipRepo.GetDetails(membershipId);
    }

    public async Task<bool> Insert(Member member)
    {
        return  await _membershipRepo.Insert(member);
    }

    public async Task<bool> Delete(int membershipId)
    {
        return  await _membershipRepo.Delete(membershipId);
    }

    public async Task<bool> Update(Member member)
    {
       return  await _membershipRepo.Update(member);
    }
}

