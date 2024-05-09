using Transflower.Entities;
using Transflower.Repositories.Interfaces;
using Transflower.Services.Interfaces;
namespace Transflower.Services;
public class HRService : IHRService
{
    private readonly IHRRepository _repo;

    public HRService(IHRRepository repository){
        _repo = repository;
    }
    public async Task<List<Employee>> GetAll()
    {
       return  await _repo.GetAll();   
    }

    public async Task<Employee> GetEmployee(int id)
    {
       return  await _repo.GetEmployee(id);   
    }

    public async Task<bool> Insert(Employee employee)
    {
       return  await _repo.Insert(employee);   
    }

    public async Task<bool> Update(int id,Employee employee)
    {
       return  await _repo.Update(id,employee);   
    }

    public async Task<bool> Delete(int id)
    {
       return  await _repo.Delete(id);   
    }  
}
