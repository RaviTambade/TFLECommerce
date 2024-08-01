using Transflower.ECommerce.Entities;

namespace Transflower.ECommerce.Repositories.Interfaces;

public interface IRolesRepository{
    public Task<List<Role>> GetAll();
    public Task<Role> GetRole(int roleId);

    public   Task<bool> Insert(Role roles);
    public   Task<bool> Update(Role roles);
    public   Task<bool> Delete(int roleId);


}