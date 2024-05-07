using System.IO;
using System.Text.Json;
using Transflower.ECommerce.Entities;
namespace Transflower.ECommerce.Helpers;

public class JSONManager{

    public List<Person> GetAll(string path){
        //Deserialization code
        string jsonString=File.ReadAllText(path);
        List<Person> retrivedPeople= JsonSerializer.Deserialize<List<Person>>(jsonString);
        return retrivedPeople;
    }

    public bool SaveAll(List<Person>  people, string path){
    //Serialization code
        bool status=false;
        var options=new JsonSerializerOptions { IncludeFields=true};
        string jsonString=JsonSerializer.Serialize<List<Person>>(people, options);
        File.WriteAllText(path, jsonString);
        status=true;
        return status;
    }
}