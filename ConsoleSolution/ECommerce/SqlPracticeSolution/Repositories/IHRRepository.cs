using Transflower.Entities;

namespace Transflower.Repositories.Interfaces;

public interface IHRRepository{
public Task<List<Employee>> GetAll();
public Task<Employee> GetEmployee(int id);
public Task<bool> Insert(Employee employee);
public Task<bool> Update(int id,Employee employee);
public Task<bool> Delete(int id);
    
}