using Transflower.ECommerce.Entities;

namespace Transflower.ECommerce.Repositories.Interfaces;

public interface IRolesRepository{
    public Task<List<Roles>> GetAllRoles();

    public Task<Roles> GetRoleDetails(int roleId);

    public   Task<bool> InsertDetails(Roles roles);
    public   Task<bool> UpdateDetails(Roles roles);
    public   Task<Roles> Delete(int roleId);


}