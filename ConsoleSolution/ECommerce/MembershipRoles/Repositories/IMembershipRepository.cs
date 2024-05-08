
using Transflower.ECommerce.HR.Entities;
namespace Transflower.ECommerce.HR.Repositories.Interfaces;
public interface IMembershipRepository
{
    public   Task<List<Member>> GetAll();
    public  Task<Member> GetDetails(int membershipId); 

    public   Task<bool> Insert(Member member);
    public   Task<bool> Update(Member member);
    public   Task<bool> Delete(int membershipId);

}