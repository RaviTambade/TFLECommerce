using Transflower.ECommerce.Entities;
using Transflower.ECommerce.Services.Interfaces;

namespace Transflower.ECommerce.Controllers;
public class RolesController{

    private IRolesService _RolesService;

    public RolesController(IRolesService rolesService)
    {
        _RolesService = rolesService;
    }

    public async Task<List<Role>> GetAll(){
        return  await  _RolesService.GetAll();
    }

    public async Task<Role> GetRole(int id){
        
        return await _RolesService.GetRole(id);
    }
}