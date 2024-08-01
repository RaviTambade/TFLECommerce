using  Transflower.ECommerce.Entities;
using Transflower.ECommerce.Repositories.Interfaces;
using Transflower.ECommerce.Helpers;

namespace Transflower.ECommerce.Repositories;
public class PeopleRepository:IPeopleRepository{
    public  List<Person> GetAll(){
        JSONManager mgr=new JSONManager();
        string path=@"D:\Ravi\TAP\TAP\TFLECommerce\Solutions\ECommerce\FileIOApp\Data\people.json";
        List<Person> list=mgr.GetAll(path) ;
        return list ;
    }
    public Person GetByAge(int age){
      JSONManager mgr=new JSONManager();
        string path=@"D:\Ravi\TAP\TAP\TFLECommerce\Solutions\ECommerce\FileIOApp\Data\people.json";
        List<Person> list=mgr.GetAll(path) ;
        Person person=list.Find(x => x.Age == age) ;
        return person ;
    }
    public bool Insert(Person person){
       JSONManager mgr=new JSONManager();
        string path=@"D:\Ravi\TAP\TAP\TFLECommerce\Solutions\ECommerce\FileIOApp\Data\people.json";
        List<Person> list=mgr.GetAll(path) ;
        list.Add(person);
        mgr.SaveAll(list,path) ;
        return true ;

    }
    public bool Update(Person person){
           return false;
    }
    public bool Delete(Person person){
        return false;
    }
}