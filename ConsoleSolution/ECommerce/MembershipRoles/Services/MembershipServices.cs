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

    public async Task<bool> InsertMemberDetails(Member member)
    {
        return  await _membershipRepo.InsertMemberDetails(member);
    }

    public async Task<bool> MemberDelete(int membershipId)
    {
        return  await _membershipRepo.MemberDelete(membershipId);
    }

    public async Task<bool> UpdateMemberDetails(Member member)
    {
       return  await _membershipRepo.UpdateMemberDetails(member);
    }
}

