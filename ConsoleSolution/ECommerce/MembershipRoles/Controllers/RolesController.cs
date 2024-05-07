using Transflower.ECommerce.Entities;
using Transflower.ECommerce.Services.Interfaces;

namespace Transflower.ECommerce.Controllers;
public class RolesController{

    private IRolesService _RolesService;

    public RolesController(IRolesService rolesService)
    {
        _RolesService = rolesService;
    }

    public async Task<List<Roles>> GetAllRoles(){
        return  await  _RolesService.GetAllRoles();
    }

    public async Task<Roles> GetRoleDetails(int id){
        
        return await _RolesService.GetRoleDetails(id);
    }
}