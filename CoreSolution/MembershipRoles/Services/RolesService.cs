using System.Security.Policy;
using Transflower.ECommerce.Entities;
using Transflower.ECommerce.Repositories.Interfaces;
using Transflower.ECommerce.Services.Interfaces;
namespace Transflower.ECommerce.Services;



public class RolesService : IRolesService
{
    private readonly IRolesRepository _RolesRepo;

    public RolesService(IRolesRepository repository){
        _RolesRepo = repository;
    }  

    public async Task<List<Role>> GetAll()
    {
       return  await _RolesRepo.GetAll();
      
    }

    public async Task<Role> GetRole(int roleId)
    {
          return  await _RolesRepo.GetRole(roleId);
    }

    public async Task<bool> Insert(Role roles)
    {
        return await _RolesRepo.Insert(roles);
    }

    public async Task<bool> Update(Role role)
    {
        return await _RolesRepo.Update(role);
    }

    public async Task<bool> Delete(int roleId)
    {
        await _RolesRepo.Delete(roleId);
        return true;
    }
}

