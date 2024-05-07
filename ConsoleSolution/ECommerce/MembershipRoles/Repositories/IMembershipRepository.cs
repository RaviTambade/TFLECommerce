
using Transflower.ECommerce.HR.Entities;
namespace Transflower.ECommerce.HR.Repositories.Interfaces;
public interface IMembershipRepository
{
    public   Task<List<Member>> GetAll();
    public  Task<Member> GetDetails(int membershipId); 

    public   Task<bool> InsertMemberDetails(Member member);
    public   Task<bool> UpdateMemberDetails(Member member);
    public   Task<Member> MemberDelete(int membershipId);

}