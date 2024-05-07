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

    public async Task<List<Roles>> GetAllRoles()
    {
       return  await _RolesRepo.GetAllRoles();
      
    }

    public async Task<Roles> GetRoleDetails(int roleId)
    {
          return  await _RolesRepo.GetRoleDetails(roleId);
    }

    public async Task<bool> InsertDetails(Roles roles)
    {
        return await _RolesRepo.InsertDetails(roles);
    }

    public async Task<bool> UpdateDetails(Roles roles)
    {
        return await _RolesRepo.UpdateDetails(roles);
    }

      public async Task<Roles> Delete(int roleId)
    {
        return await _RolesRepo.GetRoleDetails(roleId);
    }
}

