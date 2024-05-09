using Transflower.Entities;
using Transflower.Services.Interfaces;

namespace Transflower.Controllers;
public class HRController{

    private readonly IHRService _service;

    public HRController(IHRService service)
    {
        _service = service;
    }

    public async Task<List<Employee>> GetAllEmployees(){
        return await _service.GetAll();
    }

    public async Task<Employee> GetEmployee(int id){
        return await _service.GetEmployee(id);
    }

    public async Task<bool> Insert(Employee employee){
        return await _service.Insert(employee);
    }

    public async Task<bool> Update(int id,Employee employee)
    {
       return  await _service.Update(id,employee);   
    }
     public async Task<bool> Delete(int id)
    {
       return  await _service.Delete(id);   
    }
}