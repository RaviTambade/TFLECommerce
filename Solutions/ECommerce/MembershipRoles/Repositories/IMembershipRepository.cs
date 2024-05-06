
using Transflower.ECommerce.HR.Entities;
namespace Transflower.ECommerce.HR.Repositories.Interfaces;
public interface IMembershipRepository
{
    public   Task<List<Member>> GetAll();
    public  Task<Member> GetDetails(int membershipId); 

}