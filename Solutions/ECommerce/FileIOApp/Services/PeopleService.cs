
using   Transflower.ECommerce.Entities;
using Transflower.ECommerce.Services.Interfaces;
using Transflower.ECommerce.Repositories.Interfaces;
namespace Transflower.ECommerce.Services;

public class PeopleService:IPeopleService{

    private readonly IPeopleRepository _repo;
    public PeopleService(IPeopleRepository peopleRepository){
        _repo = peopleRepository;
    }
    public  List<Person> GetAll(){
         return _repo.GetAll();
    }
    public Person GetByAge(int age){
        return _repo.GetByAge(age); 
    }
    public bool Insert(Person person){ 
        return _repo.Insert(person);
    }
    public bool Update(Person person){
          return _repo.Update(person);
    }
    public bool Delete(Person person){
          return _repo.Delete(person);
    }
}