using System.IO;

using Transflower.ECommerce.IO;
 using Transflower.ECommerce.Entities;
using Transflower.ECommerce.Repositories.Interfaces;
using Transflower.ECommerce.Repositories;
using Transflower.ECommerce.Services.Interfaces;
using Transflower.ECommerce.Services;

using Transflower.ECommerce.Services;


/*
string path=@"D:\Ravi\TAP\TAP\TFLECommerce\Solutions\ECommerce\FileIOApp\Data\tfl.txt";
FileManager.ReadFile(path);

string contnet=@"India is My Country.
                 I love my conutry.";

FileManager.WriteFile(path, contnet);

FileInfo fileInfo=new FileInfo(path);
Console.WriteLine("File Name = " + fileInfo.FullName);
Console.WriteLine("Creation Time = " + fileInfo.CreationTime);
Console.WriteLine("Last Access Time = " + fileInfo.LastAccessTime);
Console.WriteLine("Last Write TIme = " + fileInfo.LastWriteTime);
Console.WriteLine("Size = " + fileInfo.Length);

*/

IPeopleRepository repo=new PeopleRepository();
IPeopleService service=new PeopleService(repo);


List<Person> retrivedPeople=service.GetAll();

foreach(Person person in retrivedPeople){
    Console.WriteLine(person.Name +   "  Age:"+person.Age);
}
