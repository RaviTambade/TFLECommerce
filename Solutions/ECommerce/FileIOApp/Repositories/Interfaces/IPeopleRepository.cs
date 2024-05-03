using Transflower.ECommerce.Entities;
namespace Transflower.ECommerce.Repositories.Interfaces;

 public interface IPeopleRepository
{
    public  List<Person> GetAll();
    public Person GetByAge(int age);
    public bool Insert(Person person);
    public bool Update(Person person);
    public bool Delete(Person person);
}